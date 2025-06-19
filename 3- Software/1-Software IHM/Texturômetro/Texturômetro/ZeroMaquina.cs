using ClassesSuporteTexturometro;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexturometroClass;

namespace Texturometer {
    public partial class ZeroMaquina : Form {
        private Texturometro tex;
        private bool Z = false; 
        private bool zerando {
            get {
                return Z;
            }
            set {
                Z= value;
                if (value) {
                    btnCancel2.Enabled=false;
                    btnFinsh.Enabled=false; 
                }
                else{
                    btnZerar.Enabled=true;
                    btnCancel2.Enabled=true;
                    btnFinsh.Enabled=true;
                }
            }
        }

        public ZeroMaquina(Texturometro tex) {
            InitializeComponent();
            this.tex=tex;

            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;
            tex.Serial.ZeroSeated+=goToFinalPosition;
            btnFinsh.Enabled = false;

        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }

        private void btnNext_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
            tabs.SelectedIndex++;
        }

        private void btnOk_Click(object sender,EventArgs e) {
            tex.Motor.ZerarPosicao(0.5,Convert.ToDouble(txbCargaLimite.Text.Trim()));
            zerando =true;
        }

        private void goToFinalPosition(object sender,SerialMessageArgument e) {
            try {
                Thread.Sleep(100);
                tex.Motor.GoTo(ModoMotor.Subir,Convert.ToDouble(txbVelociadeZero.Text),Convert.ToDouble(txbFinalPosition.Text));
                tex.Motor.ZeroSeated=true;
            } finally { }
            Task.Run(() => {
                this.Invoke(new Action(() => {
                    zerando=false;
                    //this.Close();
                }));
            });
        }

        private void number_KeyPress(object sender,KeyPressEventArgs e) {
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')&&!((sender as TextBox).Text.Length>1)) {
                e.Handled=true;
            }
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')) {
                e.Handled=true;
            }
            if(e.KeyChar==','&&((sender as TextBox).Text.Length==0||(sender as TextBox).SelectionStart==0)) {
                e.Handled=true;
            }
            if((e.KeyChar==',')&&((sender as TextBox).Text.IndexOf(',')>-1)) {
                e.Handled=true;
            }
        }

        private void ZeroMaquina_FormClosing(object sender,FormClosingEventArgs e) {
            if(zerando) {
                e.Cancel=true;
            } else {
                tex.Serial.ZeroSeated-=goToFinalPosition;
            }
        }

        private void btnFinsh_Click(object sender,EventArgs e) {
            this.Close();
        }
        
 
    }
}
