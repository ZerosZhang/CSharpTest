using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _000.更新ReadMe文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter _write = new StreamWriter(File.Open("./ReadMe.md", FileMode.OpenOrCreate));
            _write.WriteLine("## 简介");
            _write.WriteLine("该库用于一些`C#`语言的简单测试，每个项目为一个独立的模块，目录如下，可跳转对应的博客界面");
            try
            {
                string[] _directories = null;
                _directories = Directory.GetDirectories("./");
                for (int i = 0; i < _directories.Length; i++)
                {
                    if (Path.GetFileName(_directories[i]).StartsWith("."))
                    {
                        continue;
                    }
                    _write.WriteLine(Path.GetFileName(_directories[i]));
                }
                MessageBox.Show("更新完毕...");
            }
            catch (Exception)
            {
                MessageBox.Show("写入错误...");
            }
            finally
            {
                _write.Close();
            }
        }
    }
}
