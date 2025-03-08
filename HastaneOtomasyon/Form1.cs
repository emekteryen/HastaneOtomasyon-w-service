using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HastaneOtomasyon
{
    public partial class Form1 : Form
    {
        private readonly string connectionStr = "server=localhost;database=hastane;user=root;pwd=";
        doktorSservice doktorSservice = new doktorSservice();
        decimal tcno;
        string sifre,doktorad,doktorsoyad,bolumadi;
        int doktor_id,bolum;
        
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            giris();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public async void giris()
        {
            tcno =Convert.ToDecimal(textBox1.Text);
            sifre = textBox2.Text;
            doktorlar doktor = doktorSservice.doktorBilgi(tcno, sifre);
            doktor_id =doktor.doktor_id;
            //doktor_id = 1;
            bolum = doktor.bolum;
            doktorsoyad = doktor.soyad;
            doktorad = doktor.ad;
            bolumadi = doktor.bolumadi;
            MessageBox.Show("Hoþgeldiniz");
            Form2 form2 = new Form2(doktor_id,doktorad,doktorsoyad, bolum, bolumadi);
            form2.ShowDialog();
            this.Close();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
