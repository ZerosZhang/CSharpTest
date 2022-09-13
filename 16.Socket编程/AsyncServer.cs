using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16.Socket编程
{
    public partial class AsyncServer : Form
    {

        private Socket mListenSocket;                                  //监听套接字
        private Socket mCommunication;
        byte[] buffer = new byte[1024];

        public AsyncServer()
        {
            InitializeComponent();

            mListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                  //实例化一个TCP流式套接字
            mListenSocket.Bind(new IPEndPoint(IPAddress.Any, 8888));                   //将套接字绑定到本地端口
            mListenSocket.Listen(5);                               //将套接字设置为侦听状态
            mListenSocket.BeginAccept(new AsyncCallback(AcceptCallBack), mListenSocket);
        }

        void AcceptCallBack(IAsyncResult _result)
        {
            Socket _accept = (Socket)_result.AsyncState;   // 这里的_accept就是 mListenSocket，这里只是提供另一种写法
            mCommunication = _accept.EndAccept(_result);
            ShowMessage($"客户端{mCommunication.RemoteEndPoint}已连接");

            mCommunication.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), mCommunication);
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _length = mCommunication.EndReceive(_result);
                if (_length == 0)
                {
                    ShowMessage("客户端断开连接");

                    return;
                }
                ShowMessage(Encoding.Default.GetString(buffer, 0, _length));
                //清空数据，重新开始异步接收
                mCommunication.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), mCommunication);
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

        private void AsyncServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            mCommunication.Shutdown(SocketShutdown.Both);
            mCommunication.Disconnect(false);
            mCommunication.Close();
            mListenSocket.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mCommunication != null && mCommunication.Connected)
            {
                byte[] _buffer = Encoding.Default.GetBytes(richTextBox2.Text);
                mCommunication.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), mCommunication);
            }
        }

        public void SendCallback(IAsyncResult _result)
        {
            //完成消息发送
            int length = mCommunication.EndSend(_result);
        }
    }
}
