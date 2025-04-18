namespace Texturometer {
    partial class TexturometroForms {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 4D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 5D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 1D);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TexturometroForms));
            this.pnBackground = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUP = new System.Windows.Forms.Button();
            this.btnDN = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnFast = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPosition = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLoad = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.lxy = new System.Windows.Forms.Label();
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbInformations = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.arquivoStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.TAStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rodarTesteStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tararToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroMáquinaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracoesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnBackground.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.panel3.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBackground
            // 
            this.pnBackground.Controls.Add(this.tableLayoutPanel1);
            this.pnBackground.Controls.Add(this.menuStrip);
            this.pnBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBackground.Location = new System.Drawing.Point(0, 0);
            this.pnBackground.Name = "pnBackground";
            this.pnBackground.Size = new System.Drawing.Size(772, 460);
            this.pnBackground.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 436);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbPosition);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbLoad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 239);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 194);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUP);
            this.panel1.Controls.Add(this.btnDN);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnFast);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 194);
            this.panel1.TabIndex = 14;
            // 
            // btnUP
            // 
            this.btnUP.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUP.Location = new System.Drawing.Point(9, 3);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(73, 46);
            this.btnUP.TabIndex = 2;
            this.btnUP.TabStop = false;
            this.btnUP.Text = "SUBIR";
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            // 
            // btnDN
            // 
            this.btnDN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDN.Location = new System.Drawing.Point(9, 107);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(73, 46);
            this.btnDN.TabIndex = 3;
            this.btnDN.TabStop = false;
            this.btnDN.Text = "DESCER";
            this.btnDN.UseVisualStyleBackColor = true;
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(9, 55);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(73, 46);
            this.btnStop.TabIndex = 7;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnFast
            // 
            this.btnFast.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFast.Location = new System.Drawing.Point(88, 55);
            this.btnFast.Name = "btnFast";
            this.btnFast.Size = new System.Drawing.Size(73, 46);
            this.btnFast.TabIndex = 8;
            this.btnFast.TabStop = false;
            this.btnFast.Text = "RÁPIDO";
            this.btnFast.UseVisualStyleBackColor = true;
            this.btnFast.Click += new System.EventHandler(this.btnFast_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(203, 83);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Posição";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPosition
            // 
            this.lbPosition.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPosition.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPosition.Location = new System.Drawing.Point(203, 101);
            this.lbPosition.Name = "lbPosition";
            this.lbPosition.Size = new System.Drawing.Size(120, 46);
            this.lbPosition.TabIndex = 5;
            this.lbPosition.Text = "–";
            this.lbPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 7);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Força";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLoad
            // 
            this.lbLoad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLoad.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoad.Location = new System.Drawing.Point(203, 25);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(120, 46);
            this.lbLoad.TabIndex = 0;
            this.lbLoad.Text = "–";
            this.lbLoad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panelGraph, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(766, 230);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panelGraph
            // 
            this.panelGraph.Controls.Add(this.lxy);
            this.panelGraph.Controls.Add(this.Graph);
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(203, 3);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(560, 224);
            this.panelGraph.TabIndex = 0;
            // 
            // lxy
            // 
            this.lxy.AutoSize = true;
            this.lxy.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lxy.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lxy.Location = new System.Drawing.Point(121, 153);
            this.lxy.Name = "lxy";
            this.lxy.Size = new System.Drawing.Size(21, 14);
            this.lxy.TabIndex = 6;
            this.lxy.Text = "lxy";
            this.lxy.Visible = false;
            // 
            // Graph
            // 
            this.Graph.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.Maximum = 100D;
            chartArea1.AxisX.MaximumAutoSize = 90F;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.Title = "t(s)";
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Format = "0.0";
            chartArea1.AxisY.MaximumAutoSize = 90F;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.ScaleView.SmallScrollMinSize = 10D;
            chartArea1.AxisY.Title = "F(gf)";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartArea1";
            this.Graph.ChartAreas.Add(chartArea1);
            this.Graph.Cursor = System.Windows.Forms.Cursors.Default;
            this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Graph.Location = new System.Drawing.Point(0, 0);
            this.Graph.Name = "Graph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.IsVisibleInLegend = false;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Graph.Series.Add(series1);
            this.Graph.Size = new System.Drawing.Size(560, 224);
            this.Graph.TabIndex = 5;
            this.Graph.Text = "Graph";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbInformations);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 224);
            this.panel3.TabIndex = 1;
            // 
            // lbInformations
            // 
            this.lbInformations.BackColor = System.Drawing.Color.White;
            this.lbInformations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbInformations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInformations.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInformations.Location = new System.Drawing.Point(0, 0);
            this.lbInformations.Name = "lbInformations";
            this.lbInformations.ReadOnly = true;
            this.lbInformations.Size = new System.Drawing.Size(194, 224);
            this.lbInformations.TabIndex = 1;
            this.lbInformations.TabStop = false;
            this.lbInformations.Text = "";
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoStripMenu,
            this.TAStripMenu,
            this.configuracoesToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(772, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // arquivoStripMenu
            // 
            this.arquivoStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarComoToolStripMenuItem});
            this.arquivoStripMenu.Name = "arquivoStripMenu";
            this.arquivoStripMenu.Size = new System.Drawing.Size(76, 20);
            this.arquivoStripMenu.Text = "Resultado";
            // 
            // exportarComoToolStripMenuItem
            // 
            this.exportarComoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuExportExcel,
            this.ToolStripMenuExportCSV,
            this.ToolStripMenuExportPDF});
            this.exportarComoToolStripMenuItem.Name = "exportarComoToolStripMenuItem";
            this.exportarComoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportarComoToolStripMenuItem.Text = "Exportar";
            // 
            // ToolStripMenuExportExcel
            // 
            this.ToolStripMenuExportExcel.Name = "ToolStripMenuExportExcel";
            this.ToolStripMenuExportExcel.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuExportExcel.Text = "Exportar como .xlsx";
            this.ToolStripMenuExportExcel.Click += new System.EventHandler(this.ToolStripMenuExportExcel_Click);
            // 
            // ToolStripMenuExportCSV
            // 
            this.ToolStripMenuExportCSV.Name = "ToolStripMenuExportCSV";
            this.ToolStripMenuExportCSV.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuExportCSV.Text = "Exportar como .csv";
            this.ToolStripMenuExportCSV.Click += new System.EventHandler(this.ToolStripMenuExportCSV_Click);
            // 
            // ToolStripMenuExportPDF
            // 
            this.ToolStripMenuExportPDF.Name = "ToolStripMenuExportPDF";
            this.ToolStripMenuExportPDF.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuExportPDF.Text = "Exportar como .pdf";
            this.ToolStripMenuExportPDF.Click += new System.EventHandler(this.ToolStripMenuExportPDF_Click);
            // 
            // TAStripMenu
            // 
            this.TAStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rodarTesteStripMenu,
            this.calibrarToolStripMenuItem,
            this.tararToolStripMenuItem,
            this.zeroMáquinaToolStripMenuItem});
            this.TAStripMenu.Name = "TAStripMenu";
            this.TAStripMenu.Size = new System.Drawing.Size(38, 20);
            this.TAStripMenu.Text = "T.A.";
            // 
            // rodarTesteStripMenu
            // 
            this.rodarTesteStripMenu.Name = "rodarTesteStripMenu";
            this.rodarTesteStripMenu.Size = new System.Drawing.Size(158, 22);
            this.rodarTesteStripMenu.Text = "Rodar teste...";
            this.rodarTesteStripMenu.Click += new System.EventHandler(this.rodarTesteToolStripMenuItem_Click);
            // 
            // calibrarToolStripMenuItem
            // 
            this.calibrarToolStripMenuItem.Name = "calibrarToolStripMenuItem";
            this.calibrarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.calibrarToolStripMenuItem.Text = "Calibrar...";
            this.calibrarToolStripMenuItem.Click += new System.EventHandler(this.calibrarToolStripMenuItem_Click);
            // 
            // tararToolStripMenuItem
            // 
            this.tararToolStripMenuItem.Name = "tararToolStripMenuItem";
            this.tararToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.tararToolStripMenuItem.Text = "Tarar...";
            this.tararToolStripMenuItem.Click += new System.EventHandler(this.tararToolStripMenuItem_Click);
            // 
            // zeroMáquinaToolStripMenuItem
            // 
            this.zeroMáquinaToolStripMenuItem.Name = "zeroMáquinaToolStripMenuItem";
            this.zeroMáquinaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.zeroMáquinaToolStripMenuItem.Text = "Zero Máquina...";
            this.zeroMáquinaToolStripMenuItem.Click += new System.EventHandler(this.zeroMáquinaToolStripMenuItem_Click);
            // 
            // configuracoesToolStripMenuItem
            // 
            this.configuracoesToolStripMenuItem.Name = "configuracoesToolStripMenuItem";
            this.configuracoesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.configuracoesToolStripMenuItem.Text = "Configurações";
            this.configuracoesToolStripMenuItem.Click += new System.EventHandler(this.configuracoesToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // TexturometroForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 460);
            this.Controls.Add(this.pnBackground);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TexturometroForms";
            this.Text = "Titan Texture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TexturometroForms_FormClosing);
            this.Load += new System.EventHandler(this.Texturometro_Load);
            this.pnBackground.ResumeLayout(false);
            this.pnBackground.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelGraph.ResumeLayout(false);
            this.panelGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.panel3.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

#endregion
        private System.Windows.Forms.Panel pnBackground;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem arquivoStripMenu;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.ToolStripMenuItem TAStripMenu;
        private System.Windows.Forms.ToolStripMenuItem rodarTesteStripMenu;
        private System.Windows.Forms.Button btnDN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem calibrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tararToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem zeroMáquinaToolStripMenuItem;
        private System.Windows.Forms.Button btnFast;
        private System.Windows.Forms.Button btnStop;

        private System.Windows.Forms.ToolStripMenuItem exportarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuExportExcel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuExportCSV;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuExportPDF;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Label lxy;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.RichTextBox lbInformations;
        private System.Windows.Forms.Panel panel1;
#if DEBUG //----------------------------------------------------
        private System.Windows.Forms.RichTextBox txbMensRecebida;
        private System.Windows.Forms.RichTextBox txbMensEnv;
        private System.Windows.Forms.Label lbVel;
        private System.Windows.Forms.Button btnEnv;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
#endif
    }
}

