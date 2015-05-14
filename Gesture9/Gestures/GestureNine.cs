using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureNineLeft1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y > skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureNineLeft2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureNineLeft3 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X > skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureNineLeft4 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).Position.Scale().X > skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().X
                && skeleton.GetJoint(JointType.WristLeft).Position.Scale().Y > skeleton.GetJoint(JointType.ElbowLeft).Position.Scale().Y;
        }
    }

    public class GestureNineRight1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y > skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }

    public class GestureNineRight2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }

    public class GestureNineRight3 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X > skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y < skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }

    public class GestureNineRight4 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).Position.Scale().X > skeleton.GetJoint(JointType.ElbowRight).Position.Scale().X
                && skeleton.GetJoint(JointType.WristRight).Position.Scale().Y > skeleton.GetJoint(JointType.ElbowRight).Position.Scale().Y;
        }
    }
}