using System;
using System.Drawing;
using System.Windows.Forms;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using ProbeTexturometro;

namespace Texturometer {
    public partial class ConfiguracaoEnsaio : Form {

        public DataTest DadosDeEnsaio { get; private set; }
        public ConfiguracaoEnsaio() {
            InitializeComponent();
            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;

            DadosDeEnsaio = new DataTest();
        }

        private void ConfiguracaoEnsaio_Load(object sender,EventArgs e) {
            cbTipo.SelectedIndex = 0;
            cbTarget.SelectedIndex=0;
            cbTrigger.SelectedIndex=0;
            cbTara.SelectedIndex=0;
            cbProbe.SelectedIndex=0;

            txNome.Text=DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString().PadLeft(2,'0')+DateTime.Now.Day.ToString().PadLeft(2,'0')+DateTime.Now.Hour.ToString().PadLeft(2,'0')+DateTime.Now.Minute.ToString().PadLeft(2,'0')+DateTime.Now.Second.ToString().PadLeft(2,'0');
        }

        private void btnIniciar_Click(object sender,EventArgs e) {
            if(capturaDados()) {
                DialogResult=DialogResult.OK;
                this.Close();
            } else {
                MessageBox.Show("Por favor, preencha todos os dados","Erro de preenchimento",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void txb_KeyPress(object sender,KeyPressEventArgs e) {
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

        private void cb_SelectedIndexChanged(object sender,EventArgs e) {

            switch(cbTipo.SelectedItem) {
                case "TPA":
                    txTime.Enabled=true;
                    break;
                case "Compressão":
                    txTime.Enabled=false;
                    break;
                case "Tração":
                    txTime.Enabled=false;
                    break;
            }
            switch(cbTarget.SelectedItem){
                case "Distância":
                    lbTarget.Text="Distância:..........................";
                    lbUnTarget.Text="mm"; 
                    break;
                case "Força":
                    lbTarget.Text="Força:...............................";
                    lbUnTarget.Text="gf";
                    break;
                case "Deformação":
                    lbTarget.Text="Deformação:.....................";
                    lbUnTarget.Text="%";
                    break;
                default:
                    break;
            }
            switch(cbTrigger.SelectedItem) {
                case "Distância":
                    lbTrigger.Text="Distância de detecção:.....";
                    lbUnTrigger.Text="mm";
                    break;
                case "Auto (Força)":
                    lbTrigger.Text="Força de detecção...........";
                    lbUnTrigger.Text="gf";
                    break;
                default:
                    break;
            }
            switch(cbProbe.SelectedItem) {
                case "Cilíndrica":
                    lbDim1.Text="Diâmetro:..........................";
                    lbDim2.Visible=false;
                    lbUnDim2.Visible=false;
                    txDim2.Visible=false;
                    txDim2.Enabled=false;
                    break;
                case "Retangular":
                    lbDim1.Text="Largura:............................";
                    lbDim2.Visible=true;
                    lbUnDim2.Visible=true;
                    txDim2.Visible=true;
                    txDim2.Enabled=true;

                    break;
                default : 
                    break;
            }


        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }

        private bool capturaDados() {
            if(Preenchido(this)) {
                DadosDeEnsaio.Tipo=cbTipo.Text=="TPA" ? TipoDeTeste.TPA : cbTipo.Text=="Compressão" ? TipoDeTeste.Compressao : cbTipo.Text=="Tração" ? TipoDeTeste.Tracao : TipoDeTeste.CicloDuploTracao;
                DadosDeEnsaio.VelPreTeste=Convert.ToDouble(txVelPreTeste.Text);
                DadosDeEnsaio.VelTeste=Convert.ToDouble(txVel.Text);
                DadosDeEnsaio.VelPosTeste=Convert.ToDouble(txVelPosTeste.Text);
                DadosDeEnsaio.TipoLimite=cbTarget.Text=="Distância" ? TipoTarget.Distancia : cbTarget.Text=="Deformação" ? TipoTarget.Deformacao : TipoTarget.Forca;

                DadosDeEnsaio.ValorLimite=DadosDeEnsaio.TipoLimite==TipoTarget.Deformacao ? Convert.ToDouble(txTarget.Text)/100 : Convert.ToDouble(txTarget.Text);
                DadosDeEnsaio.Tempo=Convert.ToDouble(txTime.Text);
                DadosDeEnsaio.TipoDeteccao=cbTrigger.Text=="Auto (Força)" ? TipoTrigger.Forca : TipoTrigger.Distancia;
                DadosDeEnsaio.ValorDeteccao=Convert.ToDouble(txTrigger.Text);
                DadosDeEnsaio.TipoTara=cbTara.Text=="Auto" ? TipoTara.Auto : TipoTara.Manual;
                if(cbProbe.SelectedItem.ToString()=="Cilíndrica") {
                    DadosDeEnsaio.PontaDeTeste=ProbeFactoryMethod.ProbeCreate(Convert.ToDouble(txDim1.Text));
                } else if(cbProbe.SelectedItem.ToString()=="Retangular") {
                    ProbeFactoryMethod.ProbeCreate(Convert.ToDouble(txDim1.Text),Convert.ToDouble(txDim2.Text));
                }
                DadosDeEnsaio.Nome=txNome.Text;
                DadosDeEnsaio.DataHora=DateTime.Now;
                return true;
            }
            return false;
        }

        private bool Preenchido(Control container) {
            foreach(Control control in container.Controls) {
                if(control is TextBox textBox) {
                    if(string.IsNullOrWhiteSpace(textBox.Text)&&textBox.Enabled) {
                        return false;
                    }
                } else if(control is ComboBox comboBox) {
                    if(string.IsNullOrWhiteSpace(comboBox.Text)) {
                        return false;
                    }
                } else if(control.HasChildren) {
                    if(!Preenchido(control)) {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
