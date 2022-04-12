namespace Fins_test
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.transport = new System.Windows.Forms.TabControl();
            this.transport_tcp = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtIP = new System.Windows.Forms.TextBox();
            this.transport_serial = new System.Windows.Forms.TabPage();
            this.stopbits_plc = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.databits_plc = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Parity_plc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.baudRate_plc = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comportName_plc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.read_plc = new System.Windows.Forms.Button();
            this.write_plc = new System.Windows.Forms.Button();
            this.value_plc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.length_plc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.address_plc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.area_plc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_plc = new System.Windows.Forms.TextBox();
            this.data_plc = new System.Windows.Forms.TextBox();
            this.station_plc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.transport.SuspendLayout();
            this.transport_tcp.SuspendLayout();
            this.transport_serial.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // transport
            // 
            this.transport.Controls.Add(this.transport_tcp);
            this.transport.Controls.Add(this.transport_serial);
            this.transport.Location = new System.Drawing.Point(2, 3);
            this.transport.Name = "transport";
            this.transport.SelectedIndex = 0;
            this.transport.Size = new System.Drawing.Size(740, 64);
            this.transport.TabIndex = 0;
            // 
            // transport_tcp
            // 
            this.transport_tcp.Controls.Add(this.label2);
            this.transport_tcp.Controls.Add(this.TxtPort);
            this.transport_tcp.Controls.Add(this.Label1);
            this.transport_tcp.Controls.Add(this.TxtIP);
            this.transport_tcp.Location = new System.Drawing.Point(4, 22);
            this.transport_tcp.Name = "transport_tcp";
            this.transport_tcp.Padding = new System.Windows.Forms.Padding(3);
            this.transport_tcp.Size = new System.Drawing.Size(732, 38);
            this.transport_tcp.TabIndex = 0;
            this.transport_tcp.Text = "TCP";
            this.transport_tcp.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "端口号";
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(231, 9);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(50, 21);
            this.TxtPort.TabIndex = 37;
            this.TxtPort.Text = "9600";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 12);
            this.Label1.TabIndex = 36;
            this.Label1.Text = "IP 地址";
            // 
            // TxtIP
            // 
            this.TxtIP.Location = new System.Drawing.Point(64, 9);
            this.TxtIP.Name = "TxtIP";
            this.TxtIP.Size = new System.Drawing.Size(100, 21);
            this.TxtIP.TabIndex = 35;
            this.TxtIP.Text = "192.168.1.233";
            // 
            // transport_serial
            // 
            this.transport_serial.Controls.Add(this.stopbits_plc);
            this.transport_serial.Controls.Add(this.label6);
            this.transport_serial.Controls.Add(this.databits_plc);
            this.transport_serial.Controls.Add(this.label5);
            this.transport_serial.Controls.Add(this.Parity_plc);
            this.transport_serial.Controls.Add(this.label4);
            this.transport_serial.Controls.Add(this.baudRate_plc);
            this.transport_serial.Controls.Add(this.label7);
            this.transport_serial.Controls.Add(this.comportName_plc);
            this.transport_serial.Controls.Add(this.label3);
            this.transport_serial.Location = new System.Drawing.Point(4, 22);
            this.transport_serial.Name = "transport_serial";
            this.transport_serial.Padding = new System.Windows.Forms.Padding(3);
            this.transport_serial.Size = new System.Drawing.Size(732, 38);
            this.transport_serial.TabIndex = 1;
            this.transport_serial.Text = "SERIAL";
            this.transport_serial.UseVisualStyleBackColor = true;
            // 
            // stopbits_plc
            // 
            this.stopbits_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopbits_plc.FormattingEnabled = true;
            this.stopbits_plc.Items.AddRange(new object[] {
            "1",
            "2",
            "1.5"});
            this.stopbits_plc.Location = new System.Drawing.Point(467, 9);
            this.stopbits_plc.Name = "stopbits_plc";
            this.stopbits_plc.Size = new System.Drawing.Size(82, 20);
            this.stopbits_plc.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(408, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "停止位：";
            // 
            // databits_plc
            // 
            this.databits_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databits_plc.FormattingEnabled = true;
            this.databits_plc.Items.AddRange(new object[] {
            "7",
            "8"});
            this.databits_plc.Location = new System.Drawing.Point(353, 9);
            this.databits_plc.Name = "databits_plc";
            this.databits_plc.Size = new System.Drawing.Size(50, 20);
            this.databits_plc.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(294, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "数据位：";
            // 
            // Parity_plc
            // 
            this.Parity_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Parity_plc.FormattingEnabled = true;
            this.Parity_plc.Location = new System.Drawing.Point(598, 9);
            this.Parity_plc.Name = "Parity_plc";
            this.Parity_plc.Size = new System.Drawing.Size(75, 20);
            this.Parity_plc.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(554, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "校验：";
            // 
            // baudRate_plc
            // 
            this.baudRate_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRate_plc.FormattingEnabled = true;
            this.baudRate_plc.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.baudRate_plc.Location = new System.Drawing.Point(199, 9);
            this.baudRate_plc.Name = "baudRate_plc";
            this.baudRate_plc.Size = new System.Drawing.Size(90, 20);
            this.baudRate_plc.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(140, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "波特率：";
            // 
            // comportName_plc
            // 
            this.comportName_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comportName_plc.FormattingEnabled = true;
            this.comportName_plc.Location = new System.Drawing.Point(63, 9);
            this.comportName_plc.Name = "comportName_plc";
            this.comportName_plc.Size = new System.Drawing.Size(72, 20);
            this.comportName_plc.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "端口号：";
            // 
            // read_plc
            // 
            this.read_plc.Location = new System.Drawing.Point(751, 25);
            this.read_plc.Name = "read_plc";
            this.read_plc.Size = new System.Drawing.Size(120, 30);
            this.read_plc.TabIndex = 1;
            this.read_plc.Text = "开始读取";
            this.read_plc.UseVisualStyleBackColor = true;
            this.read_plc.Click += new System.EventHandler(this.read_plc_Click);
            // 
            // write_plc
            // 
            this.write_plc.Location = new System.Drawing.Point(751, 68);
            this.write_plc.Name = "write_plc";
            this.write_plc.Size = new System.Drawing.Size(120, 30);
            this.write_plc.TabIndex = 3;
            this.write_plc.Text = "写入";
            this.write_plc.UseVisualStyleBackColor = true;
            this.write_plc.Click += new System.EventHandler(this.write_plc_Click);
            // 
            // value_plc
            // 
            this.value_plc.Location = new System.Drawing.Point(537, 74);
            this.value_plc.Name = "value_plc";
            this.value_plc.Size = new System.Drawing.Size(74, 21);
            this.value_plc.TabIndex = 70;
            this.value_plc.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(479, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 25);
            this.label13.TabIndex = 69;
            this.label13.Text = "数值：";
            // 
            // length_plc
            // 
            this.length_plc.Location = new System.Drawing.Point(373, 74);
            this.length_plc.Name = "length_plc";
            this.length_plc.Size = new System.Drawing.Size(90, 21);
            this.length_plc.TabIndex = 68;
            this.length_plc.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(315, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 25);
            this.label10.TabIndex = 67;
            this.label10.Text = "长度：";
            // 
            // address_plc
            // 
            this.address_plc.Location = new System.Drawing.Point(223, 74);
            this.address_plc.Name = "address_plc";
            this.address_plc.Size = new System.Drawing.Size(90, 21);
            this.address_plc.TabIndex = 66;
            this.address_plc.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(166, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 25);
            this.label9.TabIndex = 65;
            this.label9.Text = "地址：";
            // 
            // area_plc
            // 
            this.area_plc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.area_plc.FormattingEnabled = true;
            this.area_plc.Items.AddRange(new object[] {
            "DM",
            "CIO"});
            this.area_plc.Location = new System.Drawing.Point(70, 75);
            this.area_plc.Name = "area_plc";
            this.area_plc.Size = new System.Drawing.Size(90, 20);
            this.area_plc.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(11, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 25);
            this.label8.TabIndex = 63;
            this.label8.Text = "区域：";
            // 
            // txt_plc
            // 
            this.txt_plc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_plc.Location = new System.Drawing.Point(0, 506);
            this.txt_plc.Name = "txt_plc";
            this.txt_plc.ReadOnly = true;
            this.txt_plc.Size = new System.Drawing.Size(882, 21);
            this.txt_plc.TabIndex = 71;
            // 
            // data_plc
            // 
            this.data_plc.Location = new System.Drawing.Point(6, 101);
            this.data_plc.Multiline = true;
            this.data_plc.Name = "data_plc";
            this.data_plc.ReadOnly = true;
            this.data_plc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.data_plc.Size = new System.Drawing.Size(865, 399);
            this.data_plc.TabIndex = 72;
            this.data_plc.WordWrap = false;
            // 
            // station_plc
            // 
            this.station_plc.Location = new System.Drawing.Point(683, 75);
            this.station_plc.Name = "station_plc";
            this.station_plc.Size = new System.Drawing.Size(44, 21);
            this.station_plc.TabIndex = 74;
            this.station_plc.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(625, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 25);
            this.label11.TabIndex = 73;
            this.label11.Text = "站号：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 527);
            this.Controls.Add(this.station_plc);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.data_plc);
            this.Controls.Add(this.txt_plc);
            this.Controls.Add(this.value_plc);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.length_plc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.address_plc);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.area_plc);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.write_plc);
            this.Controls.Add(this.read_plc);
            this.Controls.Add(this.transport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.transport.ResumeLayout(false);
            this.transport_tcp.ResumeLayout(false);
            this.transport_tcp.PerformLayout();
            this.transport_serial.ResumeLayout(false);
            this.transport_serial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl transport;
        private System.Windows.Forms.TabPage transport_tcp;
        private System.Windows.Forms.TabPage transport_serial;
        private System.Windows.Forms.Button read_plc;
        private System.Windows.Forms.Button write_plc;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox TxtPort;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TxtIP;
        public System.Windows.Forms.ComboBox Parity_plc;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox baudRate_plc;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox comportName_plc;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox stopbits_plc;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox databits_plc;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox value_plc;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox length_plc;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox address_plc;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox area_plc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_plc;
        private System.Windows.Forms.TextBox data_plc;
        internal System.Windows.Forms.TextBox station_plc;
        private System.Windows.Forms.Label label11;
    }
}

