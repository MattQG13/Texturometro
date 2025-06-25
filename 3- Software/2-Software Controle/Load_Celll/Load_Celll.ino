#include <Filter_MM.h>

#define  ADDO  3
#define  ADSK  2

double ReadCount();

double convert;
double filtred;
double scale = 260.8771886;  //ESCALA PARA AJUSTE
double tara = 0;

Filter FMM(5,0.1);

void setup()
{
  pinMode(ADDO, INPUT_PULLUP);
  pinMode(ADSK, OUTPUT);

  Serial.begin(57600);

  double tara1 = 0;
  for (int i = 0; i < 30; i++) {
    tara1 += (ReadCount()/30);
  }
  tara = tara1;
}

void loop()
{
  String bufferLC = "";
  for (int i = 0; i < 1; i++) {
    convert = ReadCount();
    filtred = FMM.filtrar(convert);

    bufferLC += convert;
    bufferLC += ",";
    bufferLC += filtred;
    bufferLC += "\n";
  }
  
  Serial.print(bufferLC);
}

double ReadCount()
{
  unsigned long Count = 0;
  unsigned char i;

  digitalWrite(ADSK, LOW);

  while (digitalRead(ADDO));

  for (i = 0; i < 24; i++)
  {
    digitalWrite(ADSK, HIGH);
    Count = Count << 1;
    digitalWrite(ADSK, LOW);
    if (digitalRead(ADDO)) Count++;

  }

  digitalWrite(ADSK, HIGH);
  Count = Count ^ 0x800000;
  digitalWrite(ADSK, LOW);

  return (((double)Count) / scale) - tara;
}
