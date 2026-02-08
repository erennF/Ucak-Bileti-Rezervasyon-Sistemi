using System;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class HostesEkleForm : Form
    {
       
        public TextBox txtEposta;
        public TextBox txtHostesNo;
        public Button btnKaydet;
        public Button btnCikis;

        public HostesEkleForm()
        {
            InitializeComponent();
            this.Text = "Yeni Hostes Ekle";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeUI();
        }

        private void InitializeUI()
        {
            int y = 20;

            TextBox txtAd = new TextBox { Text = "Ad:", Left = 20, Top = y, Width = 120 };
            txtAd = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            TextBox txtSoyAd = new TextBox { Text = "Soyad:", Left = 20, Top = y, Width = 120 };
            txtSoyAd = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            TextBox txtEposta = new TextBox { Text = "E-posta:", Left = 20, Top = y, Width = 120 };
            txtEposta = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            TextBox txtTelefon = new TextBox { Text = "Telefon:", Left = 20, Top = y, Width = 120 };
            txtTelefon = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            TextBox txtSifre = new TextBox { Text = "Şifre:", Left = 20, Top = y, Width = 120 };
            txtSifre = new TextBox { Left = 150, Top = y, Width = 300, PasswordChar = '●' };
            y += 40;

            TextBox txtHostesNo = new TextBox { Text = "Hostes No:", Left = 20, Top = y, Width = 120 };
            txtHostesNo = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            TextBox txtDilBeceri = new TextBox { Text = "Dil Becerileri:", Left = 20, Top = y, Width = 120 };
            txtDilBeceri = new TextBox { Left = 150, Top = y, Width = 300, Text = "Türkçe, İngilizce" };
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
                txtAd, txtAd, txtSoyAd, txtSoyAd, txtEposta, txtEposta,
                txtTelefon, txtTelefon, txtSifre, txtSifre, txtHostesNo, txtHostesNo,
                txtDilBeceri, txtDilBeceri, btnKaydet, btnCikis
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

                if (string.IsNullOrWhiteSpace(txtHostesNo.Text))
                {
                    MessageBox.Show("Hostes numarası boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHostesNo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDilBeceri.Text))
                {
                    MessageBox.Show("Dil becerileri boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDilBeceri.Focus();
                    return;
                }

                // Kaydetme işlemi
                bool sonuc = KullaniciManager.HostesEkle(
                    txtAd.Text.Trim(),
                    txtSoyAd.Text.Trim(),
                    txtEposta.Text.Trim().ToLower(),
                    txtTelefon.Text.Trim(),
                    txtSifre.Text,
                    txtHostesNo.Text.Trim(),
                    txtDilBeceri.Text.Trim()
                );

                if (sonuc)
                {
                    MessageBox.Show("Hostes başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}