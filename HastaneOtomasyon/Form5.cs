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
    public partial class Form5 : Form
    {
        DateTime tarih;
        DateTime saat;
        int hastaid;
        int doktor_id;
        int bolum;
        DateTime randevusaat;
        randevuService randevuService = new randevuService();
        public Form5(int hastaid, int doktor_id, int bolum)
        {
            InitializeComponent();
            this.hastaid = hastaid;
            this.doktor_id = doktor_id;
            this.bolum = bolum;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            saatal();
            textBox1.Text = randevusaat.ToString();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            saatal();
            textBox1.Text = randevusaat.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";
            dateTimePicker2.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saatal();
            if(randevusaat < DateTime.Now) { MessageBox.Show("Geçmiş bir tarihe randevu veremezsiniz!");return; }
            randevuService.randevual(bolum ,doktor_id, hastaid, randevusaat);
            this.Close();
        }
        public void saatal()
        {
            tarih = dateTimePicker1.Value.Date;
            saat = dateTimePicker2.Value;
            TimeSpan zaman = new TimeSpan(saat.Hour, saat.Minute, 0);
            randevusaat = tarih.Add(zaman);

        }
    }
}
