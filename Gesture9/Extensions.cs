using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Kinect;

namespace Gesture9
{
    public static class Extensions
    {
        public static Point Scale(this SkeletonPoint point)
        {
            return new Point(point.X * 300 + 300, 300 - point.Y * 300);
        }

        public static Joint GetJoint(this Skeleton skeleton, JointType type)
        {
            return skeleton.Joints.First(j => j.JointType == type);
        }
    }
}
