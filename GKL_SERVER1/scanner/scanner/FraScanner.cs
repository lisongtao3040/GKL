using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;

namespace scanner
{
    public partial class FraScanner : Form
    {
        public SerialPort serialPort;

        public FraScanner()
        {
            InitializeComponent();
        }

        //Com口遍历
        private void ComNameInit()
        {
            this.btnConnect.Enabled = true;
            this.btnStop.Enabled = false;

            cbComName.Items.Clear();
            this.cbComName.Items.Add("请选择COM口");
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.cbComName.Items.Add(portName);
            }

        }

        /// <summary>
        /// 实例化串行端口资源
        /// </summary>
        private void InstanceSerialPort()
        {
            //实例化串行端口
            serialPort = new SerialPort();
            //端口名  注:因为使用的是USB转RS232 所以去设备管理器中查看一下虚拟com口的名字
            serialPort.PortName = this.cbComName.Text;
            //波特率
            serialPort.BaudRate = Convert.ToInt32(this.comboBox_comPl.Text);
            //奇偶校验
            serialPort.Parity = Parity.None;
            //停止位
            serialPort.StopBits = StopBits.One;
            //数据位
            serialPort.DataBits = 8;
            //忽略null字节

            serialPort.DiscardNull = true;
            //接收事件
            serialPort.DataReceived += serialPort_DataReceived;
            //开启串口
            serialPort.Open();
        }

        private void FraScanner_Load(object sender, EventArgs e)
        {
            //Com口遍历
            ComNameInit();
            if (this.cbComName.Items.Count >= 2 ){
                this.cbComName.SelectedIndex = 1;
                //ConnectCom();
            }
                
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort serialPort = (SerialPort)sender;
                //开启接收数据线程
                Thread threadReceiveSub = new Thread(new ParameterizedThreadStart(ReceiveData));
                threadReceiveSub.Start(serialPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //扫描器接收数据
        private void ReceiveData(object serialPortobj)
        {
            try
            {
                SerialPort serialPort = (SerialPort)serialPortobj;

                //防止数据接收不完整 线程sleep(100)
                System.Threading.Thread.Sleep(100);

                string str = serialPort.ReadExisting();

                if (str == string.Empty)
                {
                    return;
                }
                else
                {
                    SetMessage(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="msg"></param>
        private void SetMessage(string msg)
        {
            richTextBox1.Invoke(new Action(() => { 
                richTextBox1.Text = (msg + "\r\n");
                this.Enabled = false;
                SendKeys.SendWait("{F8}");
                Thread.Sleep(300);
                SendKeys.Send(msg);
                SendKeys.SendWait("{ENTER}");
                this.Enabled = true;
            }));
        }
        //开始Com连接
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectCom();
        }
        private void ConnectCom()
        {
            try
            {
                InstanceSerialPort();
                SetControlStaus(true);
                SetMessage("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //停止Com连接
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlStaus(false);
                serialPort.Close();
                //richTextBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //控件状态设定
        private void SetControlStaus(Boolean connectStaus) {
            this.btnConnect.Enabled = !connectStaus;
            this.btnStop.Enabled = connectStaus;
            this.cbComName.Enabled = !connectStaus;
            this.comboBox_comPl.Enabled = !connectStaus;        
        }
    }
}
