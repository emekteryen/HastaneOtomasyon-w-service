using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneOtomasyon
{
    public interface IdoktorSservice
    {
        
    }
    public class doktorSservice : IdoktorSservice
    {
        private static string constr = "server=localhost;database=hastane;uid=root;pwd=;";
        /*public List<doktorSservice> doktorBilgi()
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
        }*/
    }
}
