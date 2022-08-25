using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _013.防止程序二次启动
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (CheckRepeatRun())
            {
                MessageBox.Show("软件已打开，请关闭！", "提示", MessageBoxButtons.OKCancel);
                return;
            }
            Application.Run(new Form1());
        }


        /// <summary>
        /// 检测是否已经打开了对应名字的线程，预防二次打开
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static bool CheckRepeatRun()
        {
            // 如果该程序进程数量大于，则说明该程序已经运行
            Process[] _process = Process.GetProcessesByName(Application.ProductName);
            return _process.Length > 1;
        }
    }
}
