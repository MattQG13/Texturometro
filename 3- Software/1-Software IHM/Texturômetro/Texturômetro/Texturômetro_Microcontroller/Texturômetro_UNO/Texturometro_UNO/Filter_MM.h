#ifndef FilterADC
#define FilterADC
#include "QueueArray.h"
#include <Arduino.h>



class Filter{
    public:
        Filter (int _amostras, double _resolucao);
        double filtrar(double valor);

    private: 
        int amostras ;
        double resolucao;
        QueueArray<double> valores;
        long double sum;
        int tamanhoFila;
};

#endif
