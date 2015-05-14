using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureWaveLeft1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureWaveLeft2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X > skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureWaveRight1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X > skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }

    public class GestureWaveRight2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }
}