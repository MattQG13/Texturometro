using System;
using ClassesSuporteTexturometro;

namespace LoadCellTexturometro {
	public class LoadCell {

        public double CargaMax;
        private double _val;
        private const double _g = 9.81;
        private double _valDeteccao;
        private bool _deteccaoDeCarga = false;
        private bool _targetedLoad = false;
        private double _targetLoad = 0;
        private bool _maiorque = true;
        public event EventHandler ZeroSet;
        public event EventHandler LoadReached;
        public event EventHandler CargaDetected;
        public EventHandler<SerialMessageArgument> Calibration;

        public LoadCell(double valorMax) {
            CargaMax=valorMax;
        }

        public LoadCell() {
        }

        public double Scale { get; set; }

        public double ValorLoad {
            get {
                return _val;
            }
            set {
                _val=(double)value;
                if(_maiorque?(_val>_valDeteccao):(_val<_valDeteccao)&&_deteccaoDeCarga) {
                    _deteccaoDeCarga=false;
                    CargaDetected?.Invoke(this,EventArgs.Empty);
                }
                if(_val>=_targetLoad&&_targetedLoad) {
                    _targetedLoad=false;
                    LoadReached?.Invoke(this,EventArgs.Empty);
                }
            }
        }



        public double cargaKg {
            get {
                return _val/1000d;
            }
        }

        public double cargaG {
            get {
                return _val;
            }
        }

        public double cargaN {
            get {
                return (cargaKg*_g);
            }
        }

        public void Tarar() {
            ZeroSet?.Invoke(this,EventArgs.Empty);
        }

        public void Cal(double loadCal) {
            SerialMessageArgument args = new SerialMessageArgument();
            args.doubleValue=loadCal;
            Calibration?.Invoke(this,args);
        }


        public void TargetLoad(double cargaAlvo = double.NaN) {
            if(cargaAlvo!=0||cargaAlvo!=double.NaN) {
                _targetedLoad=true;
                _targetLoad=cargaAlvo;
            } else {
                _targetedLoad=false;
                _targetLoad=0;
            }
        }

        public void DetectLoad(double cargaDeteccao = double.NaN,bool maiorque = true) {
            if(cargaDeteccao!=0||cargaDeteccao!=double.NaN) {
                _maiorque=maiorque;
                _valDeteccao=cargaDeteccao;
                _deteccaoDeCarga=true;
            } else {
                _deteccaoDeCarga=false;
                _valDeteccao=0;
            }
        }
    }

}