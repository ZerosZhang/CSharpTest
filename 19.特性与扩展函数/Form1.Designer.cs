namespace _19.特性与扩展函数
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_check_valid = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // button_check_valid
            // 
            this.button_check_valid.Location = new System.Drawing.Point(618, 438);
            this.button_check_valid.Name = "button_check_valid";
            this.button_check_valid.Size = new System.Drawing.Size(120, 40);
            this.button_check_valid.TabIndex = 0;
            this.button_check_valid.Text = "验证参数合法";
            this.button_check_valid.UseVisualStyleBackColor = true;
            this.button_check_valid.Click += new System.EventHandler(this.button_check_valid_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(430, 466);
            this.propertyGrid1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 490);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.button_check_valid);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Form1";
            this.Text = "验证参数是否合法";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_check_valid;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}

