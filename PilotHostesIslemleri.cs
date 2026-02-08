using System;
using System.Data;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class PilotHostesIslemleriForm : Form
    {
        public PilotHostesIslemleriForm()
        {
            InitializeComponent();
            this.Text = "Pilot ve Hostes İşlemleri";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeUI();
        }

        private void InitializeUI()
        {
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // TAB 1: Pilot İşlemleri
            TabPage tabPilot = new TabPage("Pilot İşlemleri");
            InitializePilotTab(tabPilot);

            // TAB 2: Hostes İşlemleri
            TabPage tabHostes = new TabPage("Hostes İşlemleri");
            InitializeHostesTab(tabHostes);

            tabControl.TabPages.Add(tabPilot);
            tabControl.TabPages.Add(tabHostes);
            this.Controls.Add(tabControl);
        }

        private void InitializePilotTab(TabPage tab)
        {
            DataGridView dgvPilotlar = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Button btnYenile = new Button
            {
                Text = "Listeyi Yenile",
                Left = 20,
                Top = 420,
                Width = 150,
                Height = 40
            };

            Button btnPilotEkle = new Button
            {
                Text = "Yeni Pilot Ekle",
                Left = 200,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.LightGreen
            };

            Button btnPilotGuncelle = new Button
            {
                Text = "Seçili Pilotu Güncelle",
                Left = 380,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.LightBlue
            };

            Button btnPilotSil = new Button
            {
                Text = "Seçili Pilotu Sil",
                Left = 560,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.IndianRed
            };

            btnYenile.Click += (s, e) =>
            {
                try
                {
                    dgvPilotlar.DataSource = KullaniciManager.PilotlariListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnPilotEkle.Click += (s, e) =>
            {
                using (var form = new PilotEkleForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dgvPilotlar.DataSource = KullaniciManager.PilotlariListele();
                    }
                }
            };

            btnPilotGuncelle.Click += (s, e) =>
            {
                if (dgvPilotlar.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek pilotu seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int pilotId = Convert.ToInt32(dgvPilotlar.SelectedRows[0].Cells["Id"].Value);
                string soyad = dgvPilotlar.SelectedRows[0].Cells["Soyad"].Value.ToString();
                string eposta = dgvPilotlar.SelectedRows[0].Cells["Eposta"].Value.ToString();
                string telefon = dgvPilotlar.SelectedRows[0].Cells["TelefonNumarasi"].Value.ToString();
                string rutbe = dgvPilotlar.SelectedRows[0].Cells["Rutbe"].Value.ToString();

                using (var form = new PilotGuncelle(pilotId, soyad, eposta, telefon, rutbe))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dgvPilotlar.DataSource = KullaniciManager.PilotlariListele();
                    }
                }
            };

            btnPilotSil.Click += (s, e) =>
            {
                if (dgvPilotlar.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek pilotu seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult cevap = MessageBox.Show("Pilotu silmek istediğinizden emin misiniz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {
                    try
                    {
                        int pilotId = Convert.ToInt32(dgvPilotlar.SelectedRows[0].Cells["Id"].Value);
                        bool sonuc = KullaniciManager.PilotSil(pilotId);

                        if (sonuc)
                        {
                            MessageBox.Show("Pilot başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvPilotlar.DataSource = KullaniciManager.PilotlariListele();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            tab.Controls.AddRange(new Control[] { dgvPilotlar, btnYenile, btnPilotEkle, btnPilotGuncelle, btnPilotSil });

            // İlk yüklemede listeyi doldur
            dgvPilotlar.DataSource = KullaniciManager.PilotlariListele();
        }

        private void InitializeHostesTab(TabPage tab)
        {
            DataGridView dgvHostesler = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Button btnYenile = new Button
            {
                Text = "Listeyi Yenile",
                Left = 20,
                Top = 420,
                Width = 150,
                Height = 40
            };

            Button btnHostesEkle = new Button
            {
                Text = "Yeni Hostes Ekle",
                Left = 200,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.LightGreen
            };

            Button btnHostesGuncelle = new Button
            {
                Text = "Seçili Hostesi Güncelle",
                Left = 380,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.LightBlue
            };

            Button btnHostesSil = new Button
            {
                Text = "Seçili Hostesi Sil",
                Left = 560,
                Top = 420,
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.IndianRed
            };

            btnYenile.Click += (s, e) =>
            {
                try
                {
                    dgvHostesler.DataSource = KullaniciManager.HostesleriListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnHostesEkle.Click += (s, e) =>
            {
                using (var form = new HostesEkleForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dgvHostesler.DataSource = KullaniciManager.HostesleriListele();
                    }
                }
            };

            btnHostesGuncelle.Click += (s, e) =>
            {
                if (dgvHostesler.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek hostesi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int hostesId = Convert.ToInt32(dgvHostesler.SelectedRows[0].Cells["Id"].Value);
                string soyad = dgvHostesler.SelectedRows[0].Cells["Soyad"].Value.ToString();
                string eposta = dgvHostesler.SelectedRows[0].Cells["Eposta"].Value.ToString();
                string telefon = dgvHostesler.SelectedRows[0].Cells["TelefonNumarasi"].Value.ToString();
                string dilBecerileri = dgvHostesler.SelectedRows[0].Cells["DilBecerileri"].Value.ToString();

                using (var form = new HostesGuncelle(hostesId, soyad, eposta, telefon, dilBecerileri))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dgvHostesler.DataSource = KullaniciManager.HostesleriListele();
                    }
                }
            };

            btnHostesSil.Click += (s, e) =>
            {
                if (dgvHostesler.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek hostesi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult cevap = MessageBox.Show("Hostesi silmek istediğinizden emin misiniz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {
                    try
                    {
                        int hostesId = Convert.ToInt32(dgvHostesler.SelectedRows[0].Cells["Id"].Value);
                        bool sonuc = KullaniciManager.HostesSil(hostesId);

                        if (sonuc)
                        {
                            MessageBox.Show("Hostes başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvHostesler.DataSource = KullaniciManager.HostesleriListele();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            tab.Controls.AddRange(new Control[] { dgvHostesler, btnYenile, btnHostesEkle, btnHostesGuncelle, btnHostesSil });

            // İlk yüklemede listeyi doldur
            dgvHostesler.DataSource = KullaniciManager.HostesleriListele();
        }
    }
}
    

   