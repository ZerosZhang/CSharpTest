using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16.Socket编程
{
    public partial class Server : Form
    {
        private Socket mConnect;            // 监听Socket
        private Socket mCommunication;      // 通讯Socket

        public Server()
        {
            InitializeComponent();

            mConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //实例化一个TCP流式套接字
            mConnect.Bind(new IPEndPoint(IPAddress.Any, 8888));
            mConnect.Listen(50);                               //将套接字设置为侦听状态

            Task.Run(AcceptThread);
        }


        private void AcceptThread()
        {
            try
            {
                mCommunication = mConnect.Accept();   //同步等待客户端连接，建立新的套接字与客户端进行通信
                ShowMessage($"客户端{mCommunication.RemoteEndPoint}已连接");
                while (true)
                {
                    byte[] receiveByte = new byte[1024];           //存放接收到消息的缓存

                    int _length = mCommunication.Receive(receiveByte);       //同步接收客户端消息，将客户端消息转换为字符串
                    if (_length == 0)
                    {
                        ShowMessage("客户端断开连接");
                        return;
                    }

                    string receiveString = Encoding.Default.GetString(receiveByte, 0, _length);
                    ShowMessage(receiveString);
                }
            }
            catch (Exception)
            {
            }
        }

        void ShowMessage(string message)
        {
            Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"接收：{message}\r\n");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mCommunication != null  && mCommunication.Connected)
            {
                byte[] _buffer = Encoding.Default.GetBytes(richTextBox2.Text);
                mCommunication.Send(_buffer);
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mConnect != null)
            {
                mConnect.Close();
            }

            if (mCommunication != null && mCommunication.Connected)
            {
                mCommunication.Shutdown(SocketShutdown.Both);
                mCommunication.Disconnect(false);
                mCommunication.Close();
            }
        }
    }
}
