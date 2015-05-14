using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gesture9.Rendering
{
    interface IRenderer
    {
        void Render(XmlJoint[] skeleton);
    }
}
