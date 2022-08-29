using System;
using System.IO;
using System.Windows.Forms;

namespace _014.监控文件变化
{
    public partial class Form1 : Form
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        string mDirectoryPath = "./Data";

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(mDirectoryPath))
            {
                Directory.CreateDirectory(mDirectoryPath);
            }
            watcher.Path = mDirectoryPath;
            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"{e.Name}修改 \r\n");
            }));
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"{e.Name}创建 \r\n");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"./Data/Test.txt";
            StreamWriter _stream = new StreamWriter(path, true);  // 此处触发创建事件
            _stream.WriteLine($"测试-{DateTime.Now}");
            _stream.Flush();
            _stream.Close();  // 在此处触发修改事件
        }
    }
}
