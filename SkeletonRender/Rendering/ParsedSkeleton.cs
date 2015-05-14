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
        public ParsedSkeleton(XmlJoint[] joints)
        {
            foreach(XmlJoint joint in joints)
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

        public XmlJoint HipCenter { get; set; }
        public XmlJoint Spine { get; set; }
        public XmlJoint ShoulderCenter { get; set; }
        public XmlJoint Head { get; set; }
        public XmlJoint ShoulderLeft { get; set; }
        public XmlJoint ElbowLeft { get; set; }
        public XmlJoint WristLeft { get; set; }
        public XmlJoint HandLeft { get; set; }
        public XmlJoint ShoulderRight { get; set; }
        public XmlJoint ElbowRight { get; set; }
        public XmlJoint WristRight { get; set; }
        public XmlJoint HandRight { get; set; }
        public XmlJoint HipLeft { get; set; }
        public XmlJoint KneeLeft { get; set; }
        public XmlJoint AnkleLeft { get; set; }
        public XmlJoint FootLeft { get; set; }
        public XmlJoint HipRight { get; set; }
        public XmlJoint KneeRight { get; set; }
        public XmlJoint AnkleRight { get; set; }
        public XmlJoint FootRight { get; set; }
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
                return GetAngle(AnkleLeft.Position, FootLeft.Position) + 180;
            }
        }

        public double BootRightAngle
        {
            get
            {
                return GetAngle(AnkleRight.Position, FootRight.Position) + 180;
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
