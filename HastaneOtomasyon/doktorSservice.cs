using iText.Layout.Element;
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
        //doktorlar doktorbilgi(decimal tcno, string sifre);
    }
    public class doktorSservice : IdoktorSservice
    {
        private static string constr = "server=localhost;database=hastane;uid=root;pwd=;";
        public doktorlar doktorBilgi(decimal tcno,string sifre)
        {
            try
            {
                using (MySqlConnection con5 = new MySqlConnection(constr))
                {
                    con5.Open();
                    string query = "select d.doktor_id, d.ad, d.soyad, d.bolum_id, b.bolum_adi from doktorlar d" +
                        " join bolumler b on d.bolum_id = b.bolum_id where tc_no=@tc_no and sifre = @sifre";
                    using (MySqlCommand cmd = new MySqlCommand(query, con5))
                    {
                        cmd.Parameters.AddWithValue("@tc_no",tcno);
                        cmd.Parameters.AddWithValue("@sifre", sifre);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new doktorlar
                                {
                                    doktor_id = int.Parse(reader["doktor_id"].ToString()),
                                    ad = reader["ad"].ToString(),
                                    soyad = reader["soyad"].ToString(),
                                    bolum = int.Parse(reader["bolum_id"].ToString()),
                                    bolumadi = reader["bolum_adi"].ToString()
                                };  
                            }
                            else
                            {
                                MessageBox.Show("HATA");
                                return null;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
