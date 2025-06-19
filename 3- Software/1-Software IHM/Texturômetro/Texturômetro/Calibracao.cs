using LoadCellTexturometro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexturometroClass;
using ClassesSuporteTexturometro;

namespace Texturometer {
    public partial class Calibracao : Form {

        Texturometro tex; 
        public Calibracao(Texturometro tex) {
            this.tex = tex;
            InitializeComponent();
            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;
        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }

        private void btnNext_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
            tabs.SelectedIndex++;
        }

        private void btnReset_Click(object sender,EventArgs e) {
            tabs.SelectedIndex++;
        }
   
        private void btnOk_Click(object sender,EventArgs e) {
            try {
               if(txCal.Text!=string.Empty&&Convert.ToDouble(txCal.Text!=string.Empty ? txCal.Text : "0")!=0){
                    tex.LoadCell.Cal(Convert.ToDouble(txCal.Text!=string.Empty ? txCal.Text : "0"));
                    this.Close();
                } else {
                    MessageBox.Show("Digite valor maior que 0","Entrada errada de dados!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            } finally { }
        }

        private void btnOk2_Click(object sender,EventArgs e) {
            try {
                if(txScale.Text!=string.Empty&&Convert.ToDouble(txScale.Text!=string.Empty ? txScale.Text : "0")!=0) {
                    double lcc = Convert.ToDouble(txScale.Text!=string.Empty ? txScale.Text : "0");

                    Properties.Settings.Default.CalLoadCell=lcc;
                    Properties.Settings.Default.Save();

                    tex.Serial.EnvCalibration(lcc!=0 ? lcc : -144.7722018); //Verificar
                    this.Close();
                } else {
                    MessageBox.Show("Digite valor maior que 0","Entrada errada de dados!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            } finally { }
        }

        private void txCal_KeyPress(object sender,KeyPressEventArgs e) {
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

        private void txScale_KeyPress(object sender,KeyPressEventArgs e) {
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')&&!((sender as TextBox).Text.Length>1)&&(e.KeyChar!='-')) {
                e.Handled=true;
            }
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')&&(e.KeyChar!='-')) {
                e.Handled=true;
            }

            if(e.KeyChar==','&&((sender as TextBox).Text.Length==0||(sender as TextBox).SelectionStart==0)) {
                e.Handled=true;
            }
            if((e.KeyChar==',')&&((sender as TextBox).Text.IndexOf(',')>-1)) {
                e.Handled=true;
            }

            if(e.KeyChar=='-'&&((sender as TextBox).Text.Length!=0&&(sender as TextBox).SelectionStart!=0)) {
                e.Handled=true;
            }
            if((e.KeyChar=='-')&&((sender as TextBox).Text.IndexOf('-')>-1)) {
                e.Handled=true;
            }

        }

    }
}
