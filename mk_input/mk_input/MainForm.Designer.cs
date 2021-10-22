/*
 * Created by SharpDevelop.
 * User: markus
 * Date: 10/13/2021
 * Time: 8:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace mk_input
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.transpCtrl1 = new mk_input.TranspCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Location = new System.Drawing.Point(1003, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(960, 720);
            this.textBox1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "COM7";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "COM5",
            "COM6",
            "COM7"});
            this.comboBox1.Location = new System.Drawing.Point(189, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 28);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "COM7";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 793);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Press right menu key to exit.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(679, 791);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 23);
            this.label2.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(424, 791);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 23);
            this.label3.TabIndex = 5;
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(12, 44);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(960, 720);
            this.videoView1.TabIndex = 6;
            this.videoView1.Text = "videoView1";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "USB3. 0 capture"});
            this.comboBox2.Location = new System.Drawing.Point(359, 10);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(167, 28);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.Text = "USB3. 0 capture";
            // 
            // transpCtrl1
            // 
            this.transpCtrl1.BackColor = System.Drawing.Color.Transparent;
            this.transpCtrl1.Location = new System.Drawing.Point(0, 44);
            this.transpCtrl1.Name = "transpCtrl1";
            this.transpCtrl1.Opacity = 100;
            this.transpCtrl1.Size = new System.Drawing.Size(990, 728);
            this.transpCtrl1.TabIndex = 8;
            this.transpCtrl1.Text = "transpCtrl1";
            this.transpCtrl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.transpCtrl1_KeyDown);
            this.transpCtrl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.transpCtrl1_KeyPress);
            this.transpCtrl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.transpCtrl1_KeyUp);
            this.transpCtrl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transpCtrl1_MouseDown);
            this.transpCtrl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.transpCtrl1_MouseMove);
            this.transpCtrl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.transpCtrl1_MouseUp);
            this.transpCtrl1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.transpCtrl1_PreviewKeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 840);
            this.Controls.Add(this.transpCtrl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.videoView1);
            this.Name = "MainForm";
            this.Text = "mk_input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private LibVLCSharp.WinForms.VideoView videoView1;
        private System.Windows.Forms.ComboBox comboBox2;
        private TranspCtrl transpCtrl1;
    }
}
