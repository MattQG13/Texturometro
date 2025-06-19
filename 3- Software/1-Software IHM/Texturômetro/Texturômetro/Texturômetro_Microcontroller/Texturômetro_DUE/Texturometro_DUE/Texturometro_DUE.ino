#include "LoadCell.h"
#include "Interpreter_S.h"
#include "Motor_C.h"
#include "VARS.h"
#include "Encoder_C.h"
#include "PID.h"
#include <DueTimer.h>


void executaComando(SerialInterpreter com);
void envMens();
char endChar = '!';

static long iniTimer = 0;
SerialInterpreter Mensagem;

Filter FMM(8, 0.1);
Filter FFMM (8, 0.1);
Filter FMMV (5, 0.01);

static bool travaLI = false;
static bool travaLS = false;
static bool travaportSTOP = false;
static bool travaportUP = false;
static bool travaportDN = false;

double filtredload = 0;

void setup() {
  Serial.begin(115200);
  configMotor();
  configADC();
  delay(100);
  #ifdef WITH_ENCODER
  configEncoder();
  #endif  
  //configEnv();
  pinMode(LI, INPUT_PULLUP);
  pinMode(LS, INPUT_PULLUP);
  pinMode(portSTOP, INPUT_PULLUP);
  pinMode(portUP, INPUT_PULLUP);
  pinMode(portDN, INPUT_PULLUP);

  while (!Serial);
}

void loop() {
  if (Serial.available()) {
    String mens = Serial.readStringUntil(endChar);
    Mensagem.AtMensagem(mens);
    executaComando(Mensagem);
    mens += endChar;
    Serial.print(mens);
  } else if (!digitalRead(0)) {
    atualizaMotor(0);
  }

  if (!digitalRead(LS)&&!travaLS) {
    atualizaMotor(0);
    digitalWrite(pulso, 0);
    digitalWrite(enMotor, 1);
    String mens = "[LS;1]";
    mens+=endChar;
    Serial.print(mens);
    travaLS=true;
  }else if(digitalRead(LS)&&travaLS){
    travaLS=false;
    String mens = "[LS;0]";
    mens+=endChar;
    Serial.print(mens);
  }

  if (!digitalRead(LI)&&!travaLI) {
    atualizaMotor(0);
    digitalWrite(pulso, 0);
    digitalWrite(enMotor, 1);
    String mens = "[LI;1]";
    mens+=endChar;
    Serial.print(mens);
    travaLI=true;
  }else if(digitalRead(LI)&&travaLI){
    travaLI=false;
    String mens = "[LI;0]";
    mens+=endChar;
    Serial.print(mens);
  }

if (!digitalRead(portSTOP)&&!travaportSTOP) {
    atualizaMotor(0);
    digitalWrite(pulso, 0);
    digitalWrite(enMotor, 1);
    String mens = "[STOP;1]";
    mens+=endChar;
    mens+= "[W;S]";
    mens+=endChar;
    Serial.print(mens);
    travaportSTOP=true;
  }else if(digitalRead(portSTOP)&&travaportSTOP){
    travaportSTOP=false;
    String mens = "[STOP;0]";
    mens+=endChar;
    Serial.print(mens);
  }


if (!digitalRead(portUP)&&!travaportUP) {
    travaportUP=true;
    String mens = "[UP;1]";
    mens+=endChar;
    Serial.print(mens);
  }else if(digitalRead(portUP)&&travaportUP){
    atualizaMotor(0);
    travaportUP=false;
    String mens = "[UP;0]";
    mens+=endChar;
    Serial.print(mens);  }
  
if (!digitalRead(portDN)&&!travaportDN) {
    travaportDN=true;
    String mens = "[DN;1]";
    mens+=endChar;
    Serial.print(mens);
  }else if(digitalRead(portDN)&&travaportDN){
    atualizaMotor(0);
    travaportDN=false;
    String mens = "[DN;0]";
    mens+=endChar;
    Serial.print(mens);
  }

  envMens();
  //PID(FMMV.filtrar(getVel()));
  delay(10);
}

void configEnv(){
  Timer2.attachInterrupt(ISR_TIMER2_COMPA_vect);
  Timer2.setPeriod(10*1000);
  Timer2.start();
}

void executaComando(SerialInterpreter com) {
  switch (com.lenght) {
    case 1:
      if (com.Comando == "TARA") {
        tarar(100);
      }
      if (com.Comando == "INITIME") {
        iniTimer = millis();
      }
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
      break;
    default:
      break;
  }
}

