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
    public partial class Form2 : Form
    {
        private string connectionString = "server=localhost;database=hastane;user=root;pwd=;";
        int doktor_id, bolum, hastaid, rowIndex;
        string h_ad,h_soyad;
        decimal h_tcno;
        kisiService kisiService = new kisiService();
        public Form2(int doktor_id)
        {
            InitializeComponent();
            this.doktor_id = doktor_id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            doktorbilgi();           
            dataGridView1.AutoGenerateColumns = true;
            hastaListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                secilenhasta(rowIndex);
                receteac(hastaid,h_ad,h_soyad,h_tcno);
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                secilenhasta(rowIndex);
                hastabilgi(hastaid);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir satır seçin");
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                secilenhasta(rowIndex);
                randevual(hastaid);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir satır seçiniz");
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {          
            hastaekle();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null)
            {
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                secilenhasta(rowIndex);
                taniyaz(hastaid,h_ad,h_soyad,h_tcno);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (textBox1.Text == "") { hastaListele();return; }
            hastaara();

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void secilenhasta(int rowIndex)
        {
            hastaid = int.Parse(dataGridView1.Rows[rowIndex].Cells["hasta_id"].Value.ToString());
            h_ad = dataGridView1.Rows[rowIndex].Cells["ad"].Value.ToString();
            h_soyad = dataGridView1.Rows[rowIndex].Cells["soyad"].Value.ToString();
            h_tcno = decimal.Parse(dataGridView1.Rows[rowIndex].Cells["tc_no"].Value.ToString());
        }
        public void hastabilgi(int hastaid)
        {
            Form3 form3 = new Form3(hastaid);
            form3.ShowDialog();
        }
        public void receteac(int hastaid,string h_ad,string h_soyad,decimal h_tcno)
        {
            Form4 form4 = new Form4(hastaid,h_ad,h_soyad,h_tcno);
            form4.ShowDialog();
        }
        public void randevual(int hastaid)
        {
            Form5 form5 = new Form5(hastaid, bolum, doktor_id);
            form5.ShowDialog();
        }
        public void hastaekle()
        {
            Form6 form6 = new Form6(bolum);
            form6.ShowDialog();
        }
        public void taniyaz(int hastaid, string h_ad, string h_soyad,decimal h_tcno)
        {
            Form7 form7 = new Form7(hastaid,h_ad,h_soyad,h_tcno);
            form7.ShowDialog();
        }
        public void hastaara()
        {
            int veri = Convert.ToInt32(textBox1.Text.Trim());
            List<Hasta> hastaara = kisiService.hastaAra(bolum, veri);
            //kisiService.hastaAra(bolum,veri);
            foreach (var hasta in hastaara)
            {
                dataGridView1.Rows.Add(hasta.hasta_id, hasta.ad, hasta.soyad, hasta.tc_no);
            }
        }
        public void hastaListele()
        {
            List<Hasta> hastalar = kisiService.hastaListe(bolum);
            foreach (var hasta in hastalar)
            {
                dataGridView1.Rows.Add(hasta.hasta_id, hasta.ad, hasta.soyad, hasta.tc_no);
            }
        }
        public void doktorbilgi()
        {
            try
            {
                using (MySqlConnection con5 = new MySqlConnection(connectionString))
                {
                    con5.Open();
                    string query = "select d.ad, d.soyad, d.bolum_id, b.bolum_adi from doktorlar d join bolumler b on d.bolum_id = b.bolum_id where doktor_id=@doktor_id";
                    using (MySqlCommand cmd = new MySqlCommand(query, con5))
                    {
                        cmd.Parameters.AddWithValue("@doktor_id", doktor_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label1.Text = reader["ad"].ToString();
                                label2.Text = reader["soyad"].ToString();
                                label3.Text = reader["bolum_adi"].ToString();
                                bolum = int.Parse(reader["bolum_id"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
