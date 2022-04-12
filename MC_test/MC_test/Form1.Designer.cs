namespace MC_test
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
            this.ProtocolType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtIP = new System.Windows.Forms.TextBox();
            this.value_plc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.write_plc = new System.Windows.Forms.Button();
            this.read_plc = new System.Windows.Forms.Button();
            this.length_plc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.address_plc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.area_plc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.data_plc = new System.Windows.Forms.TextBox();
            this.txt_plc = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ProtocolType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TxtPort);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TxtIP);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(678, 72);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PLC通信";
            // 
            // ProtocolType
            // 
            this.ProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProtocolType.FormattingEnabled = true;
            this.ProtocolType.Items.AddRange(new object[] {
            "ASCII",
            "BINARY"});
            this.ProtocolType.Location = new System.Drawing.Point(12, 28);
            this.ProtocolType.Name = "ProtocolType";
            this.ProtocolType.Size = new System.Drawing.Size(146, 29);
            this.ProtocolType.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(373, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 21);
            this.label5.TabIndex = 45;
            this.label5.Text = "Port";
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(418, 28);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(54, 29);
            this.TxtPort.TabIndex = 43;
            this.TxtPort.Text = "9600";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 21);
            this.label6.TabIndex = 44;
            this.label6.Text = "IP";
            // 
            // TxtIP
            // 
            this.TxtIP.Location = new System.Drawing.Point(192, 28);
            this.TxtIP.Name = "TxtIP";
            this.TxtIP.Size = new System.Drawing.Size(175, 29);
            this.TxtIP.TabIndex = 42;
            this.TxtIP.Text = "192.168.2.105";
            // 
            // value_plc
            // 
            this.value_plc.Location = new System.Drawing.Point(546, 96);
            this.value_plc.Name = "value_plc";
            this.value_plc.Size = new System.Drawing.Size(90, 21);
            this.value_plc.TabIndex = 64;
            this.value_plc.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(488, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 25);
            this.label13.TabIndex = 63;
            this.label13.Text = "数值：";
            // 
            // write_plc
            // 
            this.write_plc.Location = new System.Drawing.Point(349, 131);
            this.write_plc.Name = "write_plc";
            this.write_plc.Size = new System.Drawing.Size(320, 30);
            this.write_plc.TabIndex = 62;
            this.write_plc.Text = "写入";
            this.write_plc.UseVisualStyleBackColor = true;
            this.write_plc.Click += new System.EventHandler(this.write_plc_Click);
            // 
            // read_plc
            // 
            this.read_plc.Location = new System.Drawing.Point(12, 131);
            this.read_plc.Name = "read_plc";
            this.read_plc.Size = new System.Drawing.Size(320, 30);
            this.read_plc.TabIndex = 61;
            this.read_plc.Text = "读取";
            this.read_plc.UseVisualStyleBackColor = true;
            this.read_plc.Click += new System.EventHandler(this.read_plc_Click);
            // 
            // length_plc
            // 
            this.length_plc.Location = new System.Drawing.Point(382, 96);
            this.length_plc.Name = "length_plc";
            this.length_plc.Size = new System.Drawing.Size(90, 21);
            this.length_plc.TabIndex = 60;
            this.length_plc.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(324, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 25);
            this.label10.TabIndex = 59;
            this.label10.Text = "长度：";
            // 
            // address_plc
            // 
            this.address_plc.Location = new System.Drawing.Point(228, 95);
            this.address_plc.Name = "address_plc";
            this.address_plc.Size = new System.Drawing.Size(90, 21);
            this.address_plc.TabIndex = 58;
            this.address_plc.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(171, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 25);
            this.label9.TabIndex = 57;
            this.label9.Text = "地址：";
            // 
            // area_plc
            // 
            this.area_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.area_plc.FormattingEnabled = true;
            this.area_plc.Items.AddRange(new object[] {
            "D",
            "W",
            "M",
            "L",
            "S",
            "X",
            "Y",
            "Z",
            "B"});
            this.area_plc.Location = new System.Drawing.Point(75, 96);
            this.area_plc.Name = "area_plc";
            this.area_plc.Size = new System.Drawing.Size(90, 20);
            this.area_plc.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 55;
            this.label3.Text = "区域：";
            // 
            // data_plc
            // 
            this.data_plc.Location = new System.Drawing.Point(12, 167);
            this.data_plc.Multiline = true;
            this.data_plc.Name = "data_plc";
            this.data_plc.ReadOnly = true;
            this.data_plc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data_plc.Size = new System.Drawing.Size(657, 284);
            this.data_plc.TabIndex = 65;
            // 
            // txt_plc
            // 
            this.txt_plc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_plc.Location = new System.Drawing.Point(0, 461);
            this.txt_plc.Name = "txt_plc";
            this.txt_plc.ReadOnly = true;
            this.txt_plc.Size = new System.Drawing.Size(678, 21);
            this.txt_plc.TabIndex = 66;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 482);
            this.Controls.Add(this.txt_plc);
            this.Controls.Add(this.data_plc);
            this.Controls.Add(this.value_plc);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.write_plc);
            this.Controls.Add(this.read_plc);
            this.Controls.Add(this.length_plc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.address_plc);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.area_plc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ProtocolType;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox TxtPort;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox TxtIP;
        internal System.Windows.Forms.TextBox value_plc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button write_plc;
        private System.Windows.Forms.Button read_plc;
        internal System.Windows.Forms.TextBox length_plc;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox address_plc;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox area_plc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox data_plc;
        private System.Windows.Forms.TextBox txt_plc;
    }
}

