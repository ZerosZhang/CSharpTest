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
        Config _config = new Config(-10, 10);
        Config _config2 = new Config(-20, 20);
        public Form1()
        {
            InitializeComponent();
            propertyGrid1.SelectedObjects= new Config[]{ _config, _config2};
        }

        private void button_check_valid_Click(object sender, EventArgs e)
        {

            if (_config.Valid())
            {
                MessageBox.Show($"参数值为{_config.Number}，{_config.GrayValue}：配置有效");
            }
            else
            {
                MessageBox.Show($"参数值为{_config.Number}，{_config.GrayValue}：配置中有错误");
            }
        }
    }
}
