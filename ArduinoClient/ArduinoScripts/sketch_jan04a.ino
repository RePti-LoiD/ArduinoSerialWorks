#include <Arduino.h>

#define outputPin 13

int value = 0;

void setup() {
    pinMode(outputPin, OUTPUT);
    Serial.begin(9600);
}

void loop() {
    if (Serial.available() > 0) {
        
        Serial.setTimeout(100);
        String portData = Serial.readStringUntil('.');
        
        if (portData == "True") {
            digitalWrite(outputPin, HIGH);
        }
        else if (portData == "False") {
            digitalWrite(outputPin, LOW);
        }
    }
}
