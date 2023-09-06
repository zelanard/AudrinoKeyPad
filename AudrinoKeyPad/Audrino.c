#include <Keypad.h>

const int ROW_NUM = 4; //four rows
const int COLUMN_NUM = 3; //three columns

char pass[6];

char keys[ROW_NUM][COLUMN_NUM] = {
  {'1','2','3'},
  {'4','5','6'},
  {'7','8','9'},
  {'*','0','#'}
};

byte pin_rows[ROW_NUM] = { 9, 8, 7, 6 }; //connect to the row pinouts of the keypad
byte pin_column[COLUMN_NUM] = { 5, 4, 3 }; //connect to the column pinouts of the keypad

int led = A0;

Keypad keypad = Keypad(makeKeymap(keys), pin_rows, pin_column, ROW_NUM, COLUMN_NUM);

void setup() {
    Serial.begin(9600);
    pinMode(led, OUTPUT);
}

void ResetPassword() {
    for (int i = 0; i < 10; i++) {
        pass[i] = NULL;
    }
}

void Light(int time) {
    digitalWrite(led, HIGH);
    delay(time);
    digitalWrite(led, LOW);
}

void loop() {
    char key = keypad.getKey();

    if (key == '*') {
        ResetPassword();
    }
    else if (key) {
        for (short i = 0; i < sizeof(pass); i++) {
            if (pass[i] == NULL) {
                pass[i] = key;
                if (pass[sizeof(pass) - 1] != NULL) {
                    for (short j = 0; j < sizeof(pass); j++) {
                      Serial.print(pass[j]);
                    }
                    Serial.println();
                    ResetPassword();
                    Light(500);
                }
                break;
            }
        }
    }
}