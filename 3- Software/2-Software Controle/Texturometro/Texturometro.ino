#include <CS5530.h>
#include <Filter_MM.h>
#include <Interpreter_S.h>

#define pulso 10 //PB2 //10 Pulso 
#define direcao 11 //PB3 //11
#define enMotor 12 //PB4 //12

#define LI 6 //PD6 //6
#define LS 7 //PD7 //7



//========DEFINE MODULO ADC========
  //#define WITH_ADC_HX711
  #define WITH_ADC_CS5530
//=================================
//_________________________________
//========DEFINE MODULO ADC========
  //#define WITH_ENCODER
  #define WITHOUT_ENCODER
//=================================
#ifdef WITH_ADC_CS5530
  const int CSPinCS5530 = 2;
  const int MOSIPinCS5530 = 3;
  const int MISOPinCS5530 = 4;
  const int SCLKPinCS5530 = 5;
  
  CS5530 myADC (MOSIPinCS5530, MISOPinCS5530, SCLKPinCS5530, CSPinCS5530);
#endif

#ifdef WITH_ADC_HX711
  #define  ADDO  8 //PB0 //8
  #define  ADSK  9 //PB1 //9
#endif

long double ReadLoad();
void tarar(char n = 10);
static double calibrar(double val, char n = 15);

void executaComando(SerialInterpreter com);
void atualizaMotor(double vel);
void envMens();

const double passo = 5;
const double ppr = 800;

static double scale = -53.8071 ;// -26.90355;  //ESCALA PARA AJUSTE
static double load;
static double filtred;
static long double tara  = 0;

double timerValue = 0;
int intervalo;
long contador = 0;
char endChar = '!';
Filter FMM(8, 0.01);
Filter FFMM(16, 0.5);


static double posicao = 0;
static long iniTimer=0;
SerialInterpreter Mensagem;

void setup() {

  Serial.begin(115200);
  
  pinMode(pulso, OUTPUT);
  pinMode(direcao, OUTPUT);
  pinMode (enMotor, OUTPUT);
  digitalWrite(pulso,0);
  digitalWrite(direcao,0);
  digitalWrite(enMotor,0);

#ifdef WITH_ADC_HX711
  pinMode(ADSK, OUTPUT);
  pinMode(ADDO,INPUT_PULLUP);
  digitalWrite(ADSK, 0);
#endif


  pinMode(LI,INPUT_PULLUP);
  pinMode(LS,INPUT_PULLUP);

  TCCR1A = 0;

  
  TCCR1B |=  (1 << CS10);
  TCCR1B |=  (1 << CS11);
  TCCR1B &= ~(1 << CS12);

  TCNT1 = 0;
  OCR1A = 0;

  #ifdef WITH_ADC_CS5530
        myADC.Init();
        delay(1000);
  #endif
  while (!Serial);
  
  tara=0;
  tarar(100);

  

  TCCR3A = 0;
  TCCR3B = 0;
  
  
  TCCR3B &=  ~(1 << CS30);
  TCCR3B &=  ~(1 << CS31);
  TCCR3B |= (1 << CS32);
  
  OCR3A=624;
  TCNT3 = 0;
  
  TIMSK3 |= (1 << OCIE3A);
  
}


void loop() {
  if (Serial.available()) {    
    TIMSK3 &= ~(1 << OCIE3A);
    
    String mens = Serial.readStringUntil(endChar);
    Mensagem.AtMensagem(mens);
    executaComando(Mensagem);
    mens +=endChar;
    Serial.print(mens);
    
    TIMSK3 |= (1 << OCIE3A);
   }

  /*if (!(PIND & (1 << LS))) {
    TIMSK1 &= ~(1 << OCIE1A);
    PORTB &= ~(1 << pulso);
    PORTB |= (1 << enMotor);

    //Serial.println("LS");
  }
  if (!(PIND & (1 << LI))) {
    TIMSK1 &= ~(1 << OCIE1A);
    PORTB &= ~(1 << pulso);
    PORTB |= (1 << enMotor);
    //Serial.println("LI");
  }*/

  if(!digitalRead(LS)){
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso,0);
    digitalWrite(enMotor,1);
  }

  if(!digitalRead(LI)){
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso,0);
    digitalWrite(enMotor,1);
  }
  
}

