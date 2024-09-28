#include "VARS.h"
#include "Filter_MM.h"

long contador = 0;

bool positionLimited = false;
double finalPosition = 0;
double posicao = 0;
double timerValue = 0;

//PID
double SPVel = 0;

//LoadCell 
double scale = -144.77320180;
