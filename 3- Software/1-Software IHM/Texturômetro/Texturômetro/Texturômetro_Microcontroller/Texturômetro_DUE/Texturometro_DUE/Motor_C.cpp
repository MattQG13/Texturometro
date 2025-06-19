#include "Motor_C.h"
#include "VARS.h"
#include "Encoder_C.h"
#include <DueTimer.h>

   
bool zerandoMaquina = false;
double zeroMaquinaLoad = 0;
  
void atualizaMotor(double vel) {
  if (vel > 0.001 || vel < -0.001) {
    SPVel = vel;
    double timerValue = (passo * 1000) / (ppr * (vel > 0 ? vel : -vel) * 2); //ms
    setInterval(timerValue);

    if (vel > 0){
      digitalWrite(direcao,0);
    }
    if (vel < 0) {
      digitalWrite(direcao,1); 
    }
    digitalWrite(enMotor,0);
  } else {
    SPVel = 0;
    Timer1.stop();
    digitalWrite(pulso,0);
    digitalWrite(enMotor,0);
  }
}


void setInterval(double intervaloMS){
    long intervalo = (long)(intervaloMS*1000); //ms -> μs
    if (intervalo>0){
      Timer1.setPeriod(intervalo);
      Timer1.start();
    }else{
      Timer1.stop();
    }
}

void ISR_TIMER1_COMPA_vect() {    
  digitalWrite(pulso, digitalRead(pulso)^1); 
  
  #ifndef WITH_ENCODER
    if (!digitalRead(pulso))contador += digitalRead(direcao)? -dirUP : dirUP;
    posicao = ((double)contador/ppr)*passo;
  #else
    posicao = getPosicao();
  #endif
  
  if(positionLimited){
     if((posicao>finalPosition)^digitalRead(direcao)){
      atualizaMotor(0);
      positionLimited=false;
     }
  }
  if(zerandoMaquina){
    if(abs(filtredload)>=abs(zeroMaquinaLoad)){
      zerandoMaquina=false;
      atualizaMotor(0);
      #ifndef WITH_ENCODER
        contador=0;
      #else
        zeraCont();
      #endif
      Serial.print("[ZERO]!");
    }
  }
}

void configMotor(){
   pinMode(pulso, OUTPUT);
  pinMode(direcao, OUTPUT);
  pinMode (enMotor, OUTPUT);
  digitalWrite(pulso,0);
  digitalWrite(direcao,0);
  digitalWrite(enMotor,0);

  Timer1.attachInterrupt(ISR_TIMER1_COMPA_vect);

}

void rotinaZeroMaquina(double vel, double carga){
    zerandoMaquina=true;
    zeroMaquinaLoad = carga;
    atualizaMotor(-dirUP*abs(vel));
}
