# Texturômetro

* Nome do Projeto: Titan Texture
* Data de Início: 01/07/2023
* Descrição: Software de análise de textura desenvolvido para o projeto de TCC do curso de Engenharia de Controle e Automação

```XXXXXXXXXXXXXXXXXXXXK00kxxkkOKKXXNNNNNNNNNNNNNNNNNWWWWWW
XXXXXXXXXXXXXKK0xd:,''......'';cox0NNNNNNNNNNNNNNNNWWWWW
XXXXXXXXXXXKOxl;.                ..'cxXNNNNNNNNNNNNNWWWW
XXXXXXXXKkl,.                      ...'cOXNNNNNNNNNNNNNW
XXXXXXKd'       ..                    ...;kXNNNNNNNNNNNN
XXXXKO;         ,'                         ,kXNNNNNNNNNN
XXXKx.  ..      .:,.                         :KNNNNNNNNN
XXKd            .,:.                          .xXNNNNNNN
XKd      .      .:c'.                          .xXNNNNNN
Kk.             .;:'...      ...                 dXNNNNN
O'           ...'ckx:;..      .                   kXNNNN
o         ....'cxKNNX0kxxxxxxxdc,'..              ,KXNNN
'         .,co0NMMMMMMMMMMMMMMMMMWNXKkl,.          oXXNN
         'ckOKWMMMMMMMMMMMMMMMMMMMMMMMWNNNx'       .0XXN
       'lkKXKWMMMMMMMMMMMMMMMMMMMMMMMMMMWWW0l.      :XNN
.     lONWWNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKo.   .,.OXN
d.   :OXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNK: .dXkolXN
Ox,  :00XWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWNk.oWWOo:0X
0Ok: ,OKNWWWWWMMMMMMMMMMMMMMMMMMMMMMMMMWWNNWWNKcxXWNx;dX
00ldo:KNWMWWWWMMMMMMMWMMWMMMMMMMWNWNNNNXK0KNWWNd:okN0.,K
00OkOo0000KXK0KWWMWMNKXMWX000OOOxddOKXNNNWWWMWNko0d0l .O
000OOoOOkxO0K0xooxo::cONWN0OOx:,l;..ck0XWMMMMWXOldOO.  d
000xxlOXXXKkd:..,o;:oxXWMMWWWMKOOOOOKXMMMMMMMWXOdkKl   c
00OdccOWMWWKkOkkOkxOXXNMMMMMMMMMWWWWMMMMMMMMMWX0KKd.   :
OOdlo.oNMMMMMWWWWWMMWNWMMMMMMMMMMMMMMMMMMMMMMWXo;,     '
koxxx. 0WMMMMMMMMMMMWWWMMMMMMMMMMMMMMMMMMMMMMWKc .     .
xxdxx. :XWMMMMMMMMWNNNNWMMMMMMWNK0XWMMMMMMMMWNK; .
kxoxd.  kNWMMMMMWXO0NNNWMMMMMMMMWXOxOXNWWWNNXK0' .
kxdoo.  '0KXNNNXkcdKNMWWMMMMMMWWMMWXkooxO00KKXO. .
xdxxo.   'xkkkxo;o0XXXWMMMMMNNWWMMWWWXOdoOXXXK:    .
xoxdl. .  .ooo:;oO0KK0KK0KXNXNMMMWWNNNXKkXNXXx
occ:'      .dkkxddk0KKXNNWWWWNXXOOcc0NWKKXK0k.    .
             lO0kkOxlcxxxkOkOOoOxOKWWWWNK0kx.  . ..
              .lk0KKXK0KOO0O0KNNWWNNNNNX0Ox.   . ..
                .d00OOKNWMMMWWNXK0KXNNKOOd.      . .
                  .d00OxxxxkkOO0KNNWNKOd,.      ....
              .     .dKXNNWWMMMMMMWXOd:. .       . .
     .       ..       .lkKKXXXXXKOxl:,.  .       . .
     .       ..         .;:cccc::;;;'.    .      ..
     .. ... ...          .';;,,,,'..      .       .
        ..    .         ...,:;'..         .
        ..   ..         .''';c:'..  ..   .
          .....           ':;cc;.        .
```

## Protocolo Serial

Padrões de protocolo: 

* __[L;carga;tempo]__ - Valor de leitura da célula de carga e tempo da leitura [g][s]
* __[E;posição;tempo]__ - Valor de posição do encoder e tempo da leitura[mm][s]
* __[LS;valor]__ - Valor do sensor de limite superior [bool] 
* __[LI;valor]__ - Valor do sensor de limite inferior [bool]

* __[UP;valor]__ - Valor do botão de subir [bool]
* __[DN;valor]__ - Valor do botão de descer [bool]
* __[STOP;valor]__ - Valor do botão de emergência [bool]

* __[M;comando;velocidade]__ - Controle do motor com direção e velocidade [mm/s]
* __[M;comando;velocidade;posição]__ - Controle do motor com direção, velocidade e posição final [mm/s][mm]
  * __UP__ para subir			
  * __DW__ para descer	
  * __S__ de Stop
* __[ZERAR;velocidade;carga]__ - Define velocidade e carga limite para rotira de Zero Máquina [mm/s][g]
* __[ZERO]__ - Define o Zero Máquina do Texturômetro
* __[INITIME]__ - Inicia envio de tempo a partir do envio do comando
* __[TARA]__ - Define a tara da célula de carga
* __[LCC;valor]__ - Define o valor para escala de calibração da célula de carga
* __[CAL;valor]__ - Envia comando para calibração da célula de carga com valor do peso padrão [g]
* __[W;tipo]__ - Define um alarme e o tipo
	* Tipos de alarme:
		* __O__ (Overweight) - Excesso de peso
    * __S__ (STOP) - Botão de Stop acionado 
    