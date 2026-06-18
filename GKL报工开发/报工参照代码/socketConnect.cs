using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;


namespace OderConfirmForSap
{
    public class socketConnect
    {
        Socket sokClient;
        public delegate void DataHandler(string strData);
        public delegate void ConnectedHandler(bool blConnected);
        public delegate void MessageShow(string strMessage,bool bl);

        public static event DataHandler DataReceive;
        public static event ConnectedHandler ConnectedChanged;
        public static event MessageShow showMessage;

        public bool isConnected = false;
        string strIp = "10.160.192.116";
        //string strIp = "10.160.195.108";
        string strPort = "13909";

        Thread threadClient;

        bool isRec = true;

        //public string serverIp = "10.160.192.116";
        ////public string serverIp = "10.160.195.108";
        //public string serverPort = "13909";


        public socketConnect()
        {
            //this.strIp = serverIp;
            //this.strPort = strPort;
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            isRec = true;
            if (sokClient != null && sokClient.Connected == true)
            {
                return;
            }

            //实例化 套接字
            sokClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建 ip对象
            IPAddress address = IPAddress.Parse(strIp);
            //创建网络节点对象 包含 ip和port
            IPEndPoint endpoint = new IPEndPoint(address, int.Parse(strPort));
            //连接 服务端监听套接字

            try
            {
                sokClient.Connect(endpoint);

                ConnectedChanged(true);
                showMessage("服务器连接成功！",true);

                //创建负责接收 服务端发送来数据的 线程
                threadClient = new Thread(ReceiveMsg);
                threadClient.IsBackground = true;

                threadClient.Start();

                isConnected = true;
            }
            catch (System.Exception e)
            {
                if (((System.Reflection.MemberInfo)(e.GetType())).Name == "SocketException" && ((System.Net.Sockets.SocketException)(e)).ErrorCode.ToString() == "10061")
                {
                    ConnectedChanged(false);
                    showMessage("服务器端没有开启",false);
                }
                else
                {
                    ConnectedChanged(false);
                    showMessage("服务器连接错误", false);
                }
            }
        }

        public void DisConnectServer()
        {
            try
            {
                isRec = false;
                Thread.Sleep(100);

                ConnectedChanged(false);
                showMessage("服务器连接中止！",false);

                sokClient.Close();

            }
            catch (System.Exception e)
            {
                //logg.writeLog2(e.ToString());
            }

        }

        void ReceiveMsg()
        {
            while (isRec)
            {
                byte[] msgArr = new byte[1024 * 1024 * 1];//接收到的消息的缓冲区
                int length = 0;
                try
                {
                    //接收服务端发送来的消息数据
                    length = sokClient.Receive(msgArr);

                    byte[] lenBytes = msgArr.ToList().GetRange(0, 4).ToArray();
                    int packageLen = BitConverter.ToInt32(lenBytes, 0);

                    //strMessFlag = System.Text.Encoding.UTF8.GetString(msgArr, 4, 2);

                    DataReceive(System.Text.Encoding.UTF8.GetString(msgArr, 4, packageLen ));
                }
                catch (Exception ex)
                {
                    if (((System.Reflection.MemberInfo)(ex.GetType())).Name == "SocketException" && ((System.Net.Sockets.SocketException)(ex)).ErrorCode == 10054)
                    {
                        ConnectedChanged(false);
                        showMessage("服务器中止了连接",false);
                        return;
                    }
                    else
                    {
                        ConnectedChanged(false);
                        //showMessage(ex.ToString());
                        //logg.writeLog1(ex.ToString());
                        return;
                    }
                }
            }
        }

        public bool sendMessage(string strData)
        {
            try
            {
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strData);


                //先对数据进行包装,就是把包的大小作为头加入,这必须与服务器端的协议保持一致,否则造成服务器无法处理数据.  
                byte[] buff = new byte[arrMsg.Length + 4];
                Array.Copy(BitConverter.GetBytes(arrMsg.Length), buff, 4);
                Array.Copy(arrMsg, 0, buff, 4, arrMsg.Length);



                sokClient.Send(buff);
                showMessage("发送报工数据成功",true);
                return true;
            }
            catch (Exception ex)
            {
                if (((System.Reflection.MemberInfo)(ex.GetType())).Name == "SocketException" && ((System.Net.Sockets.SocketException)(ex)).ErrorCode == 10054)
                {
                    ConnectedChanged(false);
                    showMessage("服务器中断了连接",false);
                    return false;
                }
                else
                {
                    ConnectedChanged(false);
                    showMessage("数据发送错误",false);
                    return false;
                }
            }
        }

        public bool isConnect
        {
            get { return isConnected; }
        }

    }
}
