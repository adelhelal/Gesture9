using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Kinect;
using System.Diagnostics;
using Gesture9.Rendering;

namespace Gesture9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor _sensor;
        Skeleton lastTrackedSkeleton = null;
        IRenderer _skeletonRenderer;
        IRenderer _mexicanRenderer;
        IRenderer _currentRenderer;

        public MainWindow()
        {
            InitializeComponent();

            _sensor = KinectSensor.KinectSensors.FirstOrDefault();
            _skeletonRenderer = new SkeletonRenderer(this.KinectCanvas);
            _mexicanRenderer = new MexicanRenderer(this.KinectCanvas);
            _currentRenderer = _skeletonRenderer;

            if (_sensor != null)
            {
                _sensor.ColorStream.Enable();
                _sensor.SkeletonStream.Enable();

                this.InitializeGestures();

                this.Loaded += MainWindow_Loaded;
                this.Closed += MainWindow_Closed;
            }
        }

        Gesture 
            _gestureWaveLeft, 
            _gestureWaveRight,
            _gestureNineLeft,
            _gestureNineRight,
            _gestureRugby,
            _gestureAfl,
            _gestureJump,
            _gestureHatOn,
            _gestureHatOff,
            _gestureTravolta;

        public void InitializeGestures()
        {
            //Left wave

            var waveLeft1 = new GestureWaveLeft1();
            var waveLeft2 = new GestureWaveLeft2();

            _gestureWaveLeft = new Gesture(new IGesture[]
            {
                waveLeft1,
                waveLeft2,
                waveLeft1,
                waveLeft2,
            });

            _gestureWaveLeft.GestureRecognized += gestureWaveLeft_GestureRecognized;

            //Right wave

            var waveRight1 = new GestureWaveRight1();
            var waveRight2 = new GestureWaveRight2();

            _gestureWaveRight = new Gesture(new IGesture[]
            {
                waveRight1,
                waveRight2,
                waveRight1,
                waveRight2,
            });

            _gestureWaveRight.GestureRecognized += _gestureWaveRight_GestureRecognized;

            //Left nine

            var nineLeft1 = new GestureNineLeft1();
            var nineLeft2 = new GestureNineLeft2();
            var nineLeft3 = new GestureNineLeft3();
            var nineLeft4 = new GestureNineLeft4();

            _gestureNineLeft = new Gesture(new IGesture[]
            {
                nineLeft1,
                nineLeft2,
                nineLeft3,
                nineLeft4,
                nineLeft1,
            });

            _gestureNineLeft.GestureRecognized += _gestureNineLeft_GestureRecognized;

            //Right nine

            var nineRight1 = new GestureNineRight1();
            var nineRight2 = new GestureNineRight2();
            var nineRight3 = new GestureNineRight3();
            var nineRight4 = new GestureNineRight4();

            _gestureNineRight = new Gesture(new IGesture[]
            {
                nineRight1,
                nineRight2,
                nineRight3,
                nineRight4,
                nineRight1,
            });

            _gestureNineRight.GestureRecognized += _gestureNineRight_GestureRecognized;

            //Rugby

            var rugby1 = new GestureRugby1();
            var rugby2 = new GestureRugby2();

            _gestureRugby = new Gesture(new IGesture[]
            {
                rugby1,
                rugby2,
            });

            _gestureRugby.GestureRecognized += _gestureRugby_GestureRecognized;

            //Afl

            var afl1 = new GestureAfl1();
            var afl2 = new GestureAfl2();

            _gestureAfl = new Gesture(new IGesture[]
            {
                afl1,
                afl2,
            });

            _gestureAfl.GestureRecognized += _gestureAfl_GestureRecognized;

            //Jump

            var jump1 = new GestureJump1();
            var jump2 = new GestureJump2();

            _gestureJump = new Gesture(new IGesture[]
            {
                jump1,
                jump2,
                jump1,
            });

            _gestureJump.GestureRecognized += _gestureJump_GestureRecognized;

            //Hat

            var hatOn = new GestureHat1();
            var hatOff = new GestureHat2();

            _gestureHatOn = new Gesture(new IGesture[]
            {
                hatOn,
            });

            _gestureHatOn.GestureRecognized += _gestureHatOn_GestureRecognized;

            _gestureHatOff = new Gesture(new IGesture[]
            {
                hatOff,
            });

            _gestureHatOff.GestureRecognized += _gestureHatOff_GestureRecognized;

            //Travolta

            var travolta1 = new GestureTravolta1();
            var travolta2 = new GestureTravolta2();

            _gestureTravolta = new Gesture(new IGesture[]
            {
                travolta1,
                travolta2,
            });

            _gestureTravolta.GestureRecognized += _gestureTravolta_GestureRecognized;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor.ColorFrameReady += Kinect_ColorFrameReady;
            _sensor.SkeletonFrameReady += Kinect_SkeletonFrameReady;

            _sensor.Start();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _sensor.Stop();
        }

        private void Kinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    var bytes = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(bytes);
                    KinectImage.Source = BitmapSource.Create(frame.Width, frame.Height, 96, 96, PixelFormats.Bgr32, null, bytes, frame.Width * 4);
                }
            }
        }

        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    var skeletonsFrame = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletonsFrame);

                    var skeleton = (from s in skeletonsFrame where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                    if (skeleton != null)
                    {
                        lastTrackedSkeleton = skeleton;
                        Render(skeleton);

                        //Get Gestures

                        if (GesturesToggle.IsChecked ?? false)
                        {
                            GetGesture(skeleton);
                        }
                    }
                    else
                    {
                        KinectCanvas.Children.Clear();
                    }
                }
            }
        }

        private void Render(Skeleton skeleton)
        {   
            KinectCanvas.Children.Clear();
            _currentRenderer.Render(skeleton.Joints);
        }

        public void GetGesture(Skeleton skeleton)
        {
            var gestureWaveLeft = _gestureWaveLeft.GetGesture(skeleton);
            var gestureWaveRight = _gestureWaveRight.GetGesture(skeleton);
            var gestureNineLeft = _gestureNineLeft.GetGesture(skeleton);
            var gestureNineRight = _gestureNineRight.GetGesture(skeleton);
            var gestureRugby = _gestureRugby.GetGesture(skeleton);
            var gestureAfl = _gestureAfl.GetGesture(skeleton);
            var gestureJump = _gestureJump.GetGesture(skeleton);
            var gestureHatOn = _gestureHatOn.GetGesture(skeleton);
            var gestureHatOff = _gestureHatOff.GetGesture(skeleton);
            var gestureTravolta = _gestureTravolta.GetGesture(skeleton);

            GestureTest.Content = Math.Round(skeleton.GetJoint(JointType.Spine).Position.Scale().Y, 0).ToString();
        }

        public void ResetGestures()
        {
            _gestureWaveLeft.Reset(); 
            _gestureWaveRight.Reset();
            _gestureNineLeft.Reset();
            _gestureNineRight.Reset();
            _gestureRugby.Reset();
            _gestureAfl.Reset();
            _gestureJump.Reset();
            _gestureHatOn.Reset();
            _gestureHatOff.Reset();
            _gestureTravolta.Reset();
        }

        public void StartFirefox(string process)
        {
            Process.Start("firefox.exe", process);
        }

        private void gestureWaveLeft_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Left Wave";
            var processes = (from p in Process.GetProcesses() 
                             where (new string[] { "OUTLOOK", "firefox" }).Contains(p.ProcessName) 
                             select p).ToList();

            foreach (var process in processes)
            {
                process.Kill();
            }
            ResetGestures();
        }

        private void _gestureWaveRight_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Right Wave";
            Process.Start("mailto:feedback@mi9.com.au?Subject=I%20just%20wanted%20to%20say%20hello%20Mi9!!!&body=Gesture%209%20rocks!");
            ResetGestures();
        }

        private void _gestureNineLeft_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Left Nine";
            StartFirefox("http://ninemsn.com.au");
            ResetGestures();
        }

        private void _gestureNineRight_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Right Nine";
            StartFirefox("http://9news.com.au");
            ResetGestures();
        }

        private void _gestureRugby_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Rugby League";
            StartFirefox("http://wwos.com.au/league");
            ResetGestures();
        }

        private void _gestureAfl_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "AFL";
            StartFirefox("http://wwos.com.au/afl");
            ResetGestures();
        }

        private void _gestureJump_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Jump";
            StartFirefox("http://9jumpin.com.au");
            ResetGestures();
        }

        private void _gestureHatOn_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Hat On";
            _currentRenderer = _mexicanRenderer;
            ResetGestures();
        }

        private void _gestureHatOff_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Hat Off";
            _currentRenderer = _skeletonRenderer;
            ResetGestures();
        }

        private void _gestureTravolta_GestureRecognized(object sender, EventArgs e)
        {
            GestureAction.Content = "Travolta";
            StartFirefox("http://i.dailymail.co.uk/i/pix/2013/07/10/article-2359745-1AC12E42000005DC-308_634x1246.jpg");
            ResetGestures();
        }
    }
}
