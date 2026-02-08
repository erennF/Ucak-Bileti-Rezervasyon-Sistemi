
// Pilot Güncelleme Formu
using System;
using System.Windows.Forms;
using uçakSistemi;
namespace uçakSistemi
{
    public partial class PilotGuncelle : Form
    {
        private int _pilotId;
        private TextBox txtSoyad;
        private TextBox txtEposta;
        private TextBox txtTelefon;
        private TextBox txtRutbe;
        private Button btnKaydet;
        private Button btnCikis;

        public PilotGuncelle(int pilotId, string soyad, string eposta, string telefon, string rutbe)
        {
            InitializeComponent();
            _pilotId = pilotId;
            this.Text = "Pilot Güncelle";
            this.Size = new System.Drawing.Size(500, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeUI(soyad, eposta, telefon, rutbe);
        }

        private void InitializeUI(string soyad, string eposta, string telefon, string rutbe)
        {
            int y = 20;

            Label lblInfo = new Label
            {
                Text = "Not: Ad değiştirilemez",
                Left = 20,
                Top = y,
                Width = 450,
                ForeColor = System.Drawing.Color.Red
            };
            y += 40;

            Label lblSoyad = new Label { Text = "Soyad:", Left = 20, Top = y, Width = 120 };
            txtSoyad = new TextBox { Left = 150, Top = y, Width = 300, Text = soyad };
            y += 40;

            Label lblEposta = new Label { Text = "E-posta:", Left = 20, Top = y, Width = 120 };
            txtEposta = new TextBox { Left = 150, Top = y, Width = 300, Text = eposta };
            y += 40;

            Label lblTelefon = new Label { Text = "Telefon:", Left = 20, Top = y, Width = 120 };
            txtTelefon = new TextBox { Left = 150, Top = y, Width = 300, Text = telefon };
            y += 40;

            Label lblRutbe = new Label { Text = "Rütbe:", Left = 20, Top = y, Width = 120 };
            txtRutbe = new TextBox { Left = 150, Top = y, Width = 300, Text = rutbe };
            y += 50;

            btnKaydet = new Button
            {
                Text = "Güncelle",
                Left = 150,
                Top = y,
                Width = 120,
                Height = 40,
                BackColor = System.Drawing.Color.LightBlue
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
                lblInfo, lblSoyad, txtSoyad, lblEposta, txtEposta,
                lblTelefon, txtTelefon, lblRutbe, txtRutbe, btnKaydet, btnCikis
            });
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                    string.IsNullOrWhiteSpace(txtEposta.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefon.Text) ||
                    string.IsNullOrWhiteSpace(txtRutbe.Text))
                {
                    MessageBox.Show("Tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool sonuc = KullaniciManager.PilotGuncelle(
                    _pilotId,
                    txtSoyad.Text.Trim(),
                    txtEposta.Text.Trim().ToLower(),
                    txtTelefon.Text.Trim(),
                    txtRutbe.Text.Trim()
                );

                if (sonuc)
                {
                    MessageBox.Show("Pilot başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
