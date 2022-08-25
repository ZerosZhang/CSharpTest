using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _012.PropertyGrid控件使用
{
    public partial class Form1 : Form
    {
        Config _config = new Config();
        public Form1()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = _config;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _config.Length = 1.0;
            propertyGrid1.Refresh();
        }
    }

    public class Config
    {
        private int number;
        private string message;
        private double length;

        [DisplayName("数量"), Description("设置配置的数量")]
        public int Number { get => number; set => number = value; }
        [DisplayName("消息"), Description("设置配置的消息")]
        public string Message { get => message; set => message = value; }
        [DisplayName("长度"), Description("设置配置的长度")]
        public double Length { get => length; set => length = value; }

        public Config()
        {
            number = 1;
            message = "这是一条信息";
            length = 5.3;
        }
    }
}
