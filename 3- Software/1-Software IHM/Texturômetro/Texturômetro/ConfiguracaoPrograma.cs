using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;

namespace Texturometer {
    public partial class ConfiguracaoPrograma : Form {
        bool salvo = true;
        TexturometroForms ft;

        public ConfiguracaoPrograma(TexturometroForms ft) {
            InitializeComponent();
            this.ft=ft;
        }

        private void ConfiguracaoPrograma_Load(object sender,EventArgs e) {
            addPortsCb();

            try {
                string[] ports = SerialPort.GetPortNames();
                int index = Array.IndexOf(ports,Properties.Settings.Default.PortaCOM);
                cbCOM.SelectedIndex=index; 
            } finally{ }

            cbBaud.SelectedItem=Properties.Settings.Default.Baudrate.ToString();
            cbDistancia.SelectedItem=Properties.Settings.Default.UnDistance.ToString();
            cbForca.SelectedItem=Properties.Settings.Default.UnForce.ToString();
            cbTempo.SelectedItem=Properties.Settings.Default.UnTime.ToString();
            txbVelMan.Text = Properties.Settings.Default.VelManual.ToString();
            txbVelManRapida.Text=Properties.Settings.Default.VelManualRapida.ToString();
            salvo=true;
        }

        private void addPortsCb() {
            string[] ports = SerialPort.GetPortNames();
            string[] names = SerialPort.GetPortNames();

            for(int i = 0;i<ports.Length;i++) {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%("+ports[i]+")%'");

                foreach(ManagementObject device in searcher.Get()) {
                    names[i]=device["Name"].ToString();
                }
            }

            for(int i = 0;i<ports.Length;i++) {
                cbCOM.Items.Add(names[i]);
            }
        }
        private string nomePorta(string port) {
            string[] ports = SerialPort.GetPortNames();
            string[] names = SerialPort.GetPortNames();

            for(int i = 0;i<ports.Length;i++) {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%("+ports[i]+")%'");
                
                foreach(ManagementObject device in searcher.Get()) {
                    names[i]=device["Name"].ToString();
                }
            }

            for(int i = 0;i<names.Length;i++) {
                if(names[i]==port) {
                    return ports[i];
                }
            }
            return null;
        }

        private void ChangedConfig (object sender,EventArgs e) {
            salvo = false;
        }

        private void ConfiguracaoPrograma_FormClosing(object sender,FormClosingEventArgs e) {
            if(salvo==false) {
                var result = MessageBox.Show("Salvar alterações?","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                if(result==DialogResult.Cancel) { e.Cancel=true; }
                if(result==DialogResult.Yes) { e.Cancel=true; btnSalvar.PerformClick(); }
            }
        }

        private void btnSalvar_Click(object sender,EventArgs e) {

            Properties.Settings.Default.PortaCOM=nomePorta(cbCOM.Text);
            Properties.Settings.Default.Baudrate=Convert.ToInt32(cbBaud.Text);
            Properties.Settings.Default.UnTime = cbTempo.Text;
            Properties.Settings.Default.UnForce = cbForca.Text;
            Properties.Settings.Default.UnDistance = cbDistancia.Text;
            Properties.Settings.Default.VelManual = Convert.ToDouble(txbVelMan.Text);
            Properties.Settings.Default.VelManualRapida = Convert.ToDouble(txbVelManRapida.Text);

            Properties.Settings.Default.Save();
            salvo=true;
            this.Close();
            ft.reconfigura();
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

    }
}
