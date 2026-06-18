using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CameraTest
{
    public partial class Form1 : Form
    {
        cVideo video = null;
        public Form1()
        {
            InitializeComponent();
            video = new cVideo(previewPictureBox.Handle, 0, 0, 400, 300);
            previewPictureBox.Visible = true;
            showPictureBox.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            video.StartVideo();
            previewPictureBox.Visible = true;
            showPictureBox.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            video.StopVideo();
            MessageBox.Show("关闭视频成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            video.Images("F:/1.jpg");
            video.StopVideo();

            showPictureBox.ImageLocation = "F:/1.jpg";
            previewPictureBox.Visible = false;
            showPictureBox.Visible = true;
            MessageBox.Show("拍照成功");
        }

        //C#照片显示在panel1中

    }
}
