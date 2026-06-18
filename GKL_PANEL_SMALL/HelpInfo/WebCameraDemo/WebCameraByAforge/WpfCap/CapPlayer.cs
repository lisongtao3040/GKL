using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace WpfCap
{
    public class CapPlayer : Image,IDisposable
    {
        public CapPlayer()
        {
            //initBitmap();
            //Application.Current.Exit += new ExitEventHandler(Current_Exit);

            
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            this.Dispose();
        }
        public void start()
        { initBitmap(); }

        void initBitmap()
        {
            
            if (Device == null)
            {
                Device = new CapDevice();
                Device.OnNewBitmapReady += new EventHandler(_device_OnNewBitmapReady);
            }
            else
            {
                Device.Start();
            }
        }

        void _device_OnNewBitmapReady(object sender, EventArgs e)
        {
            Binding b = new Binding();
            b.Source = Device;
            b.Path = new PropertyPath(CapDevice.FramerateProperty);
            this.SetBinding(CapPlayer.FramerateProperty, b);

            this.Source = Device.BitmapSource;
        }


        public CapDevice _device;

        public CapDevice Device
        {
            get { return _device; }
            set { _device = value; }
        }


        public float Framerate
        {
            get { return (float)GetValue(FramerateProperty); }
            set { SetValue(FramerateProperty, value); }
        }
        public static readonly DependencyProperty FramerateProperty =
            DependencyProperty.Register("Framerate", typeof(float), typeof(CapPlayer), new UIPropertyMetadata(default(float)));








        #region IDisposable Members

        public void Dispose()
        {
            if (Device != null)
                Device.Dispose();
        }

        #endregion
    }
}
