using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Reflection;


namespace OderConfirmForSap
{
    public partial class Form1 : Form
    {

        public string pubBarCode = "";
        public bool inProcessing = false;
        public string inProcessingOrderNo = "";

        public string userID;
        public string userPass;


        MsgWindow msg = new MsgWindow();

        socketConnect socC = null;

        //public string serverIp = "10.160.192.116";
        ////public string serverIp = "10.160.195.108";
        //public string serverPort = "13909";

        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            inProcessing = false;

            //Bt.ScanLib.Control.btScanEnable();
            msg.OnGetBarcode += new GetBarcodeEventHandler(getBarCode);

            socketConnect.ConnectedChanged += new socketConnect.ConnectedHandler(showConnectStatus);
            socketConnect.showMessage += new socketConnect.MessageShow(MessShow);
            socketConnect.DataReceive += new socketConnect.DataHandler(_dataRecive);

            ControlFormat();
            ConnectToServer();
            
            CheckUpdate();
        }


        /// <summary>
        /// 显示处理消息
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="bl"></param>
        public  void MessShow(string mess, bool bl)
        {
            this.Invoke(
                new Action(() =>
                {
                    this.lbl_Message.Text = mess;
                    this.lbl_Message.BackColor = bl ? Color.Lime : Color.Red;
                }
                    )
                );
        }

        public void getBarCode(string strBarCode)
        {
            //inProcessing = false;
            //testRead(strBarCode);

            strBarCode = strBarCode.Replace("\n", "").Replace("\r", "").Replace(" ", "");


            //新物料CODE/旧物料CODE/工单数量/捆包数量/库位/向先/托盘序号/工单号
            string[] ss = strBarCode.Split('/');
            if (ss.Length != 8)
            {
                MessShow("不是合法的生产明细书QR码",false);
                return;
            }

            this.lbl_OrderNo.Text = ss[7].Trim();
            this.lbl_MaterialCode.Text = ss[1].Trim();
            this.lbl_Amount.Text = ss[2].Trim();
            this.lbl_TrolleyNo.Text = ss[6].Trim();

            this.txt_ConfirmAmount.Text = ss[2].Trim();
            pubBarCode = strBarCode;
            MessShow("读取生产明细书成功",true);
            //this.button1.Focus();
        }


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 控件清空
        /// </summary>
        private void ControlFormat()
        {
            this.lbl_Amount.Text = "";
            this.lbl_MaterialCode.Text = "";
            this.lbl_OrderNo.Text = "";
            this.lbl_TrolleyNo.Text = "";
            this.txt_ConfirmAmount.Text = "";
            this.txt_ConfirmAmount.Enabled = false;
            pubBarCode = "";
            inProcessing = false;

        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            Bt.ScanLib.Control.btScanDisable();
            msg.OnGetBarcode -= new GetBarcodeEventHandler(getBarCode);
            socketConnect.ConnectedChanged -= new socketConnect.ConnectedHandler(showConnectStatus);
            socketConnect.showMessage -= new socketConnect.MessageShow(MessShow);
            socketConnect.DataReceive -= new socketConnect.DataHandler(_dataRecive);
        }

        private void ConnectToServer()
        {
            try
            {
                if (socC == null)
                    socC = new socketConnect();

                if (socC.isConnect == true)
                {
                    this.pnl_Login.Visible = true;
                }
                else
                {
                    socC = null;
                }
            }
            catch
            {

            }

        }

        private void disConnectToServer()
        {
            try
            {
                if (socC != null)
                {
                    socC.DisConnectServer();
                    socC = null;
                    showConnectStatus(false);
                }
            }
            catch 
            {
                //MessageBox.Show(ex.ToString());
                //return;
                //logg.writeLog2(ex.ToString());
            }
            finally
            {
            }
        }

        private void showConnectStatus(bool bl)
        {
            this.Invoke(
               new Action(() =>
               {
                   //this.lbl_ServerStatus.Text = bl ? "OK" : "NG";
                   this.lbl_ServerStatus.BackColor = bl ? Color.Lime : Color.Red;

                   if (bl == false)
                       MessShow("服务器连接中断",false);

               }
                   )
               );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataSend();
        }

