using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19.特性与扩展函数
{
    public partial class Form1 : Form
    {
        Config _config1 = new Config(-10, 10);
        Config _config2 = new Config(-20, -20);
        public Form1()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = _config1;
            //propertyGrid1.SelectedObjects = new Config[] { _config1, _config2 };  // 并不能同时显示两个对象中的变量
        }

        private void button_check_valid_Click(object sender, EventArgs e)
        {
            if (_config1.Valid())
            {
                MessageBox.Show($"参数值为{_config1.Number}，{_config1.GrayValue}：配置有效");
            }
            else
            {
                MessageBox.Show($"参数值为{_config1.Number}，{_config1.GrayValue}：配置中有错误");
            }
        }
    }
}
