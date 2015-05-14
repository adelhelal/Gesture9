using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesture9.Rendering
{
    public static class Scale
    {
        public static double X(float x)
        {
            return 400 + (x * 250);
        }

        public static double Y(float y)
        {
            return 400 + (-y * 250);
        }
    }

}
