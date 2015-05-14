using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect;

namespace Gesture9
{
    public class GestureJump1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.Spine).Position.Scale().Y > 300;
        }
    }

    public class GestureJump2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.Spine).Position.Scale().Y < 300;
        }
    }
}