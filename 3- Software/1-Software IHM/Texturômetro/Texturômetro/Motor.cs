using ClassesSuporteTexturometro;
using System;

namespace MotorTexturometro {
	public class Motor {
        private double _SPVel;
        private ModoMotor _modoOper;

        public event EventHandler SPVelChanged;
        public  EventHandler<MotorArgument> MotorStarted;
        public  EventHandler<MotorArgument> MotorStopped;
        public  EventHandler<SerialMessageArgument> ZeroSeating;
        public  EventHandler<MotorArgument> MotorGoTo;
		public bool ZeroSeated = false;

        public double SPVel {
			get {
				return _SPVel;
			}
			set {
				_SPVel = value;
                SPVelChanged?.Invoke(this,EventArgs.Empty);
            }
		}

		public Motor() {
			ModoMotor=ModoMotor.Parado;
		}

		public ModoMotor ModoMotor {
			get {
				return _modoOper;
			}
			set {
				_modoOper = value;
			}
		}

        public void Start(ModoMotor modo) {
			_modoOper = modo;
            MotorArgument args = new MotorArgument();
            args.Modo =_modoOper;
			args.Vel=_SPVel;
            MotorStarted?.Invoke(this,args);
        }

        public void Start(ModoMotor modo,double Vel) {
            _modoOper=modo;
            _SPVel= Vel;
            MotorArgument args = new MotorArgument();
            args.Modo=_modoOper;
            args.Vel=_SPVel;
            MotorStarted?.Invoke(this,args);
        }

		public void Stop() {
            _modoOper=ModoMotor.Parado;

            MotorArgument args = new MotorArgument();
            args.Modo=_modoOper;
            args.Vel=0;
            MotorStopped?.Invoke(this,args);
		}

        public void GoTo(ModoMotor modo, double Vel, double finalposition) {
            _modoOper=modo;
            _SPVel=Vel;
            MotorArgument args = new MotorArgument();
            args.Modo=_modoOper;
            args.Vel=_SPVel;
            args.FinalPosition=finalposition;
            MotorGoTo?.Invoke(this,args);
        }
        public void ZerarPosicao(double vel,double carga) { 
			SerialMessageArgument args = new SerialMessageArgument();
			args.doubleValue1 = vel;
			args.doubleValue2 = carga;
			ZeroSeating?.Invoke(this, args);
		}


    }

}

