namespace FX_test
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bitValue = new System.Windows.Forms.ComboBox();
            this.write_value = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.return_value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.address_reduce = new System.Windows.Forms.Button();
            this.address_add = new System.Windows.Forms.Button();
            this.address_type = new System.Windows.Forms.ComboBox();
            this.count = new System.Windows.Forms.TextBox();
            this.address_ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.read = new System.Windows.Forms.Button();
            this.write = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comportName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectPLC = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.TextBox();
            this.recv_msg = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.send_msg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.recv_msg);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.send_msg);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.Time);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.bitValue);
            this.groupBox2.Controls.Add(this.write_value);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.return_value);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.address_reduce);
            this.groupBox2.Controls.Add(this.address_add);
            this.groupBox2.Controls.Add(this.address_type);
            this.groupBox2.Controls.Add(this.count);
            this.groupBox2.Controls.Add(this.address_);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.read);
            this.groupBox2.Controls.Add(this.write);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(18, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(767, 512);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 25);
            this.label7.TabIndex = 89;
            this.label7.Text = "开关量";
            // 
            // bitValue
            // 
            this.bitValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bitValue.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bitValue.FormattingEnabled = true;
            this.bitValue.Items.AddRange(new object[] {
            "False",
            "True"});
            this.bitValue.Location = new System.Drawing.Point(473, 124);
            this.bitValue.Name = "bitValue";
            this.bitValue.Size = new System.Drawing.Size(95, 22);
            this.bitValue.TabIndex = 88;
            // 
            // write_value
            // 
            this.write_value.Location = new System.Drawing.Point(266, 115);
            this.write_value.Name = "write_value";
            this.write_value.Size = new System.Drawing.Size(126, 33);
            this.write_value.TabIndex = 17;
            this.write_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "数值";
            // 
            // return_value
            // 
            this.return_value.Location = new System.Drawing.Point(77, 167);
            this.return_value.Multiline = true;
            this.return_value.Name = "return_value";
            this.return_value.ReadOnly = true;
            this.return_value.Size = new System.Drawing.Size(684, 258);
            this.return_value.TabIndex = 15;
            this.return_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "返回";
            // 
            // address_reduce
            // 
            this.address_reduce.Location = new System.Drawing.Point(77, 73);
            this.address_reduce.Name = "address_reduce";
            this.address_reduce.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.address_reduce.Size = new System.Drawing.Size(20, 32);
            this.address_reduce.TabIndex = 6;
            this.address_reduce.Text = "<";
            this.address_reduce.UseVisualStyleBackColor = true;
            this.address_reduce.Click += new System.EventHandler(this.address_reduce_Click);
            // 
            // address_add
            // 
            this.address_add.Location = new System.Drawing.Point(183, 71);
            this.address_add.Name = "address_add";
            this.address_add.Size = new System.Drawing.Size(20, 32);
            this.address_add.TabIndex = 7;
            this.address_add.Text = ">";
            this.address_add.UseVisualStyleBackColor = true;
            this.address_add.Click += new System.EventHandler(this.address_add_Click);
            // 
            // address_type
            // 
            this.address_type.FormattingEnabled = true;
            this.address_type.Items.AddRange(new object[] {
            "D",
            "M",
            "X",
            "Y"});
            this.address_type.Location = new System.Drawing.Point(77, 28);
            this.address_type.Name = "address_type";
            this.address_type.Size = new System.Drawing.Size(126, 33);
            this.address_type.TabIndex = 13;
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(77, 115);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(126, 33);
            this.count.TabIndex = 6;
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // address_
            // 
            this.address_.Location = new System.Drawing.Point(103, 73);
            this.address_.Name = "address_";
            this.address_.Size = new System.Drawing.Size(74, 33);
            this.address_.TabIndex = 12;
            this.address_.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.address_.TextChanged += new System.EventHandler(this.address__TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "存储";
            // 
            // read
            // 
            this.read.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.read.Location = new System.Drawing.Point(263, 28);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(89, 51);
            this.read.TabIndex = 8;
            this.read.Text = "读(&R)";
            this.read.UseVisualStyleBackColor = true;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // write
            // 
            this.write.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.write.Location = new System.Drawing.Point(386, 28);
            this.write.Name = "write";
            this.write.Size = new System.Drawing.Size(89, 51);
            this.write.TabIndex = 8;
            this.write.Text = "写(&W)";
            this.write.UseVisualStyleBackColor = true;
            this.write.Click += new System.EventHandler(this.write_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "个数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comportName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.connectPLC);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(18, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 72);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC通信";
            // 
            // comportName
            // 
            this.comportName.FormattingEnabled = true;
            this.comportName.Location = new System.Drawing.Point(173, 27);
            this.comportName.Name = "comportName";
            this.comportName.Size = new System.Drawing.Size(90, 29);
            this.comportName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(59, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "端口号：";
            // 
            // connectPLC
            // 
            this.connectPLC.Location = new System.Drawing.Point(282, 25);
            this.connectPLC.Name = "connectPLC";
            this.connectPLC.Size = new System.Drawing.Size(86, 30);
            this.connectPLC.TabIndex = 0;
            this.connectPLC.Text = "连接";
            this.connectPLC.UseVisualStyleBackColor = true;
            this.connectPLC.Click += new System.EventHandler(this.connectPLC_Click);
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(585, 28);
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Size = new System.Drawing.Size(126, 33);
            this.Time.TabIndex = 90;
            this.Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // recv_msg
            // 
            this.recv_msg.Location = new System.Drawing.Point(77, 470);
            this.recv_msg.Name = "recv_msg";
            this.recv_msg.ReadOnly = true;
            this.recv_msg.Size = new System.Drawing.Size(684, 33);
            this.recv_msg.TabIndex = 94;
            this.recv_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 473);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 25);
            this.label9.TabIndex = 93;
            this.label9.Text = "接收";
            // 
            // send_msg
            // 
            this.send_msg.Location = new System.Drawing.Point(77, 431);
            this.send_msg.Name = "send_msg";
            this.send_msg.ReadOnly = true;
            this.send_msg.Size = new System.Drawing.Size(684, 33);
            this.send_msg.TabIndex = 92;
            this.send_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 434);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 25);
            this.label8.TabIndex = 91;
            this.label8.Text = "发送";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 617);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox write_value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox return_value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button address_reduce;
        private System.Windows.Forms.Button address_add;
        private System.Windows.Forms.ComboBox address_type;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.TextBox address_;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.Button write;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox comportName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectPLC;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox bitValue;
        private System.Windows.Forms.TextBox Time;
        private System.Windows.Forms.TextBox recv_msg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox send_msg;
        private System.Windows.Forms.Label label8;

    }
}

