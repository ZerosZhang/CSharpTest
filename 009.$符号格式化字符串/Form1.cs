using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _009._符号格式化字符串
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double _num1 = 10;
            double _num2 = 20;
            richTextBox1.AppendText($"_num1 = {_num1:0###} \r\n");
            richTextBox1.AppendText($"_num2 = {_num2:f2} \r\n");
            richTextBox1.AppendText($"_num1 + _num2 = {_num1 + _num2} \r\n");
        }
    }
}