        private void dataSend()
        {
            if (inProcessing)
            {
                MessShow("请等待上一条数据处理成功", false);
                return;
            }


            this.txt_ConfirmAmount.Enabled = false;

            if (string.IsNullOrEmpty(pubBarCode))
            {
                MessShow("没有可发送的数据", false);
                return;
            }

            string[] ss = pubBarCode.Split('/');
            inProcessingOrderNo = ss[7];
            ss[2] = this.txt_ConfirmAmount.Text;
            string sd = string.Join("/", ss.ToArray());
            if (socC == null)
            {
                MessShow("服务器未连接，不可报工", false);
                return;
            }

            socC.sendMessage("XX" + sd);

            pubBarCode = "";
            inProcessing = true;
        }
        private void btn_AmountR_Click(object sender, EventArgs e)
        {
            this.txt_ConfirmAmount.Enabled = true;
            this.txt_ConfirmAmount.Focus();
            this.txt_ConfirmAmount.Select(0, txt_ConfirmAmount.Text.Length);

        }

        private void _dataRecive(string strData)
        {
            inProcessing = false;
            string strDataFlag;
            if (strData.Length < 2)
            {
                MessShow("接收到不可识别数据",false);
                return;
            }

            strDataFlag = strData.Substring(0, 2);
            if (strDataFlag.ToUpper() == "XY")
            {
                MessShow(inProcessingOrderNo + " 报工成功",true);
                return;
            }
            else if (strDataFlag.ToUpper() == "XZ")
            {
                MessShow(inProcessingOrderNo + strData.Substring(2), false);
                return;
            }
            else if (strDataFlag.ToUpper() == "ZY")
            {
                MessShow("登录成功", true);
                this.Invoke(
                      new Action(() =>
                      {
                          this.pnl_Login.Visible = false;
                          this.txt_UserID.Enabled = false;
                          this.txt_UserPass.Enabled = false;
                          this.btn_Login.Enabled = false;
                          this.pnl_Login.Enabled = false;
                      }
                          )
                      );
                Bt.ScanLib.Control.btScanEnable();
                return;
            }
            else if (strDataFlag.ToUpper() == "ZZ")
            {
                MessShow(strData.Substring(2), false);
                return;
            }
            else if (strDataFlag.ToUpper() == "YZ")
            {

                isUpdate(strData);
            }

        }


        private void btn_Login_Click(object sender, EventArgs e)
        {
            socC.sendMessage("ZX" + this.txt_UserID.Text.PadRight(15, ' ') + this.txt_UserPass.Text.PadRight(15, ' '));
            return;
        }

        private void CheckUpdate()
        {
            if (!socC.isConnected || socC==null)
                return;


            else
            {
                var appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                socC.sendMessage("YX" + appVersion.ToString());
            }
        }

        private void isUpdate(string strData)
        {
            if (MessageBox.Show("服务器有更新程序，是否进行自动更新？" + "/r/n" + "当前版本:" + Assembly.GetExecutingAssembly().GetName().Version.ToString()
                , "更新提示", 
                MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;


            String path = this.GetType().Assembly.GetModules()[0].FullyQualifiedName;
            Int32 en = path.LastIndexOf("\\");
            string AppPath = path.Substring(0, en);

            FileStream fs = new FileStream(AppPath + "\\serverurl.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(strData.Substring(2));
            sw.Close();
            fs.Close();

            //启动升级
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = AppPath + "\\update.exe";
            process.Start();
            //System.Diagnostics.Process.Start(AppPath + "\\OderConfirmForSap.exe");
            this.Close();

        }

        private void testRead(string data)
        {

            String path = this.GetType().Assembly.GetModules()[0].FullyQualifiedName;
            Int32 en = path.LastIndexOf("\\");
            string AppPath = path.Substring(0, en);

            FileStream fs = new FileStream(AppPath + "\\tmpdata.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(data);
            sw.Close();
            fs.Close();
        }



        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == 13)
            {
                dataSend();
            }
        }





    }
}