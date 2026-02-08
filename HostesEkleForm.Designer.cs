namespace uçakSistemi
{
    partial class HostesEkleForm
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
            this.TxtEposta = new System.Windows.Forms.TextBox();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.HostesNo = new System.Windows.Forms.TextBox();
            this.txtDilBeceri = new System.Windows.Forms.TextBox();
            this.txtKaydet = new System.Windows.Forms.Button();
            this.txtCikis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(309, 134);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(100, 22);
            this.txtAd.TabIndex = 2;
            this.txtAd.Text = "AD";
            // 
            // txtSoyAd
            // 
            this.txtSoyAd.Location = new System.Drawing.Point(309, 184);
            this.txtSoyAd.Name = "txtSoyAd";
            this.txtSoyAd.Size = new System.Drawing.Size(100, 22);
            this.txtSoyAd.TabIndex = 3;
            this.txtSoyAd.Text = "SOYAD";
            // 
            // TxtEposta
            // 
            this.TxtEposta.Location = new System.Drawing.Point(309, 232);
            this.TxtEposta.Name = "TxtEposta";
            this.TxtEposta.Size = new System.Drawing.Size(100, 22);
            this.TxtEposta.TabIndex = 5;
            this.TxtEposta.Text = "EPOSTA";
            // 
            // txtTelefon
            // 
            this.txtTelefon.Location = new System.Drawing.Point(309, 281);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(100, 22);
            this.txtTelefon.TabIndex = 6;
            this.txtTelefon.Text = "TELEFONNO";
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(309, 320);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(100, 22);
            this.txtSifre.TabIndex = 7;
            this.txtSifre.Text = "ŞİFRE";
            // 
            // HostesNo
            // 
            this.HostesNo.Location = new System.Drawing.Point(309, 370);
            this.HostesNo.Name = "HostesNo";
            this.HostesNo.Size = new System.Drawing.Size(100, 22);
            this.HostesNo.TabIndex = 8;
            this.HostesNo.Text = "HostesNo";
            // 
            // txtDilBeceri
            // 
            this.txtDilBeceri.Location = new System.Drawing.Point(309, 416);
            this.txtDilBeceri.Name = "txtDilBeceri";
            this.txtDilBeceri.Size = new System.Drawing.Size(100, 22);
            this.txtDilBeceri.TabIndex = 9;
            this.txtDilBeceri.Text = "Dil Becerileri";
            // 
            // txtKaydet
            // 
            this.txtKaydet.Location = new System.Drawing.Point(129, 351);
            this.txtKaydet.Name = "txtKaydet";
            this.txtKaydet.Size = new System.Drawing.Size(75, 23);
            this.txtKaydet.TabIndex = 10;
            this.txtKaydet.Text = "KAYDET";
            this.txtKaydet.UseVisualStyleBackColor = true;
            // 
            // txtCikis
            // 
            this.txtCikis.Location = new System.Drawing.Point(558, 351);
            this.txtCikis.Name = "txtCikis";
            this.txtCikis.Size = new System.Drawing.Size(75, 23);
            this.txtCikis.TabIndex = 11;
            this.txtCikis.Text = "ÇIKIŞ";
            this.txtCikis.UseVisualStyleBackColor = true;
            // 
            // HostesEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCikis);
            this.Controls.Add(this.txtKaydet);
            this.Controls.Add(this.txtDilBeceri);
            this.Controls.Add(this.HostesNo);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.TxtEposta);
            this.Controls.Add(this.txtSoyAd);
            this.Controls.Add(this.txtAd);
            this.Name = "HostesEkleForm";
            this.Text = "HostesEkleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.TextBox txtSoyAd;
        private System.Windows.Forms.TextBox TxtEposta;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.TextBox HostesNo;
        private System.Windows.Forms.TextBox txtDilBeceri;
        private System.Windows.Forms.Button txtKaydet;
        private System.Windows.Forms.Button txtCikis;
    }
}