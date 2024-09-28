#include "Filter_MM.h"

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
    
    valores.push(valor/amostras);
    sum += valor/amostras   ;
    tamanhoFila++;
    
    double mediaMovel = sum;
    double res = round(mediaMovel / resolucao) * resolucao;
    
    return res;
}