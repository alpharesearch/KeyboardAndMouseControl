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
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.AcceptsTab = true;
			this.textBox1.Location = new System.Drawing.Point(12, 53);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(254, 179);
			this.textBox1.TabIndex = 0;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1KeyDown);
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1KeyPress);
			this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBox1MouseDown);
			this.textBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox1MouseMove);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(137, 10);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(129, 28);
			this.comboBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 30);
			this.button1.TabIndex = 2;
			this.button1.Text = "Connect";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 279);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(254, 21);
			this.label1.TabIndex = 3;
			this.label1.Text = "Press ALT+CTRL to relase mouse.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 246);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(254, 23);
			this.label2.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 306);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.textBox1);
			this.Name = "MainForm";
			this.Text = "mk_input";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
