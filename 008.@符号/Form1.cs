using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _008._符号
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _str = @"C:\Users\Zeros遇见\Desktop\1.txt";
            richTextBox1.AppendText(_str + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _str = "C:\\Users\\Zeros遇见\\Desktop\\1.txt \r\n";
            richTextBox1.AppendText(_str);
        }

        [Obsolete]
        void Test()
        {
            string @string = "不推荐使用";
            richTextBox1.AppendText(@string);
        }
    }
}
