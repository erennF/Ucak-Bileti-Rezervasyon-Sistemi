using System;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class PilotEkleForm : Form
    {

        public PilotEkleForm()
        {
            InitializeComponent();
            this.Text = "Yeni Pilot Ekle";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeUI();
        }

        private void InitializeUI()
        {
            int y = 20;

            Label lblAd = new Label { Text = "Ad:", Left = 20, Top = y, Width = 120 };
            txtAd = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblSoyad = new Label { Text = "Soyad:", Left = 20, Top = y, Width = 120 };
            txtSoyAd = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblEposta = new Label { Text = "E-posta:", Left = 20, Top = y, Width = 120 };
            txtEposta = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblTelefon = new Label { Text = "Telefon:", Left = 20, Top = y, Width = 120 };
            txtTelefon = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblSifre = new Label { Text = "Şifre:", Left = 20, Top = y, Width = 120 };
            txtSifre = new TextBox { Left = 150, Top = y, Width = 300, PasswordChar = '●' };
            y += 40;

            Label lblLisansNo = new Label { Text = "Lisans No:", Left = 20, Top = y, Width = 120 };
            txtLisansNo = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblRutbe = new Label { Text = "Rütbe:", Left = 20, Top = y, Width = 120 };
            txtRutbe = new TextBox { Left = 150, Top = y, Width = 300, Text = "Kaptan" };
            y += 50;

            btnKaydet = new Button
            {
                Text = "Kaydet",
                Left = 150,
                Top = y,
                Width = 120,
                Height = 40,
                BackColor = System.Drawing.Color.LightGreen
            };

            btnCikis = new Button
            {
                Text = "İptal",
                Left = 290,
                Top = y,
                Width = 120,
                Height = 40,
                BackColor = System.Drawing.Color.LightCoral
            };

            btnKaydet.Click += BtnKaydet_Click;
            btnCikis.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] {
                lblAd, txtAd, lblSoyad, txtSoyAd, lblEposta, txtEposta,
                lblTelefon, txtTelefon, lblSifre, txtSifre, lblLisansNo, txtLisansNo,
                lblRutbe, txtRutbe, btnKaydet, btnCikis
            });
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(txtAd.Text))
                {
                    MessageBox.Show("Ad alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAd.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSoyAd.Text))
                {
                    MessageBox.Show("Soyad alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoyAd.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEposta.Text) || !txtEposta.Text.Contains("@"))
                {
                    MessageBox.Show("Geçerli bir e-posta adresi girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEposta.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelefon.Text) || txtTelefon.Text.Length < 10)
                {
                    MessageBox.Show("Geçerli bir telefon numarası girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefon.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSifre.Text) || txtSifre.Text.Length < 6)
                {
                    MessageBox.Show("Şifre en az 6 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLisansNo.Text))
                {
                    MessageBox.Show("Lisans numarası boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLisansNo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtRutbe.Text))
                {
                    MessageBox.Show("Rütbe boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRutbe.Focus();
                    return;
                }

                // Kaydetme işlemi
                bool sonuc = KullaniciManager.PilotEkle(
                    txtAd.Text.Trim(),
                    txtSoyAd.Text.Trim(),
                    txtEposta.Text.Trim().ToLower(),
                    txtTelefon.Text.Trim(),
                    txtSifre.Text,
                    txtLisansNo.Text.Trim(),
                    txtRutbe.Text.Trim()
                );

                if (sonuc)
                {
                    MessageBox.Show("Pilot başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PilotEkleForm_Load(object sender, EventArgs e)
        {

        }
    }

}