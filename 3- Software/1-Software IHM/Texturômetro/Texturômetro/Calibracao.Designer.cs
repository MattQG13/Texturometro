namespace Texturometer {
    partial class Calibracao {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposin
        /// >true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calibracao));
            this.tabs = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txCal = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tab2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tab1);
            this.tabs.Controls.Add(this.tab2);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(342, 316);
            this.tabs.TabIndex = 6;
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.Color.Transparent;
            this.tab1.Controls.Add(this.label7);
            this.tab1.Controls.Add(this.pictureBox1);
            this.tab1.Controls.Add(this.panel1);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(334, 290);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "tab1";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label7.Location = new System.Drawing.Point(140, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 76);
            this.label7.TabIndex = 11;
            this.label7.Text = "1. Retire todos os produtos antes de realizar a calibração.\r\n\r\n2. Pressione \"Próx" +
    "imo\".\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 246);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel1);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 258);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 29);
            this.panel1.TabIndex = 10;
            // 
            // btnCancel1
            // 
            this.btnCancel1.Location = new System.Drawing.Point(5, 4);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(75, 23);
            this.btnCancel1.TabIndex = 1;
            this.btnCancel1.Text = "Cancelar";
            this.btnCancel1.UseVisualStyleBackColor = true;
            this.btnCancel1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(248, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Próximo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.Color.Transparent;
            this.tab2.Controls.Add(this.label3);
            this.tab2.Controls.Add(this.label2);
            this.tab2.Controls.Add(this.label1);
            this.tab2.Controls.Add(this.txCal);
            this.tab2.Controls.Add(this.button4);
            this.tab2.Controls.Add(this.button5);
            this.tab2.Controls.Add(this.pictureBox2);
            this.tab2.Controls.Add(this.panel2);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(334, 263);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "tab2";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(155, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 117);
            this.label3.TabIndex = 12;
            this.label3.Text = "1. Posicione a carga de calibração no local indicado.\r\n\r\n2. Indique corretamente " +
    "a carga de calibração abaixo.\r\n\r\n3. Pressione \"Ok\".\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label2.Location = new System.Drawing.Point(158, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "Carga para calibração:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.Location = new System.Drawing.Point(276, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "g";
            // 
            // txCal
            // 
            this.txCal.Location = new System.Drawing.Point(155, 181);
            this.txCal.Name = "txCal";
            this.txCal.Size = new System.Drawing.Size(117, 20);
            this.txCal.TabIndex = 6;
            this.txCal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txCal_KeyPress);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 369);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Back";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(709, 371);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Next";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(8, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(141, 196);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel2);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 231);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 29);
            this.panel2.TabIndex = 11;
            // 
            // btnCancel2
            // 
            this.btnCancel2.Location = new System.Drawing.Point(5, 4);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(75, 23);
            this.btnCancel2.TabIndex = 5;
            this.btnCancel2.Text = "Cancelar";
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(248, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Calibracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 316);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Calibracao";
            this.Text = "Calibração";
            this.tabs.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.Button btnCancel1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txCal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
    }
}