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

namespace _005.Invoke
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Action _action = FormFunc;
            _action();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Action _action = FormFunc;
            _action.Invoke();  // Delegate.Invoke()方法，等价于_action()
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Action _action = FormFunc;
            Invoke(_action);  // Control.Invoke方法，在拥有此控件的线程上（主线程）执行该委托
        }

        public void FormFunc()
        {
            MessageBox.Show("执行函数");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread _thread = new Thread(ButtonFunc);
            _thread.Start();
        }

        public void ButtonFunc()
        {
           Invoke(new Action(() =>
           {
               button4.Text = "跨线程调用成功";
           }));
        }
    }
}
