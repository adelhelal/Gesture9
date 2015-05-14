using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureRugby1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y < skeleton.GetJoint(JointType.HipCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < skeleton.GetJoint(JointType.HipCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y > skeleton.GetJoint(JointType.HandRight).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandLeft).Position.Scale().X > skeleton.GetJoint(JointType.HipCenter).Position.Scale().X
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().X > skeleton.GetJoint(JointType.HipCenter).Position.Scale().X;
        }
    }

    public class GestureRugby2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y < skeleton.GetJoint(JointType.HipCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < skeleton.GetJoint(JointType.HipCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y > skeleton.GetJoint(JointType.HandRight).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandLeft).Position.Scale().X < skeleton.GetJoint(JointType.HipCenter).Position.Scale().X
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().X < skeleton.GetJoint(JointType.HipCenter).Position.Scale().X;
        }
    }

    public class GestureAfl1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y < skeleton.GetJoint(JointType.ShoulderCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < skeleton.GetJoint(JointType.ShoulderCenter).Position.Scale().Y;
        }
    }

    public class GestureAfl2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).Position.Scale().Y > skeleton.GetJoint(JointType.ShoulderCenter).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().Y > skeleton.GetJoint(JointType.ShoulderCenter).Position.Scale().Y;
        }
    }
}