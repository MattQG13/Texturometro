#ifndef PID_h
#define PID_h

#include <Arduino.h>

//static double P;
//static double I;

void PID(double vel);
static double calcFreq(double value);

const double kd = 0.3;
const double ki = 0.58;
const double kp = 1.1;

//kp: 25

#endif
