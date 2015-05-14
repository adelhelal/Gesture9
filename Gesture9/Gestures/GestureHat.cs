using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureHat1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandRight).Position.Scale().X - skeleton.GetJoint(JointType.Head).Position.Scale().X < 55
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < skeleton.GetJoint(JointType.Head).Position.Scale().Y
                && skeleton.GetJoint(JointType.Head).Position.Scale().Y - skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < 55;
        }
    }

    public class GestureHat2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.Head).Position.Scale().X - skeleton.GetJoint(JointType.HandLeft).Position.Scale().X < 55
                && skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y < skeleton.GetJoint(JointType.Head).Position.Scale().Y
                && skeleton.GetJoint(JointType.Head).Position.Scale().Y - skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y < 55;
        }
    }
}