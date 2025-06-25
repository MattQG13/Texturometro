#ifndef PID_h
#define PID_h

#include <Arduino.h>

//static double P;
//static double I;

void PID(double vel);
static double calcFreq(double value);

const double kd = 0.0001;
const double ki = 0.7;
const double kp = 3.2;

//kp: 25

#endif
