using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyon
{
    public interface ItaniService
    {
        DataTable taniGetir(int hastaid);
    }
    public class taniService : ItaniService
    {
        private static string constr = "server=localhost;database=hastane;uid=root;pwd=;";

        public DataTable taniGetir(int hastaid)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query = "select tani_verisi,tani_tarihi from tani where hasta_id=@hasta_id";
                    using (MySqlCommand cm = new MySqlCommand(query, con))
                    {
                        cm.Parameters.AddWithValue("@hasta_id", hastaid);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cm))
                        {
                            da.Fill(dt);
                        }
                        return dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Tanı getirme başarısız " + ex.Message); return null; }
           
        }
        public static void taniKaydet(int hastaid, string taniVerisi)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query = "INSERT INTO tani (hasta_id, tani_verisi, tani_tarihi) VALUES (@hasta_id, @tani_verisi, @tani_tarihi)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@hasta_id", hastaid);
                        cmd.Parameters.AddWithValue("@tani_verisi", taniVerisi);
                        cmd.Parameters.AddWithValue("@tani_tarihi", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Tanı başarıyla eklendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tanı eklenirken hata oluştu: " + ex.Message);
            }
        }
    }
}
