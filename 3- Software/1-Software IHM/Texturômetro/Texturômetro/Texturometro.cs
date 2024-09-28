/*
* Nome do Arquivo: Classes.cs
* Autor: Mateus Quintino
* Data de Início: 01/07/2023
* Descrição: Este código possui classes para serem usadas
* no controlador do texturômetro desenvolvido para projeto de TCC
*/

/*
XXXXXXXXXXXXXXXXXXXXK00kxxkkOKKXXNNNNNNNNNNNNNNNNNWWWWWW
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
 */

using ClassesSuporteTexturometro;
using LoadCellTexturometro;
using MotorTexturometro;
using ProdutoTexturometro;
using SerialManagerTexturometro;
using System;
using System.Windows.Forms;
using EnsaioTextuometro;
using DadosDeEnsaio;
using EncoderMotor;
using System.Reflection;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Threading;

namespace TexturometroClass {
	public class Texturometro {
        public Motor Motor;
        public Encoder Encoder;
        public Chave SensorLS;
        public Chave SensorLI;
        public Ensaio Teste;
        public DataTest DadosTeste;
        public LoadCell LoadCell;
        public SerialManager Serial;
		public CorpoDeProva Produto;
		private static Timer _timer;
        private bool Warning = true;
        public EventHandler fimTeste;
        public event EventHandler ZerarTime;

        public bool testRunning = false;
        private object NextSender;


        public Texturometro() {
			LoadCell = new LoadCell();

            SensorLS=new Chave();
			SensorLI=new Chave();


            Serial=new SerialManager();

            Serial.LSDetected+=_atualizaLS;
			Serial.LIDetected+=_atualizaLI;
			Serial.LoadCellDetected+=_atualizaLoadCell;
			Serial.EncoderDetected+=_atualizaEncoder;
			Serial.TimeSeted+=StartAddResults;
            Serial.WarningDetected+=ShowWarning;

            Motor=new Motor();
			Motor.MotorStarted+=_enviaSerialMotor;
			Motor.MotorStopped+=_enviaSerialMotor;
            Motor.MotorGoTo+=_enviaSerialMotorGoTo;
			Motor.ZeroSeating+= Serial.EnvZeroMaquina;
            

            LoadCell.ZeroSet+=Serial.EnvTARA;
			LoadCell.Calibration+=Serial.CalLC;
			ZerarTime+=Serial.EnvZeroTime;
            
            Encoder=new Encoder();
            Produto = new CorpoDeProva();
            _timer = new Timer();
            _timer.Elapsed+=AutoStop;
            //_timer.Tick+=AutoStop;
            _timer.Enabled=true;
        }

        #region Controle_Teste
        public void TesteStart(DataTest DadosDoTeste) {
            DadosTeste = DadosDoTeste;    
            Teste = EnsaioFactoryMethod.criarTeste(DadosTeste.Tipo);
            Produto=new CorpoDeProva();
            if(DadosTeste.TipoTara==TipoTara.Auto) {
                LoadCell.Tarar();
                Thread.Sleep(1000);
            }
            testRunning=true;
            ExecTeste(this, new EventArgs());
		}

        public void TesteStop(){
            DadosTeste=null;
            Teste=null;
            StopAddResults();
            Produto.Resultado.Clear();
            testRunning=false;
            RemoveEvento(NextSender);
            fimTeste?.Invoke(this,new EventArgs());
        }

