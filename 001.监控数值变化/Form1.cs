using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _001.监控数值变化
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 定义需要监测的数值
        /// </summary>
        private int _value = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnMyValueChanged += new Action(WhenValueChanged);
            richTextBox1.AppendText($"初始_value = {mValue}\n");
        }

        private Action OnMyValueChanged;

        /// <summary>
        /// 使用属性的set来判断数值是否发生了改变
        /// </summary>
        public int mValue
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (OnMyValueChanged != null)
                    {
                        OnMyValueChanged();
                    }
                }
            }
        }

        private void WhenValueChanged()
        {
            richTextBox1.AppendText($"当前_value = {mValue}，");
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText($"_value发生了改变...\n");
            richTextBox1.SelectionColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mValue++;
        }
    }
}
