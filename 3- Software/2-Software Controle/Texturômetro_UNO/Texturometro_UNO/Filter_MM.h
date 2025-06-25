#ifndef FilterADC
#define FilterADC
#include "QueueArray.h"
#include <Arduino.h>



class Filter{
    public:
        Filter (int _amostras, double _resolucao);
        double filtrarMMA(double valor);
        double filtrarMMP(double valor);

    private: 
        int amostras ;
        double resolucao;
        QueueArray<double> valores;
        long double sum;
        int tamanhoFila;
        double div8[8] = {0.25,0.225,0.15,0.125,0.1,0.075,0.05,0.025};
        double div16[16] = {0.15,0.13,0.11,0.09,0.08,0.075,0.07,0.06,0.055,0.05,0.04,0.025,0.025,0.02,0.01,0.01};


};

#endif
