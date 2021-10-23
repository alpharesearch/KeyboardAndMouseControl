/*
 * Created by SharpDevelop.
 * User: markus
 * Date: 10/13/2021
 * Time: 8:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace mvk_input
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox receiveTextBox;
        private System.Windows.Forms.ComboBox comportComboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label infoLabel1;
        private System.Windows.Forms.Label infoLabelKeyboard;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label infoLabelMouse;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private System.Windows.Forms.ComboBox usbComboBox;
        private TranspCtrl mouseAndKeyboardCatcherTranspCtrl;
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
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
            this.receiveTextBox = new System.Windows.Forms.TextBox();
            this.comportComboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.infoLabelKeyboard = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.infoLabelMouse = new System.Windows.Forms.Label();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.usbComboBox = new System.Windows.Forms.ComboBox();
            this.mouseAndKeyboardCatcherTranspCtrl = new mvk_input.TranspCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.SuspendLayout();
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.AcceptsReturn = true;
            this.receiveTextBox.AcceptsTab = true;
            this.receiveTextBox.BackColor = System.Drawing.Color.White;
            this.receiveTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.receiveTextBox.Location = new System.Drawing.Point(1206, -5);
            this.receiveTextBox.Multiline = true;
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.ShortcutsEnabled = false;
            this.receiveTextBox.Size = new System.Drawing.Size(960, 53);
            this.receiveTextBox.TabIndex = 0;
            this.receiveTextBox.Visible = false;
            // 
            // comportComboBox
            // 
            this.comportComboBox.DisplayMember = "COM7";
            this.comportComboBox.FormattingEnabled = true;
            this.comportComboBox.Location = new System.Drawing.Point(189, 15);
            this.comportComboBox.Name = "comportComboBox";
            this.comportComboBox.Size = new System.Drawing.Size(107, 28);
            this.comportComboBox.TabIndex = 1;
            this.comportComboBox.Text = "COM7";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 8);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(171, 40);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // infoLabel1
            // 
            this.infoLabel1.Location = new System.Drawing.Point(532, 18);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(209, 21);
            this.infoLabel1.TabIndex = 3;
            this.infoLabel1.Text = "Press right menu key to exit.";
            // 
            // infoLabelKeyboard
            // 
            this.infoLabelKeyboard.Location = new System.Drawing.Point(962, 18);
            this.infoLabelKeyboard.Name = "infoLabelKeyboard";
            this.infoLabelKeyboard.Size = new System.Drawing.Size(229, 23);
            this.infoLabelKeyboard.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // infoLabelMouse
            // 
            this.infoLabelMouse.Location = new System.Drawing.Point(739, 18);
            this.infoLabelMouse.Name = "infoLabelMouse";
            this.infoLabelMouse.Size = new System.Drawing.Size(217, 21);
            this.infoLabelMouse.TabIndex = 5;
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(0, 54);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(1200, 720);
            this.videoView1.TabIndex = 6;
            this.videoView1.TabStop = false;
            this.videoView1.Text = "videoView1";
            // 
            // usbComboBox
            // 
            this.usbComboBox.FormattingEnabled = true;
            this.usbComboBox.Location = new System.Drawing.Point(302, 15);
            this.usbComboBox.Name = "usbComboBox";
            this.usbComboBox.Size = new System.Drawing.Size(224, 28);
            this.usbComboBox.TabIndex = 7;
            this.usbComboBox.Text = "USB3. 0 capture";
            this.usbComboBox.SelectedValueChanged += new System.EventHandler(this.VideoComboBox_SelectedValueChanged);
            // 
            // mouseAndKeyboardCatcherTranspCtrl
            // 
            this.mouseAndKeyboardCatcherTranspCtrl.BackColor = System.Drawing.Color.Transparent;
            this.mouseAndKeyboardCatcherTranspCtrl.Location = new System.Drawing.Point(0, 54);
            this.mouseAndKeyboardCatcherTranspCtrl.Name = "mouseAndKeyboardCatcherTranspCtrl";
            this.mouseAndKeyboardCatcherTranspCtrl.Opacity = 100;
            this.mouseAndKeyboardCatcherTranspCtrl.Size = new System.Drawing.Size(631, 457);
            this.mouseAndKeyboardCatcherTranspCtrl.TabIndex = 8;
            this.mouseAndKeyboardCatcherTranspCtrl.Text = "transpCtrl1";
            this.mouseAndKeyboardCatcherTranspCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TranspCtrl1_KeyDown);
            this.mouseAndKeyboardCatcherTranspCtrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TranspCtrl1_KeyPress);
            this.mouseAndKeyboardCatcherTranspCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TranspCtrl1_KeyUp);
            this.mouseAndKeyboardCatcherTranspCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TranspCtrl1_MouseDown);
            this.mouseAndKeyboardCatcherTranspCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TranspCtrl1_MouseMove);
            this.mouseAndKeyboardCatcherTranspCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TranspCtrl1_MouseUp);
            this.mouseAndKeyboardCatcherTranspCtrl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TranspCtrl1_PreviewKeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 751);
            this.Controls.Add(this.usbComboBox);
            this.Controls.Add(this.infoLabelMouse);
            this.Controls.Add(this.infoLabelKeyboard);
            this.Controls.Add(this.infoLabel1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.comportComboBox);
            this.Controls.Add(this.mouseAndKeyboardCatcherTranspCtrl);
            this.Controls.Add(this.videoView1);
            this.Controls.Add(this.receiveTextBox);
            this.Name = "MainForm";
            this.Text = "KVM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
