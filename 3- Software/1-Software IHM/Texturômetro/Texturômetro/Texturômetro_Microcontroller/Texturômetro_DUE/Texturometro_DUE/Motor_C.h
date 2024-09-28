#ifndef MotorController
#define MotorController

#include <Arduino.h>

  void atualizaMotor(double vel);
  void configMotor();
  void rotinaZeroMaquina(double vel, double carga);
  void setInterval(double intervaloMS);
  
  const double passo = 25.4/5;
  const int ppr = 3200;//800;
  const char dirUP = 1;
  const int IntervalorMax = (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 250);

     
  extern bool zerandoMaquina;
  extern double zeroMaquinaLoad;

#endif
