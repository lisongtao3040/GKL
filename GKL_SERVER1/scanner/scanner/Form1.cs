using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace scanner
{
    public partial class Form1 : Form
    {
        CommBar commBar;

        bool buttonBool = false;//指示按钮是开还是关

        public Form1()
        {
            InitializeComponent();
            commBar = new CommBar();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox_Com.Items.Clear();
            this.comboBox_Com.Items.Add("请选择COM口");
            string[] comNames = commBar.GetComName();
            for (int i = 0; i < comNames.Length; i++)
            {
                this.comboBox_Com.Items.Add(comNames[i]);
            }
            OpenCom();
        }
        private void button_Test_Click(object sender, EventArgs e)
        {
            OpenCom();
        }
        private void OpenCom()
        {
            //关闭
            if (buttonBool)
            {
                buttonBool = false;

                commBar.Close();
                this.button_Test.Text = "点击连接";
            }
            //开始
            else if (!buttonBool)
            {
                buttonBool = true;
                this.button_Test.Text = "连接中";

                int dataBits = 8;
                int stopBits = 1;
                int parity = 0;

                int i;

                string coms;

                if (this.comboBox_Com.Text == "请选择COM口" || this.comboBox_Com.Text == "")
                {
                    for (i = 0; i <= this.comboBox_Com.Items.Count - 1; i++)
                    {
                        try
                        {
                            coms = this.comboBox_Com.Items[i].ToString();
                            this.comboBox_Com.SelectedIndex = i;
                            //注册一该串口
                            commBar.SerialPortValue(coms, Convert.ToInt32(this.comboBox_comPl.Text), dataBits, stopBits, parity);
                            //打开串口
                            if (commBar.Open())
                            {
                                //commBar.serialPort.Write("111");

                               // byte[] data = Convert.FromBase64String(commBar.serialPort.ReadLine());
                                //MessageBox.Show(Encoding.Unicode.GetString(data));
                                //关联事件处理程序
                                if (commBar.serialPort.IsOpen)
                                {
                                    commBar.serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                                    this.button_Test.Text = "连接 IS Connected";
                                    this.button_Test.Enabled = false;
                                    this.comboBox_Com.Enabled = false;
                                    this.comboBox_comPl.Enabled = false;
                                    //this.WindowState = FormWindowState.Minimized;
                                }

                                
                            }

                            return;


                        }
                        catch ( Exception e)
                        {
                            this.button_Test.Text = "请连接";
                        }

                    }
                }
                else
                {
                    //注册一该串口
                    commBar.SerialPortValue(this.comboBox_Com.Text, Convert.ToInt32(this.comboBox_comPl.Text), dataBits, stopBits, parity);
                    //打开串口
                    if (commBar.Open())
                    {
                        //关联事件处理程序
                        if (commBar.serialPort.IsOpen)
                        {
                            commBar.serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                            this.button_Test.Text = "连接 IS Connected";
                            this.button_Test.Enabled = false;
                            this.comboBox_Com.Enabled = false;
                            this.comboBox_comPl.Enabled = false;
                        }
                    }

                }

            }

        }



        //用来为文本框赋值
        string barcode;
        private void CodeText(CommBar commBar)
        {
            barcode += commBar.Code.ToString();

            if (barcode.Length > 300)
            {
                barcode = "";
            }

            if (barcode.IndexOf("\r") > 0 || barcode.IndexOf("\n") > 0)
            { 
                barcode = barcode.Replace("\n", "");
                barcode = barcode.Replace("\r", "");
                //this.textBox1.Text = "";
                //
                //SendKeys.SendWait(119);
                //Thread.Sleep(100);
                SendKeys.SendWait("{F8}");
                Thread.Sleep(300);

                //foreach (char c in barcode)
                //{
                //    SendKeys.Send(c.ToString());
                //    Thread.Sleep(500);
                //    //Console.WriteLine(c.ToString());
                //}
              
              
               SendKeys.Send(barcode);
               this.button1.Enabled = false;

               SendKeys.SendWait("{ENTER}");
               this.textBox1.Text = barcode;
               this.button1.Enabled = true;
               barcode = "";
            }
        }

        //委托，指向CodeText方法
        private delegate void ModifyButton_dg(CommBar commBar);

        //串口接收接收事件处理程序
        //每当串口讲到数据后激发
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            commBar.getCode(sender, e);
            //barcode = "";
            this.Invoke(new ModifyButton_dg(CodeText), commBar);//调用委托，将值传给文本框

            //CodeText(commBar);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            commBar.Close();
            commBar.serialPort.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //commBar.serialPort.DataReceived = "";

            try
            {
                if (commBar.serialPort.IsOpen)
                {
                    commBar.serialPort.DataReceived -= new SerialDataReceivedEventHandler(serialPort_DataReceived); ;
                    commBar.serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                //throw (new Exception(ex.Message));
            }
            
               
            commBar.Close();
           
            buttonBool = false;
            this.button_Test.Text = "连接";
            this.button_Test.Enabled = true;
            this.comboBox_Com.Enabled = true;
            this.comboBox_comPl.Enabled = true;
        }
    }
}