void atualizaMotor(double vel) {
  if (vel > 0.001 || vel < -0.001) {
    TIMSK1 &= ~(1 << OCIE1A);
    
    timerValue = (passo * 1000) / (ppr * (vel > 0 ? vel : -vel) * 2);
    intervalo = (int)(timerValue * 250);
    OCR1A = intervalo;
    TCNT1 = OCR1A - 1;
    TIMSK1 |= (1 << OCIE1A);

    if (vel > 0) digitalWrite(direcao,1); //PORTB |= (1 << direcao);
    if (vel < 0) digitalWrite(direcao,0); //PORTB &= ~(1 << direcao);
    //PORTB &= ~(1 << enMotor);
    digitalWrite(enMotor,0);

  } else {
    intervalo = (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 250);
    OCR1A = intervalo;
  }
}

ISR(TIMER1_COMPA_vect)
{
  TCNT1 = 0;
  if (OCR1A >= (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 250)) {
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso,0);
    digitalWrite(enMotor,1);
    //PORTB &= ~(1 << pulso);
    //PORTB |= (1 << enMotor);
  } else {
    digitalWrite(pulso, digitalRead(pulso)^1); //PORTB ^= (1 << pulso);
    //if (!(PINB & (1 << pulso))) contador += PORTB & (1 << direcao) ? 1 : -1;
    #ifdef WITHOUT_ENCODER
      if (!digitalRead(pulso))contador += digitalRead(direcao)?1:-1;
      posicao = ((double)contador/ppr)*passo;
    #endif
  }
}

ISR(TIMER3_COMPA_vect) {
  TCNT3 = 0;
  envMens();
}

void executaComando(SerialInterpreter com) {
  switch (com.lenght) {
    case 1 :
      if (com.Comando == "TARA"){
        tarar(100);
      }
      if (com.Comando == "INITIME"){
        iniTimer=millis();
      }
      if (com.Comando == "ZEROMAQ"){
        posicao=0;
      }
      String S ="["+com.Comando+"]"+endChar;
        Serial.print(S);
      break;
    case 2 :
      if (com.Comando == "CAL") {
        double cal = calibrar(com.Valor);
        String mens ="[LCC;";
        mens+=String(cal,8);
        mens+="]";
        mens+=endChar;
        Serial.print(mens);
      }
      break;
    case 3:
      if (com.Comando == "M") atualizaMotor((com.Modo == "UP" ? 1 : -1)*com.Valor);
      break;
    default:
      break;
  }
}

#ifdef WITH_ADC_CS5530
  long double ReadLoad(){
    long valor;
    myADC.LePeso((long *)&valor);
  
    return (((long double)valor) / scale) - tara;
  }

#endif

#ifdef WITH_ADC_HX711
  long double ReadLoad(){
    unsigned long Count = 0;
  
    digitalWrite(ADSK,0); //PORTB &= ~(1 << ADSK);
    
    while (digitalRead(ADDO));//(PINB & (1 << ADDO)));
  
    for (char i = 0; i < 24; i++){
      digitalWrite(ADSK,1);//PORTB |= (1 << ADSK);
      Count = Count << 1;
      digitalWrite(ADSK,0);//PORTB &= ~(1 << ADSK);
      if(digitalRead(ADDO))Count++; //if (PINB & (1 << ADDO)) Count++;
    }
  
    digitalWrite(ADSK,1); //PORTB |= (1 << ADSK);
    Count = Count ^ 0x800000;
    digitalWrite(ADSK,0);//PORTB &= ~(1 << ADSK);
  
    return (((double)Count) / scale) - tara;
  }
#endif

static double calibrar(double val, char n = 15) {
  long double cal = 0;
  for (int i = 0; i < n; i++){
    delay(20);
    cal += (ReadLoad() / n);
  }

  scale *= cal / val;
  tarar(100);
  tara -= val;
  return scale;
}

void tarar(char n=10) {
  long double _tara = 0;
  for (int i = 0; i < n; i++) {
    delay(20);
    _tara += (ReadLoad() / n);
  }
  tara += _tara;
}

void envMens(){
  String bufferText = "";
  
  load = ReadLoad();
  filtred = FFMM.filtrar(FMM.filtrar(load));
  double difTimer = (double)(millis()-iniTimer)/1000;
  bufferText +="[L;";
  bufferText += String (filtred,1);
  if(iniTimer>0){
    bufferText+=";";
    bufferText += String (difTimer,3);
  }

  bufferText += "]";
  bufferText += endChar;

  bufferText += "[E;";
  bufferText += String (posicao,1);
  if (iniTimer>0){
    bufferText+=";";
    bufferText += String (difTimer,3); 
  }
  
  bufferText += "]";
  bufferText += endChar;
  
  Serial.print(bufferText);
}

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
