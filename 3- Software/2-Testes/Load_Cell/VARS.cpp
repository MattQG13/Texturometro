#include "VARS.h"
#include "Filter_MM.h"

long contador = 0;

bool positionLimited = false;
double finalPosition = 0;
double posicao = 0;
double timerValue = 0;

//LoadCell 
double scale =1; // -464.4160886090; HX 10kg//144.69064537 ALPA instrumentos//  //ESCALA PARA AJUSTE
