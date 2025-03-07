using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneOtomasyon
{
    public interface IkisiService
    {
        Hasta HastaGetir(int hastaid);
        List<Hasta> hastaListe(int bolum);
        List<Hasta> hastaAra(int bolum, int veri);

    }
    public class kisiService : IkisiService
    {
        private static string constr = "server=localhost;database=hastane;user=root;pwd=;";
        int hastaid;
        public List<Hasta> kisigetir;


        public Hasta HastaGetir(int hastaid)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(constr);
                connection.Open();

                string query = "SELECT * FROM hastalar WHERE hasta_id=@hasta_id";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@hasta_id", hastaid);

                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Hasta
                    {
                        ad = reader["ad"].ToString(),
                        soyad = reader["soyad"].ToString(),
                        tc_no = decimal.Parse(reader["tc_no"].ToString())
                    };

                }
                else { return null; }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); return null;
            }

        }
        public List<Hasta> hastaAra(int bolum,int veri)
        {
            List<Hasta> hastaara = new List<Hasta>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(constr))
                {
                    conn.Open();
                    string query = "SELECT * FROM hastalar WHERE bolum_id = @bolum_id AND CAST(tc_no AS CHAR) LIKE @tcno";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bolum_id", bolum);
                        cmd.Parameters.AddWithValue("@tcno", "%" +veri + "%");
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Hasta hasta = new Hasta
                                {
                                    hasta_id = rdr.GetInt32("hasta_id"),
                                    ad = rdr.GetString("ad"),
                                    soyad = rdr.GetString("soyad"),
                                    tc_no = rdr.GetDecimal("tc_no")
                                };
                                hastaara.Add(hasta);
                            }
                            return hastaara;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return null;
            }
        }
        public void hastakaydet(string ad, string soyad, decimal tc_no,int bolum)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query = "insert into hastalar (ad,soyad,tc_no,bolum_id,yatis_tarihi) values" +
                        " (@ad,@soyad,@tc_no,@bolum_id,@yatis_tarihi)";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ad", ad);
                        cmd.Parameters.AddWithValue("@soyad", soyad);
                        cmd.Parameters.AddWithValue("@tc_no", tc_no);
                        cmd.Parameters.AddWithValue("@bolum_id", bolum);
                        cmd.Parameters.AddWithValue("@yatis_tarihi", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata " + ex.Message);
            }
        }
        public List<Hasta> hastaListe(int bolum)
        {
            List<Hasta> hastalar = new List<Hasta>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query = "select hasta_id,ad,soyad,tc_no from hastalar where bolum_id=@bolum_id";
                    using (MySqlCommand cmd3 = new MySqlCommand(query, con))
                    {
                        cmd3.Parameters.AddWithValue("@bolum_id", bolum);
                        
                        using (MySqlDataReader dr = cmd3.ExecuteReader())
                        {
                            //List<Hasta> hastalar = new List<Hasta>();
                            while (dr.Read())
                            {
                                Hasta hastane = new Hasta
                                {
                                    hasta_id = dr.GetInt32("hasta_id"),
                                    ad = dr.GetString("ad"),
                                    soyad = dr.GetString("soyad"),
                                    tc_no = dr.GetDecimal("tc_no")
                                };
                                hastalar.Add(hastane);
                            }
                        }
                    }
                    return hastalar;
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı göster
                MessageBox.Show(ex.Message);
                return null;  // Eğer hata varsa null döndürüyoruz
            }
            
        }

    }
}
