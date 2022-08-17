namespace _010.图片显示控件
{
    partial class PictureBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ActivityPanel = new System.Windows.Forms.Panel();
            this.ActivityPictureBox = new System.Windows.Forms.PictureBox();
            this.ActivityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActivityPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ActivityPanel
            // 
            this.ActivityPanel.Controls.Add(this.ActivityPictureBox);
            this.ActivityPanel.Location = new System.Drawing.Point(0, 0);
            this.ActivityPanel.Name = "ActivityPanel";
            this.ActivityPanel.Size = new System.Drawing.Size(458, 294);
            this.ActivityPanel.TabIndex = 1;
            // 
            // ActivityPictureBox
            // 
            this.ActivityPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ActivityPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ActivityPictureBox.Location = new System.Drawing.Point(0, 0);
            this.ActivityPictureBox.Name = "ActivityPictureBox";
            this.ActivityPictureBox.Size = new System.Drawing.Size(458, 294);
            this.ActivityPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ActivityPictureBox.TabIndex = 1;
            this.ActivityPictureBox.TabStop = false;
            this.ActivityPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivityPictureBox_MouseDown);
            this.ActivityPictureBox.MouseLeave += new System.EventHandler(this.ActivityPictureBox_MouseLeave);
            this.ActivityPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivityPictureBox_MouseMove);
            this.ActivityPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ActivityPictureBox_MouseUp);
            // 
            // PictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ActivityPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PictureBox";
            this.Size = new System.Drawing.Size(458, 294);
            this.ActivityPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActivityPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel ActivityPanel;
        private System.Windows.Forms.PictureBox ActivityPictureBox;
    }
}
