// uçakSistemi\HostesForm.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class HostesForm : Form
    {
        private List<Ucus> _ucuslar = new List<Ucus>();
        private Dictionary<Ucus, string> _iptalNedenleri = new Dictionary<Ucus, string>();
        private Dictionary<string, List<Ucus>> _hostesUcusAtamasi = new Dictionary<string, List<Ucus>>();
        private Hostes _currentHostes;

        public HostesForm()
        {
            InitializeComponent();            
            btnKisiselBilgiler.Click += btnKisiselBilgiler_Click; // Kişisel Bilgilerim
            btnBilgilerimiGuncelle.Click += btnBilgileriGuncelle_Click; // Bilgilerimi Güncelle

            SeedSampleData();
        }

        private void HostesForm_Load(object sender, EventArgs e)
        {
            // Eğer Form açılırken Tag ile Hostes objesi gelmişse kullanılır, yoksa örnek bir Hostes oluştur
            if (this.Tag is Hostes h) _currentHostes = h;
            else
            {
                _currentHostes = new Hostes("HST999", 0, "Türkçe") { Ad = "Örnek", Soyad = "Hostes", Eposta = "hostes@ornek.com", TelefonNumarasi = "0000000000" };
            }
        }

        private void SeedSampleData()
        {
            var u1 = new Ucus("İstanbul", DateTime.Now.AddDays(-30).AddHours(9), "Ankara", DateTime.Now.AddDays(-30).AddHours(10), TimeSpan.FromHours(1), "A1", "08:30", 9, 200m, "ÖrnekAir");
            var u2 = new Ucus("İstanbul", DateTime.Now.AddDays(2).AddHours(14), "İzmir", DateTime.Now.AddDays(2).AddHours(15), TimeSpan.FromHours(1), "B3", "13:30", 13, 300m, "ÖrnekAir");
            var u3 = new Ucus("Ankara", DateTime.Now.AddDays(4).AddHours(12), "İstanbul", DateTime.Now.AddDays(4).AddHours(13), TimeSpan.FromHours(1), "C2", "11:30", 11, 260m, "ÖrnekAir");
            var u4 = new Ucus("İstanbul", DateTime.Now.AddDays(8).AddHours(10), "Adana", DateTime.Now.AddDays(8).AddHours(11), TimeSpan.FromHours(1), "D4", "09:30", 15, 350m, "ÖrnekAir");

            _ucuslar.AddRange(new[] { u1, u2, u3, u4 });

            _iptalNedenleri[u3] = "Hava koşulları nedeniyle iptal edildi (admin notu).";

            _hostesUcusAtamasi["HST999"] = new List<Ucus> { u1, u2, u3 };
            _hostesUcusAtamasi["HST100"] = new List<Ucus> { u4 };
        }

        //  Hostese ait tüm uçuşları görüntüleme ve önümüzdeki 1 haftayı görüntüleme>
        private void btnUcuslariGoruntule_Click(object sender, EventArgs e)
        {
            var myKey = _currentHostes?.HostesNumarasi ?? "HST999";
            var assigned = _hostesUcusAtamasi.ContainsKey(myKey) ? _hostesUcusAtamasi[myKey] : new List<Ucus>();

            using (var f = new Form())
            {
                f.Text = "Uçuşlarım";
                f.Width = 900;
                f.Height = 520;

                var rbAll = new RadioButton { Text = "Tüm Uçuşlar", Left = 10, Top = 10, Checked = true };
                var rbNextWeek = new RadioButton { Text = "Önümüzdeki 1 Hafta", Left = 130, Top = 10 };

                var lblTotal = new Label { Left = 300, Top = 12, Width = 300, Text = "" };
                var lblNextWeekCount = new Label { Left = 600, Top = 12, Width = 250, Text = "" };

                var lv = new ListView { Left = 10, Top = 40, Width = 860, Height = 420, View = View.Details, FullRowSelect = true };
                lv.Columns.Add("Kalkış"); lv.Columns.Add("Varış"); lv.Columns.Add("Kalkış Tarihi"); lv.Columns.Add("Varış Tarihi");
                lv.Columns.Add("Uçak Kodu"); lv.Columns.Add("İptal Nedeni");

                Action refresh = () =>
                {
                    lv.Items.Clear();
                    IEnumerable<Ucus> list = assigned;
                    var now = DateTime.Now;
                    var weekEnd = now.AddDays(7);

                    // toplam atanan uçuş sayısı
                    var totalCount = assigned.Count;
                    // önümüzdeki 1 haftadaki uçuş sayısı
                    var nextWeekCount = assigned.Count(u => u.KalkısTarihi >= now && u.KalkısTarihi <= weekEnd);

                    if (rbNextWeek.Checked)
                    {
                        list = list.Where(u => u.KalkısTarihi >= now && u.KalkısTarihi <= weekEnd);
                    }

                    foreach (var uc in list.OrderBy(u => u.KalkısTarihi))
                    {
                        string neden = _iptalNedenleri.ContainsKey(uc) ? _iptalNedenleri[uc] : "";
                        var item = new ListViewItem(new[]
                        {
                            uc.KalkisNoktasi,
                            uc.VarisNoktasi,
                            uc.KalkısTarihi.ToString(),
                            uc.VarisTarihi.ToString(),
                            uc.UcakKodu.ToString(),
                            neden
                        });
                        item.Tag = uc;
                        lv.Items.Add(item);
                    }

                    lblTotal.Text = $"Toplam Atanan Uçuş: {totalCount}";
                    lblNextWeekCount.Text = $"Önümüzdeki 1 Hafta: {nextWeekCount}";
                };

                rbAll.CheckedChanged += (s, ea) => refresh();
                rbNextWeek.CheckedChanged += (s, ea) => refresh();

                refresh();

                var lblInfo = new Label { Left = 10, Top = 470, Width = 860, Text = "Not: Uçuş iptalleri yalnızca admin tarafından yapılır; iptal nedenleri listede görünür." };

                f.Controls.AddRange(new Control[] { rbAll, rbNextWeek, lblTotal, lblNextWeekCount, lv, lblInfo });
                f.ShowDialog();
            }
        }

        // Hostesin kişisel bilgilerini görüntüleme
        private void btnKisiselBilgiler_Click(object sender, EventArgs e)
        {
            using (var f = new Form())
            {
                f.Text = "Kişisel Bilgilerim (Görüntüleme)";
                f.Width = 400;
                f.Height = 300;

                var y = 10;
                void AddLabel(string title, string value)
                {
                    var lblTitle = new Label { Left = 10, Top = y, Width = 120, Text = title };
                    var lblValue = new TextBox { Left = 140, Top = y - 3, Width = 220, Text = value, ReadOnly = true };
                    f.Controls.Add(lblTitle);
                    f.Controls.Add(lblValue);
                    y += 30;
                }

                AddLabel("Ad:", _currentHostes?.Ad ?? "");
                AddLabel("Soyad:", _currentHostes?.Soyad ?? "");
                AddLabel("E-posta:", _currentHostes?.Eposta ?? "");
                AddLabel("Telefon:", _currentHostes?.TelefonNumarasi ?? "");
                AddLabel("Hostes No:", _currentHostes?.HostesNumarasi ?? "");
                AddLabel("Dil Becerileri:", _currentHostes?.DilBecerileri ?? "");

                f.ShowDialog();
            }
        }

        // Hostesin bilgilerini güncelleme 
        private void btnBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            using (var f = new Form())
            {
                f.Text = "Bilgilerimi Güncelle";
                f.Width = 400;
                f.Height = 320;

                var lblAd = new Label { Text = "Ad:", Left = 10, Top = 10 };
                var tbAd = new TextBox { Left = 120, Top = 8, Width = 200, Text = _currentHostes?.Ad ?? "", Enabled = false }; // Ad değiştirilemez
                var lblSoyad = new Label { Text = "Soyad:", Left = 10, Top = 50 };
                var tbSoyad = new TextBox { Left = 120, Top = 48, Width = 200, Text = _currentHostes?.Soyad ?? "" };
                var lblEposta = new Label { Text = "E-posta:", Left = 10, Top = 90 };
                var tbEposta = new TextBox { Left = 120, Top = 88, Width = 200, Text = _currentHostes?.Eposta ?? "" };
                var lblTel = new Label { Text = "Telefon:", Left = 10, Top = 130 };
                var tbTel = new TextBox { Left = 120, Top = 128, Width = 200, Text = _currentHostes?.TelefonNumarasi ?? "" };
                var lblDil = new Label { Text = "Dil Becerileri:", Left = 10, Top = 170 };
                var tbDil = new TextBox { Left = 120, Top = 168, Width = 200, Text = _currentHostes?.DilBecerileri ?? "" };

                var btnSave = new Button { Text = "Kaydet", Left = 120, Top = 210, Width = 100 };
                btnSave.Click += (s, ea) =>
                {
                    if (_currentHostes != null)
                    {
                        _currentHostes.Soyad = tbSoyad.Text;
                        _currentHostes.Eposta = tbEposta.Text;
                        _currentHostes.TelefonNumarasi = tbTel.Text;
                        _currentHostes.DilBecerileri = tbDil.Text;
                        MessageBox.Show("Bilgiler güncellendi.");
                        f.Close();
                    }
                };

                f.Controls.AddRange(new Control[] { lblAd, tbAd, lblSoyad, tbSoyad, lblEposta, tbEposta, lblTel, tbTel, lblDil, tbDil, btnSave });
                f.ShowDialog();
            }
        }
    }
}