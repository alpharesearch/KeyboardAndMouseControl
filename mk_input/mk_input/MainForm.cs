/*
 * Created by SharpDevelop.
 * User: markus
 * Date: 10/13/2021
 * Time: 8:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace mk_input
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private SerialPort _serialPort;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		bool connected = false;
		void Button1Click(object sender, EventArgs e)
		{
			// all of the options for a serial device
			// ---- can be sent through the constructor of the SerialPort class
			// ---- PortName = "COM1", Baud Rate = 19200, Parity = None,
			// ---- Data Bits = 8, Stop Bits = One, Handshake = None
			if (_serialPort != null)
				_serialPort.Close();
			_serialPort = new SerialPort("COM7", 921600, Parity.None, 8, StopBits.One);
			_serialPort.Handshake = Handshake.None;
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
			_serialPort.Open();
			try {
				if (!(_serialPort.IsOpen))
					_serialPort.Open();
				_serialPort.Write("t\r\n");
			} catch (Exception ex) {
				MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
			}
//			this.Cursor = new Cursor(Cursor.Current.Handle);
//			//Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
//			Cursor.Position = new Point(this.Location.X + this.textBox1.Location.X + 8, this.Location.Y + this.textBox1.Location.Y + 30);
//			Cursor.Clip = new Rectangle(Cursor.Position, this.textBox1.Size);
			connected = true;
		}

		delegate void SetTextCallback(string text);
		void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			Thread.Sleep(500);
			string data = _serialPort.ReadLine();
			// Invokes the delegate on the UI thread, and sends the data that was received to the invoked method.
			// ---- The "si_DataReceived" method will be executed on the UI thread which allows populating of the textbox.
			//this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
			this.BeginInvoke(new SetTextCallback(si_DataReceived), new object[] { data });
		}
		
		private void si_DataReceived(string data)
		{
			textBox1.Text += data.Trim();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_serialPort != null)
				_serialPort.Close();
		}

		Point myPoint;
		void TextBox1MouseMove(object sender, MouseEventArgs e)
		{
			if (connected) {
				try {
					if (e.Location.X > myPoint.X)
						_serialPort.Write("r5\n");
					if (e.Location.X < myPoint.X)
						_serialPort.Write("l5\n");
					if (e.Location.Y < myPoint.Y)
						_serialPort.Write("u5\n");
					if (e.Location.Y > myPoint.Y)
						_serialPort.Write("d5\n");
				} catch (Exception ex) {
					MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
				}
				
				myPoint = e.Location;
			}
		}
		
		void TextBox1MouseDown(object sender, MouseEventArgs e)
		{
			if (connected) {
				try {
					if (e.Button == MouseButtons.Left)
						_serialPort.Write("m0\n");
					if (e.Button == MouseButtons.Right)
						_serialPort.Write("m1\n");
					if (e.Button == MouseButtons.Middle)
						_serialPort.Write("m2\n");
				} catch (Exception ex) {
					MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
				}
			}
		}
		void TextBox1KeyDown(object sender, KeyEventArgs e)
		{
			//https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
			
			//if (Control.ModifierKeys == Keys.Control && Control.ModifierKeys == Keys.Alt)
			
			if (connected) {
				try {
					int buf = (int)e.KeyCode;
					if (buf == 13) buf = 176; //send return key
					label2.Text = buf.ToString();
					_serialPort.Write("k" + buf.ToString() + "\n");
				} catch (Exception ex) {
					MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
				}
			}
		}
		void TextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
	}
}