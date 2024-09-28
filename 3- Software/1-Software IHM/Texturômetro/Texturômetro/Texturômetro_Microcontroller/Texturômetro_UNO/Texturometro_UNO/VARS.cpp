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
double scale = 224.419;//-53.8071 ;// -26.90355;  //ESCALA PARA AJUSTE
