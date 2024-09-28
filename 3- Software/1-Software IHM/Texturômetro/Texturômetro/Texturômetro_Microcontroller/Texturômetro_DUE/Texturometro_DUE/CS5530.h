#ifndef CS5530_H
#define CS5530_H

#include <stdio.h>
#include <string.h>
#include <Arduino.h>
#include <SPI.h>

/*
**  Defines de programação do ADC
*/
#define UNIPOLAR    1
#define BIPOLAR     0

#define RATE_7HALF_HZ   4
#define RATE_15_HZ      3
#define RATE_30_HZ      2
#define RATE_60_HZ      1
#define RATE_120_HZ     0
#define RATE_240_HZ     12
#define RATE_480_HZ     11
#define RATE_960_HZ     10
#define RATE_1920_HZ    9
#define RATE_3840_HZ    8

#define VRS_LESS_EQUAL_THAN_2_5V        0
#define VRS_GREATER_THAN_2_5V           1

/*
**      Configuração inicial do ADC
*/
#define MODO_DEFAULT        BIPOLAR
#define WORD_RATE_DEFAULT   RATE_120_HZ //RATE_7HALF_HZ
#define VREF_DEFAULT        VRS_LESS_EQUAL_THAN_2_5V

/*
**      Retorno da função LePeso()
*/
#define ADC_SUCESSO                 0
#define ADC_FALHA_NAO_RESPONDE      1
#define ADC_FALHA_OUT_OF_RANGE      2

/*
**      Timeout de leitura do ADC
*/
#define ADC_TIMEOUT_LEITURA         500         // Timeout de 500ms

/*
**      Offset para ser somado ao valor lido para evitar valores negativos
*/
#define ADC_OFFSET                  50000

/*
**      Fator de divisão para diminuir a ordem de grandeza do valor lido
**      Normalmente multiplo de 2 para gerar código otimizado
*/
#define ADC_FATOR_DIVISAO           8

typedef struct
{
    unsigned char   Command;
    unsigned char   Byte3;
    unsigned char   Byte2;
    unsigned char   Byte1;
    unsigned char   Byte0;
} TRegister;

typedef union
{
    unsigned char   Bytes[4];
    unsigned long   UInt32;
} TLongByte;

static TRegister reset_rs_bit_set     = {  0x03, 0x20, 0x00, 0x00 ,0x00 };
static TRegister reset_rs_bit_clear   = {  0x03, 0x00, 0x00, 0x00 ,0x00 };

class CS5530
{
public:
    CS5530(int MOSI, int MISO, int SCLK, int CSADC);
    void Init();
    void Close();
    void Config(long WordRate, long UniBipolar, long VRef);
    int LePeso(long *RawCount);
private:
    int CSADC_PIN;
    int SCLK_PIN;
    int MOSI_PIN;
    int MISO_PIN;
    void write_to_register( TRegister * Register );
    void read_register( TRegister * Register );
    void transfer_byte(unsigned char data);
    unsigned char receive_byte(void);
};

#endif
