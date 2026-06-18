using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CameraTest
{
    ///   <summary>   
    ///   一个控制摄像头的纴E  
    ///   </summary>   
    public class cVideo
    {
        //导葋EPI函数
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //常量设置
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        private const int WM_CAP_FILE_SAVEAS = WM_CAP_START + 23;   //将捕捉文件保存为龙罨个用户指定的文件。这个消息不会改变捕捉文件的名字和内容,

        //全局变量
        private IntPtr hWndC;//句眮E
        private IntPtr mControlPtr;//句眮E
        private bool bWorkStart = false;
        private int mWidth;//视频显示窥胰
        private int mHeight;   //视频显示高度
        private int mLeft;    //视频显示左边緛E
        private int mTop;     //视频显示上边緛E
        ///   <summary>   
        ///   初始化显示图蟻E  
        ///   </summary>   
        ///   <param   name="handle">播放视频控件的句眮E/param>   
        ///   <param   name="left">视频显示的左边緛E/param>   
        ///   <param   name="top">视频显示的上边緛E/param>
        ///   <param   name="down">视频显示的下边緛E/param>
        ///   <param   name="right">视频显示的右边緛E/param>
        ///   <param   name="width">要显示视频的窥胰</param>   
        ///   <param   name="height">要显示视频的长度</param>   
        public cVideo(IntPtr handle, int left, int top, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
            mLeft = left;
            mTop = top;
        }
        ///   <summary>   
        ///   打开视频 
        ///   </summary>   
        public void StartVideo()
        {
            if (bWorkStart)
                return;
            bWorkStart = true;
            byte[] lpszName = new byte[100];
            //创建播放窗口
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);

            if (hWndC.ToInt32() != 0)
            {
                //显示视频
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 100, 0);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
            }
        }
        ///   <summary>   
        ///   关闭视频  
        ///   </summary>   
        public void StopVideo()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bWorkStart = false;
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="path">文件路径</param>
        public void Images(string path)
        {
            IntPtr imagePath = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, imagePath.ToInt32());
        }

        /// <summary>
        /// 录蟻E
        /// </summary>
        /// <param name="path">要保存avi文件的路径</param>
        public void Kinescope(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt32());
            SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);
        }

        /// <summary>
        /// 停止录蟻E
        /// </summary>
        public void StopKinescope()
        {
            SendMessage(hWndC, WM_CAP_STOP, 0, 0);
        }
    }
}
