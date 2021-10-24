# Keyboard, Video And MouseControl

This is a software KVM (keyboard, video, mouse) project that uses inexpensive off-the-shelf components. This lets you control a PC or Raspberry PI (aka guest) without needing a second keyboard, mouse, and monitor. You will need a Windows PC (aka host) with two USB ports or USB hub if you only have one port available. 

The KeyboardAndMouseControl folder contains the microcontroller source code.  
The mk_input folder contains the C# source code for the Windows portion of the project.

You need to get two pieces of hardware: a microcontroller with two USB ports and an HDMI capture USB dongle that works with VLC direct show capture. 

I'm using an ESP32-S2-DEVKITC-1, but there are many different other choices:
https://www.digikey.com/short/rm80t4z3  

You can find USB 3 HDMI capture dongles at Amazon:  
https://amzn.to/3CavHh1 (paid link) or https://amzn.to/3GjZarp (paid link)  

As an Amazon Associate I earn from qualifying purchases. You can also use this link to donate to me directly: https://www.paypal.com/donate?hosted_button_id=2FVM75EDKDLNU  
Thank you for your support.

After you load the Arduino sketch to the microcontroller you are able to send the keyboard and mouse inputs from the host to the guest via serial communication. I selected the Espressif ESP32-S2-DevKitC-1 because it has two USB ports. But any microcontroller that can be made to work with a serial port on one USB port and a HID keyboard and mouse port on another USB port should work. I used Arduino 1.8.16, under Linux the compilation is very fast, but under Windows not so much. At this point in time, to get the S2 working I had to install the ESP32 beta V2.0 board version.

The KVM program does the rest, like showing the video and sending the mouse and keyboard over serial accordingly. I started to program the software under SharpDevelop (RIP), but I had to move to Visual Studio 2019 to download all the dependents (like libVLCsharp) via NuGet. I will make the compiled version available as a download on GitHub.

To use the system just connect the UART USB and USB portion of the USB HDMI dongle to the host and connect the HID USB and the HDMI portion of the USB HDMI dongle to the guest.

Start the program, resize the window, select the com port, select the USB video device, and press connect button. The 'menu' key (it's left next to the right CTRL key) releases the mouse and keyboard from the KVM window or you can just press ALT + F4 to close the program.