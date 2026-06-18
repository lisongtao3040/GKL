namespace scanner
{
    partial class FraScanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbComName = new System.Windows.Forms.ComboBox();
            this.comboBox_comPl = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbComName
            // 
            this.cbComName.FormattingEnabled = true;
            this.cbComName.Location = new System.Drawing.Point(83, 10);
            this.cbComName.Name = "cbComName";
            this.cbComName.Size = new System.Drawing.Size(121, 20);
            this.cbComName.TabIndex = 0;
            // 
            // comboBox_comPl
            // 
            this.comboBox_comPl.FormattingEnabled = true;
            this.comboBox_comPl.Items.AddRange(new object[] {
            "19200"});
            this.comboBox_comPl.Location = new System.Drawing.Point(83, 40);
            this.comboBox_comPl.Name = "comboBox_comPl";
            this.comboBox_comPl.Size = new System.Drawing.Size(121, 20);
            this.comboBox_comPl.TabIndex = 9;
            this.comboBox_comPl.Text = "9600";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "可用串口：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 66);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(543, 183);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(258, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(140, 51);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(421, 10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(136, 51);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "停止连接";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // FraScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 261);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox_comPl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbComName);
            this.Name = "FraScanner";
            this.Text = "FraScanner";
            this.Load += new System.EventHandler(this.FraScanner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComName;
        private System.Windows.Forms.ComboBox comboBox_comPl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnStop;
    }
}