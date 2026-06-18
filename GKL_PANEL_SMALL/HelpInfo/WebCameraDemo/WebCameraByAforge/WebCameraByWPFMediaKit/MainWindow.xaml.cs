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
using WPFMediaKit.DirectShow.Controls;
using System.IO;

namespace WebCameraByWPFMediaKit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string fileName = "";
        private void btnCap_Click(object sender, RoutedEventArgs e)
        {
          
            //vce.VideoCaptureDevice
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)vce.ActualWidth, (int)vce.ActualHeight, 96, 96, PixelFormats.Default);
            vce.Measure(vce.RenderSize);
            vce.Arrange(new Rect(vce.RenderSize));
            bmp.Render(vce);

            BitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                byte[] captureData = ms.ToArray();
                fileName = "photos\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                File.WriteAllBytes(fileName, captureData);
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            vce.VideoCaptureSource = null;


        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                vce.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];

            }
            else
            {
                MessageBox.Show("未检测到任何可用摄像头!");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vce.VideoCaptureSource = null;
        }
    }
}
