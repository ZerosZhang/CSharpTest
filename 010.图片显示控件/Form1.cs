using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _010.图片显示控件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dialog = new OpenFileDialog();
            _dialog.Filter = "图片|*.png;*.bmp;*.jpg;*.tif";
            _dialog.Title = "打开图像文件";
            _dialog.RestoreDirectory = true;

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                Image _image = Image.FromFile(_dialog.FileName);
                pictureBox2.AddImage(_image);
            }
        }
    }
}
