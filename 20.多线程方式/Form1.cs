using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20.多线程方式
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();
        private object _lock = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void button_sync_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            var stopwatch = Stopwatch.StartNew();

            foreach (var site in Contents.WebSites)
            {
                var result = DownloadWebSiteSync(site);
                richTextBox1.Text += result;
                Application.DoEvents();
            }

            richTextBox1.Text += $"总耗时: {stopwatch.Elapsed}{Environment.NewLine}";
        }

        private void ReportResult(string result)
        {
            Invoke(new Action(() =>
            {
                richTextBox1.Text += result;
                Application.DoEvents();
            }));
        }

        private async void button_async_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            var stopwatch = Stopwatch.StartNew();
            await DownLoad_Async();
            richTextBox1.Text += $"总耗时: {stopwatch.Elapsed}{Environment.NewLine}";

        }

        public async Task DownLoad_Async()
        {
            // 每个网站一个下载线程
            foreach (var site in Contents.WebSites)
            {
                byte[] result = await Task.Run(() =>
                {
                    return DownloadWebSite(site);
                });
                richTextBox1.Text += $"从{site}下载数据结束：共下载 {result.Length}字节。{Environment.NewLine}";
            }
        }

        public byte[] DownloadWebSite(string url)
        {
            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            byte[] responsePayloadBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            return responsePayloadBytes;
        }

        private string DownloadWebSiteSync(string url)
        {
            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            var responsePayloadBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            return $"从{url}下载数据结束：共下载 {responsePayloadBytes.Length}字节。{Environment.NewLine}";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            var stopwatch = Stopwatch.StartNew();

            Thread _thread = new Thread(() =>
            {
                foreach (var site in Contents.WebSites)
                {
                    var result = DownloadWebSiteSync(site);
                    Invoke(new Action(() =>
                    {
                        richTextBox1.Text += result;
                        Application.DoEvents();
                    }));
                }
            });
            _thread.Start();
            // 如果这里使用了Join方法，会造成死锁,，且会阻塞UI线程
            // _thread.Join();
            richTextBox1.Text += $"总耗时: {stopwatch.Elapsed}{Environment.NewLine}";
        }



        /// <summary>
        /// 异步并发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            var stopwatch = Stopwatch.StartNew();

            List<Task> DownLoadTaskList = new List<Task>();

            foreach (var site in Contents.WebSites)
            {
                DownLoadTaskList.Add(Task.Run(() =>
                {
                    var response = httpClient.GetAsync(site).GetAwaiter().GetResult();
                    var responsePayloadBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                    ReportResult($"从{site}下载数据结束：共下载 {responsePayloadBytes.Length}字节。{Environment.NewLine}");
                }));
            }
            Task.WhenAll(DownLoadTaskList).ContinueWith(t =>
            {
                Invoke(new Action(() =>
                {
                    richTextBox1.Text += $"总耗时: {stopwatch.Elapsed}{Environment.NewLine}";
                }));
            });
        }

        private void button_download_async_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            var stopwatch = Stopwatch.StartNew();

            List<Task> DownLoadTaskList = new List<Task>();

            foreach (var site in Contents.WebSites)
            {
                DownLoadTaskList.Add(DownLoad_Async(site));
            }
            Task.WhenAll(DownLoadTaskList).ContinueWith(t =>
            {
                Invoke(new Action(() =>
                {
                    richTextBox1.Text += $"总耗时: {stopwatch.Elapsed}{Environment.NewLine}";
                }));
            });
        }


        private async Task DownLoad_Async(string site)
        {
            var response = await httpClient.GetAsync(site);
            var responsePayloadBytes = await response.Content.ReadAsByteArrayAsync();
            ReportResult($"从{site}下载数据结束：共下载 {responsePayloadBytes.Length}字节。{Environment.NewLine}");
        }
    }


    public class Contents
    {
        public static readonly IEnumerable<string> WebSites = new string[]
        {
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com",
            "https://www.zhihu.com"
        };
    }
}
