namespace A1E
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.text_ip = new System.Windows.Forms.TextBox();
            this.text_port = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.text_err = new System.Windows.Forms.TextBox();
            this.text_addr = new System.Windows.Forms.TextBox();
            this.text_num = new System.Windows.Forms.TextBox();
            this.ReadBtn = new System.Windows.Forms.Button();
            this.text_data = new System.Windows.Forms.TextBox();
            this.ReadFBtn = new System.Windows.Forms.Button();
            this.ReadBitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_ip
            // 
            this.text_ip.Location = new System.Drawing.Point(13, 13);
            this.text_ip.Name = "text_ip";
            this.text_ip.Size = new System.Drawing.Size(130, 28);
            this.text_ip.TabIndex = 0;
            this.text_ip.Text = "192.168.1.36";
            // 
            // text_port
            // 
            this.text_port.Location = new System.Drawing.Point(150, 12);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(100, 28);
            this.text_port.TabIndex = 1;
            this.text_port.Text = "5008";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(266, 14);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 40);
            this.ConnectBtn.TabIndex = 2;
            this.ConnectBtn.Text = "连接";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Location = new System.Drawing.Point(361, 14);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(75, 40);
            this.DisconnectBtn.TabIndex = 3;
            this.DisconnectBtn.Text = "断开";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // text_err
            // 
            this.text_err.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.text_err.Location = new System.Drawing.Point(0, 305);
            this.text_err.Name = "text_err";
            this.text_err.ReadOnly = true;
            this.text_err.Size = new System.Drawing.Size(938, 28);
            this.text_err.TabIndex = 4;
            // 
            // text_addr
            // 
            this.text_addr.Location = new System.Drawing.Point(13, 95);
            this.text_addr.Name = "text_addr";
            this.text_addr.Size = new System.Drawing.Size(130, 28);
            this.text_addr.TabIndex = 5;
            this.text_addr.Text = "D100";
            // 
            // text_num
            // 
            this.text_num.Location = new System.Drawing.Point(150, 95);
            this.text_num.Name = "text_num";
            this.text_num.Size = new System.Drawing.Size(100, 28);
            this.text_num.TabIndex = 6;
            this.text_num.Text = "20";
            // 
            // ReadBtn
            // 
            this.ReadBtn.Location = new System.Drawing.Point(266, 88);
            this.ReadBtn.Name = "ReadBtn";
            this.ReadBtn.Size = new System.Drawing.Size(92, 38);
            this.ReadBtn.TabIndex = 7;
            this.ReadBtn.Text = "读取(字)";
            this.ReadBtn.UseVisualStyleBackColor = true;
            this.ReadBtn.Click += new System.EventHandler(this.ReadBtn_Click);
            // 
            // text_data
            // 
            this.text_data.Location = new System.Drawing.Point(13, 141);
            this.text_data.Multiline = true;
            this.text_data.Name = "text_data";
            this.text_data.ReadOnly = true;
            this.text_data.Size = new System.Drawing.Size(913, 141);
            this.text_data.TabIndex = 8;
            // 
            // ReadFBtn
            // 
            this.ReadFBtn.Location = new System.Drawing.Point(379, 88);
            this.ReadFBtn.Name = "ReadFBtn";
            this.ReadFBtn.Size = new System.Drawing.Size(190, 38);
            this.ReadFBtn.TabIndex = 9;
            this.ReadFBtn.Text = "读取(单个浮点数)";
            this.ReadFBtn.UseVisualStyleBackColor = true;
            this.ReadFBtn.Click += new System.EventHandler(this.ReadFBtn_Click);
            // 
            // ReadBitBtn
            // 
            this.ReadBitBtn.Location = new System.Drawing.Point(584, 88);
            this.ReadBitBtn.Name = "ReadBitBtn";
            this.ReadBitBtn.Size = new System.Drawing.Size(92, 38);
            this.ReadBitBtn.TabIndex = 10;
            this.ReadBitBtn.Text = "读取(位)";
            this.ReadBitBtn.UseVisualStyleBackColor = true;
            this.ReadBitBtn.Click += new System.EventHandler(this.ReadBitBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 333);
            this.Controls.Add(this.ReadBitBtn);
            this.Controls.Add(this.ReadFBtn);
            this.Controls.Add(this.text_data);
            this.Controls.Add(this.ReadBtn);
            this.Controls.Add(this.text_num);
            this.Controls.Add(this.text_addr);
            this.Controls.Add(this.text_err);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.text_port);
            this.Controls.Add(this.text_ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_ip;
        private System.Windows.Forms.TextBox text_port;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.TextBox text_err;
        private System.Windows.Forms.TextBox text_addr;
        private System.Windows.Forms.TextBox text_num;
        private System.Windows.Forms.Button ReadBtn;
        private System.Windows.Forms.TextBox text_data;
        private System.Windows.Forms.Button ReadFBtn;
        private System.Windows.Forms.Button ReadBitBtn;
    }
}

