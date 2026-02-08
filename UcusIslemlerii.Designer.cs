namespace uçakSistemi
{
    partial class UcusIslemlerii
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
            this.btnUcusEkle = new System.Windows.Forms.Button();
            this.btnUcusGuncelle = new System.Windows.Forms.Button();
            this.btnUcusIptal = new System.Windows.Forms.Button();
            this.btnRotaGuncelle = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUcusEkle
            // 
            this.btnUcusEkle.Location = new System.Drawing.Point(309, 42);
            this.btnUcusEkle.Name = "btnUcusEkle";
            this.btnUcusEkle.Size = new System.Drawing.Size(176, 62);
            this.btnUcusEkle.TabIndex = 0;
            this.btnUcusEkle.Text = "Uçuş Ekle";
            this.btnUcusEkle.UseVisualStyleBackColor = true;
            // 
            // btnUcusGuncelle
            // 
            this.btnUcusGuncelle.Location = new System.Drawing.Point(309, 125);
            this.btnUcusGuncelle.Name = "btnUcusGuncelle";
            this.btnUcusGuncelle.Size = new System.Drawing.Size(176, 62);
            this.btnUcusGuncelle.TabIndex = 1;
            this.btnUcusGuncelle.Text = "Uçuş Güncelle";
            this.btnUcusGuncelle.UseVisualStyleBackColor = true;
            // 
            // btnUcusIptal
            // 
            this.btnUcusIptal.Location = new System.Drawing.Point(309, 208);
            this.btnUcusIptal.Name = "btnUcusIptal";
            this.btnUcusIptal.Size = new System.Drawing.Size(176, 62);
            this.btnUcusIptal.TabIndex = 2;
            this.btnUcusIptal.Text = "Uçuş İptal";
            this.btnUcusIptal.UseVisualStyleBackColor = true;
            this.btnUcusIptal.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnRotaGuncelle
            // 
            this.btnRotaGuncelle.Location = new System.Drawing.Point(309, 290);
            this.btnRotaGuncelle.Name = "btnRotaGuncelle";
            this.btnRotaGuncelle.Size = new System.Drawing.Size(176, 62);
            this.btnRotaGuncelle.TabIndex = 3;
            this.btnRotaGuncelle.Text = "Rota Güncelle";
            this.btnRotaGuncelle.UseVisualStyleBackColor = true;
            this.btnRotaGuncelle.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.Location = new System.Drawing.Point(723, 429);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(75, 23);
            this.btnCikis.TabIndex = 4;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            this.btnCikis.Click += new System.EventHandler(this.button5_Click);
            // 
            // UçuşİşlemleriForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnRotaGuncelle);
            this.Controls.Add(this.btnUcusIptal);
            this.Controls.Add(this.btnUcusGuncelle);
            this.Controls.Add(this.btnUcusEkle);
            this.Name = "UçuşİşlemleriForm";
            this.Text = "UçuşİşlemleriForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUcusEkle;
        private System.Windows.Forms.Button btnUcusGuncelle;
        private System.Windows.Forms.Button btnUcusIptal;
        private System.Windows.Forms.Button btnRotaGuncelle;
        private System.Windows.Forms.Button btnCikis;
    }
}