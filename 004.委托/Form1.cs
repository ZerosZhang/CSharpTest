using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _004.委托
{
    public partial class Form1 : Form
    {
        public delegate void DlgFormFunc();   // 委托类型——无参数，无返回值

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DlgFormFunc _dlg = FormFunc1;
            _dlg();
        }

        event DlgFormFunc _event;    // DlgFormFunc类型的事件
        private void button2_Click(object sender, EventArgs e)
        {
            _event += FormFunc1;
            _event += FormFunc2;
            _event();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Action _action = FormFunc1;
            _action();
        }

        public void FormFunc1()
        {
            // todo something
            MessageBox.Show("执行函数1");
        }

        public void FormFunc2()
        {
            // todo something
            MessageBox.Show("执行函数2");
        }
    }
}
