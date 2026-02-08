namespace uçakSistemi
{
    partial class UcakEkleForm
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
            this.txtKaydet = new System.Windows.Forms.Button();
            this.txtCikis = new System.Windows.Forms.Button();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtPlaka = new System.Windows.Forms.TextBox();
            this.txtKapasite = new System.Windows.Forms.TextBox();
            this.txtSirket = new System.Windows.Forms.TextBox();
            this.txtUcakKodu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtKaydet
            // 
            this.txtKaydet.Location = new System.Drawing.Point(113, 275);
            this.txtKaydet.Name = "txtKaydet";
            this.txtKaydet.Size = new System.Drawing.Size(75, 23);
            this.txtKaydet.TabIndex = 0;
            this.txtKaydet.Text = "KAYDET";
            this.txtKaydet.UseVisualStyleBackColor = true;
            // 
            // txtCikis
            // 
            this.txtCikis.Location = new System.Drawing.Point(518, 275);
            this.txtCikis.Name = "txtCikis";
            this.txtCikis.Size = new System.Drawing.Size(75, 23);
            this.txtCikis.TabIndex = 1;
            this.txtCikis.Text = "ÇIKIŞ";
            this.txtCikis.UseVisualStyleBackColor = true;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(294, 74);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(100, 22);
            this.txtModel.TabIndex = 4;
            this.txtModel.Text = "Model";
            // 
            // txtPlaka
            // 
            this.txtPlaka.Location = new System.Drawing.Point(294, 125);
            this.txtPlaka.Name = "txtPlaka";
            this.txtPlaka.Size = new System.Drawing.Size(100, 22);
            this.txtPlaka.TabIndex = 5;
            this.txtPlaka.Text = "Plaka";
            // 
            // txtKapasite
            // 
            this.txtKapasite.Location = new System.Drawing.Point(294, 185);
            this.txtKapasite.Name = "txtKapasite";
            this.txtKapasite.Size = new System.Drawing.Size(100, 22);
            this.txtKapasite.TabIndex = 6;
            this.txtKapasite.Text = "Kapasite";
            // 
            // txtSirket
            // 
            this.txtSirket.Location = new System.Drawing.Point(294, 241);
            this.txtSirket.Name = "txtSirket";
            this.txtSirket.Size = new System.Drawing.Size(100, 22);
            this.txtSirket.TabIndex = 7;
            this.txtSirket.Text = "Şirket";
            // 
            // txtUcakKodu
            // 
            this.txtUcakKodu.Location = new System.Drawing.Point(294, 290);
            this.txtUcakKodu.Name = "txtUcakKodu";
            this.txtUcakKodu.Size = new System.Drawing.Size(100, 22);
            this.txtUcakKodu.TabIndex = 8;
            this.txtUcakKodu.Text = "Uçak Kodu";
            // 
            // UcakEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtUcakKodu);
            this.Controls.Add(this.txtSirket);
            this.Controls.Add(this.txtKapasite);
            this.Controls.Add(this.txtPlaka);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtCikis);
            this.Controls.Add(this.txtKaydet);
            this.Name = "UcakEkleForm";
            this.Text = "UcakEkleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button txtKaydet;
        private System.Windows.Forms.Button txtCikis;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtPlaka;
        private System.Windows.Forms.TextBox txtKapasite;
        private System.Windows.Forms.TextBox txtSirket;
        private System.Windows.Forms.TextBox txtUcakKodu;
    }
}