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
    public partial class AsyncClient : Form
    {
        private Socket mCommunication;
        private byte[] buffer = new byte[1024];

        public AsyncClient()
        {
            InitializeComponent();

            mCommunication = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mCommunication.BeginConnect(IPAddress.Parse("127.0.0.1"), 8888, new AsyncCallback(ConnectCallBack), mCommunication);
        }

        public void ConnectCallBack(IAsyncResult _result)
        {
            mCommunication.EndConnect(_result);
            mCommunication.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), mCommunication);
        }

        public void ReceiveCallBack(IAsyncResult _result)
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
                mCommunication.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), mCommunication);
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

        private void AsyncClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mCommunication != null && mCommunication.Connected)
            {
                mCommunication.Shutdown(SocketShutdown.Both);
                mCommunication.Disconnect(false);
                mCommunication.Close();
            }
        }
    }
}
