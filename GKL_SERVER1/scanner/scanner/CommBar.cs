using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace scanner
{
    /// <summary>
    /// 扫描枪工作类
    /// </summary>
    class CommBar
    {
        //串口引用
        public SerialPort serialPort;

        //存储转换的数据值
        public string Code { get; set; }

        public CommBar()
        {
            serialPort = new SerialPort();
        }

        //串口状态
        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        //打开串口
        public bool Open()
        {
            if (serialPort.IsOpen)
            {
                Close();
            }
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                return true;
            }
            else
            {
                MessageBox.Show("串口打开失败！");
                return false;
            }
        }

        //关闭串口
        public void Close()
        {
            serialPort.Close();
        }

        //定入数据，这里没有用到
        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                serialPort.Write(send, offSet, count);
            }
        }

        //获取可用串口
        public string[] GetComName()
        {
            string[] names = null;
            try
            {
                names = SerialPort.GetPortNames(); // 获取所有可用串口的名字
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("找不到串口");
            }
            return names;
        }

        //注册一个串口
        public void SerialPortValue(string portName, int baudRate,int dataBits,int stopBits,int parity)
        {
            //串口名
            serialPort.PortName = portName;
            //波特率
            serialPort.BaudRate = baudRate;
            //数据位
            serialPort.DataBits = dataBits;//8

            //停止位
            //不使用停止位。System.IO.Ports.SerialPort.StopBits 属性不支持此值。None = 0,
            //使用一个停止位。 One = 1,
            //使用两个停止位。Two = 2,
            //使用 1.5 个停止位。OnePointFive = 3
            serialPort.StopBits = (StopBits)stopBits;//StopBits.One

            //校验位
            //不发生奇偶校验检查。None = 0,
            //设置奇偶校验位，使位数等于奇数。Odd = 1,
            //设置奇偶校验位，使位数等于偶数。Even = 2,
            // 将奇偶校验位保留为 1。Mark = 3,
            //将奇偶校验位保留为 0。Space = 4
            serialPort.Parity =(Parity)parity;//Parity.None
            serialPort.ReadTimeout = 100;
            //commBar.serialPort.WriteTimeout = -1;
        }


        //委托，指向CodeText方法
        //private delegate void ModifyButton_dg(CommBar commBar);

        //串口接收接收事件处理程序
        //每当串口讲到数据后激发
        public void getCode(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] m_recvBytes = new byte[serialPort.BytesToRead];//定义缓冲区大小
            int result = serialPort.Read(m_recvBytes, 0, m_recvBytes.Length);//从串口读取数据
            if (result <= 0)
                return;
            Code = Encoding.ASCII.GetString(m_recvBytes, 0, m_recvBytes.Length);//对数据进行转换
        }
    }
}
