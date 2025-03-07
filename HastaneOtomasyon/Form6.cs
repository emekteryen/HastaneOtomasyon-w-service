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
    public partial class Form6 : Form
    {
        kisiService kisiService=new kisiService();
        private readonly string constr = "server=localhost;database=hastane;user=root;pwd=;";
        int bolum;
        public Form6(int bolum)
        {
            this.bolum = bolum;
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            decimal tc_no = Convert.ToDecimal(textBox3.Text);
            kisiService.hastakaydet(ad, soyad, tc_no, bolum);
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
