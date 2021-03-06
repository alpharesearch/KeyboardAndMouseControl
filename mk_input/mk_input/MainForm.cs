/*
    Keyboard Video Mouse control
    Copyright (C) 2021  Markus Schulz

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using LibVLCSharp.Shared;

namespace mvk_input
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public LibVLC _libVLC;
        public MediaPlayer _mediaPlayer;

        private SerialPort _serialPort;
        private bool connected = false;
        private Point myPoint;
        private bool key_Handeled = false;
        private int delay = 0;
        private MyUserSettings settings;

        public MainForm()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            this.comportComboBox.Items.AddRange(ports);
            FilterInfoCollection videoDevices;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                this.usbComboBox.Items.Add(device.Name);
            }
            _libVLC = new LibVLC();
            VideoComboBox_SelectedValueChanged(this, new EventArgs());
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            settings = new MyUserSettings();
            usbComboBox.Text = settings.UsbDevice;
            comportComboBox.Text = settings.ComPort;
            videoView1.MediaPlayer.Play();
            MainForm_SizeChanged(sender, e);
        }
        void ConnectButtonClick(object sender, EventArgs e)
        {
            settings.UsbDevice = usbComboBox.Text;
            settings.ComPort = comportComboBox.Text;
            settings.Save();
            // all of the options for a serial device
            // ---- can be sent through the constructor of the SerialPort class
            // ---- PortName = "COM1", Baud Rate = 19200, Parity = None,
            // ---- Data Bits = 8, Stop Bits = One, Handshake = None
            if (_serialPort != null)
                _serialPort.Close();
            _serialPort = new SerialPort(comportComboBox.Text, 921600, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None
            };
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(Sp_DataReceived);
            _serialPort.Open();
            connected = true;
            try
            {
                _serialPort.Write("s 0 255 0\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }
            mouseAndKeyboardCatcherTranspCtrl.Location = videoView1.Location;
            mouseAndKeyboardCatcherTranspCtrl.Size = videoView1.Size;
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(this.Location.X + this.mouseAndKeyboardCatcherTranspCtrl.Location.X + 8 + this.mouseAndKeyboardCatcherTranspCtrl.Size.Width / 2, this.Location.Y + this.mouseAndKeyboardCatcherTranspCtrl.Location.Y + 30 + this.mouseAndKeyboardCatcherTranspCtrl.Size.Height / 2);
            myPoint = Cursor.Position;
            Cursor.Clip = new Rectangle(this.Location.X + this.mouseAndKeyboardCatcherTranspCtrl.Location.X + 10, this.Location.Y + this.mouseAndKeyboardCatcherTranspCtrl.Location.Y + 34, this.mouseAndKeyboardCatcherTranspCtrl.Size.Width - 20, this.mouseAndKeyboardCatcherTranspCtrl.Size.Height - 27);//77
            mouseAndKeyboardCatcherTranspCtrl.Select();
            mouseAndKeyboardCatcherTranspCtrl.Focus();
            Cursor.Hide();
            timer1.Enabled = true;
            connectButton.Enabled = false;
            comportComboBox.Enabled = false;
            usbComboBox.Enabled = false;
            VideoComboBox_SelectedValueChanged(sender, e);
            Cursor.Hide(); // not sure why it needs this for the 2nd connnect
        }
        private void VideoComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (videoView1.MediaPlayer != null) videoView1.MediaPlayer.Dispose();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;
            _mediaPlayer.EnableKeyInput = false;
            _mediaPlayer.EnableMouseInput = false;
            _mediaPlayer.Media = new Media(_libVLC, "dshow:// ", FromType.FromLocation);
            _mediaPlayer.Media.AddOption(":dshow-aspect-ratio=16:9");
            _mediaPlayer.Media.AddOption(":dshow-adev=none");
            _mediaPlayer.Media.AddOption(":dshow-vdev=" + usbComboBox.Text);
            _mediaPlayer.Media.AddOption(":dshow-vcodec=mjpeg");
            _mediaPlayer.Media.AddOption(":dshow-size=1920x1080");
            _mediaPlayer.Media.AddOption(":dshow-fps=60");
            //_mediaPlayer.AspectRatio = "1920:1080";
            //_mediaPlayer.Scale = 0;
            videoView1.MediaPlayer.Play();
        }
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            videoView1.Location = new Point(0, 34);
            videoView1.Size = new Size(this.Size.Width-14, this.Size.Height-74) ;
            mouseAndKeyboardCatcherTranspCtrl.Location = videoView1.Location;
            mouseAndKeyboardCatcherTranspCtrl.Size = videoView1.Size;
            
        }
        private int oldWidth = 1222;
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (oldWidth != this.Width) this.Height = 77 + ((int)((double)this.Width / ((double)((double)16 / (double)9))));
            else this.Width = -120 + ((int)((double)this.Height * ((double)((double)16 / (double)9))));
            oldWidth = this.Width;
        }
        
        void Timer1Tick(object sender, EventArgs e)
        {
            if (connected)
            {
                //mouseAndKeyboardCatcherTranspCtrl.Select();
                mouseAndKeyboardCatcherTranspCtrl.Focus();
            }
        }
        delegate void SetTextCallback(string text);
        void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = _serialPort.ReadLine();
            // Invokes the delegate on the UI thread, and sends the data that was received to the invoked method.
            // ---- The "si_DataReceived" method will be executed on the UI thread which allows populating of the textbox.
            //this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
            this.BeginInvoke(new SetTextCallback(Si_DataReceived), new object[] { data });
        }
        private void Si_DataReceived(string data)
        {
            receiveTextBox.Text += data.Trim();
        }
        void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor.Show();
            if (connected)
            {
                try
                {
                    _serialPort.Write("m 9 0 0\n");
                    _serialPort.Write("s 0 0 255\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
            }
            if (_serialPort != null)
                _serialPort.Close();
            connected = false;
        }
        private void TranspCtrl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (connected)
            {
                try
                {
                    if (e.Button == MouseButtons.Left)
                        _serialPort.Write("m 3 0 0\n");
                    if (e.Button == MouseButtons.Right)
                        _serialPort.Write("m 4 0 0\n");
                    if (e.Button == MouseButtons.Middle)
                        _serialPort.Write("m 5 0 0\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
                infoLabelKeyboard.Text = "D: " + e.Button + "mouse button";
            }
        }
        private void TranspCtrl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (connected)
            {
                try
                {
                    if (e.Button == MouseButtons.Left)
                        _serialPort.Write("m 6 0 0\n");
                    if (e.Button == MouseButtons.Right)
                        _serialPort.Write("m 7 0 0\n");
                    if (e.Button == MouseButtons.Middle)
                        _serialPort.Write("m 8 0 0\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
                infoLabelKeyboard.Text = "U: " + e.Button + "mouse button";
            }

        }
        private void TranspCtrl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (delay > 0) delay--;
            else
            {
                delay = 5;
                Point testPoint = new Point(this.Location.X + this.mouseAndKeyboardCatcherTranspCtrl.Location.X + 8 + e.Location.X + 0, this.Location.Y + this.mouseAndKeyboardCatcherTranspCtrl.Location.Y + 30 + e.Location.Y + 1);
                string buf;// = "not connected";
                if (connected)
                {
                    try
                    {
                        buf = "a " + (testPoint.X - myPoint.X) * 1 + " " + (testPoint.Y - myPoint.Y) * 1 + " 0\n";
                        //buf = "a\n 2\n 2\n";
                        _serialPort.Write(buf);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                    }

                    myPoint = testPoint;
                    if (e.Location.X < 3 || e.Location.X > this.mouseAndKeyboardCatcherTranspCtrl.Size.Width - 30 || e.Location.Y < 3 || e.Location.Y > this.mouseAndKeyboardCatcherTranspCtrl.Size.Height - 28)
                    {
                        Cursor.Position = new Point(this.Location.X + this.mouseAndKeyboardCatcherTranspCtrl.Location.X + 8 + this.mouseAndKeyboardCatcherTranspCtrl.Size.Width / 2, this.Location.Y + this.mouseAndKeyboardCatcherTranspCtrl.Location.Y + 30 + this.mouseAndKeyboardCatcherTranspCtrl.Size.Height / 2);
                        myPoint = Cursor.Position;
                    }
                    this.infoLabelMouse.Text = "Mouse: " + e.Location.ToString();
                }
            }
        }
        private void TranspCtrl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control || e.Alt || e.Shift || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }
        private void TranspCtrl1_KeyDown(object sender, KeyEventArgs e)
        {
            string testKeyCode = Program.KeyCodeToUnicode(e.KeyCode);
            int intTestKeyCode = 0;
            if(testKeyCode.Length > 0) intTestKeyCode = (int)testKeyCode[0];
            
            // handle CTRL + key
            if (e.Control && e.KeyCode != Keys.ControlKey)
            {
                e.SuppressKeyPress = true;
                if (connected)
                {
                    try
                    {
                        _serialPort.Write("p 128 0 0\n");
                        _serialPort.Write("p " + intTestKeyCode + " 0 0\n");
                        _serialPort.Write("m 9 0 0\n");
                        key_Handeled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                    }
                }
                infoLabelKeyboard.Text = "D: USB " + (int)e.KeyCode + " ASCII " + intTestKeyCode + " CTRL ";
                return;
            }

            // handle CTRL + key
            if (e.Alt && e.KeyCode != Keys.Alt)
            {
                e.SuppressKeyPress = true;
                if (connected)
                {
                    try
                    {
                        _serialPort.Write("p 130 0 0\n");
                        _serialPort.Write("p " + intTestKeyCode + " 0 0\n");
                        _serialPort.Write("m 9 0 0\n");
                        key_Handeled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                    }
                }
                infoLabelKeyboard.Text = "D: USB " + (int)e.KeyCode + " ASCII " + intTestKeyCode + " ALT ";
                return;
            }

            //https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-5.0
            //if (Control.ModifierKeys == Keys.Control && Control.ModifierKeys == Keys.Alt)
            int buf = (int)e.KeyCode;
            //int mod = (int)e.Modifiers;
            bool key_handeled = false;
            if (buf == 93)
            {
                Cursor.Clip = new Rectangle(0, 0, 0, 0);
                timer1.Enabled = false;
                MainFormFormClosing(sender, new FormClosingEventArgs(CloseReason.None, true));
                connectButton.Enabled = true;
                comportComboBox.Enabled = true;
                usbComboBox.Enabled = true;
                Cursor.Show();
            }
            if (buf == 244)
            {//17
                buf = 128; //KEY_LEFT_CTRL
                key_handeled = true;
            }
            if (buf == 245)
            {//16
                buf = 129; //KEY_LEFT_SHIFT
                key_handeled = true;
            }
            if (buf == 18)
            {//
                buf = 130; //KEY_LEFT_ALT
                key_handeled = true;
            }
            if (buf == 247)
            {//91
                buf = 131; //KEY_LEFT_GUI
                key_handeled = true;
            }
            if (buf == 163)
            {//
                buf = 132; //KEY_RIGHT_CTRL
                key_handeled = true;
            }
            //			if (buf == 245) {//16
            //				buf = 133; //KEY_RIGHT_SHIFT
            //				key_handeled = true;
            //			}
            //			if (buf == 18) {
            //				buf = 134; //KEY_RIGHT_ALT
            //				key_handeled = true;
            //			}
            if (buf == 92)
            {
                buf = 135; //KEY_RIGHT_GUI
                key_handeled = true;
            }
            if (buf == 38)
            {
                buf = 218; //KEY_UP_ARROW
                key_handeled = true;
            }
            if (buf == 40)
            {
                buf = 217; //KEY_DOWN_ARROW
                key_handeled = true;
            }
            if (buf == 37)
            {
                buf = 216; //KEY_LEFT_ARROW
                key_handeled = true;
            }
            if (buf == 39)
            {
                buf = 215; //KEY_RIGHT_ARROW
                key_handeled = true;
            }
            if (buf == 8)
            {
                buf = 178; //KEY_BACKSPACE
                key_handeled = true;
            }
            if (buf == 9)
            {
                buf = 179; //KEY_TAB
                key_handeled = true;
            }
            if (buf == 13)
            {//
                buf = 176; //KEY_RETURN
                key_handeled = true;
            }
            if (buf == 27)
            {
                buf = 177; //KEY_ESC
                key_handeled = true;
            }
            if (buf == 45)
            {
                buf = 209; //KEY_INSERT
                key_handeled = true;
            }
            if (buf == 46)
            {
                buf = 212; //KEY_DELETE
                key_handeled = true;
            }
            if (buf == 33)
            {
                buf = 211; //KEY_PAGE_UP
                key_handeled = true;
            }
            if (buf == 34)
            {
                buf = 214; //KEY_PAGE_DOWN
                key_handeled = true;
            }
            if (buf == 36)
            {
                buf = 210; //KEY_HOME
                key_handeled = true;
            }
            if (buf == 35)
            {
                buf = 213; //KEY_END
                key_handeled = true;
            }
            if (buf == 20)
            {
                buf = 193; //KEY_CAPS_LOCK
                key_handeled = true;
            }
            if (buf == 112)
            {
                buf = 194; //KEY_F1
                key_handeled = true;
            }
            if (buf == 113)
            {
                buf = 195; //KEY_F2
                key_handeled = true;
            }
            if (buf == 114)
            {
                buf = 196; //KEY_F3
                key_handeled = true;
            }
            if (buf == 115)
            {
                buf = 197; //KEY_F4
                key_handeled = true;
            }
            if (buf == 116)
            {
                buf = 198; //KEY_F5
                key_handeled = true;
            }
            if (buf == 117)
            {
                buf = 199; //KEY_F6
                key_handeled = true;
            }
            if (buf == 118)
            {
                buf = 200; //KEY_F7
                key_handeled = true;
            }
            if (buf == 119)
            {
                buf = 201; //KEY_F8
                key_handeled = true;
            }
            if (buf == 120)
            {
                buf = 202; //KEY_F9
                key_handeled = true;
            }
            if (buf == 121)
            {
                buf = 203; //KEY_F10
                key_handeled = true;
            }
            if (buf == 122)
            {
                buf = 204; //KEY_F11
                key_handeled = true;
            }
            if (buf == 123)
            {
                buf = 205; //KEY_F12
                key_handeled = true;
            }
            if (buf == 124)
            {
                buf = 240; //KEY_F13
                key_handeled = true;
            }
            if (buf == 125)
            {
                buf = 241; //KEY_F14
                key_handeled = true;
            }
            if (buf == 126)
            {
                buf = 242; //KEY_F15
                key_handeled = true;
            }
            if (buf == 127)
            {
                buf = 243; //KEY_F16
                key_handeled = true;
            }
            if (buf == 128)
            {
                buf = 244; //KEY_F17
                key_handeled = true;
            }
            if (buf == 129)
            {
                buf = 245; //KEY_F18
                key_handeled = true;
            }
            if (buf == 130)
            {
                buf = 246; //KEY_F19
                key_handeled = true;
            }
            if (buf == 131)
            {
                buf = 247; //KEY_F20
                key_handeled = true;
            }
            if (buf == 132)
            {
                buf = 248; //KEY_F21
                key_handeled = true;
            }
            if (buf == 133)
            {
                buf = 249; //KEY_F22
                key_handeled = true;
            }
            if (buf == 134)
            {
                buf = 250; //KEY_F23
                key_handeled = true;
            }
            if (buf == 135)
            {
                buf = 251; //KEY_F24
                key_handeled = true;
            }
            //			if (buf == 67) {
            //				buf = 'c'; //KEY_F24
            //				key_handeled = true;
            //			}
            //			if (buf == 86) {
            //				buf = 'v'; //KEY_F24
            //				key_handeled = true;
            //			}


            if (connected && key_handeled)
            {
                try
                {
                    infoLabelKeyboard.Text = buf.ToString();
                    _serialPort.Write("p " + buf.ToString() + " 0 0\n");
                    key_Handeled = key_handeled;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
            }

            infoLabelKeyboard.Text = "D: USB " + buf + " ASCII " + intTestKeyCode;

        }

        private void TranspCtrl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (key_Handeled)
            {
                key_Handeled = false;
                e.Handled = true;
                return;
            }
            else if (connected)
            {
                try
                {
                    int buf = (int)e.KeyChar;
                    infoLabelKeyboard.Text += " keychar" + buf.ToString();
                    _serialPort.Write("k " + buf.ToString() + " 0 0\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
            }
        }

        private void TranspCtrl1_KeyUp(object sender, KeyEventArgs e)
        {
            string testKeyCode = Program.KeyCodeToUnicode(e.KeyCode);
            int intTestKeyCode = 0;
            if (testKeyCode.Length > 0) intTestKeyCode = (int)testKeyCode[0];
            //https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-5.0
            //if (Control.ModifierKeys == Keys.Control && Control.ModifierKeys == Keys.Alt)
            int buf = (int)e.KeyCode;
            //int mod = (int)e.Modifiers;
            bool key_handeled = false;
            if (buf == 17)
            {//17
                buf = 128; //KEY_LEFT_CTRL
                key_handeled = true;
            }
            if (buf == 16)
            {//16
                buf = 129; //KEY_LEFT_SHIFT
                key_handeled = true;
            }
            if (buf == 18)
            {//
                buf = 130; //KEY_LEFT_ALT
                key_handeled = true;
            }
            if (buf == 91)
            {//91
                buf = 131; //KEY_LEFT_GUI
                key_handeled = true;
            }
            if (buf == 163)
            {//
                buf = 132; //KEY_RIGHT_CTRL
                key_handeled = true;
            }
            //			if (buf == 16) {//16
            //				buf = 133; //KEY_RIGHT_SHIFT
            //				key_handeled = true;
            //			}
            //			if (buf == 18) {
            //				buf = 134; //KEY_RIGHT_ALT
            //				key_handeled = true;
            //			}
            if (buf == 92)
            {
                buf = 135; //KEY_RIGHT_GUI
                key_handeled = true;
            }
            if (buf == 38)
            {
                buf = 218; //KEY_UP_ARROW
                key_handeled = true;
            }
            if (buf == 40)
            {
                buf = 217; //KEY_DOWN_ARROW
                key_handeled = true;
            }
            if (buf == 37)
            {
                buf = 216; //KEY_LEFT_ARROW
                key_handeled = true;
            }
            if (buf == 39)
            {
                buf = 215; //KEY_RIGHT_ARROW
                key_handeled = true;
            }
            if (buf == 8)
            {
                buf = 178; //KEY_BACKSPACE
                key_handeled = true;
            }
            if (buf == 9)
            {
                buf = 179; //KEY_TAB
                key_handeled = true;
            }
            if (buf == 13)
            {//
                buf = 176; //KEY_RETURN
                key_handeled = true;
            }
            if (buf == 27)
            {
                buf = 177; //KEY_ESC
                key_handeled = true;
            }
            if (buf == 45)
            {
                buf = 209; //KEY_INSERT
                key_handeled = true;
            }
            if (buf == 46)
            {
                buf = 212; //KEY_DELETE
                key_handeled = true;
            }
            if (buf == 33)
            {
                buf = 211; //KEY_PAGE_UP
                key_handeled = true;
            }
            if (buf == 34)
            {
                buf = 214; //KEY_PAGE_DOWN
                key_handeled = true;
            }
            if (buf == 36)
            {
                buf = 210; //KEY_HOME
                key_handeled = true;
            }
            if (buf == 35)
            {
                buf = 213; //KEY_END
                key_handeled = true;
            }
            if (buf == 20)
            {
                buf = 193; //KEY_CAPS_LOCK
                key_handeled = true;
            }
            if (buf == 112)
            {
                buf = 194; //KEY_F1
                key_handeled = true;
            }
            if (buf == 113)
            {
                buf = 195; //KEY_F2
                key_handeled = true;
            }
            if (buf == 114)
            {
                buf = 196; //KEY_F3
                key_handeled = true;
            }
            if (buf == 115)
            {
                buf = 197; //KEY_F4
                key_handeled = true;
            }
            if (buf == 116)
            {
                buf = 198; //KEY_F5
                key_handeled = true;
            }
            if (buf == 117)
            {
                buf = 199; //KEY_F6
                key_handeled = true;
            }
            if (buf == 118)
            {
                buf = 200; //KEY_F7
                key_handeled = true;
            }
            if (buf == 119)
            {
                buf = 201; //KEY_F8
                key_handeled = true;
            }
            if (buf == 120)
            {
                buf = 202; //KEY_F9
                key_handeled = true;
            }
            if (buf == 121)
            {
                buf = 203; //KEY_F10
                key_handeled = true;
            }
            if (buf == 122)
            {
                buf = 204; //KEY_F11
                key_handeled = true;
            }
            if (buf == 123)
            {
                buf = 205; //KEY_F12
                key_handeled = true;
            }
            if (buf == 124)
            {
                buf = 240; //KEY_F13
                key_handeled = true;
            }
            if (buf == 125)
            {
                buf = 241; //KEY_F14
                key_handeled = true;
            }
            if (buf == 126)
            {
                buf = 242; //KEY_F15
                key_handeled = true;
            }
            if (buf == 127)
            {
                buf = 243; //KEY_F16
                key_handeled = true;
            }
            if (buf == 128)
            {
                buf = 244; //KEY_F17
                key_handeled = true;
            }
            if (buf == 129)
            {
                buf = 245; //KEY_F18
                key_handeled = true;
            }
            if (buf == 130)
            {
                buf = 246; //KEY_F19
                key_handeled = true;
            }
            if (buf == 131)
            {
                buf = 247; //KEY_F20
                key_handeled = true;
            }
            if (buf == 132)
            {
                buf = 248; //KEY_F21
                key_handeled = true;
            }
            if (buf == 133)
            {
                buf = 249; //KEY_F22
                key_handeled = true;
            }
            if (buf == 134)
            {
                buf = 250; //KEY_F23
                key_handeled = true;
            }
            if (buf == 135)
            {
                buf = 251; //KEY_F24
                key_handeled = true;
            }
            //			if (buf == 67) {
            //				buf = 'c'; //KEY_F24
            //				key_handeled = true;
            //			}
            //			if (buf == 86) {
            //				buf = 'v'; //KEY_F24
            //				key_handeled = true;
            //			}

            if (connected && key_handeled)
            {
                try
                {
                    infoLabelKeyboard.Text = buf.ToString();
                    _serialPort.Write("e " + buf.ToString() + " 0 0\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }
            }
            infoLabelKeyboard.Text = "U: USB " + buf + " ASCII " + intTestKeyCode;
        }
    }
}