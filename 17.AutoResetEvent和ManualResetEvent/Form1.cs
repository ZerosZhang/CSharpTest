using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace _17.AutoResetEvent和ManualResetEvent
{
    public partial class Form1 : Form
    {
        // 初始值为true，waitone不会阻塞，否则默认阻塞
        ManualResetEvent _lock = new ManualResetEvent(false);
        //AutoResetEvent _lock = new AutoResetEvent(false);

        public Form1()
        {
            InitializeComponent();
            Task.Run(TestFunctionAsync);
        }

        public void TestFunctionAsync()
        {
            while (true)
            {
                _lock.WaitOne();  // 如果状态为false则会阻塞
                Task.Delay(1000).Wait();
                Invoke(new Action(()=> 
                {
                    richTextBox1.AppendText(DateTime.Now.ToString() + "\r\n");
                }));
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _lock.Set();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            _lock.Reset();
        }
    }
}
