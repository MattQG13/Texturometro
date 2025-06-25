#include "Filter_MM.h"

Filter::Filter (int _amostras, double _resolucao){
    amostras = _amostras;
    resolucao = _resolucao;
    sum = 0;
    tamanhoFila = 0;
}

double Filter:: filtrarMMA(double valor) {
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

double Filter:: filtrarMMP(double valor) {
  if(amostras==8||amostras==16){
    if (tamanhoFila >= amostras) {
        valores.pop();
        tamanhoFila--;
    }
    
    valores.push(valor);
    tamanhoFila++;
    double mediaMovel=0;
    double sum = 0;
    
    if(amostras==8){
      for (int i = 0; i<amostras;i++){
        mediaMovel += valores.contents[i]*div8[7-i];
      }
    }
    sum = 0;
    if(amostras==16){
      for (int i = 0; i<amostras;i++){
        mediaMovel += valores.contents[i]*div16[15-i];
      }
    }
       
    double res = round(mediaMovel / resolucao) * resolucao;
    
    return res;
  }
    return filtrarMMA(valor);
  
}
