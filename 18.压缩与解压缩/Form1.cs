using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18.压缩与解压缩
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dialog = new OpenFileDialog();
            _dialog.Multiselect = false;
            _dialog.Title = "压缩文件";
            _dialog.RestoreDirectory = true;

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                // 压缩文件为相同路径下的同名压缩包
                string _file_path = _dialog.FileName;
                string _file_name = Path.GetFileNameWithoutExtension(_file_path);
                string _parent_path = Path.GetDirectoryName(_file_path);
                string _zip_path = Path.Combine(_parent_path, _file_name + ".zip");
                string _zip_name = Path.GetFileName(CompressFileZip(_file_path, _zip_path));
                MessageBox.Show($"文件压缩完成 —— {_zip_name}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();
            _dialog.Description = "压缩文件夹";
            if (_dialog.ShowDialog()== DialogResult.OK)
            {
                // 压缩文件夹为相同路径下的同名压缩包
                string _directory_path = _dialog.SelectedPath;
                string _directory_name = Path.GetFileName(_directory_path);
                string _parent_path = Path.GetDirectoryName(_directory_path);
                string _zip_path = Path.Combine(_parent_path, _directory_name + ".zip");
                string _zip_name = Path.GetFileName(CompressDirectoryZip(_directory_path, _zip_path));
                MessageBox.Show($"文件夹压缩完成 —— {_zip_name}");
            }
        }


        /// <summary>
        /// 将指定目录压缩为Zip文件，返回压缩包路径
        /// </summary>
        /// <param name="_directory_path">文件夹地址 D:/1/ </param>
        /// <param name="_zip_path">zip地址 D:/1.zip </param>
        public static string CompressDirectoryZip(string _directory_path, string _zip_path)
        {
            try
            {
                // 如果文件已存在，则需要添加后缀
                string _temp_zip_path = _zip_path;
                int _index = 0;
                while (File.Exists(_temp_zip_path))
                {
                    string _new_zip_name = Path.GetFileNameWithoutExtension(_zip_path) + $"({_index++})";
                    _temp_zip_path = Path.Combine(Path.GetDirectoryName(_zip_path), _new_zip_name + ".zip");
                }
                _zip_path = _temp_zip_path;

                // ZipFile.CreateFromDirectory 函数不支持自动创建父文件夹，因此需要判断
                string _parent_directory = Path.GetDirectoryName(_zip_path);
                if (!Directory.Exists(_parent_directory))
                {
                    Directory.CreateDirectory(_parent_directory);
                }
                // 创建压缩文件
                ZipFile.CreateFromDirectory(_directory_path, _zip_path, CompressionLevel.Optimal, false);
                return _zip_path;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }


        /// <summary>
        /// 将指定文件压缩为Zip文件，返回压缩包路径
        /// </summary>
        /// <param name="_file_path">文件地址 D:/1.txt </param>
        /// <param name="_zip_path">zip地址 D:/1.zip </param>
        public static string CompressFileZip(string _file_path, string _zip_path)
        {
            // 创建临时存储柜
            string _temp_directory = Path.Combine(Path.GetDirectoryName(_file_path), $"{Guid.NewGuid()}_temp");
            if (!Directory.Exists(_temp_directory))
            {
                Directory.CreateDirectory(_temp_directory);
            }
            try
            {
                // 如果文件已存在，则需要添加后缀
                string _temp_zip_path = _zip_path;
                int _index = 0;
                while (File.Exists(_temp_zip_path))
                {
                    string _new_zip_name = Path.GetFileNameWithoutExtension(_zip_path) + $"({_index++})";
                    _temp_zip_path = Path.Combine(Path.GetDirectoryName(_zip_path), _new_zip_name + ".zip");
                }
                _zip_path = _temp_zip_path;

                // 先将文件移动到临时文件夹，然后压缩文件夹
                string _file_name = Path.GetFileName(_file_path);
                File.Copy(_file_path, Path.Combine(_temp_directory, _file_name));
                CompressDirectoryZip(_temp_directory, _zip_path);
                return _zip_path;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                if (Directory.Exists(_temp_directory))
                {
                    Directory.Delete(_temp_directory, true);
                }
            }
        }


        /// <summary>
        /// 解压Zip文件到指定目录，文件名保持不变
        /// </summary>
        /// <param name="_zip_path">zip地址 D:/1.zip</param>
        /// <param name="_directory_path">文件夹地址 D:/1/</param>
        public static string DecompressZip(string _zip_path, string _directory_path)
        {
            try
            {
                if (!Directory.Exists(_directory_path))
                {
                    Directory.CreateDirectory(_directory_path);
                }
                else
                {
                    // 如果文件已存在，则需要添加后缀
                    string _temp_directory_path = _directory_path;
                    int _index = 0;
                    while (Directory.Exists(_temp_directory_path))
                    {
                        string _new_directory_name = Path.GetFileName(_directory_path) + $"({_index++})";
                        _temp_directory_path = Path.Combine(Path.GetDirectoryName(_directory_path), _new_directory_name);
                    }
                    _directory_path = _temp_directory_path;
                }
                ZipFile.ExtractToDirectory(_zip_path, _directory_path);
                return _directory_path;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dialog = new OpenFileDialog();
            _dialog.Multiselect = false;
            _dialog.Title = "压缩文件";
            _dialog.RestoreDirectory = true;

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                // 压缩文件为相同路径下的同名压缩包
                string _zip_path = _dialog.FileName;
                string _zip_name = Path.GetFileNameWithoutExtension(_zip_path);
                string _parent_path = Path.GetDirectoryName(_zip_path);
                string _directory_path = Path.Combine(_parent_path, _zip_name);
                string _directory_name = Path.GetFileName(DecompressZip(_zip_path, _directory_path));
                MessageBox.Show($"文件解压缩完成 —— {_directory_name}");
            }
        }
    }
}
