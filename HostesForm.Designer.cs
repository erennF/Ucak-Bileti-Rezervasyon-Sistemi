namespace uçakSistemi
{
    partial class HostesForm
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
            this.btnUcuslariGoruntule = new System.Windows.Forms.Button();
            this.btnKisiselBilgiler = new System.Windows.Forms.Button();
            this.btnBilgilerimiGuncelle = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUcuslariGoruntule
            // 
            this.btnUcuslariGoruntule.Location = new System.Drawing.Point(185, 86);
            this.btnUcuslariGoruntule.Name = "btnUcuslariGoruntule";
            this.btnUcuslariGoruntule.Size = new System.Drawing.Size(177, 53);
            this.btnUcuslariGoruntule.TabIndex = 0;
            this.btnUcuslariGoruntule.Text = "Uçuşlarımı Görüntüle";
            this.btnUcuslariGoruntule.UseVisualStyleBackColor = true;
            this.btnUcuslariGoruntule.Click += new System.EventHandler(this.btnUcuslariGoruntule_Click);
            // 
            // btnKisiselBilgiler
            // 
            this.btnKisiselBilgiler.Location = new System.Drawing.Point(185, 172);
            this.btnKisiselBilgiler.Name = "btnKisiselBilgiler";
            this.btnKisiselBilgiler.Size = new System.Drawing.Size(177, 53);
            this.btnKisiselBilgiler.TabIndex = 1;
            this.btnKisiselBilgiler.Text = "Kişisel Bilgilerim";
            this.btnKisiselBilgiler.UseVisualStyleBackColor = true;
            // 
            // btnBilgilerimiGuncelle
            // 
            this.btnBilgilerimiGuncelle.Location = new System.Drawing.Point(185, 254);
            this.btnBilgilerimiGuncelle.Name = "btnBilgilerimiGuncelle";
            this.btnBilgilerimiGuncelle.Size = new System.Drawing.Size(177, 53);
            this.btnBilgilerimiGuncelle.TabIndex = 2;
            this.btnBilgilerimiGuncelle.Text = "Bilgilerimi Güncelle";
            this.btnBilgilerimiGuncelle.UseVisualStyleBackColor = true;
            // 
            // btnCikis
            // 
            this.btnCikis.Location = new System.Drawing.Point(722, 426);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(75, 23);
            this.btnCikis.TabIndex = 3;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            // 
            // HostesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnBilgilerimiGuncelle);
            this.Controls.Add(this.btnKisiselBilgiler);
            this.Controls.Add(this.btnUcuslariGoruntule);
            this.Name = "HostesForm";
            this.Text = "HostesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUcuslariGoruntule;
        private System.Windows.Forms.Button btnKisiselBilgiler;
        private System.Windows.Forms.Button btnBilgilerimiGuncelle;
        private System.Windows.Forms.Button btnCikis;
    }
}