#ifndef Encoder_h
#define Encoder_h

#include  <Arduino.h>

void configEncoder ();
double getPosicao();
double getVel();  
void a1();
void a2();
void zeraCont();

const int pprEncoder = 1200;
const double passoEncoder = 25.4/5;
static volatile long contadorEncoder = 0;

static volatile double millisAnt = 0;
static volatile double posAnt = 0; 

#endif
