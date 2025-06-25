#include "LoadCell.h"
#include "Interpreter_S.h"
#include "Motor_C.h"
#include "VARS.h"
#include "Encoder_C.h"
#include "PID.h"
#define LI 6 //PD6 //6
#define LS 7 //PD7 //7


void executaComando(SerialInterpreter com);
void envMens();
char endChar = '!';

static long iniTimer = 0;
SerialInterpreter Mensagem;

Filter FMM(8, 0.1);
Filter FFMM (16, 1);
Filter FMMV (5, 0.01);

double load = 0;
double filtredload = 0;
char contVel=0;
void setup() {
  Serial.begin(115200);
  configMotor();
  configADC();
  #ifdef WITH_ENCODER
  configEncoder();
  #endif
  //configEnv();
  pinMode(LI, INPUT_PULLUP);
  pinMode(LS, INPUT_PULLUP);

  while (!Serial);
}

void loop() {
  if (Serial.available()) {
    //TIMSK3 &= ~(1 << OCIE3A);

    String mens = Serial.readStringUntil(endChar);
    Mensagem.AtMensagem(mens);
    executaComando(Mensagem);
    mens += endChar;
    Serial.print(mens);

    //TIMSK3 |= (1 << OCIE3A);
  } else if (!digitalRead(0)) {
    atualizaMotor(0);
  }

  if (!digitalRead(LS)) {
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso, 0);
    digitalWrite(enMotor, 1);
  }

  if (!digitalRead(LI)) {
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso, 0);
    digitalWrite(enMotor, 1);
  }
  
  envMens();
  //PID(FMMV.filtrarMMA(getVel()));
  delay(10);
}

/*void configEnv(){
  TCCR4A = 0;
  TCCR4B = 0;
  
  
  TCCR4B &=  ~(1 << CS30);
  TCCR4B &=  ~(1 << CS31);
  TCCR4B |= (1 << CS32);
  
  OCR4A=624;
  TCNT4 = 0;
  TIMSK4 |= (1 << OCIE4A);  
}*/
void executaComando(SerialInterpreter com) {
  switch (com.lenght) {
    case 1:
      if (com.Comando == "TARA") {
        tarar(100);
      }
      if (com.Comando == "INITIME") {
        iniTimer = millis();
      }
      //String S ="["+com.Comando+"]"+endChar;
      break;

    case 2:
      if (com.Comando == "CAL") {
        double cal = calibrar(com.Valor);
        String mens = "[LCC;";
        mens += String(cal, 8);
        mens += "]";
        mens += endChar;
        Serial.print(mens);
      }
      if(com.Comando == "LCC"){
        scale = com.Valor;
        tarar(50);
      }
      break;

    case 3:
      if (com.Comando == "M") {
        if (com.Modo == "UP") {
          atualizaMotor(dirUP * abs(com.Valor));
        } else if (com.Modo == "DN") {
          atualizaMotor(-dirUP * abs(com.Valor));
        } else if (com.Modo == "S") {
          atualizaMotor(0);
        }
        positionLimited = false;
      }
      if (com.Comando == "ZERAR") {
        rotinaZeroMaquina(com.Valor, com.Valor2);
      }
      break;
    case 4:
      if (com.Comando == "M") {
        if (com.Modo == "UP") {
          atualizaMotor(dirUP * abs(com.Valor));
        } else if (com.Modo == "DN") {
          atualizaMotor(-dirUP * abs(com.Valor));
        } else if (com.Modo == "S") {
          atualizaMotor(0);
        }
        positionLimited = true;
        finalPosition = com.Valor2;
      }
      /*Serial.println(com.Comando);
      Serial.println(com.Valor);
      Serial.println(com.Valor2);
      Serial.println(positionLimited);*/
      break;
    default:
      break;
  }
}

void envMens() {
  String bufferText = "";

  load = ReadLoad();
  filtredload = FFMM.filtrarMMP(FMM.filtrarMMP(load));
  double difTimer = (double)(millis() - iniTimer) / 1000;
  bufferText += "[L;";
  bufferText += String (filtredload, 1);
  if (iniTimer > 0) {
    bufferText += ";";
    bufferText += String (difTimer, 3);
  }
  bufferText += "]";
  bufferText += endChar;
  bufferText += "[E;"; 
  
  #ifdef WITH_ENCODER
    posicao = getPosicao();
  #endif

    bufferText += String (posicao, 1);
    if (iniTimer > 0) {
      bufferText += ";";
      bufferText += String (difTimer, 3);
    }
  
    bufferText += "]";
    bufferText += endChar;
  
  if(abs(filtredload)>10000){
    atualizaMotor(0);
    Serial.print("[W;O]!");
  }
  #ifdef WITH_ENCODER
  if(contVel>=1){
    bufferText += "[V;"; 
    double velReal = FMMV.filtrarMMA(getVel());
    bufferText += String (velReal, 1);
  
    bufferText += "]";
    bufferText += endChar;
    
    contVel=0;
    #ifdef WITH_ENCODER
      #ifdef WITH_PID
        PID(velReal);
      #endif
    #endif
  }
  contVel++;
  #endif
  
  Serial.print(bufferText);
}

