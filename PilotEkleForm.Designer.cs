namespace uçakSistemi
{
    partial class PilotEkleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtAd = new System.Windows.Forms.TextBox();
            this.txtSoyAd = new System.Windows.Forms.TextBox();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.txtLisansNo = new System.Windows.Forms.TextBox();
            this.txtRutbe = new System.Windows.Forms.TextBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(338, 104);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(100, 22);
            this.txtAd.TabIndex = 0;
            this.txtAd.Text = "AD";
            // 
            // txtSoyAd
            // 
            this.txtSoyAd.Location = new System.Drawing.Point(338, 163);
            this.txtSoyAd.Name = "txtSoyAd";
            this.txtSoyAd.Size = new System.Drawing.Size(100, 22);
            this.txtSoyAd.TabIndex = 1;
            this.txtSoyAd.Text = "SOYAD";
            // 
            // txtEposta
            // 
            this.txtEposta.Location = new System.Drawing.Point(338, 205);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(100, 22);
            this.txtEposta.TabIndex = 7;
            this.txtEposta.Text = "EPOSTA";
            // 
            // txtTelefon
            // 
            this.txtTelefon.Location = new System.Drawing.Point(338, 244);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(100, 22);
            this.txtTelefon.TabIndex = 8;
            this.txtTelefon.Text = "TELEFONNO";
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(338, 283);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(100, 22);
            this.txtSifre.TabIndex = 9;
            this.txtSifre.Text = "ŞİFRE";
            // 
            // txtLisansNo
            // 
            this.txtLisansNo.Location = new System.Drawing.Point(338, 324);
            this.txtLisansNo.Name = "txtLisansNo";
            this.txtLisansNo.Size = new System.Drawing.Size(100, 22);
            this.txtLisansNo.TabIndex = 10;
            this.txtLisansNo.Text = "Lisans NO";
            // 
            // txtRutbe
            // 
            this.txtRutbe.Location = new System.Drawing.Point(338, 372);
            this.txtRutbe.Name = "txtRutbe";
            this.txtRutbe.Size = new System.Drawing.Size(100, 22);
            this.txtRutbe.TabIndex = 11;
            this.txtRutbe.Text = "Rutbe";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(169, 324);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 12;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = true;
            // 
            // btnCikis
            // 
            this.btnCikis.Location = new System.Drawing.Point(614, 324);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(75, 23);
            this.btnCikis.TabIndex = 13;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            // 
            // PilotEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtRutbe);
            this.Controls.Add(this.txtLisansNo);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.txtEposta);
            this.Controls.Add(this.txtSoyAd);
            this.Controls.Add(this.txtAd);
            this.Name = "PilotEkleForm";
            this.Text = "PilotEkleForm";
            this.Load += new System.EventHandler(this.PilotEkleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.TextBox txtSoyAd;
        private System.Windows.Forms.TextBox txtEposta;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.TextBox txtLisansNo;
        private System.Windows.Forms.TextBox txtRutbe;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnCikis;
    }
}