# Keyboard, Video And MouseControl

This is a software KVM that uses inexpsesive of the shelf componenets. This lets you control a PC or Raspberry PI without needing a secound keyboard, mouse and monitor. 

The KeyboardAndMouseControl folder contains the microcontroller source code.  
The mk_input folder contains the C# source code for the Windows portion of the project.

You need to get two pieces of hardware, a microcontroller with two USB ports and a HDMI capture USB dongle that works with VLC. Here is what I got, but there are many different choises:  
https://www.digikey.com/short/rm80t4z3  
https://amzn.to/3CavHh1

After you load the Arduino sketch to the micocontroller you can send keyboard and mouse from the host to the guest.

The KVM program does the rest, like showing the video and sending the mouse and keyboard. 

Connect the UART USB and USB portion of the USB HDMI dongle the the host and connect the HID USB port and the HDMI portion of the USB HDMI dongle to the guest.

Start the program, resize the window, select the com port, select the USB video device and press connect. The windows menu key left next to the right CTRL key releases the mouse and keyboard from the KVM windows or you can press ALT + F4 to close the window.
