using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureTravolta1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandRight).Position.Scale().Y > skeleton.GetJoint(JointType.Spine).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().X < skeleton.GetJoint(JointType.Spine).Position.Scale().X
                && skeleton.GetJoint(JointType.KneeRight).Position.Scale().X - skeleton.GetJoint(JointType.KneeLeft).Position.Scale().X > 100;
        }
    }

    public class GestureTravolta2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandRight).Position.Scale().Y < skeleton.GetJoint(JointType.Spine).Position.Scale().Y
                && skeleton.GetJoint(JointType.HandRight).Position.Scale().X > skeleton.GetJoint(JointType.Spine).Position.Scale().X
                && skeleton.GetJoint(JointType.KneeRight).Position.Scale().X - skeleton.GetJoint(JointType.KneeLeft).Position.Scale().X > 100;
        }
    }
}