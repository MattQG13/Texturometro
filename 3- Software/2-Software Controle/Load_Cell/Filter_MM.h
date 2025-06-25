#ifndef FilterADC
#define FilterADC
#include "QueueArray.h"
#include <Arduino.h>



class Filter{
    public:
        Filter (int _amostras, double _resolucao);
        double filtrarMMA(double valor);
        double filtrarMMP(double valor);
        double filtrarMME(double valor);

    private: 
        int amostras ;
        double resolucao;
        QueueArray<double> valores;
        long double sum;
        int tamanhoFila;
        double div8[8] = {0.30,0.25,0.15,0.115,0.07,0.065,0.03,0.02};
        double div16[16] = {0.20,0.15,0.11,0.09,0.08,0.075,0.06,0.04,0.035,0.03,0.03,0.025,0.025,0.02,0.01,0.01};
        double MMEa_1 =0;

};

#endif
