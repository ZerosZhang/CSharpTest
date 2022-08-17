using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006.Invoke和BeginInvoke的区别
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 同步调用委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("Invoke开始 \r\n");
            Invoke(new Action(() => { Test(); }));
            richTextBox1.AppendText("Invoke结束 \r\n");
        }

        /// <summary>
        /// 异步调用委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("BeginInvoke开始 \r\n");
            BeginInvoke(new Action(() => { Test(); }));
            richTextBox1.AppendText("BeginInvoke结束 \r\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("等待BeginInvoke开始 \r\n");
            IAsyncResult asyncResult = BeginInvoke(new Action(() => { Test(); }));
            EndInvoke(asyncResult);
            richTextBox1.AppendText("等待BeginInvoke结束 \r\n");
        }


        /// <summary>
        /// 耗时操作
        /// </summary>
        private void Test()
        {
            int sum = 0;
            for (int i = 0; i < 999999999; i++)
            {
                sum += i;
            }
            richTextBox1.AppendText("耗时操作结束 \r\n");
        }
    }
}
