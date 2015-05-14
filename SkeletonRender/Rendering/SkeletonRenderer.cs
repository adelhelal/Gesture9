using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace Gesture9.Rendering
{
    public class SkeletonRenderer : IRenderer
    {
        private Canvas _canvas;

        private Brush _boneColor = Brushes.White;
        private Brush _backgroundColor = Brushes.Black;

        private int _boneThicknesss = 8;
        private int _knobThickness = 16;

        AttachedImage _leftHand;
        AttachedImage _rightHand;
        AttachedImage _head;
        AttachedImage _leftFoot;
        AttachedImage _rightFoot;
        AttachedImage _ribCage;
        AttachedImage _hips;

        public SkeletonRenderer(Canvas canvas)
        {
            _canvas = canvas;
            _hips = new AttachedImage(GetPath("mexican/hips.png"), AttachedImage.TopCenter, s => s.HipCenter.Position, s => s.HipsAngle);
            _ribCage = new AttachedImage(GetPath("skeleton/ribcage.png"), AttachedImage.TopCenter, s => s.ShoulderCenter.Position, s => s.RibCageAngle);
            _head = new AttachedImage(GetPath("skeleton/skull.png"), AttachedImage.BottomCenter, s => s.ShoulderCenter.Position, s => s.HeadAngle);
            _leftFoot = new AttachedImage(GetPath("skeleton/leftfoot.png"), AttachedImage.CenterLeft, s => s.FootLeft.Position, s => s.FootLeftAngle);
            _rightFoot = new AttachedImage(GetPath("skeleton/rightfoot.png"), AttachedImage.CenterRight, s => s.FootRight.Position, s => s.FootRightAngle);
            _leftHand = new AttachedImage(GetPath("skeleton/lefthand.png"), AttachedImage.BottomCenter, s => s.HandLeft.Position, s => s.HandLeftAngle);
            _rightHand = new AttachedImage(GetPath("skeleton/righthand.png"), AttachedImage.BottomCenter, s => s.HandRight.Position, s => s.HandRightAngle);
        }

        private string GetPath(string file)
        {
            var path = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var dir = System.IO.Path.GetDirectoryName(path);
            var fullPath = System.IO.Path.Combine(dir, "Images", file);
            return fullPath;
        }

        public void Render(XmlJoint[] joints)
        {
            var skeleton = new ParsedSkeleton(joints);

            DrawBone(skeleton.KneeRight, skeleton.AnkleRight);
            DrawBone(skeleton.HipRight, skeleton.KneeRight);

            DrawBone(skeleton.KneeLeft, skeleton.AnkleLeft);
            DrawBone(skeleton.HipLeft, skeleton.KneeLeft);


            DrawBone(skeleton.ElbowRight, skeleton.WristRight);
            DrawBone(skeleton.ShoulderRight, skeleton.ElbowRight);

            DrawBone(skeleton.ElbowLeft, skeleton.WristLeft);
            DrawBone(skeleton.ShoulderLeft, skeleton.ElbowLeft);

            DrawSpine(skeleton.Spine, skeleton.ShoulderCenter, 8);

            _canvas.Children.Add(_leftFoot.GetImage(skeleton));
            _canvas.Children.Add(_rightFoot.GetImage(skeleton));
            _canvas.Children.Add(_leftHand.GetImage(skeleton));
            _canvas.Children.Add(_rightHand.GetImage(skeleton));
            _canvas.Children.Add(_hips.GetImage(skeleton));
            _canvas.Children.Add(_ribCage.GetImage(skeleton));
            _canvas.Children.Add(_head.GetImage(skeleton));
        }

        private void DrawMarker(double x, double y)
        {
            var line = new Line();
            line.X1 = x;
            line.Y1 = y;
            line.X2 = x + 5;
            line.Y2 = y + 5;
            line.StrokeThickness = 5;
            line.Stroke = Brushes.Red;
            _canvas.Children.Add(line);
        }

        private Point AttachCenter(SkeletonPoint point, BitmapImage image)
        {
            return new Point(Scale.X(point.X) - (image.Width / 2), Scale.Y(point.Y) - (image.Height / 2));
        }

        private Point AttachBottomMiddle(SkeletonPoint point, BitmapImage image)
        {
            return new Point(Scale.X(point.X) - (image.Width / 2), Scale.Y(point.Y) - image.Height);
        }

        public void RenderImage(BitmapImage imageSource, SkeletonPoint point, double angle, Point margin)
        {
            var x = Scale.X(point.X);
            var y = Scale.Y(point.Y);

            DrawMarker(x, y);

            var image = new Image();
            image.Source = imageSource;
            image.RenderTransform = new RotateTransform(angle, x, y);
            image.Margin = new Thickness(margin.X, margin.Y, 0, 0);
            _canvas.Children.Add(image);
        }

        public void DrawBone(XmlJoint joint1, XmlJoint joint2)
        {
            var bone = new Line();
            bone.X1 = Scale.X(joint1.Position.X);
            bone.Y1 = Scale.Y(joint1.Position.Y);
            bone.X2 = Scale.X(joint2.Position.X);
            bone.Y2 = Scale.Y(joint2.Position.Y);
            bone.StrokeThickness = _boneThicknesss;
            bone.Stroke = _boneColor;
            _canvas.Children.Add(bone);

            var knobShadow = new Ellipse();
            knobShadow.Width = _knobThickness + 2;
            knobShadow.Height = _knobThickness + 2;
            knobShadow.HorizontalAlignment = HorizontalAlignment.Center;
            knobShadow.VerticalAlignment = VerticalAlignment.Center;
            knobShadow.StrokeThickness = _knobThickness / 2;
            knobShadow.Stroke = _backgroundColor;
            knobShadow.Margin = new Thickness(Scale.X(joint1.Position.X) - (_knobThickness / 2), Scale.Y(joint1.Position.Y) - (_knobThickness / 2), 0, 0);
            _canvas.Children.Add(knobShadow);

            var knob = new Ellipse();
            knob.Width = _knobThickness;
            knob.Height = _knobThickness;
            knob.HorizontalAlignment = HorizontalAlignment.Center;
            knob.VerticalAlignment = VerticalAlignment.Center;
            knob.StrokeThickness = _knobThickness / 2;
            knob.Stroke = _boneColor;
            knob.Margin = new Thickness(Scale.X(joint1.Position.X) - (_knobThickness / 2), Scale.Y(joint1.Position.Y) - (_knobThickness / 2), 0, 0);
            _canvas.Children.Add(knob);

        }

        public void DrawSpine(XmlJoint bottom, XmlJoint top, int vertebrae)
        {
            var deltaX = (Scale.X(top.Position.X) - Scale.X(bottom.Position.X)) / vertebrae;
            var deltaY = (Scale.Y(top.Position.Y) - Scale.Y(bottom.Position.Y)) / vertebrae;

            var x = Scale.X(bottom.Position.X);
            var y = Scale.Y(bottom.Position.Y);
            for (var i = 0; i < vertebrae; i += 1)
            {
                var bone = new Line();
                bone.X1 = x;
                bone.Y1 = y;
                bone.X2 = x + (deltaX * 0.8);
                bone.Y2 = y + (deltaY * 0.8);
                bone.StrokeThickness = _boneThicknesss;
                bone.Stroke = _boneColor;
                _canvas.Children.Add(bone);

                x += deltaX;
                y += deltaY;
            }
        }
    }
}
