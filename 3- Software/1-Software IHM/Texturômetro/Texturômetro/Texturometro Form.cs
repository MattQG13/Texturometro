using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TexturometroClass;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Linq;
using ExportacaoResultado;
using ProdutoTexturometro;
using System.Threading.Tasks;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;

namespace Texturometer {
    public partial class TexturometroForms : Form {

        static Texturometro tex = new Texturometro();
        Series series = new Series("Pontos");

        static Timer tick = new Timer();
        static BackgroundWorker bkWork = new BackgroundWorker();
        CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();

        Timer tmRampa = new Timer();
        static double velRampa = 0;

        public bool salvo = true;

        public TexturometroForms() {
            InitializeComponent();
            tick.Interval =16;
            tick.Tick +=new EventHandler(atGraph);
            tick.Enabled=true;
            bkWork.DoWork+= bkWork_DoWork;
            bkWork.WorkerSupportsCancellation = true;

            typeof(Chart).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });

            typeof(Label).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });

#if DEBUG
            tmRampa.Interval=100;
            tmRampa.Enabled=true;
            tmRampa.Tick+=new EventHandler(Rampa);
            tmRampa.Stop();
#endif

        }

        private void Texturometro_Load(object sender,EventArgs e) {

            tex.setSerial(Properties.Settings.Default.PortaCOM,Properties.Settings.Default.Baudrate);

            series.ChartType=SeriesChartType.Line;
            series.BorderWidth = 2;

            Graph.Series.Clear();
            Graph.Series.Add(series);

            #region Estetica
            Series s = new Series("0");
            s.ChartType=SeriesChartType.Spline;
            List<double> ly = new List<double>();
            List<double> lx = new List<double>();

            ly.Add(0);
            ly.Add(3);
            lx.Add(0);
            lx.Add(0.01);

            s.Points.DataBindXY(lx,ly);
            Graph.Series.Add(s);
            Graph.Update();
            #endregion

            Graph.MouseMove+=Graph_MouseMove;
            Graph.ChartAreas[0].Position.X=0;
            Graph.ChartAreas[0].Position.Y = 0;
            Graph.ChartAreas[0].Position.Width=100;
            Graph.ChartAreas[0].Position.Height=100;
            tick.Start();

            tex.iniciaSerial();

            double lcc = Properties.Settings.Default.CalLoadCell;
            tex.Serial.EnvCalibration(lcc!=0?lcc:108); //Verificar

            tex.Serial.LoadCellDetected+=atualizaLbLoad;
            tex.Serial.EncoderDetected+=atualizaLbPosition;
            tex.Serial.LoadCalibrated+=LoadCelCalibrated;
            tex.Serial.UPDetected+=_UPdetected;
            tex.Serial.DNDetected+=_DNdetected;
            tex.Serial.STOPDetected+=_STOPdetected;

            Thread.Sleep(100);
            #if DEBUG
            tex.ZeroTime();
            #endif

            tex.Serial.DiscardInBuffer();
            tex.Produto.Resultado.Clear();

            tex.fimTeste+=execFimTeste;

            #if DEBUG
            tex.Serial.VelDetected+=atualizaLbVel;
            tex.Serial.MessageRecieved+=EscreveMensTxb;
            #endif

        }

        #region Botoes_Exportacao

        private void ToolStripMenuExportCSV_Click(object sender,EventArgs e) {
            //CorpoDeProva cp = Dados.getCP();
            //tex.Produto.Resultado=cp.Resultado;
            if(tex.Produto.Resultado.Count!=0) {
                if(ExportacaoCSV.exportarCSV(tex.Produto))salvo=true;


            } else {
                MessageBox.Show("Não há resultados para serem exportados","Erro de exportação",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void ToolStripMenuExportExcel_Click(object sender,EventArgs e) {
            //CorpoDeProva cp = Dados.getCP();
            //tex.Produto.Resultado=cp.Resultado;
            if(tex.Produto.Resultado.Count!=0) {
                if(ExportacaoExcel.exportarExcel(tex.Produto,tex.DadosTeste)) salvo=true;
            } else {
                MessageBox.Show("Não há resultados para serem exportados","Erro de exportação",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void ToolStripMenuExportPDF_Click(object sender,EventArgs e) {

            //tex.Produto.Resultado=cp.Resultado;
            if(tex.Produto.Resultado.Count!=0) {
                if(ExportacaoRelatorioPDF.exportaPDF(tex.Produto,tex.DadosTeste,getImgGrafico(panelGraph))) salvo=true;
                //tex.testRunning=false;
                //execFimTeste(this,EventArgs.Empty);
            } else {
                MessageBox.Show("Não há resultados para serem exportados","Erro de exportação",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Botoes_Controle_TA

        private void rodarTesteToolStripMenuItem_Click(object sender,EventArgs e) {
            if(tex.Produto.Resultado.Count!=0&&!salvo) {
                var res = MessageBox.Show("Há resultados não salvos, deseja descartá-los?","Resultados não salvos",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                if(res==DialogResult.Cancel) {
                    return;
                }
            }
#if DEBUG
            if(true) {
#else
            if(tex.Motor.ZeroSeated) {
#endif
                ConfiguracaoEnsaio ConfigEnsaio = new ConfiguracaoEnsaio();

                var DialogResult = ConfigEnsaio.ShowDialog();

                if(DialogResult==DialogResult.OK&&ConfigEnsaio.DadosDeEnsaio!=null) {

                    ConfigEnsaio.DadosDeEnsaio.PosInicial=tex.Encoder.Position;
                    tex.TesteStart(ConfigEnsaio.DadosDeEnsaio);
                    AtInfoLabel(ConfigEnsaio.DadosDeEnsaio);
                  // controlsEnabled(false);
                }
                ConfigEnsaio.Dispose();
            } else {
                var result = MessageBox.Show("Zero Máquina não definido!\nDefinir Zero Máquina?","Aviso!",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                if(result==DialogResult.OK) {

                    zeroMáquinaToolStripMenuItem_Click(this,e);
                }
            }
        }

        private void calibrarToolStripMenuItem_Click(object sender,EventArgs e) {
            Calibracao FCal = new Calibracao(tex);
            FCal.ShowDialog();
        }

        private void tararToolStripMenuItem_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
        }

        private void zeroMáquinaToolStripMenuItem_Click(object sender,EventArgs e) {
            ZeroMaquina zm = new ZeroMaquina(tex);
            zm.ShowDialog();
        }
#endregion

        #region Botoes_Controle_JOG
        private void btnUP_Click(object sender,EventArgs e) {
            if(!tex.testRunning)
                tex.Motor.Start(ModoMotor.Subir,Properties.Settings.Default.VelManual);
        }
        private void btnDN_Click(object sender,EventArgs e) {
            if(!tex.testRunning)
                tex.Motor.Start(ModoMotor.Descer,Properties.Settings.Default.VelManual);
        }
        private void btnStop_Click(object sender,EventArgs e) {
            if(tex.testRunning) {
                tex.TesteStop();
            }
            tex.Motor.Stop();
            
        }
        private void btnFast_Click(object sender,EventArgs e) {
            if(!tex.testRunning)
                switch(tex.Motor.ModoMotor) {
                case ModoMotor.Subir:
                    tex.Motor.Start(ModoMotor.Subir,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Descer:
                    tex.Motor.Start(ModoMotor.Descer,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Parado:
                    tex.Motor.Stop();
                    break;
            }
        }
        #endregion

        #region Botoes_Funcao
        private void configuracoesToolStripMenuItem_Click(object sender,EventArgs e) {
            ConfiguracaoPrograma confP = new ConfiguracaoPrograma(this);
            confP.ShowDialog();
        }

        private void sobreToolStripMenuItem_Click(object sender,EventArgs e) {
            Sobre sb = new Sobre();
            sb.ShowDialog();

        }
        #endregion

        private void TexturometroForms_FormClosing(object sender,FormClosingEventArgs e) {
            if(tex.Produto.Resultado.Count!=0&&!salvo) {
                var res =  MessageBox.Show("Há resultados não salvos, deseja descartá-los?","Resultados não salvos",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                if(res==DialogResult.Cancel) {
                    e.Cancel = true;
                    return;
                }
            }

            try {
                tex.Motor.Stop();
                tex.Serial.LoadCellDetected=null;
                tex.Serial.EncoderDetected=null;
                tex.Serial.MotorDetected=null;
                tex.Serial.MessageRecieved=null;

                tick.Stop();
                bkWork.CancelAsync();

                if(tex.Serial.IsOpen) {
                    tex.Serial.DiscardInBuffer();
                    tex.Serial.Close();
                }
            } finally { }
        }

        #region Controle_Grafico
        private void atGraph(object sender,EventArgs e) {
            if(!bkWork.IsBusy) {
                bkWork.RunWorkerAsync();
            }
        }

        private void bkWork_DoWork(object sender,DoWorkEventArgs e) {
            if(InvokeRequired) {
                this.Invoke(new Action(() => {
                    GC.Collect();
                    Graph.SuspendLayout();
                   // if(tex.Produto.Resultado.Count!=0) {
                        try {
                            series.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetXvalues());

                        } finally { }

                        if(Graph.ChartAreas[0].AxisX.Maximum<=100&&tex.testRunning) {
                            var tempo = tex.Produto.Resultado.GetZvalues();
                            if(tempo.Count!=0) {
                                var lTempo = tempo.Last();
                                if(lTempo>=100) {
                                    Graph.ChartAreas[0].AxisX.Maximum=Double.NaN;
                                    Graph.ChartAreas[0].RecalculateAxesScale();
                                } else {
                                    Graph.ChartAreas[0].AxisX.Maximum=100;
                                    Graph.ChartAreas[0].RecalculateAxesScale();
                                }
                            } else {
                                Graph.ChartAreas[0].AxisX.Maximum=100;
                                Graph.ChartAreas[0].RecalculateAxesScale();
                            }
                        }
                  //  }

                    Graph.Invalidate();
                    Graph.ResumeLayout();
                }));
            }
        }

        private void Graph_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {

            try {

                if(Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X)>Graph.ChartAreas[0].AxisX.Minimum&&
                   Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X)<Graph.ChartAreas[0].AxisX.Maximum&&
                   Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y)>Graph.ChartAreas[0].AxisY.Minimum&&
                   Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y)<Graph.ChartAreas[0].AxisY.Maximum) {

                    double mouseX = Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    double mouseY = Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    lxy.Visible=true;

                    Graph.ChartAreas[0].CursorX.Position=mouseX;
                    Graph.ChartAreas[0].CursorY.Position=mouseY;

                    lxy.Text=$"x:{mouseX:F1}  y:{mouseY:F1}";
                    lxy.Location=new Point(e.X+5,e.Y-20);

                } else {
                    lxy.Visible=false;
                    Graph.ChartAreas[0].CursorX.Position=double.NaN;
                    Graph.ChartAreas[0].CursorY.Position=double.NaN;
                }
            } finally {
                Graph.Update();
            }
        }

        private void Graph_MouseLeave(object sender,EventArgs e) {
            lxy.Visible=false;
            Graph.ChartAreas[0].CursorX.Position=double.NaN;
            Graph.ChartAreas[0].CursorY.Position=double.NaN;
            Graph.Update();

        }

        private static Image getImgGrafico(Panel panel) {
            Image img;

            Bitmap bitmap = new Bitmap(panel.Width,panel.Height);
            panel.DrawToBitmap(bitmap,new Rectangle(0,0,panel.Width,panel.Height));

            using(Graphics g = Graphics.FromImage(bitmap)) {
                foreach(Control control in panel.Controls) {
                    if(control!=null) {
                        using(Bitmap controlBitmap = new Bitmap(control.Width,control.Height)) {
                            control.DrawToBitmap(controlBitmap,new Rectangle(Point.Empty,control.Size));
                            g.DrawImage(controlBitmap,control.Location);
                        }
                    }
                }
            }

            img=bitmap;

            return img;
        }
        #endregion
        
        #region Atualizacao_Labels
        private void atualizaLbLoad(object sender,SerialMessageArgument e) {
            if(lbLoad.InvokeRequired) {
                lbLoad.BeginInvoke((MethodInvoker)delegate {
                    lbLoad.Text=e.doubleValue1.ToString()+" gf";
                });
            } else {
                lbLoad.Text=e.doubleValue1.ToString()+" gf";
            }
        }

        private void atualizaLbPosition(object sender,SerialMessageArgument e) {
            if(lbPosition.InvokeRequired) {
                lbPosition.BeginInvoke((MethodInvoker)delegate {
                    lbPosition.Text=e.doubleValue1.ToString()+" mm";
                });
            } else {
                lbPosition.Text=e.doubleValue1.ToString()+" mm";
            }
        }
#if DEBUG //------------------------------------
        public void atualizaLbVel(object sender,SerialMessageArgument e) {
            if(lbVel.InvokeRequired) {
                lbVel.BeginInvoke((MethodInvoker)delegate {
                    lbVel.Text=e.doubleValue.ToString()+" mm";
                });
            } else {
                lbVel.Text=e.doubleValue.ToString()+" mm";
            }
        }
#endif
#endregion

        #region Funcoes_Especiais
        protected override bool ProcessCmdKey(ref Message msg,Keys keyData) {
            Key key = KeyInterop.KeyFromVirtualKey((int)keyData);
            KeyEventArgs e = new KeyEventArgs(keyData);

            if(key>0) {
                if(Keyboard.IsKeyDown(key)) {
                }
                if(Keyboard.IsKeyUp(key)) {
                }
            }

            return base.ProcessCmdKey(ref msg,keyData);
        }

        public void controlsEnabled(bool enabled) {
            btnDN.Enabled = enabled;
            btnUP.Enabled = enabled;
            btnFast.Enabled = enabled;
            configuracoesToolStripMenuItem.Enabled = enabled;
            TAStripMenu.Enabled= enabled;
            arquivoStripMenu.Enabled= enabled;
        }


        public void reconfigura() {
            try {
                tex.Serial.LoadCellDetected-=atualizaLbLoad;
                tex.Serial.EncoderDetected-=atualizaLbPosition;

                cancelTokenSrc.Cancel();
                tick.Stop();
                bkWork.CancelAsync();

                if(tex.Serial.IsOpen) {
                    tex.Serial.DiscardInBuffer();
                    tex.Serial.Close();
                }
            } finally { }

            try {
                tex.setSerial(Properties.Settings.Default.PortaCOM,Properties.Settings.Default.Baudrate);
            } finally { }
            tex.Serial.LoadCellDetected+=atualizaLbLoad;
            tex.Serial.EncoderDetected+=atualizaLbPosition;

            cancelTokenSrc=new CancellationTokenSource();
            tex.iniciaSerial();
            tick.Start();

        }
        
        private void LoadCelCalibrated(object sender, SerialMessageArgument args) {
            Properties.Settings.Default.CalLoadCell=args.doubleValue;
            Properties.Settings.Default.Save();
        }
        private void _UPdetected(object sender,SerialMessageArgument args) {
            if(args.boolValue) {
                if(!tex.testRunning)
                    tex.Motor.Start(ModoMotor.Subir,Properties.Settings.Default.VelManual);
            } else {
                if(tex.testRunning) {
                    tex.TesteStop();
                }
                tex.Motor.Stop();
            }
        }

        private void _DNdetected(object sender,SerialMessageArgument args) {
            if(args.boolValue) {
                if(!tex.testRunning)
                tex.Motor.Start(ModoMotor.Descer,Properties.Settings.Default.VelManual);
            } else {
                if(tex.testRunning) {
                    tex.TesteStop();
                }
                tex.Motor.Stop();
            }
            
        }
        private void _STOPdetected(object sender,SerialMessageArgument args) {
            if(args.boolValue) {
                if(tex.testRunning) {
                    tex.TesteStop();
                }
                tex.Motor.Stop();
            }
        }
        private void execFimTeste(object sender,EventArgs e) {
            tex.StopAddResults();

            var tb = tex.Produto.Resultado;

            if(tb.Count!=0) {
                salvo=false;
                Task.Run(() => {
                    this.Invoke(new Action(() => {
                        Graph.ChartAreas[0].AxisX.Maximum=Math.Ceiling(tex.Produto.Resultado.GetZvalues().Last());
                        Graph.ChartAreas[0].RecalculateAxesScale();
                    }));
                });

                if(tex.DadosTeste.Tipo==TipoDeTeste.TPA) {

                    ResultadosTPA res = ResultadosTPA.CalcTPA(tb,5); //OU OUTRO VALOR ARBITRÁRIO 

                    Task.Run(() => {
                        this.Invoke(new Action(() => {

                            WriteLineLabel("Resultados:","\n");
                            WriteLineLabel("Tamanho do produto: ",$"{Math.Round(res.TamProd,2)} mm");
                            WriteLineLabel("Dureza: ",$"{Math.Round(res.Hardness,2)} gf");
                            WriteLineLabel("Elasticidade: ",$"{Math.Round(res.Springiness*100,2)} %");
                            WriteLineLabel("Coesividade: ",$"{Math.Round(res.Cohesiveness*100,2)} %");
                            WriteLineLabel("Resiliência: ",$"{Math.Round(res.Resilience*100,2)} %");
                            WriteLineLabel("Adesividade : ",$"{Math.Round(res.Adhesiveness,2)} gf.s");
                            WriteLineLabel("Gomosidade: ",$"{Math.Round(res.Gumminess,2)} gf");
                            WriteLineLabel("Mastigabilidade: ",$"{Math.Round(res.Chewiness,2)} gf");

                        }));
                    });
                }
            }
            Task.Run(() => {
                this.Invoke(new Action(() => {
                    controlsEnabled(true);
                }));
            });
        }

        private void AtInfoLabel(DataTest DadosDeEnsaio) {

            String un;
            lbInformations.Clear();
            WriteLineLabel("Informações do Teste:","\n");

            WriteLineLabel("Tipo de Ensaio: ",$"{DadosDeEnsaio.Tipo}");
            WriteLineLabel("Velocidade pré-teste: ",$"{DadosDeEnsaio.VelPreTeste} mm/s");
            WriteLineLabel("Velocidade de Teste: ",$"{DadosDeEnsaio.VelTeste} mm/s");
            WriteLineLabel("Velocidade pós-teste: ",$"{DadosDeEnsaio.VelPosTeste} mm/s");
            WriteLineLabel("Tipo de Alvo: ",$"{DadosDeEnsaio.TipoLimite}");

            un=DadosDeEnsaio.TipoLimite==TipoTarget.Distancia ? "mm" : DadosDeEnsaio.TipoLimite==TipoTarget.Deformacao ? "%" : "gf";
            var valAlvo = DadosDeEnsaio.TipoLimite==TipoTarget.Deformacao ? DadosDeEnsaio.ValorLimite*100 : DadosDeEnsaio.ValorLimite;

            WriteLineLabel("Valor Alvo: ",$"{valAlvo} {un}");
            WriteLineLabel("Tempo de Intervalo: ",$"{DadosDeEnsaio.Tempo} s");
            WriteLineLabel("Tipo de Detecção: ",$"{DadosDeEnsaio.TipoDeteccao}");

            un=DadosDeEnsaio.TipoDeteccao==TipoTrigger.Distancia ? "mm" : DadosDeEnsaio.TipoDeteccao==TipoTrigger.Forca ? "gf" : String.Empty;

            WriteLineLabel("Valor de Detecção: ",$"{DadosDeEnsaio.ValorDeteccao} {un}");
            WriteLineLabel("Tipo de Tara: ",$"{DadosDeEnsaio.TipoTara}");
            WriteLineLabel("Tipo de Probe: ",$"{DadosDeEnsaio.PontaDeTeste.Tipo}");
            WriteLineLabel("Dimensões da Probe: ",$"{DadosDeEnsaio.PontaDeTeste.getDimin()}");
            lbInformations.AppendText($"\n");

        }

        private void WriteLineLabel(String text1,String text2) {
            lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Bold);

            lbInformations.AppendText(text1);
            flipLbStyle();
            lbInformations.AppendText(text2+"\n");
            flipLbStyle();
        }

        private string flipLbStyle() {
            if(!lbInformations.SelectionFont.Bold) {
                lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Bold); // Definir a fonte em negrito
            } else {
                lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Regular); // Definir a fonte em itálico
            }
            return String.Empty;
        }

#if DEBUG //----------------------------------------------------
        private void button1_Click_1(object sender,EventArgs e) {
            /*tex.testRunning=false;
            execFimTeste(this,EventArgs.Empty);*/  
        }

        private void btnEnv_Click(object sender,EventArgs e) {
            tex.Serial.Write(txbMensEnv.Text);
        }

        private void EscreveMensTxb(object sender,SerialMessageArgument e) {
                Task.Run(() => {
                    this.Invoke(new Action(() => {
                        if(tex.Serial.IsOpen) {

                            if(!(e.stringValue.Contains("[L;")||e.stringValue.Contains("[E;")||e.stringValue.Contains("[V;"))) {
                                txbMensRecebida.AppendText(e.stringValue+"\n");
                                txbMensRecebida.SelectionStart=txbMensRecebida.Text.Length;
                                txbMensRecebida.ScrollToCaret();
                            }
                        }

                    }));

                });
            
        }
#endif
#endregion


#if DEBUG //----------------------------------------------------

        private void button2_Click(object sender,EventArgs e) {
            /*velRampa=0;
            tex.Motor.Start(ModoMotor.Subir,0.1);
            tmRampa.Start();*/
        }

        private void Rampa (object sender, EventArgs e) {

            if(tex.Motor.ModoMotor==ModoMotor.Parado) {
                tex.Motor.Stop();
                tmRampa.Stop();
            } else {
                tex.Motor.Start(ModoMotor.Subir,velRampa);
            }
            velRampa+=0.1;
            if(velRampa>=10) {
                tex.Serial.EnvComandoMotor(ModoMotor.Parado,0);
                tmRampa.Stop();
            }
        }
#endif
    }
}
