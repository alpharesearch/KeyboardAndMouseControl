/*
  KeyboardAndMouseControl

  Hardware:
  - five pushbuttons attached to D12, D13, D14, D15, D0

  The mouse movement is always relative. This sketch reads four pushbuttons, and
  uses them to set the movement of the mouse.

  WARNING: When you use the Mouse.move() command, the Arduino takes over your
  mouse! Make sure you have control before you use the mouse commands.

  created 15 Mar 2012
  modified 27 Mar 2012
  by Tom Igoe

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/KeyboardAndMouseControl
*/

#include "USB.h"
#include "USBHIDMouse.h"
#include "USBHIDKeyboard.h"
USBHIDMouse Mouse;
USBHIDKeyboard Keyboard;

void setup() {
  Serial.begin(921600);
  // initialize mouse control:
  Mouse.begin();
  Keyboard.begin();
  USB.begin();
}

void loop() {
  // use serial input to control the mouse:
  if (Serial.available() > 0) {
    char inChar = Serial.read();
    int value = Serial.parseInt();
    if (Serial.read() == '\n') {
      switch (inChar) {
        case 'u':
          // move mouse up
          Mouse.move(0, -1 * value);
          break;
        case 'd':
          // move mouse down
          Mouse.move(0, 1 * value);
          break;
        case 'l':
          // move mouse left
          Mouse.move(-1 * value, 0);
          break;
        case 'r':
          // move mouse right
          Mouse.move(1 * value, 0);
          break;
        case 'm':
          // perform mouse left click
          if (value == 0)Mouse.click(MOUSE_LEFT);
          if (value == 1)Mouse.click(MOUSE_RIGHT);
          if (value == 2)Mouse.click(MOUSE_MIDDLE);
          break;
        case 'k':
          // perform keyboard stuff
          Keyboard.write(value);
          break;
      }
    }
  }
  //delay(5);
}
