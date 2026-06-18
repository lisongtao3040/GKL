using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;


namespace WPFtest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fullPath;
        public MainWindow()
        {
            InitializeComponent();
            cb.ItemsSource = MultimediaUtil.VideoInputNames;
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                cb.SelectedIndex = 0;//第0个摄像头为默认摄像头
            }
            else
            {
                MessageBox.Show("电脑没有安装任何可用摄像头");
                return;
            }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vce.VideoCaptureSource = (string)cb.SelectedItem;
        }

        /// <summary>
        /// 拍照
        /// </summary>

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            // 建立目标渲染图像器，高度为前台控件实显高度，此处不能使用.width或.height属性，否则出现错误。
            // 为了避免图像抓取出现黑边现象，需要对图象进行重新测量及缩放
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)vce.ActualWidth, (int)vce.ActualHeight, 96, 96, PixelFormats.Default);
            //VideoCaptureElement的Stretch="Fill"
            vce.Measure(vce.RenderSize);
            vce.Arrange(new Rect(vce.RenderSize));
            // 指定图像渲染目标
            bitmap.Render(vce);
            // 建立图像解码器。类型为jpeg
            BitmapEncoder encoder = new JpegBitmapEncoder();
            // 将当前渲染器中渲染位图作为一个位图帧加入解码器，进行解码，取得数据流。
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            // 建立内存流，将得到解码图像流写入内存流。
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                byte[] pics = stream.ToArray(); // 将流以文件形式存储于计算机中。
                string fileName = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                fullPath = System.IO.Path.Combine(@"E:\Photo", fileName + "_cap.jpg");
                //保存图片
                File.WriteAllBytes(fullPath, pics);
            }   // 预览效果暂停。
            vce.Pause();
        }

        /// <summary>
        /// 重拍
        /// </summary>

        private void btnanew_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(fullPath.ToString());
            vce.Play();
        }

    }
}