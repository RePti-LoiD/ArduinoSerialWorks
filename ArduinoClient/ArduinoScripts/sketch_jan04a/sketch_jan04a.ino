#include <Arduino.h>
#include <GParser.h>

#define outputPin 9
#define outputPin2 13

char buff[1024];
int ints[72];

void setup() {
    pinMode(outputPin, OUTPUT);
    pinMode(outputPin2, OUTPUT);
    
    Serial.begin(9600);
}

void loop() {    
    if (Serial.available() > 0) {
        

        
        String string = Serial.readStringUntil('.');
        string.toCharArray(buff, 1024);
        
        GParser data2(buff);

        int am2 = data2.parseInts(ints);
        
        for (byte i = 0; i < 72; i++) {
            int a = ints[i];
            digitalWrite(outputPin2, HIGH);
            delay(a * 100);
            digitalWrite(outputPin2, LOW);
            delay(a * 100);
        }
    }
}
