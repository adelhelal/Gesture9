using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gesture9.Rendering
{
    public class ParsedSkeleton
    {
        public ParsedSkeleton(JointCollection joints)
        {
            foreach(Joint joint in joints)
            {
                switch(joint.JointType)
                {
                    case Microsoft.Kinect.JointType.HipCenter: this.HipCenter = joint; break;
                    case Microsoft.Kinect.JointType.Spine: this.Spine = joint; break;
                    case Microsoft.Kinect.JointType.ShoulderCenter: this.ShoulderCenter = joint; break;
                    case Microsoft.Kinect.JointType.Head: this.Head = joint; break;
                    case Microsoft.Kinect.JointType.ShoulderLeft: this.ShoulderLeft = joint; break;
                    case Microsoft.Kinect.JointType.ElbowLeft: this.ElbowLeft = joint; break;
                    case Microsoft.Kinect.JointType.WristLeft: this.WristLeft = joint; break;
                    case Microsoft.Kinect.JointType.HandLeft: this.HandLeft = joint; break;
                    case Microsoft.Kinect.JointType.ShoulderRight: this.ShoulderRight = joint; break;
                    case Microsoft.Kinect.JointType.ElbowRight: this.ElbowRight = joint; break;
                    case Microsoft.Kinect.JointType.WristRight: this.WristRight = joint; break;
                    case Microsoft.Kinect.JointType.HandRight: this.HandRight = joint; break;
                    case Microsoft.Kinect.JointType.HipLeft: this.HipLeft = joint; break;
                    case Microsoft.Kinect.JointType.KneeLeft: this.KneeLeft = joint; break;
                    case Microsoft.Kinect.JointType.AnkleLeft: this.AnkleLeft = joint; break;
                    case Microsoft.Kinect.JointType.FootLeft: this.FootLeft = joint; break;
                    case Microsoft.Kinect.JointType.HipRight: this.HipRight = joint; break;
                    case Microsoft.Kinect.JointType.KneeRight: this.KneeRight = joint; break;
                    case Microsoft.Kinect.JointType.AnkleRight: this.AnkleRight = joint; break;
                    case Microsoft.Kinect.JointType.FootRight: this.FootRight = joint; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Joint HipCenter { get; set; }
        public Joint Spine { get; set; }
        public Joint ShoulderCenter { get; set; }
        public Joint Head { get; set; }
        public Joint ShoulderLeft { get; set; }
        public Joint ElbowLeft { get; set; }
        public Joint WristLeft { get; set; }
        public Joint HandLeft { get; set; }
        public Joint ShoulderRight { get; set; }
        public Joint ElbowRight { get; set; }
        public Joint WristRight { get; set; }
        public Joint HandRight { get; set; }
        public Joint HipLeft { get; set; }
        public Joint KneeLeft { get; set; }
        public Joint AnkleLeft { get; set; }
        public Joint FootLeft { get; set; }
        public Joint HipRight { get; set; }
        public Joint KneeRight { get; set; }
        public Joint AnkleRight { get; set; }
        public Joint FootRight { get; set; }

        private double GetAngle(SkeletonPoint point1, SkeletonPoint point2)
        {
            return Math.Atan2((point2.X - point1.X), (point2.Y - point1.Y)) * (180 / Math.PI);
        }

        public double HandLeftAngle
        {
            get
            {
                return GetAngle(WristLeft.Position, HandLeft.Position);
            }
        }

        public double HandRightAngle
        {
            get
            {
                return GetAngle(WristRight.Position, HandRight.Position);
            }
        }
        public double FootLeftAngle
        {
            get
            {
                return GetAngle(AnkleLeft.Position, FootLeft.Position);
            }
        }

        public double FootRightAngle
        {
            get
            {
                return GetAngle(AnkleRight.Position, FootRight.Position);
            }
        }
        public double BootLeftAngle
        {
            get
            {
                return GetAngle(AnkleLeft.Position, KneeLeft.Position);
            }
        }

        public double BootRightAngle
        {
            get
            {
                return GetAngle(AnkleRight.Position, KneeRight.Position);
            }
        }

        public double HeadAngle
        {
            get
            {
                return GetAngle(ShoulderCenter.Position, Head.Position);
            }
        }

        public double RibCageAngle
        {
            get
            {
                return GetAngle(Spine.Position, ShoulderCenter.Position);
            }
        }
        public double HipsAngle
        {
            get
            {
                return GetAngle(HipLeft.Position, HipRight.Position) - 90;
            }
        }
    }
}
