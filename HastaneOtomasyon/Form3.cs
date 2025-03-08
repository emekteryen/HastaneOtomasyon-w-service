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
    public partial class Form3 : Form
    {
        int hastaid;
        taniService taniService = new taniService();
        kisiService kisiService = new kisiService();
        ReceteService receteService = new ReceteService();
        randevuService randevuService = new randevuService();
        public Form3(int hastaid)
        {
            InitializeComponent();
            this.hastaid = hastaid;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            label4.Text = "randevular";
            label5.Text = "reçeteler";
            hastabilgi();
            randevugetir();
            recetegetir();
            tanigetir();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int secim = dataGridView1.CurrentCell.RowIndex;
                randevusil(secim);

            }
            else { MessageBox.Show("Hata"); }
        }
        public void hastabilgi()
        {
            Hasta hasta = kisiService.HastaGetir(hastaid);
            label1.Text = hasta.ad;
            label2.Text = hasta.soyad;
            label3.Text = Convert.ToString(hasta.tc_no);
        }
        public void randevugetir()
        {
            DataTable randevugetir = randevuService.randevuGetir(hastaid);
            dataGridView1.DataSource = randevugetir;
        }
        public void recetegetir()
        {
            DataTable receteliste = receteService.ilacGetir(hastaid);
            dataGridView2.DataSource = receteliste;
        }
        public void randevusil(int secim)
        {
            DateTime tarih = Convert.ToDateTime(dataGridView1.Rows[secim].Cells["randevu_tarihi"].Value.ToString());
            randevuService.randevuSilme(tarih);
            dataGridView1.DataSource = null;
            randevugetir();

        }
        public void tanigetir()
        {
            DataTable tanilar = taniService.taniGetir(hastaid);
            dataGridView3.DataSource = tanilar;
        }
    }
}