        private void ExecTeste(object sender,EventArgs args) {
            RemoveEvento(sender);
            Acao Action = Teste.AcaoAtual;
            Teste.Next();

            switch(Action) {

                case Acao.DescerPreTeste: //Desce pre teste até deteccao //Ok
                    if(Teste.DirecaoTeste) {
                        Motor.Start(ModoMotor.Descer,DadosTeste.VelPreTeste);
                        switch(DadosTeste.TipoDeteccao) {
                            case TipoTrigger.Forca:
                                LoadCell.DetectLoad(DadosTeste.ValorDeteccao);
                                LoadCell.CargaDetected+=ExecTeste;
                                NextSender=LoadCell;

                                break;
                            case TipoTrigger.Distancia:
                                Encoder.TargetPosition(DadosTeste.ValorDeteccao,Encoder.Position);
                                Encoder.positionReached+=ExecTeste;
                                NextSender=Encoder;
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case Acao.SubirPreTeste: //Sobe em velocidade de preteste //Ok
                    if(!Teste.DirecaoTeste) {
                        Motor.Start(ModoMotor.Subir,DadosTeste.VelPreTeste);

                        switch(DadosTeste.TipoDeteccao) {
                            case TipoTrigger.Forca:
                                LoadCell.DetectLoad(-DadosTeste.ValorDeteccao,false);
                                LoadCell.CargaDetected+=ExecTeste;
                                NextSender=LoadCell;
                                break;
                            case TipoTrigger.Distancia:
                                Encoder.TargetPosition(DadosTeste.ValorDeteccao,Encoder.Position);
                                Encoder.positionReached+=ExecTeste;
                                NextSender=Encoder;
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case Acao.DescerTeste:  //Desce para teste até limite //Ok Acho
                    Motor.Start(ModoMotor.Descer,DadosTeste.VelTeste);
                    if(Teste.DirecaoTeste) {
                        if(Produto.TamanhoOriginal==0) {
                            Produto.TamanhoOriginal=Encoder.Position;
                            Serial.EncoderDetected+=_atualizaTamanho;
                            ZeroTime();
                        } else if(Produto.TamanhoRecuperacao==0) {
                            Produto.TamanhoRecuperacao=Encoder.Position;
                        }

                        switch(DadosTeste.TipoLimite) {
                            case TipoTarget.Deformacao:
                                Produto.TargetDeformation(DadosTeste.ValorLimite);
                                Produto.DeformacaoReached+=ExecTeste;
                                NextSender=Produto;

                                break;
                            case TipoTarget.Distancia:
                                Encoder.TargetPosition(DadosTeste.ValorLimite,Encoder.Position);
                                Encoder.positionReached+=ExecTeste;
                                NextSender=Encoder;
                                break;
                            case TipoTarget.Forca:
                                LoadCell.TargetLoad(DadosTeste.ValorLimite);
                                LoadCell.LoadReached+=ExecTeste;
                                NextSender=LoadCell;
                                break;
                            default:
                                break;
                        }
                    } else {
                        Encoder.TargetPosition(Produto.TamanhoOriginal-0.2,Encoder.Position);
                        Encoder.positionReached+=ExecTeste;
                        NextSender=Encoder;
                    }
                    break;

                case Acao.SubirTeste: //Sobe em velocidade de teste  //Ok Acho
                    Motor.Start(ModoMotor.Subir,DadosTeste.VelTeste);

                    if(Teste.DirecaoTeste) {
                        Encoder.TargetPosition(Produto.TamanhoOriginal+0.2,Encoder.Position);
                        Encoder.positionReached+=ExecTeste;
                        NextSender=Encoder;
                    } else {
                        if(Produto.TamanhoOriginal==0) {
                            Produto.TamanhoOriginal=Encoder.Position;
                            Serial.EncoderDetected+=_atualizaTamanho;
                            ZeroTime();
                        } else if(Produto.TamanhoRecuperacao==0) {
                            Produto.TamanhoRecuperacao=Encoder.Position;
                        }

                        switch(DadosTeste.TipoLimite) {
                            case TipoTarget.Deformacao:
                                Produto.TargetDeformation(-DadosTeste.ValorLimite,false);
                                Produto.DeformacaoReached+=ExecTeste;
                                NextSender=Produto;
                                break;
                            case TipoTarget.Distancia:
                                Encoder.TargetPosition(DadosTeste.ValorLimite,Encoder.Position);
                                Encoder.positionReached+=ExecTeste;
                                NextSender=Encoder;
                                break;
                            case TipoTarget.Forca:
                                LoadCell.DetectLoad(-DadosTeste.ValorLimite,false);
                                LoadCell.LoadReached+=ExecTeste;
                                NextSender=LoadCell;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
               
                case Acao.EsperarAssentamento: //Aguarda tempo de assentamento // OK
                    Motor.Stop();
                    _timer.Interval=Convert.ToInt32(DadosTeste.Tempo*1000);
                    _timer.Elapsed+=ExecTeste;
                    NextSender=_timer;
                    _timer.Start();
                    break;
                case Acao.SubirPosTeste: // OK
                    Motor.Start(ModoMotor.Subir,DadosTeste.VelPosTeste);
                    Encoder.TargetPosition(DadosTeste.PosInicial,Encoder.Position);
                    Encoder.positionReached+=ExecTeste;
                    NextSender=Encoder;
                    break;
                case Acao.DescerPosTeste: // OK
                    Motor.Start(ModoMotor.Descer,DadosTeste.VelPosTeste);
                    Encoder.TargetPosition(DadosTeste.PosInicial,Encoder.Position);
                    Encoder.positionReached+=ExecTeste;
                    NextSender=Encoder;
                    break;
                case Acao.Fim: // OK
                    Motor.Stop();
                    testRunning=false;
                    fimTeste?.Invoke(this,EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region SetSensores
        private void _atualizaLS(object sender, SerialMessageArgument e) {
			SensorLS.Estado = e.boolValue;			
		}

		private void _atualizaLI(object sender,SerialMessageArgument e) {
            SensorLI.Estado=e.boolValue;
        }

		private void _atualizaLoadCell(object sender,SerialMessageArgument e) {
            LoadCell.ValorLoad=e.doubleValue1;
        }
        private void _atualizaEncoder(object sender,SerialMessageArgument e) {
            Encoder.Position=e.doubleValue1;
        }
        private void _atualizaTamanho(object sender,SerialMessageArgument e) {
            Produto.TamanhoAtual=e.doubleValue1;
        }
        #endregion

        public void ZeroTime() {
            ZerarTime?.Invoke(this,EventArgs.Empty);
        }

        public void StartAddResults(object sender,SerialMessageArgument e) {
            if(!ContainsHandler(Serial.LoadCellDetected,_atualizaResultadoL)&&!ContainsHandler(Serial.EncoderDetected,_atualizaResultadoE)) {
                Produto.Resultado.Clear();
                Serial.LoadCellDetected+=_atualizaResultadoL;
                Serial.EncoderDetected+=_atualizaResultadoE;
            }
        }

        public void StopAddResults() {
            for(int i = 0;i<10;i++)
                try {
                Serial.LoadCellDetected-=_atualizaResultadoL;
                Serial.EncoderDetected-=_atualizaResultadoE;
                
            } finally { }
        }

        public void setSerial(string com,int baud = 115200) {
			Serial.SetCOM(com,baud);
		}

        public void iniciaSerial() {
            try {
                if(Serial.IsOpen) {
                    Serial.Close();
                }
                Serial.Open();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void _atualizaResultadoL(object sender,SerialMessageArgument e) {
            if(Teste != null)
                Produto.Resultado.AddXZvalue(e.doubleValue1*(Teste.DirecaoTeste?1:-1),e.doubleValue2);
            else
                Produto.Resultado.AddXZvalue(e.doubleValue1,e.doubleValue2);
        }
        private void _atualizaResultadoE(object sender,SerialMessageArgument e) {
            Produto.Resultado.AddYZvalue(e.doubleValue1,e.doubleValue2);
        }

        private void _enviaSerialMotor(object sender, MotorArgument e) {
			Serial.EnvComandoMotor(e.Modo,e.Vel);
		}
        private void _enviaSerialMotorGoTo(object sender, MotorArgument e) {
            Serial.EnvComandoMotor(e.Modo,e.Vel,e.FinalPosition);
        }


        #region Funcoes_Especiais
        private void RemoveEvento(object sender) {
            EventInfo[] eventos = sender.GetType().GetEvents();
            foreach(EventInfo evento in eventos) {
                try {
                    if(evento.Name!="Elapsed") {
                        evento.RemoveEventHandler(sender,new EventHandler(ExecTeste));
                    } else {
                        evento.RemoveEventHandler(sender,new ElapsedEventHandler(ExecTeste));
                    }

                } finally { }
            }
        }

        private void AutoStop(object sender,EventArgs args) {
            _timer.Stop();
        }

        static bool ContainsHandler(EventHandler<SerialMessageArgument> eventDelegate,EventHandler<SerialMessageArgument> handler) {
            if(eventDelegate==null)
                return false;

            foreach(Delegate existingHandler in eventDelegate.GetInvocationList()) {
                if(existingHandler==handler) {
                    return true;
                }
            }

            return false;
        }

        public void ShowWarning(object sender, SerialMessageArgument args) {
            MessageBox.Show(args.stringValue,"Aviso!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            TesteStop();
        }

        #endregion
    }
}