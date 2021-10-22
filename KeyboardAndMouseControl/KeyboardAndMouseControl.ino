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
    int value = 0;
    int value2 = 0;
    byte r = 0, g = 0, b = 0;
    if (Serial.read() == '\n') {
      switch (inChar) {
        case 'a':
          // move mouse
          value = Serial.parseInt();
          value2 = Serial.parseInt();
          Mouse.move(value, value2);
          break;
        case 'm':
          value = Serial.parseInt();
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
          value = Serial.parseInt();
          // perform keyboard stuff
          //https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
          Keyboard.write(value);
          break;
        case 'p':
          value = Serial.parseInt();
          // perform keyboard press
          Keyboard.press(value);
          break;
        case 'e':
          value = Serial.parseInt();
          // perform keyboard release
          Keyboard.release(value);
          break;
        case 's':
          r = Serial.parseInt();
          g = Serial.parseInt();
          b = Serial.parseInt();
          // change led
          leds[0] = CRGB(r, g, b);
          FastLED.show();
          break;
      }
    }
  }
  //delay(5);
}
