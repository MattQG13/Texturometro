#ifndef variaveis
#define variaveis

#include <Arduino.h>
#include "Filter_MM.h"
#include "CS5530.h"

//====DEFINE LEITOR DE POSIÇÃO=====
#define WITH_ENCODER
//=================================
#define WITH_PID

//========DEFINE MODULO ADC========
  //#define WITH_ADC_HX711
  //#define WITH_ADC_CS5530
  #define WITH_NO_ADC
//=================================

//========PORTAS IO MOTOR========
#define pulso 10 //PB2 //10 Pulso 
#define direcao 11 //PB3 //11
#define enMotor 12 //PB4 //12

//=====PORTAS IO FIM DE CURSO====
#define LI 6 
#define LS 7 

//======PORTAS IO DOS BOTOES=====
#define portSTOP 14
#define portUP 15  
#define portDN 16

//=======PORTAS IO ENCODER=======
#define A 18
#define B 19

//========PORTAS IO CS5530=======
#ifdef WITH_ADC_CS5530
  #define CSPinCS5530 2
  #define MOSIPinCS5530 3
  #define MISOPinCS5530 4
  #define SCLKPinCS5530 5
#endif

//========PORTAS IO HX711========
#ifdef WITH_ADC_HX711
  #define  ADDO  8 
  #define  ADSK  9 
#endif

extern long contador;

extern bool positionLimited;
extern double finalPosition;
extern double posicao;
extern double filtredload;

//PID
extern double SPVel;

//LOAD CELL=========================================

extern double scale;


#endif
