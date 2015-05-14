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
    public class AttachedImage
    {
        private Point attachmentPoint;
        private BitmapImage image;
        private Func<ParsedSkeleton, SkeletonPoint> attachTo;
        private Func<ParsedSkeleton, double> angle;

        public AttachedImage(string path, Func<BitmapImage, Point> attachmentPoint, Func<ParsedSkeleton, SkeletonPoint> attachTo, Func<ParsedSkeleton, double> angle)
        {
            this.image = new BitmapImage(new Uri(path));
            this.attachmentPoint = attachmentPoint(this.image);
            this.attachTo = attachTo;
            this.angle = angle;
        }
        private double GetAngle(SkeletonPoint point1, SkeletonPoint point2)
        {
            return Math.Atan2((point2.X - point1.X), (point2.Y - point1.Y)) * (180 / Math.PI);
        }

        public UIElement GetImage(ParsedSkeleton skeleton)
        {
            var angle = this.angle(skeleton);
            var attachTo = this.attachTo(skeleton);

            var image = new Image();
            image.Source = this.image;
            image.RenderTransform = new RotateTransform(angle, attachmentPoint.X, attachmentPoint.Y);
            image.Margin = new Thickness(Scale.X(attachTo.X) - attachmentPoint.X, Scale.Y(attachTo.Y) - attachmentPoint.Y, 0, 0);
            return image;
        }

        public static Point Center(BitmapImage image)
        {
            return new Point(image.Width / 2, image.Height / 2);
        }

        public static Point BottomCenter(BitmapImage image)
        {
            return new Point(image.Width / 2, image.Height);
        }
        public static Point BottomLeft(BitmapImage image)
        {
            return new Point(15, image.Height);
        }

        public static Point BottomRight(BitmapImage image)
        {
            return new Point(image.Width - 15, image.Height);
        }

        public static Point TopCenter(BitmapImage image)
        {
            return new Point(image.Width / 2, 0);
        }

        public static Point CenterLeft(BitmapImage image)
        {
            return new Point(0, image.Height / 2);
        }
        public static Point CenterRight(BitmapImage image)
        {
            return new Point(image.Width, image.Height / 2);
        }
    }

}
