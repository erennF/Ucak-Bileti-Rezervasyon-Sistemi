namespace uçakSistemi
{
    partial class UcakIslemlerii
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
            this.btnUcakEkleGuncelle = new System.Windows.Forms.Button();
            this.btnUcakSil = new System.Windows.Forms.Button();
            this.btnUcaklariGoruntule = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUcakEkleGuncelle
            // 
            this.btnUcakEkleGuncelle.Location = new System.Drawing.Point(302, 89);
            this.btnUcakEkleGuncelle.Name = "btnUcakEkleGuncelle";
            this.btnUcakEkleGuncelle.Size = new System.Drawing.Size(186, 53);
            this.btnUcakEkleGuncelle.TabIndex = 0;
            this.btnUcakEkleGuncelle.Text = "Uçak Ekle/Güncelle";
            this.btnUcakEkleGuncelle.UseVisualStyleBackColor = true;
            this.btnUcakEkleGuncelle.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUcakSil
            // 
            this.btnUcakSil.Location = new System.Drawing.Point(302, 194);
            this.btnUcakSil.Name = "btnUcakSil";
            this.btnUcakSil.Size = new System.Drawing.Size(186, 53);
            this.btnUcakSil.TabIndex = 1;
            this.btnUcakSil.Text = "Uçak Sil";
            this.btnUcakSil.UseVisualStyleBackColor = true;
            // 
            // btnUcaklariGoruntule
            // 
            this.btnUcaklariGoruntule.Location = new System.Drawing.Point(302, 300);
            this.btnUcaklariGoruntule.Name = "btnUcaklariGoruntule";
            this.btnUcaklariGoruntule.Size = new System.Drawing.Size(186, 53);
            this.btnUcaklariGoruntule.TabIndex = 2;
            this.btnUcaklariGoruntule.Text = "Uçakları Görüntüle";
            this.btnUcaklariGoruntule.UseVisualStyleBackColor = true;
            // 
            // btnCikis
            // 
            this.btnCikis.Location = new System.Drawing.Point(723, 427);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(75, 23);
            this.btnCikis.TabIndex = 3;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            // 
            // UçakİşlemleriForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnUcaklariGoruntule);
            this.Controls.Add(this.btnUcakSil);
            this.Controls.Add(this.btnUcakEkleGuncelle);
            this.Name = "UçakİşlemleriForm";
            this.Text = "UçakİşlemleriForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUcakEkleGuncelle;
        private System.Windows.Forms.Button btnUcakSil;
        private System.Windows.Forms.Button btnUcaklariGoruntule;
        private System.Windows.Forms.Button btnCikis;
    }
}