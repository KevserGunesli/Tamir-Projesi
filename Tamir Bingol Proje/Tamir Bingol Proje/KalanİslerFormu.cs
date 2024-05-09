using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Tamir_Bingol_Proje.Tamir;

namespace Tamir_Bingol_Proje
{
    public partial class KalanİslerFormu : Form
    {
        public KalanİslerFormu()
        {
            InitializeComponent();
        }

        private void KalanİslerFormu_Load(object sender, EventArgs e)
        {
            // Kalan işleri rich text boxlara yazdır
            YazdirKalanIsler();
        }

        private void YazdirKalanIsler()
        {
            // Tamirde kalan işleri rich text boxa yazdır
            foreach (Is isNesnesi in TamirdeKalanİsPaketleri)
            {
                richtxtTamirIsler.AppendText($"Paket ID: {isNesnesi.PaketID}, Arıza Türleri: {isNesnesi.ArizaTurleri}, Tamir Türleri: {isNesnesi.TamirTurleri}, Tamir Süresi: {isNesnesi.TamirSuresi}, Geliş Saati: {isNesnesi.GelisSaatiHour}:{isNesnesi.GelisSaatiMinute}, Çıkış Saati: {isNesnesi.CikisSaati}\n");
            }

            // Dağıtım ünitesindeki işleri rich text boxa yazdır
            foreach (Is isNesnesi in DagitimUnitesindekiİsPaketleri)
            {
                richtxtDagitimIsler.AppendText($"Paket ID: {isNesnesi.PaketID}, Arıza Türleri: {isNesnesi.ArizaTurleri}, Tamir Türleri: {isNesnesi.TamirTurleri}, Tamir Süresi: {isNesnesi.TamirSuresi}, Geliş Saati: {isNesnesi.GelisSaatiHour}:{isNesnesi.GelisSaatiMinute}, Çıkış Saati: {isNesnesi.CikisSaati}\n");
            }

            // Biten işleri rich text boxa yazdır
            foreach (Is isNesnesi in BitenİsPaketleri)
            {
                richtxtBitenIsler.AppendText($"Paket ID: {isNesnesi.PaketID}, Arıza Türleri: {isNesnesi.ArizaTurleri}, Tamir Türleri: {isNesnesi.TamirTurleri}, Tamir Süresi: {isNesnesi.TamirSuresi}, Geliş Saati: {isNesnesi.GelisSaatiHour}:{isNesnesi.GelisSaatiMinute}, Çıkış Saati: {isNesnesi.CikisSaati}\n");
            }
        }
    }
}