/*ISR(TIMER4_COMPA_vect) {
  envMens();
  TCNT4 = 0;
  //digitalWrite(50, digitalRead(50)^1); 
}*/

/*
  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
  xxxxxxxxxxxxxxxxxxxxxddddddddxxxxxxxxxxxxxxxxxxxxxxxxxxx
  xxxxxxxxxxxxxxddol:,,,'.'',',,;:codxxxxxxxxxxxxxxxxxxxxx
  xxxxxxxxxxxddlc;..................',coxxxxxxxxxxxxxxxxxx
  xxxxxxxxdoc;'......................''',cdxxxxxxxxxxxxxxx
  xxxxxxdc,...............................':oxxxxxxxxxxxxx
  xxxxdd;.........;,.........................;oxxxxxxxxxxx
  xxxdl...'.......';,..........................:xxxxxxxxxx
  xxdl.............;;'..................     ...,dxxxxxxxx
  xdl.  ..........'::,...............          ..'oxxxxxxx
  do'   ..........';:,...............             .oxxxxxx
  d,     .........':ol;,'............              'dxxxxx
  c.     ....''',:ldxxxdllllllllc:,'...             ;xxxxx
  ,       ..';:cdxO0000KKKKK000KK00Okkdl:,.. .      .lxxxx
  .      ..,:oddO0KKKKKKKKKKKKKKKKKK000OOxxxc,.      'dxxx
  .     .,:oxxdOKKKKKKKKKKKKKKKKKK00000OOOkOkd:.     .cxxx
  .    .:dxkkkO0KXXKKKKKKKKKKKKKKKKK00OOOOOOOkdc.....',dxx
  l.  .:dxkkOO0KXXXKKKKKKKKKKKKKKKK000OOOOOOOkkd,..lxl:lxx
  ol,..:ddxkkOO0KKKKK00KKKKK000000OOOOOOOOOOkkkkc.cxxl:;xx
  dol;.,ddkkkkkkOOOO000000K0000KK0OO0OOOOOkkkkkkd;lxxxl'ox
  dd:c:;dkkOkkkkO000OOOOOOO0000000kkkkkkxxddxkkkxc;coxo.:x
  ddolocddddxxxdxkO0OOkdxOOxdxdodoolloxkkkkkkOOkxlclcd;.'d
  dddoocooooodddlcclc;;:dkkkddol;,:;.':ldxkOOOOkxo:coo...o
  dddllcoxxxdol;'.,c,;clxkOOkkkkdooooodxO000OOOkxolod:...c
  dool::okOkkdoollolloxxkO00OkOOOOkkkOO00K00OOOkxdddc....:
  oolcc.ckOO0000OOOOOOkkk0KKOOOOO0K000000KK0OOOkxc;,.....,
  oclll..dkO0KXKKKK00Okkk0KKOOOOOOO000000KK0OOOkd;.......'
  lllll'.:xkOKKK0000OkkkkO0K0OOOkkxdkkOOOOOOOOkkd,........
  olcll' .okkOOOOOkxodkkkk0K0OOOOkOxoldxkkkkkkxxd'........
  lllcc' .,dxxkkkxoclxkOkkO00OOOkkOOkxoclodddxxxl.........
  llllc'...,looooc:cdxxxkO000Okkkkkkkkkxolcdxxxx,.........
  lcllc.....'clc:;cdxxxdddddxkxkOOOkkkkxxdoxkxxl..........
  c:c:,......'loolllodxxxkkkOOkkxxdd::dkkdxxxdo...........
  .........   .:ddooooc:lllodooocolodkkkkxxdol. .'..'.....
  .......      .'codxxxdodoododdxkOkkkkkkxddl.   ...'.....
  .......      . .'lddddxkOOOOOkkxxdxxkkxdoc.    .........
  .......      ..   'ldddollooooddxkkkkxol,'.   .....'....
  ........  .. ..     .lxxkkkOOOOOOOkxdl;...     ..'.'....
  ..................    'codxxxxxxxdlc;,.....   ..........
  ..............'.  .   ..';::::::;;;;,... ..  ....... ...
  .........''....  ..   ...',;,,,,,,'..... .' ........ ...
  .........'......  .........,;;'............... ....   ..
  .........'.....  . .. ...,,';:;'...........   ....    .
  ...............  .. .  .,;;:;,'..........  ...
*/
