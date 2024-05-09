using System.Windows.Forms;
using Tamir_Bingol_Proje;
using static Tamir_Bingol_Proje.Tamir;

namespace Tamir_Bingol_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTamir_Click(object sender, EventArgs e)
        {
            Tamir.TamirBirimiOku();
            //TamirBirimleriniGoster();
            btnTamir.Enabled = false;
            btnIsPaketi.Enabled = true;

        }

        private void btnIsPaketi_Click(object sender, EventArgs e)
        {
            Tamir.IsPaketiOku();
            btnIsPaketi.Enabled = false;
            btnServis.Enabled = true;
        }

        private void btnServis_Click(object sender, EventArgs e)
        {

            Tamir.IsPaketleriniSiralaVeIDVer();

            // Ýþ paketlerini richTextBox'a yazdýr
            richtxtbox.AppendText("Ýþ Paketleri:\n");
            Tamir.Is isPaket = Tamir.Isler;
            while (isPaket != null)
            {
                richtxtbox.AppendText($"Paket ID: {isPaket.PaketID}, Arýza Türleri: {isPaket.ArizaTurleri}, Tamir Türleri: {isPaket.TamirTurleri}, Tamir Süresi: {isPaket.TamirSuresi}, Geliþ Saati: {isPaket.GelisSaatiHour}.{isPaket.GelisSaatiMinute}, Çýkýþ Saati: {isPaket.CikisSaati}\n");
                isPaket = isPaket.Next;
            }

            // Tamir birimlerini richTextBox'a yazdýr
            richtxtbox.AppendText("\nTamir Birimleri:\n");
            foreach (var birim in Tamir.TamirBirimleri)
            {
                richtxtbox.AppendText($"Birim ID: {birim.TamirBirimID}, Çalýþan Sayýsý: {birim.CalisanSayisi}, Kapasite: {birim.Kapasite}\n");
            }

            // Ýþ paketlerini ve tamir birimlerini göster
            TamirBirimleriniGoster();

            // Servis baþlatýldýðýnda iþleri daðýtým ünitesine ve tamir birimlerine daðýt


            while (Tamir.ServisSaati< new DateTime(1, 1, 1, 19, 0, 0))
            {
                if (Tamir.DagitilmaDurumu())
                {
                   Tamir.IsleriBirimlereDagit();
                   Tamir.TamireBasla();
                }
                else
                {
                    Tamir.DagitimUnitesineÝsGonder();
                }
            }
            KalanÝsleriBul();
            MessageBox.Show("Servis Saati Dolmuþtur.");

            KalanÝslerFormu kalanÝslerFormu = new KalanÝslerFormu();
            kalanÝslerFormu.ShowDialog();

            

            btnServis.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnIsPaketi.Enabled=false;
            btnServis.Enabled=false;
        }

        public void TamirBirimleriniGoster()
        {
            int x = 10; // Baþlangýç X konumu
            int y = 10; // Baþlangýç Y konumu
            int padding = 5; // Kontroller arasý boþluk
            int birimlerPerRow = 2; // Her satýrda gösterilecek tamir birimi sayýsý

            for (int i = 0; i < TamirBirimleri.Length; i++)
            {
                // Tamir birimi için panel oluþtur
                Panel panelTamirBirimi = new Panel
                {
                    Location = new Point(x + (i % birimlerPerRow) * (200 + padding), y + (i / birimlerPerRow) * (200 + padding)),
                    Size = new Size(200, 250),
                    BorderStyle = BorderStyle.FixedSingle
                };
                this.Controls.Add(panelTamirBirimi);

                TamirBirimi birim = TamirBirimleri[i];

                // Tamir birimi baþlýðý oluþtur
                Label lblBirimBaslik = new Label
                {
                    Text = $"{birim.TamirBirimID} Nolu Tamir Birimi",
                    Location = new Point(padding, padding),
                    Size = new Size(panelTamirBirimi.Width - 2 * padding, 20),
                    Font = new Font("Arial", 12, FontStyle.Bold)
                };
                panelTamirBirimi.Controls.Add(lblBirimBaslik);

                // Ýþ paketleri için panel oluþtur
                Panel panelIsPaketleri = new Panel
                {
                    Location = new Point(padding, lblBirimBaslik.Bottom + padding),
                    Size = new Size(panelTamirBirimi.Width - 2 * padding, panelTamirBirimi.Height - lblBirimBaslik.Bottom - 3 * padding),
                    BorderStyle = BorderStyle.None // Ýþ paketleri arasýnda sýnýr olmasýn
                };
                panelTamirBirimi.Controls.Add(panelIsPaketleri);

                // Ýþ paketlerini listele
                for (int j = 0; j < birim.Kapasite; j++)
                {
                    Button btnIs = new Button
                    {
                        Size = new Size(panelIsPaketleri.Width - 2 * padding, 20),
                        Location = new Point(padding, padding + j * (20 + padding)),
                        Font = new Font("Arial", 8)
                    };

                    Label lblIs = new Label
                    {
                        Size = new Size(panelIsPaketleri.Width - 2 * padding, 20),
                        Location = new Point(padding, btnIs.Bottom + padding),
                        Font = new Font("Arial", 8)
                    };

                    if (birim.TamirdeBulunanÝsler[j] != null)
                    {
                        Is isNesnesi = birim.TamirdeBulunanÝsler[j];
                        btnIs.Text = $"Ýþ {j + 1}";
                        lblIs.Text = $"Paket ID: {isNesnesi.PaketID}, Arýza Türleri: {isNesnesi.ArizaTurleri}";

                        // Ýþ paketine týklandýðýnda detaylarý göster
                        btnIs.Tag = isNesnesi;
                        btnIs.Click += (sender, e) =>
                        {
                            Is clickedIs = (Is)((Button)sender).Tag;
                            MessageBox.Show($"Paket ID: {clickedIs.PaketID}\nArýza Türleri: {clickedIs.ArizaTurleri}\nTamir Türleri: {clickedIs.TamirTurleri}\nTamir Süresi: {clickedIs.TamirSuresi}\nGeliþ Saati: {clickedIs.GelisSaatiHour}:{clickedIs.GelisSaatiMinute}\nÇýkýþ Saati: {clickedIs.CikisSaati}");
                        };
                    }
                    else
                    {
                        btnIs.Text = "Boþ";
                        lblIs.Text = "Ýþ Yok";
                        btnIs.Enabled = false; // Boþ iþ paketleri týklanamaz
                    }

                    panelIsPaketleri.Controls.Add(btnIs);
                    panelIsPaketleri.Controls.Add(lblIs);
                }
            }
        }





    }
}
