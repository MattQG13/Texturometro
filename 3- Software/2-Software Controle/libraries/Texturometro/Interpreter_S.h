#ifndef SerialInterpreter_h
#define SerialInterpreter_h

#include  <Arduino.h>


class SerialInterpreter{
    public:
        SerialInterpreter();
        String Comando;
        String Modo;
        double Valor;
        int lenght;
        void AtMensagem(String mensagem);

    private:
        static const int maxTokens = 10;
        int maxTokenLength = 50;
        char delimiter = ';';   
        String input;
        String tokens[maxTokens];
        int splitString(const String &inputString, char delimiter);
        void removeChar(String &str, char charToRemove);
};



#endif