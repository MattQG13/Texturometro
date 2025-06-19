
#include "PID.h"
#include "VARS.h"
#include "Encoder_C.h"
#include "Motor_C.h"
 
static double P=0;
static double I=0; 
static double D=0;

static double ErroAnt=0;
static double timeAnt=0;
void PID(double vel){
    
    double Erro = calcFreq(SPVel)-calcFreq(vel);
    P = Erro*kp;
    I += Erro*ki;
    D = (Erro-ErroAnt)/(millis()-timeAnt);
    double PID = P+I+D;
    
    setInterval(1/PID);

    ErroAnt = Erro;
    timeAnt = millis();
    /*Serial.print(calcFreq(SPVel));
    Serial.print(" ");
    Serial.print(calcFreq(vel));
    Serial.print(" ");
    Serial.print(Erro);
    Serial.print("   P:");
    Serial.print(P);
    Serial.print(" I:");
    Serial.print(I);
    Serial.print("  PI:");
    Serial.println(1/ControlePI);
    */

}

static double calcFreq(double value){
  if(value!=0){
    return (ppr * abs(value) )/(passo * 1000);
  }else{
    return 0;
  }
}
