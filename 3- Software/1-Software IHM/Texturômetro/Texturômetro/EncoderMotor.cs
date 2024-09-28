using System;
using System.Drawing.Printing;

namespace EncoderMotor {
    public class Encoder {
        private double _position = 0;
        private double _targetPosition=0;
        private bool _targetedPositional = false;
        private bool _direcaoTarget = false;
        public event EventHandler positionChanged;
        public event EventHandler positionReached;

        public double Position {
            get {
                return _position;
            }
            set {
                _position = value;
                if(_direcaoTarget?(_targetPosition<=_position):(_targetPosition>=_position)&&_targetedPositional) {
                    _targetedPositional=false;
                    positionReached?.Invoke(this,EventArgs.Empty);
                }
                positionChanged?.Invoke(this,EventArgs.Empty);
            }
        }

        public Encoder() {

        }

        public void TargetPosition(double posicaoAlvo=double.NaN,double posicaoAtual=double.NaN) {
            if(posicaoAlvo!=0||posicaoAlvo!=double.NaN) {
                _targetedPositional=true;
                _targetPosition = posicaoAlvo;
                if(posicaoAtual<posicaoAlvo) {
                    _direcaoTarget=true;
                } else {
                    _direcaoTarget=false;
                }
            } else {
                _targetedPositional=false;
                _targetPosition=0;
            }
        }
    }
}