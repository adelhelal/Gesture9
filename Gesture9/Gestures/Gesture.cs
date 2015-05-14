using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gesture9
{
    public class Gesture
    {
        IGesture[] _gestures;
        int _gestureIndex = 0;
        int _frameCount = 0;

        public event EventHandler GestureRecognized;

        public Gesture(IGesture[] gestures)
        {
            _gestures = gestures;
        }

        public int GetGesture(Skeleton skeleton)
        {
            if (_gestureIndex >= _gestures.Count() || _gestures[_gestureIndex].Update(skeleton))
            {
                _gestureIndex++;
                if (_gestureIndex >= _gestures.Count())
                {
                    if (GestureRecognized != null)
                    {
                        GestureRecognized(this, new EventArgs());
                        Reset();
                    }
                }
            }
            else if (_frameCount > 100)
            {
                Reset();
            }

            _frameCount++;
            return _gestureIndex;
        }

        public void Reset()
        {
            _frameCount = 0;
            _gestureIndex = 0;
        }
    }

    public interface IGesture
    {
        bool Update(Skeleton skeleton);
    }
}
