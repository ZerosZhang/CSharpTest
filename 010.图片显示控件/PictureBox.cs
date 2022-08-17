using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _010.图片显示控件
{
    public partial class PictureBox : UserControl
    {
        private WindowState mWindowState;    // 标记控件的模式，在不同的位置点击会进入不同的模式
        private enum WindowState
        {
            MODE_NONE,                                  // 初始值：无模式
            MODE_ZOOM,                                  // 缩放模式,ROI与图片一起缩放
            MODE_MOVE_IMAG,                             // 移动图片
            MODE_EDIT_ROI,                              // 移动ROI的角点，包含移动ROI和编辑ROI
            MODE_CREATE_ROI                             // 创建ROI带角点，包含移动ROI和编辑ROI
        }

        private bool mMousePressed = false;
        private double mMouseDownX, mMouseDownY;        // 记录按下鼠标的位置

        private Image mImage;
        private int mImageWidth, mImageHeight;          // 图片宽,高
        private double mZoomFactor = 1;                 // 图片缩放比例
        double MinScale = Math.Pow(0.9, 30);            // 最小的缩放比例
        double MaxScale = Math.Pow(1 / 0.9, 30);        // 最大的缩放比例

        public PictureBox()
        {
            InitializeComponent();

            ActivityPanel.MouseWheel += ActivityPanel_MouseWheel;
            Resize += PictureBox_Resize;
        }

        /// <summary>
        /// 调整尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Resize(object sender, EventArgs e)
        {
            // Panel和PictureBox的尺寸必须和控件一样，不能设置Docker为Fill
            ActivityPanel.Size = ClientSize;
            ActivityPictureBox.Size = ClientSize;
            ActivityPictureBox.Location = new Point(0, 0);
            ActivityPictureBox.Refresh();
        }

        #region 鼠标事件
        private void ActivityPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMousePressed = true;
                mWindowState = WindowState.MODE_MOVE_IMAG;
                mMouseDownX = e.X;
                mMouseDownY = e.Y;
            }
        }

        private void ActivityPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMousePressed = false;
                mWindowState = WindowState.MODE_NONE;
            }
        }

        private void ActivityPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            /*win7系统下鼠标滚轮事件与win10不同，需要手动判断活动窗口*/
            OperatingSystem os = Environment.OSVersion;
            Version version = os.Version;
            if (version.Major == 6 && version.Minor == 1)     //windows 7
            {
                ActivityPictureBox.Focus();
            }

            // 在鼠标按下的情况下，如果条件满足，执行图片移动或者ROI移动
            if (mMousePressed)
            {
                int motionX, motionY;
                if (mWindowState == WindowState.MODE_MOVE_IMAG)
                {
                    motionX = (int)(e.X - mMouseDownX);
                    motionY = (int)(e.Y - mMouseDownY);

                    if ((motionX != 0) || (motionY != 0))
                    {
                        MoveImage(motionX, motionY);
                    }
                }
            }
        }

        private void ActivityPictureBox_MouseLeave(object sender, EventArgs e)
        {
            mMousePressed = false;
            mWindowState = WindowState.MODE_NONE;
        }

        /// <summary>
        /// Panel的鼠标滚轮缩放，这个优点是少计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            /*注意：该函数是Panel的事件，因此e是相对于Panel左上角的位置*/
            double _scale;

            if (e.Delta < 0)
                _scale = 0.9;
            else
                _scale = 1 / 0.9;

            ZoomImage(e.X, e.Y, _scale);
        }

        #endregion

        /// <summary>
        /// 移动图片
        /// </summary>
        /// <param name="_offset_x"></param>
        /// <param name="_offset_y"></param>
        private void MoveImage(int _offset_x, int _offset_y)
        {
            ActivityPictureBox.Location = new Point(ActivityPictureBox.Location.X + _offset_x,
                                                        ActivityPictureBox.Location.Y + _offset_y);
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="_center_x"></param>
        /// <param name="_center_y"></param>
        /// <param name="_scale"></param>
        private void ZoomImage(double _center_x, double _center_y, double _scale)
        {
            double _factor =  _scale * mZoomFactor;
            //超过一定缩放比例就不在缩放
            if (_factor < MinScale || _factor > MaxScale)
            {
                return;
            }
            mZoomFactor = _factor;

            int interval_x = (int)(ActivityPictureBox.Width * (_scale -1));
            int interval_y = (int)(ActivityPictureBox.Height * (_scale - 1));
            ActivityPictureBox.Size = new Size(ActivityPictureBox.Width + interval_x, ActivityPictureBox.Height + interval_y);
            ActivityPictureBox.Location = new Point((int)(ActivityPictureBox.Location.X - ((_center_x - ActivityPictureBox.Location.X) / (ActivityPictureBox.Width - interval_x) * interval_x)),
                                                    (int)(ActivityPictureBox.Location.Y - ((_center_y - ActivityPictureBox.Location.Y) / (ActivityPictureBox.Height - interval_y) * interval_y)));
        }



        /// <summary>
        /// 给控件添加图片
        /// </summary>
        /// <param name="_image"></param>
        public void AddImage(Image _image)
        {
            if (_image == null)
            {
                return;
            }
            if (mImage != null)
            {
                mImage.Dispose();
            }

            mImage = _image;
            mImageWidth = mImage.Width;
            mImageHeight = mImage.Height;

            ActivityPictureBox.Image = mImage;
        }

    }
}
