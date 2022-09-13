using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16.Socket编程
{
    public partial class Client : Form
    {
        private Socket mCommunication;

        public Client()
        {
            InitializeComponent();

            mCommunication = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mCommunication.Connect(IPAddress.Parse("127.0.0.1"), 8888);  // 127.0.0.1表示本地服务器

            Task.Run(ReceiveThread);
        }


        private void ReceiveThread()
        {
            while (true)
            {
                try
                {
                    //输出接收的消息
                    byte[] _byte = new byte[1024];           //存放接收到消息的缓存

                    int _length = mCommunication.Receive(_byte, _byte.Length, 0);//同步接收服务端消息
                    if (_length == 0)
                    {
                        ShowMessage("服务器断开连接");
                        return;
                    }
                    string _message = Encoding.Default.GetString(_byte, 0, _length);
                    ShowMessage(_message);
                }
                catch (Exception)
                {
                }
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
                mCommunication.Send(_buffer);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
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
