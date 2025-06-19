#include "LoadCell.h"
#include "VARS.h"

#define MMP 13

Filter FMME(5, 0.1);
Filter FMMA (5, 0.1);

double load = 0;
double filtredloadMMA = 0;
double filtredloadMME = 0;

void setup() {
  Serial.begin(115200);
  pinMode(MMP,INPUT_PULLUP);
  Serial.println("Serial OK");
  configADC();
  Serial.println("ADC OK");
  delay(100);
}

void loop(){

  load = ReadLoad();//+random(-100,100);
    filtredloadMMA = FMMA.filtrarMMA(load);
    filtredloadMME = FMME.filtrarMME(load);

  Serial.println(load);
  Serial.print(',');
  Serial.print(filtredloadMMA);
  Serial.print(',');
  Serial.println(filtredloadMME);
  

  delay(10);
}
