using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyon
{
    public partial class Form7 : Form
    {
        int hastaid;
        string h_ad, h_soyad;
        decimal h_tcno;
        taniService taniService = new taniService();
        public Form7(int hastaid, string h_ad,string h_soyad,decimal h_tcno)
        {
            InitializeComponent();
            this.hastaid = hastaid;
            this.h_ad = h_ad;
            this.h_soyad = h_soyad;
            this.h_tcno = h_tcno;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            label1.Text = h_ad;
            label2.Text = h_soyad;
            label3.Text = Convert.ToString(h_tcno);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string taniVerisi = richTextBox1.Text;
            
            taniService.taniKaydet(hastaid, taniVerisi);
            yazdır();
        }
        public void yazdır()
        {
            string outputFilePath = "C:\\Users\\Emek\\Documents\\tani.pdf";
            using (var writer = new PdfWriter(outputFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    document.Add(new Paragraph("Emek Hastanesİ"));
                    document.Add(new Paragraph("TANI RAPORU"));
                    document.Add(new Paragraph("ADI: " + h_ad + " SOYADI: "+h_soyad));
                    document.Add(new Paragraph("TC KİMLİK NUMARASI: " + h_tcno));
                    document.Add(new Paragraph("TANINIZ: " + richTextBox1.Text));
                    document.Add(new Paragraph(Convert.ToString(DateTime.Now)));
                }
            }
            MessageBox.Show("PDF dosyası başarıyla oluşturuldu!");
        }
    }
}
    
