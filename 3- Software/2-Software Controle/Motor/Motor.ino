
#define pulso PB2 //10
#define direcao PB3 //11
#define enMotor PB4 //12
#define LS PD7
#define LI PD6

const double passo = 5;
const double ppr = 200;
double timerValue = 0;
double VelLin;
int intervalo;
long contador = 0;

void setup()
{
  Serial.begin(9600);


  DDRB |= (1 << pulso) | (1 << direcao) | (1 << enMotor);
  PORTB &= ~(1 << pulso) | ~(1 << direcao) | ~(1 << enMotor);

  DDRD &= ~(1 << LS) | ~(1 << LI);
  PORTD |= (1 << LS) | (1 << LI);

  TCCR1A = 0;

  TCCR1B |=  (1 << CS10);
  TCCR1B &= ~(1 << CS11);
  TCCR1B |=  (1 << CS12);

  TCNT1 = 0;
  OCR1A = 0;
}

void loop()
{
  if (Serial.available()) {
    VelLin = Serial.readString().toDouble();
    if (VelLin > 0.001 || VelLin < -0.001) {

      timerValue = (passo * 1000) / (ppr * (VelLin > 0 ? VelLin : -VelLin) * 2);
      intervalo = (int)(timerValue * 15.625);
      OCR1A = intervalo;
      TIMSK1 |= (1 << OCIE1A);


      if (VelLin > 0) PORTB |= (1 << direcao);
      if (VelLin < 0) PORTB &= ~(1 << direcao);
      PORTB |= (1 << enMotor);

    } else {
      intervalo = (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 15.625);
      OCR1A = intervalo;
    }
  }

  if (!(PIND & (1 << LS))) {
    TIMSK1 &= ~(1 << OCIE1A);
    PORTB &= ~(1 << pulso);
    PORTB &= ~(1 << enMotor);
    Serial.println("LS");
  }
  if (!(PIND & (1 << LI))) {
    TIMSK1 &= ~(1 << OCIE1A);
    PORTB &= ~(1 << pulso);
    PORTB &= ~(1 << enMotor);
    Serial.println("LI");
  }
}

ISR(TIMER1_COMPA_vect)
{
        TCNT1 = 0;
  if (OCR1A >= (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 15.625)) {
    TIMSK1 &= ~(1 << OCIE1A);
    PORTB &= ~(1 << pulso);
    PORTB &= ~(1 << enMotor);
  } else {
    PORTB ^= (1 << pulso);
    if (!(PINB & (1 << pulso))) contador += PORTB & (1 << direcao) ? 1 : -1;
  }

}