void envMens() {
  String bufferText = "";
  filtredload = FFMM.filtrar(FMM.filtrar(ReadLoad()));

  double difTimer = (double)(millis() - iniTimer) / 1000;
  bufferText += "[L;";
  bufferText   += String (filtredload, 1);
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
    bufferText += "[V;"; 
    double velReal = FMMV.filtrar(getVel());
    bufferText += String (velReal, 1);
  
    bufferText += "]";
    bufferText += endChar;
    
    #ifdef WITH_ENCODER
      #ifdef WITH_PID
        PID(velReal);
      #endif
    #endif
  #endif
  
  Serial.print(bufferText);
}

void ISR_TIMER2_COMPA_vect() {
  envMens();
}

/*
.........          ....,,;,,'...
.........   ....,:okkO0KK000Okxoc;.
............';lxXMWWNWWMWWWWWNXXXXKko,.
.........,lxKNWWWWNNWWWWWWWWWWWNNNXK00Oo'
.......:OXXXNNNWKKNWWWWWWWWNNNNNNNWWNXXK0x'
......xNNNNWWNWNkOXNWWMMMWWWWNNNNWWWWNWNNNXk,
....;KWNKXWWWWNN0dxXNNWWMMWWNNNNXXNNWWMMMWWWXd.
...:NMWNXNNWWWNNXkdKXNWWWMMWWNXXXNNWWMMMMMMMWW0;
..;NMMMWXXWWWWWNKdoOKXNNWMMMWNNNNWWMMMMMMMMMMMMX;.
.;KMMMMWNWWWWWNNKxdOKXXXWWWWNXKKNWMMMMMMMMMMMMMMX:.
'OMMMMMWNNNXXXKXOo,;dxOKNNNNNXXXNWMMMMMMMMMMMMMMMN,.
cWMMMMMWNNK0K0Oo;.  ..,;,,;;;;:oxO0XWMMMMMMMMMMMMMk.
OMMMMMMMWX0koc.                    ..,lxKNWWMMMMMMW:.
NMMMMMMWNOo,'.                            ;kNMMMMMMK.
WMMMMMNOl,...                              .lKMMMMMWo.
XMMMMXl.                                    .c0WMWKk0'
:KMMWd'.                                     .dWX; 'cl
';kWNd...                                     ,0c  'cd.
.',dNk'.                                      .d;   ;x:.
..l:cd.                                ....    :dc' .Xk.
..','c..... ...      ..  ....''';::'..         ,:.:.lWX'
....'l'',;'...;cc;cddo'   .'';dklxK0o,..       'l:''KNN:
...;;l' ...,:d0Xkcxdc;.       .''''..         .':,.lNNNo
..'coo'    .,',,',;.                           ...:XNNNd
'':lcKc                                       .:xkNNNNNO
,c;;;KN.                                      .oNXNWNNNK
;;:;;OMd.                       ...           .xNXNWNNNN
,;c;:0MN'        .'.             .';..      ...kWXNNNWNN
;;ccl0MWk..    .,oc.               .,cc;'.....'KWXXNWWNN
;:;;c0MNNO;,,,;lxc...                .':c'.  .dWWXXXWNNW
:c;:lXWXNW0cccdxc'.........            .,.  .;NNWNXNNWWW
coodOWNNWMWK:,,;::,....       ...'oo.  .....,0MNWXKNXNWW
NNNNNMWWMMMMNl'.,,';lo;;;,',''c';'.     ..,;0MM0MX0NNNWW
WWWWWMWMMMMMMW0l,........'.'..          .';0MMMXWX0NWNNW
WWWWWWWMMMMMMWMW0:..''.        .....  .'':0MMMMWXXXXNNNN
WWWWWWNMMMMMMNWMMWO:..';;;,,,''..    .':k0NMMMWWXKK0WWNN
WWWWWNNWMMWWMN0MMMMM0:.            .':dKNKWMMMMWWKN0WWNN
WWWNNXNNWWWNXX0NNWMMMW0l,........';lxkKNWXNMMMMWWKNKMWNN
WWNNXXXNNXNXNK0NMMWMMMMW0xdoooodxxxxOKNNMWXMMMWWMXXNMMNN
WWWNXXXWX0KNKX0WWMWMMMWWXKOxkkkkkO0KXNWWMW0MWWWWWWXWMMWW
WWWNXNNWK0NWNXKWMMMMWMWNXKKkdxOKXNNWWNWWWW0WWMMWMWNMMMWW
MMMWWWWWK0NWNXXMWWMWWMWNKOOOxodOKXXXXXNWWXNMMMWMMWMMMMMM
MMMMMWMMXWXXXXKNWMMWWMWMMNOdxoox0XNNWNWMNXWMMWWMMMMMMMMM
*/
