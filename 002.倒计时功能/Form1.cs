using System.Windows.Forms;
using System.Threading;
using Timer = System.Threading.Timer;

namespace _002.倒计时功能
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        const int COUNTDOWNTIME = 5000;  // 超时时间
        static Timer mThreadTimer = new Timer(new TimerCallback(TimerCountDown), null, Timeout.Infinite, COUNTDOWNTIME);

        /// <summary>
        /// 定时到点执行的事件
        /// </summary>
        /// <param name="value"></param>
        private static void TimerCountDown(object _)
        {
            TimerStop();
            MessageBox.Show("提示！");
        }

        /// <summary>
        /// 倒计时开始
        /// </summary>
        private static void TimerStart()
        {
            mThreadTimer.Change(COUNTDOWNTIME, COUNTDOWNTIME);
        }

        /// <summary>
        /// 倒计时结束
        /// </summary>
        private static void TimerStop()
        {
            mThreadTimer.Change(Timeout.Infinite, COUNTDOWNTIME);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TimerStart();

            //下面的代码会导致界面卡死
            //Thread.Sleep(5000);
            //MessageBox.Show("提示！");
        }
    }
}
