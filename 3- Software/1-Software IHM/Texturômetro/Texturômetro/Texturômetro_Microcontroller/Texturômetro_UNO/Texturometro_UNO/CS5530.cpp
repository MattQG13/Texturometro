#include "CS5530.h"

//-----------------------------------------------------------------------------------//
CS5530::CS5530(int MOSI, int MISO, int SCLK, int CSADC)
{
    //Variaveis privadas com a definição dos pinos da SPI emulada
    CSADC_PIN = CSADC;
    SCLK_PIN = SCLK;
    MOSI_PIN = MOSI;
    MISO_PIN = MISO;

    //Configuração do tipo de I/O (Entrada ou Saída)
    pinMode(CSADC_PIN, OUTPUT);
    pinMode(SCLK_PIN, OUTPUT);
    pinMode(MOSI_PIN, OUTPUT);
    pinMode(MISO_PIN, INPUT);
    
    //Definição do estado inicial dos pinos
    digitalWrite(CSADC_PIN, HIGH);
    digitalWrite(SCLK_PIN, LOW);
    digitalWrite(MOSI_PIN, LOW);
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::Init()
{
    unsigned int i;
    
    // Inicializa os ports 
    digitalWrite(CSADC_PIN, HIGH);
    digitalWrite(SCLK_PIN, LOW);
    digitalWrite(MOSI_PIN, LOW);
    delay(10);                           

    // Reset no chip do AD 
    digitalWrite(CSADC_PIN, LOW);
    digitalWrite(MOSI_PIN, HIGH);        

    for(i=0;i<127;i++) 
    {               
        digitalWrite(SCLK_PIN, HIGH);            
        digitalWrite(SCLK_PIN, LOW);     
    }
    
    digitalWrite(MOSI_PIN, LOW);        
    digitalWrite(SCLK_PIN, HIGH);               
    digitalWrite(SCLK_PIN, LOW);        
    digitalWrite(CSADC_PIN, HIGH);
    delay(10);                     

    // Força um reset no conversor AD 
    write_to_register( &reset_rs_bit_set );
    delay(200);                    

    write_to_register( &reset_rs_bit_clear );
    delay(10);                     // Espera 10ms
     
    // Altera Configuração    
    Config( WORD_RATE_DEFAULT, MODO_DEFAULT, VREF_DEFAULT );

    // Dispara conversao continua 
    digitalWrite(CSADC_PIN, LOW);
    transfer_byte(0xC0);              /* conversao continua */
    digitalWrite(CSADC_PIN, HIGH);;
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::Close()
{
  
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::Config(long WordRate, long UniBipolar, long VRef)
{
    TLongByte ConfigValues;
    TRegister ConfigRegister;
    
    ConfigValues.UInt32 = (WordRate << 11) | (UniBipolar << 10) | (VRef << 25);

    ConfigRegister.Command = 0x03;
    ConfigRegister.Byte3 = ConfigValues.Bytes[3];
    ConfigRegister.Byte2 = ConfigValues.Bytes[2];
    ConfigRegister.Byte1 = ConfigValues.Bytes[1];
    ConfigRegister.Byte0 = ConfigValues.Bytes[0];
    
    write_to_register(&ConfigRegister);  
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
int CS5530::LePeso(long *RawCount)
{
    TLongByte ValorLido;
    TRegister ConversionRegister;
    long TempoInicial;
    char AdcFlags;

    ValorLido.UInt32 = 0;
    //TempoInicial = systimer_now();
    
    // Aguarda o término da conversão 
    digitalWrite(SCLK_PIN, LOW);          /* Posicao inicial */
    digitalWrite(MOSI_PIN, LOW);          /* Mantem SDI = 0 */
    digitalWrite(CSADC_PIN, LOW);

    while(digitalRead(MISO_PIN)!=0)    /* Wait for conversion to Complete */
    {
        //if( systimer_elapsed(TempoInicial) >= ADC_TIMEOUT_LEITURA )
        //{
        //    return ADC_FALHA_NAO_RESPONDE;
        //}
    }

    /* Conversão pronto, agora é só ler */
    ConversionRegister.Command = 0x00;
    read_register( &ConversionRegister );
    AdcFlags = ConversionRegister.Byte0;
    ValorLido.Bytes[2] = ConversionRegister.Byte3;
    ValorLido.Bytes[1] = ConversionRegister.Byte2;
    ValorLido.Bytes[0] = ConversionRegister.Byte1;

    if( ConversionRegister.Byte3 & 0x80 )
        ValorLido.Bytes[3] = 0xff;
    else
        ValorLido.Bytes[3] = 0;
    
    /* Acerta o valor lido aplicando um valor de divisão e um offset */
   // ValorLido.UInt32 = (ValorLido.UInt32/ADC_FATOR_DIVISAO) + ADC_OFFSET;

    /* Verifica se a conversão obtida é válida. Se o bit OF estiver em '1', 
       esta conversão é inválida */
    if( (AdcFlags & 0x04) || (ValorLido.UInt32 <= 0) )
    {
        return ADC_FALHA_OUT_OF_RANGE;
    }
    
    /* Leitura obtida com sucesso, salva o valor e retorna */   
    //*RawCount = (ValorLido.UInt32)/2;
    *RawCount = ValorLido.UInt32;
    return ADC_SUCESSO;  
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::write_to_register( TRegister * Register )
{
    digitalWrite(CSADC_PIN, LOW);
    transfer_byte(Register->Command);
    transfer_byte(Register->Byte3);         /* Transfer bytes high byte first */
    transfer_byte(Register->Byte2);
    transfer_byte(Register->Byte1);
    transfer_byte(Register->Byte0);
    digitalWrite(CSADC_PIN, HIGH);  
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::read_register( TRegister * Register )
{
    digitalWrite(CSADC_PIN, LOW);
    transfer_byte(Register->Command);            /*Send Command*/
    Register->Byte3 = receive_byte();            /* Receive_ Bytes High byte first */
    Register->Byte2 = receive_byte();
    Register->Byte1 = receive_byte();
    Register->Byte0 = receive_byte();
    digitalWrite(CSADC_PIN, HIGH);
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
void CS5530::transfer_byte(unsigned char data)
{
    unsigned char i;
    
    digitalWrite(SCLK_PIN, LOW);;

    for (i=0;i<8;i++)
    {       
        if(((data&0x80)>> 7) == 0)
        {
            digitalWrite(MOSI_PIN, LOW); 
        }
        else
        {
            digitalWrite(MOSI_PIN, HIGH); 
        }
        data = data<<1;      
                
        digitalWrite(SCLK_PIN, HIGH);        
        digitalWrite(SCLK_PIN, LOW);
    }  
}
//-----------------------------------------------------------------------------------//

//-----------------------------------------------------------------------------------//
unsigned char CS5530::receive_byte(void)
{
    unsigned char i;
    unsigned char recebido;

    digitalWrite(MOSI_PIN, LOW);
    recebido = 0;

    for (i=0;i<8;i++)
    {
        recebido = recebido << 1;
        recebido |= digitalRead(MISO_PIN);    /* Armazena o bit recebido */
        
        digitalWrite(SCLK_PIN, HIGH);         /* Pulsa o clock para receber o bit */         
        digitalWrite(SCLK_PIN, LOW);
    }
    return recebido;  
}
//-----------------------------------------------------------------------------------//
