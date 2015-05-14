using Microsoft.Kinect;
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
using Gesture9.Rendering;

namespace Gesture9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRenderer[] renderers;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private XmlJoint[] LoadSkeletonXml()
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlJoint[]));
            using (var file = System.IO.File.OpenRead(GetPath(@"skeleton.xml")))
            {
                var xmlJoints = (XmlJoint[])serializer.Deserialize(file);
                return xmlJoints;
            }
        }
        private string GetPath(string file)
        {
            var path = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var dir = System.IO.Path.GetDirectoryName(path);
            var fullPath = System.IO.Path.Combine(dir, file);
            return fullPath;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.renderers = new IRenderer[] {  new MexicanRenderer(this.canvas) };

            RenderAll();
        }

        private void RenderAll()
        {
            var joints = LoadSkeletonXml();
            foreach(var renderer in this.renderers)
            {
                renderer.Render(joints);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenderAll();
        }
        
    }
}
