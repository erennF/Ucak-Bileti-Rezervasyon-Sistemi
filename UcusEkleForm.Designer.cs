namespace uçakSistemi
{
    partial class UcusEkleForm
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
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.txtKalkisNokta = new System.Windows.Forms.TextBox();
            this.txtVarisNokta = new System.Windows.Forms.TextBox();
            this.txtKalkisTarihi = new System.Windows.Forms.TextBox();
            this.txtUcusSuresi = new System.Windows.Forms.TextBox();
            this.txtKapiNo = new System.Windows.Forms.TextBox();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.txtUcakKodu = new System.Windows.Forms.TextBox();
            this.txtSirket = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(168, 306);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = true;
            // 
            // btnCikis
            // 
            this.btnCikis.Location = new System.Drawing.Point(625, 306);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(75, 23);
            this.btnCikis.TabIndex = 1;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            // 
            // txtKalkisNokta
            // 
            this.txtKalkisNokta.Location = new System.Drawing.Point(358, 76);
            this.txtKalkisNokta.Name = "txtKalkisNokta";
            this.txtKalkisNokta.Size = new System.Drawing.Size(100, 22);
            this.txtKalkisNokta.TabIndex = 2;
            this.txtKalkisNokta.Text = "Kalkış Noktası";
            // 
            // txtVarisNokta
            // 
            this.txtVarisNokta.Location = new System.Drawing.Point(358, 119);
            this.txtVarisNokta.Name = "txtVarisNokta";
            this.txtVarisNokta.Size = new System.Drawing.Size(100, 22);
            this.txtVarisNokta.TabIndex = 3;
            this.txtVarisNokta.Text = "Varış Noktası";
            // 
            // txtKalkisTarihi
            // 
            this.txtKalkisTarihi.Location = new System.Drawing.Point(358, 158);
            this.txtKalkisTarihi.Name = "txtKalkisTarihi";
            this.txtKalkisTarihi.Size = new System.Drawing.Size(100, 22);
            this.txtKalkisTarihi.TabIndex = 4;
            this.txtKalkisTarihi.Text = "Kalkış Tarihi";
            // 
            // txtUcusSuresi
            // 
            this.txtUcusSuresi.Location = new System.Drawing.Point(358, 206);
            this.txtUcusSuresi.Name = "txtUcusSuresi";
            this.txtUcusSuresi.Size = new System.Drawing.Size(100, 22);
            this.txtUcusSuresi.TabIndex = 5;
            this.txtUcusSuresi.Text = "Uçuş Süresi";
            // 
            // txtKapiNo
            // 
            this.txtKapiNo.Location = new System.Drawing.Point(358, 250);
            this.txtKapiNo.Name = "txtKapiNo";
            this.txtKapiNo.Size = new System.Drawing.Size(100, 22);
            this.txtKapiNo.TabIndex = 6;
            this.txtKapiNo.Text = "Kapı  Numarası";
            // 
            // txtFiyat
            // 
            this.txtFiyat.Location = new System.Drawing.Point(358, 296);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(100, 22);
            this.txtFiyat.TabIndex = 7;
            this.txtFiyat.Text = "Fiyat";
            // 
            // txtUcakKodu
            // 
            this.txtUcakKodu.Location = new System.Drawing.Point(358, 338);
            this.txtUcakKodu.Name = "txtUcakKodu";
            this.txtUcakKodu.Size = new System.Drawing.Size(100, 22);
            this.txtUcakKodu.TabIndex = 8;
            this.txtUcakKodu.Text = "Uçak Kodu";
            // 
            // txtSirket
            // 
            this.txtSirket.Location = new System.Drawing.Point(358, 390);
            this.txtSirket.Name = "txtSirket";
            this.txtSirket.Size = new System.Drawing.Size(100, 22);
            this.txtSirket.TabIndex = 9;
            this.txtSirket.Text = "Şirket";
            // 
            // UcusEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSirket);
            this.Controls.Add(this.txtUcakKodu);
            this.Controls.Add(this.txtFiyat);
            this.Controls.Add(this.txtKapiNo);
            this.Controls.Add(this.txtUcusSuresi);
            this.Controls.Add(this.txtKalkisTarihi);
            this.Controls.Add(this.txtVarisNokta);
            this.Controls.Add(this.txtKalkisNokta);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnKaydet);
            this.Name = "UcusEkleForm";
            this.Text = "UcusEkleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.TextBox txtKalkisNokta;
        private System.Windows.Forms.TextBox txtVarisNokta;
        private System.Windows.Forms.TextBox txtKalkisTarihi;
        private System.Windows.Forms.TextBox txtUcusSuresi;
        private System.Windows.Forms.TextBox txtKapiNo;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.TextBox txtUcakKodu;
        private System.Windows.Forms.TextBox txtSirket;
    }
}