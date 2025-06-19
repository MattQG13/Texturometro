using ClassesSuporteTexturometro;
using System;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SerialManagerTexturometro{
    public class SerialManager {
        private SerialPort _serialPort;

        public EventHandler<SerialMessageArgument> MessageRecieved;
        public EventHandler<SerialMessageArgument> MessageInterpreted;
        public EventHandler<SerialMessageArgument> LSDetected;
        public EventHandler<SerialMessageArgument> LIDetected;
        public EventHandler<SerialMessageArgument> UPDetected;
        public EventHandler<SerialMessageArgument> DNDetected;
        public EventHandler<SerialMessageArgument> STOPDetected;
        public EventHandler<SerialMessageArgument> LoadCellDetected;
        public EventHandler<SerialMessageArgument> EncoderDetected;
        public EventHandler<SerialMessageArgument> MotorDetected;
        public EventHandler<SerialMessageArgument> TimeSeted;
        public EventHandler<SerialMessageArgument> ZeroSeated;
        public EventHandler<SerialMessageArgument> LoadCalibrated;
        public EventHandler<SerialMessageArgument> VelDetected;
        public EventHandler<SerialMessageArgument> WarningDetected;

        private CultureInfo culture = new CultureInfo("en-US"); //CultureInfo.InvariantCulture;
        private char endChar = '!';

        public SerialManager(string portName,int baudRate) {
            _serialPort=new SerialPort(portName,baudRate);
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;
        }

        public SerialManager(string portName) {
            _serialPort=new SerialPort(portName,115200);
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;

        }

        public SerialManager() {
            _serialPort=new SerialPort();
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;
        }

        #region SerialPort

        public void SetCOM (string portName,int baudRate) {
            if(!string.IsNullOrEmpty(portName))_serialPort.PortName=portName;
                _serialPort.BaudRate=baudRate;
        }
        public void SetCOM(string portName) {
            if(!string.IsNullOrEmpty(portName))_serialPort.PortName=portName;
        }

        public void SetCOM(int baudRate) {
                _serialPort.BaudRate=baudRate;
        }

        public bool IsOpen {
            get {
                return _serialPort.IsOpen;
            }
        }

        public bool DiscardNull {
            get {
                return _serialPort.DiscardNull;
            }
            set {
                _serialPort.DiscardNull=value;
            }
        }


        public async void Open() {
            if(!string.IsNullOrEmpty(_serialPort.PortName)) {
                try {
                    _serialPort.Open();
                    _serialPort.DiscardInBuffer();
                    while(!_serialPort.IsOpen) { }
                    } catch(Exception ex) {
                    MessageBox.Show("Erro de conexão com texturômetro!","ERRO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        public void Close() {
            /*LSDetected=null;
            LIDetected=null;
            LoadCellDetected=null;
            EncoderDetected=null;
            MotorDetected=null;
            MessageInterpreted=null;*/
            _serialPort.Close();
        }

        public void DiscardInBuffer() {
            if(IsOpen) {
                _serialPort.DiscardInBuffer();
            }
        }

        public void Write(string message) {
            if (IsOpen)
            {
                _serialPort.Write(message+endChar.ToString(culture));
            }
        }

        private void _dataReceived(object sender,SerialDataReceivedEventArgs e) {
            if(_serialPort.IsOpen)
                while(_serialPort.BytesToRead>0) {
                try {
                    string mensagem = _serialPort.ReadTo("!");
                    MessageRecieved?.Invoke(this,new SerialMessageArgument() {stringValue =mensagem });
                    string[] partesDaMensagem = _processaSerial(mensagem);
                    _interpretaMensagem(partesDaMensagem);
                } catch(TimeoutException) { _serialPort.Close(); }
            }
        }

        #endregion

        private static string[] _processaSerial(string mensagem) {
            mensagem=mensagem.Replace("[",string.Empty).Replace("]",string.Empty);
            string[] partes = mensagem.Split(';');
            return partes;
        }

        private void _interpretaMensagem(string[] partesDaMensagem) {
            try {
                SerialMessageArgument args = new SerialMessageArgument();
                args.Objeto=partesDaMensagem[0];

                switch(partesDaMensagem.Length) {
                    case 2: //Load cell ou Encoder
                        if(args.Objeto=="LS"||args.Objeto=="LI") {
                            args.boolValue=partesDaMensagem[1]=="1" ? true : false;
                        }
                        if(args.Objeto=="UP"||args.Objeto=="DN") {
                            args.boolValue=partesDaMensagem[1]=="1" ? true : false;
                        }
                        if(args.Objeto=="STOP") {
                            args.boolValue=partesDaMensagem[1]=="1" ? true : false;
                        }
                        if(args.Objeto=="E") {
                            args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                        }
                        if(args.Objeto=="CAL") {
                            args.doubleValue=double.Parse(partesDaMensagem[1],culture);
                        }
                        if(args.Objeto=="L") {
                            args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                        }
                        if(args.Objeto=="LCC") {
                            args.doubleValue=double.Parse(partesDaMensagem[1],culture);
                        }
                        if(args.Objeto=="V") {
                            args.doubleValue=double.Parse(partesDaMensagem[1],culture);
                        }
                        if(args.Objeto=="W") {
                            switch(partesDaMensagem[1]) {
                                case "O":
                                    args.stringValue="Limite de carga atingido!";
                                    break;
                                case "S":
                                    args.stringValue= "Botão de emergência foi acionado!";
                                    break;
                                default: 
                                    break;
                            }                        
                        }
                        break;
                    case 3: //Motor
                        if(args.Objeto=="M") {
                            args.Comando=partesDaMensagem[1];
                            args.doubleValue=double.Parse(partesDaMensagem[2],culture);
                        }

                        if(args.Objeto=="L") {
                            args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                            args.doubleValue2=double.Parse(partesDaMensagem[2],culture);
                        }
                        if(args.Objeto=="E") {
                            args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                            args.doubleValue2=double.Parse(partesDaMensagem[2],culture);
                        }
                        break;
                    default:
                        break;
                }

                MessageInterpreted?.Invoke(this,args);

                switch(args.Objeto) {
                    case "LS":
                        LSDetected?.Invoke(this,args);
                        break;
                    case "LI":
                        LIDetected?.Invoke(this,args);
                        break;
                    case "STOP":
                        STOPDetected?.Invoke(this,args);
                        break;
                    case "UP":
                        UPDetected?.Invoke(this,args);
                        break;
                    case "DN":
                        DNDetected?.Invoke(this,args);
                        break;
                    case "L":
                        LoadCellDetected?.Invoke(this,args);
                        break;
                    case "E":
                        EncoderDetected?.Invoke(this,args);
                        break;
                    case "M":
                        MotorDetected?.Invoke(this,args);
                        break;
                    case "INITIME":
                        TimeSeted?.Invoke(this,args);
                        break;
                    case "ZERO":
                        ZeroSeated?.Invoke(this,args);
                        break;
                    case "LCC":
                        LoadCalibrated?.Invoke(this,args);
                        break;
                    case "V":
                        VelDetected?.Invoke(this,args);
                        break;
                    case "W":
                        WarningDetected?.Invoke(this,args);
                        break;
                }
            } catch (Exception ex) {
            }
        }

        #region Envia_Mensagens
        public void EnvComandoMotor(ModoMotor comando,double vel=0) {
            StringBuilder sB = new StringBuilder("");
            sB.Append("[M;");
            switch(comando) {
                case ModoMotor.Parado:
                    sB.Append("S;");
                    break;
                case ModoMotor.Subir:
                    sB.Append("UP;");
                    break;
                case ModoMotor.Descer:
                    sB.Append("DN;");
                    break;
            }
            sB.Append(vel.ToString(culture));
            sB.Append("]");

            Write(sB.ToString());
        }

        public void EnvComandoMotor(ModoMotor comando,double vel,double finalPosition) {
            StringBuilder sB = new StringBuilder("");
            sB.Append("[M;");
            switch(comando) {
                case ModoMotor.Parado:
                    sB.Append("S;");
                    break;
                case ModoMotor.Subir:
                    sB.Append("UP;");
                    break;
                case ModoMotor.Descer:
                    sB.Append("DN;");
                    break;
            }
            sB.Append(vel.ToString(culture));
            sB.Append(";");
            sB.Append(finalPosition.ToString(culture));
            sB.Append("]");

            for (int i=0;i<3;i++)
                Write(sB.ToString());
        }

        public void CalLC(object sender,SerialMessageArgument e) {
            string s = "[CAL;"+e.doubleValue.ToString(culture).Replace(",",string.Empty)+"]";
            Write(s);
        }

        public void EnvZeroMaquina(object sender,SerialMessageArgument e) {
            string s = "[ZERAR;"+e.doubleValue1.ToString(culture).Replace(",",string.Empty)+";"+e.doubleValue2.ToString(culture).Replace(",",string.Empty)+"]";
            Write(s);
        }

        public void EnvTARA(object sender,EventArgs e) {
            string s = "[TARA]";
            Write(s);
        }

        public void EnvZeroTime(object sender, EventArgs e) {
            string s = "[INITIME]";
            for (int i=0;i<3;i++)
               Write(s);
        }

        public void EnvCalibration(double Cal) {
            string s = "[LCC;"+Cal.ToString("N8",culture).Replace(",",string.Empty)+"]";
            for(int i = 0;i<3;i++) 
                Write(s);
        }

        public void EnvCalibration(object sender,SerialMessageArgument e) {
            string s = "[LCC;"+e.doubleValue.ToString("N8",culture).Replace(",",string.Empty)+"]";
            for(int i = 0;i<3;i++)
                Write(s);
        }
        #endregion
    }
}