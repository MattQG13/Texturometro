#ifndef LoadCell_H
#define LoadCell_H

#include <Arduino.h>

long double ReadLoad();
void tarar(char n = 10);
double calibrar(double val, char n = 15);
void rotinaZeroMaquina(double vel, double carga);
void configADC();

#endif
