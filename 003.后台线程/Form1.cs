using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _003.后台线程
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label1.Text = "线程关闭";
            label1.ForeColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "线程开启";
            label1.ForeColor = Color.LimeGreen;

            Thread _thread = new Thread(UpdateTime);
            // 注释该行时，关闭窗口后线程无法关闭，继续执行
             _thread.IsBackground = true;        
            _thread.Start();
        }

        public void UpdateTime()
        {
            while (true)
            {
                DateTime.Now.ToString();
            }
        }
    }
}
