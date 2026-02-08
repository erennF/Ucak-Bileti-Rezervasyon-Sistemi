using System;
using System.Windows.Forms;

namespace uçakSistemi
{
    // Lab-3: ": Form" kalıtımı Dispose hatasını çözer
    public partial class HostesGuncelle : Form
    {
        private int _hostesId;
        private string soyad;
        private string eposta;
        private string telefon;
        private string dilBecerileri;

        public HostesGuncelle(int hostesId, string soyad, string eposta, string telefon, string dilBecerileri)
        {
            _hostesId = hostesId;
            this.soyad = soyad;
            this.eposta = eposta;
            this.telefon = telefon;
            this.dilBecerileri = dilBecerileri;
        }

        // Formu açarken 5 parametre gönderdiğiniz için bu imzayı kullanıyoruz
        public HostesGuncelle(int id, string ad, string soyad, string eposta, string telefon, string dil)
        {
            InitializeComponent(); // Bu çağrı 'bağlamda yok' hatasını çözer
            _hostesId = id;
            txtAd.Text = ad;
            txtSoyad.Text = soyad;
            txtEposta.Text = eposta;
            txtTelefon.Text = telefon;
            txtDil.Text = dil;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL Güncelleme işlemi
                bool sonuc = KullaniciManager.HostesGuncelle(
                    _hostesId,
                    txtAd.Text.Trim(),
                    txtSoyad.Text.Trim(),
                    txtEposta.Text.Trim(),
                    txtTelefon.Text.Trim(),
                    txtDil.Text.Trim()
                );

                if (sonuc)
                {
                    MessageBox.Show("Hostes bilgileri başarıyla güncellendi.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
        }
    }
}