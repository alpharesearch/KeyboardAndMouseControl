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
  leds[0] = CRGB(255, 0, 0);
  FastLED.show();
  sleep(1);
  leds[0] = CRGB(0, 0, 255);
  FastLED.show();
}

void loop() {
  // use serial input to control the mouse:
  if (Serial.available() > 0) {
    char inChar = Serial.read();
    int value0 = Serial.parseInt();
    int value1 = Serial.parseInt();
    int value2 = Serial.parseInt();
    if (Serial.read() == '\n') {
      switch (inChar) {
        case 'a':
          // move mouse
          Mouse.move(value0, value1);
          break;
        case 'm':
          // perform mouse left click
          if (value0 == 0)Mouse.click(MOUSE_LEFT);
          if (value0 == 1)Mouse.click(MOUSE_RIGHT);
          if (value0 == 2)Mouse.click(MOUSE_MIDDLE);
          if (value0 == 3)Mouse.press(MOUSE_LEFT);
          if (value0 == 4)Mouse.press(MOUSE_RIGHT);
          if (value0 == 5)Mouse.press(MOUSE_MIDDLE);
          if (value0 == 6)Mouse.release(MOUSE_LEFT);
          if (value0 == 7)Mouse.release(MOUSE_RIGHT);
          if (value0 == 8)Mouse.release(MOUSE_MIDDLE);
          if (value0 == 9) {
            Mouse.release(MOUSE_LEFT);
            Mouse.release(MOUSE_RIGHT);
            Mouse.release(MOUSE_MIDDLE);
            Keyboard.releaseAll();
          }
          break;
        case 'k':
          // perform keyboard stuff
          //https://www.arduino.cc/reference/en/language/functions/usb/keyboard/keyboardmodifiers/
          Keyboard.write(value0);
          break;
        case 'p':
          // perform keyboard press
          Keyboard.press(value0);
          break;
        case 'e':
          // perform keyboard release
          Keyboard.release(value0);
          break;
        case 's':
          // change led
          leds[0] = CRGB(value0, value1, value2);
          FastLED.show();
          break;
      }
    }
  }
}
