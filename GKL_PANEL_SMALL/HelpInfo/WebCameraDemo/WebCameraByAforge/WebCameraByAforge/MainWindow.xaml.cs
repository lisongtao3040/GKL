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
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
 

namespace WebCameraByAforge
{
    struct Cam
    {
        public string Name;
        public string MonikerString;
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Cam> camList;
        private FilterInfoCollection collection;

        private VideoCaptureDevice visDevice;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDetectCam_Click(object sender, RoutedEventArgs e)
        {
            if (!InitWebcam())
            {

            }
        }
        private void btnSelectCam_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }
        /// <summary>
        /// 打开摄像头
        /// </summary>
        /// <returns></returns>
        private bool InitWebcam()
        {
            comboCamera.Items.Clear();
            comboResolution.Items.Clear();
            collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (collection.Count == 0)
            {
                MessageBox.Show("未检测到摄像头");
                return false;
            }
 
            int count = 1;
            camList = new List<Cam>();
            foreach (FilterInfo dev in collection)
            {

                Cam cam = new Cam();
                cam.Name = "摄像头" + count.ToString();
                cam.MonikerString = dev.MonikerString;
                comboCamera.Items.Add(cam.Name);
                camList.Add(cam);
                count++;

            }
           
            if (comboCamera.Items.Count > 0)
                comboCamera.SelectedIndex = 0; 
            return true;
        }
        private void StartCamera()
        {
            CloseWebcam();
            videoSourcePlayer1.VideoSource = visDevice;
            videoSourcePlayer1.Start();
        }
        private bool CloseWebcam()
        {
            if (videoSourcePlayer1.IsRunning)
                videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
            return true;
        }

        private void comboResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboResolution.SelectedIndex == -1)
                return;
            visDevice.VideoResolution = visDevice.VideoCapabilities[comboResolution.SelectedIndex];
        }

        private void comboCamera_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboCamera.SelectedIndex == -1)
                return;

            foreach (Cam cam in camList)
            {
                if (cam.Name == comboCamera.SelectedItem.ToString())
                {
                    visDevice = new VideoCaptureDevice(cam.MonikerString);
                }
            }
            if (visDevice == null)
            {
                MessageBox.Show("请检查摄像头");
                return;
            }

            comboResolution.Items.Clear();

            if (visDevice.VideoCapabilities == null || visDevice.VideoCapabilities.Length == 0)
            {
                MessageBox.Show("所选摄像头无支持分辨率");
                return;
            }

            foreach (VideoCapabilities vc in visDevice.VideoCapabilities)
            {
                comboResolution.Items.Add(string.Format("{0} x {1}", vc.FrameSize.Width, vc.FrameSize.Height));
            }

            comboResolution.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }

        private void btnCap_Click(object sender, RoutedEventArgs e)
        {
         System.Drawing.Bitmap bitmap =    videoSourcePlayer1.GetCurrentVideoFrame();
         if (bitmap != null)
         {
             bitmap.Save(   DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
             bitmap.Dispose();
         }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            CloseWebcam();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseWebcam();
        }
    }
}
