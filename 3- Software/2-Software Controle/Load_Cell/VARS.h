#ifndef variaveis
#define variaveis

#include <Arduino.h>
#include "Filter_MM.h"

#define pulso 10 //PB2 //10 Pulso 
#define direcao 11 //PB3 //11
#define enMotor 12 //PB4 //12


//====DEFINE LEITOR DE POSIÇÃO=====
#define WITH_ENCODER
//=================================

extern long contador;

extern bool positionLimited;
extern double finalPosition;
extern double posicao;
extern double timerValue;
extern double filtredload;

//LOAD CELL=========================================

extern double scale;


#endif
