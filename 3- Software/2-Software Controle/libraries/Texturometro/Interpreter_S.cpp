#include "Interpreter_S.h"
#include <Arduino.h>



SerialInterpreter::SerialInterpreter (){
    
}

void SerialInterpreter::AtMensagem(String mensagem) {
    removeChar(mensagem,']');
    removeChar(mensagem,'[');
    int numTokens = splitString(mensagem, delimiter);
    lenght = numTokens;

    switch(numTokens){
        case 1:
            Comando = tokens[0];
            break;
        case 2:
            Comando = tokens[0];
            Valor = tokens[1].toDouble();
            break;
        case 3:
            Comando = tokens[0];
            Modo = tokens[1];
            Valor = tokens[2].toDouble();
            break;
        default:   
            break;
    }
    
}

int SerialInterpreter::splitString(const String &inputString, char delimiter) {
    int currentIndex = 0;
    int arrayIndex = 0;
    int inputLength = inputString.length();
    String token = "";

    for (int i = 0; i < inputLength; i++) {
        char currentChar = inputString.charAt(i);
        
        if (currentChar != delimiter) {
            token += currentChar;
        } else {
            if (arrayIndex < maxTokens) {
                tokens[arrayIndex] = token; 
                arrayIndex++;
                token = "";
            }
        }
    }

    if (token != "" && arrayIndex < maxTokens) {
        tokens[arrayIndex] = token;
        arrayIndex++;
    }

    return arrayIndex; 
}

void SerialInterpreter::removeChar(String &str, char charToRemove) {
    int length = str.length();
    int currentIndex = 0;

    for (int i = 0; i < length; i++) {
        if (str[i] != charToRemove) {
            str[currentIndex++] = str[i];
        }
    }

    str[currentIndex] = '\0'; 
}