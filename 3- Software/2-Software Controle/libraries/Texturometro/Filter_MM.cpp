#include "Filter_MM.h"
#include <Arduino.h>

Filter::Filter (int _amostras, double _resolucao){
    amostras = _amostras;
    resolucao = _resolucao;
    sum = 0;
    tamanhoFila = 0;
}

double Filter:: filtrar(double valor) {
    if (tamanhoFila >= amostras) {
        sum -= valores.pop();
        tamanhoFila--;
    }
    
    valores.push(valor);
    sum += valor;
    tamanhoFila++;
    
    double mediaMovel = sum / amostras;
    double res = static_cast<int>(mediaMovel / resolucao) * resolucao;
    
    return res;
}