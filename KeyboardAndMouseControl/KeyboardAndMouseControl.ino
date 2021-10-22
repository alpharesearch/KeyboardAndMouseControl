/*
  KeyboardAndMouseControl

  C 2021 Markus Schulz
*/

#include <FastLED.h>
#include "USB.h"
#include "USBHIDMouse.h"
#include "USBHIDKeyboard.h"
#define LED_PIN     18
#define NUM_LEDS    1
CRGB leds[NUM_LEDS];
byte r = 0, g = 0, b = 0;
USBHIDMouse Mouse;
USBHIDKeyboard Keyboard;

void setup() {
  Serial.begin(921600);
  // initialize mouse control:
  Mouse.begin();
  Keyboard.begin();
  USB.begin();
  FastLED.addLeds<WS2812, LED_PIN, GRB>(leds, NUM_LEDS);
  leds[0] = CRGB(0, 0, 255);
  FastLED.show();
}

void loop() {
  // use serial input to control the mouse:
  if (Serial.available() > 0) {
    char inChar = Serial.read();
    int value = Serial.parseInt();
    int value2 = 0;
    if (Serial.read() == '\n') {
      switch (inChar) {
        case 'a':
          // move mouse
          value2 = Serial.parseInt();
          Mouse.move(value, value2);
          break;
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
          if (value == 3)Mouse.press(MOUSE_LEFT);
          if (value == 4)Mouse.press(MOUSE_RIGHT);
          if (value == 5)Mouse.press(MOUSE_MIDDLE);
          if (value == 6)Mouse.release(MOUSE_LEFT);
          if (value == 7)Mouse.release(MOUSE_RIGHT);
          if (value == 8)Mouse.release(MOUSE_MIDDLE);
          if (value == 9) {
            Mouse.release(MOUSE_LEFT);
            Mouse.release(MOUSE_RIGHT);
            Mouse.release(MOUSE_MIDDLE);
            Keyboard.releaseAll();
          }
          break;
        case 'k':
          // perform keyboard stuff
          //https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
          Keyboard.write(value);
          break;
        case 'p':
          // perform keyboard press
          Keyboard.press(value);
          break;
        case 'e':
          // perform keyboard release
          Keyboard.release(value);
          break;
        case 'x':
          r = value;
          break;
        case 'y':
          // change led
          g = value;
          break;
        case 'z':
          // change led
          b = value;
          break;
        case 's':
          // change led
          leds[0] = CRGB(r, g, b);
          FastLED.show();
          break;
      }
    }
  }
  //delay(5);
}
