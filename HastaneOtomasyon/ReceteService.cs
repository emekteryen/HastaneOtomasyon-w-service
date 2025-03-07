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
    public interface IReceteService
    {
        List<ilaclar> ilacBul(string veri);
        DataTable ilacGetir(int hastaid);
    }
    public class ReceteService : IReceteService
    {
        private readonly string constr = "server=localhost;database=hastane;user=root;pwd=;";
        public List<ilaclar> ilacBul(string veri)
        {
            List<ilaclar> ilaclarlist = new List<ilaclar>();
            try
            {
                using MySqlConnection connection = new MySqlConnection(constr);
                {
                    connection.Open();
                    string query = "SELECT * FROM ilaclar WHERE ilac_ad LIKE @ilacad";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ilacad", "%" + veri + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                ilaclar ilac = new ilaclar
                                {
                                    ilac_id = reader.GetInt32(0),
                                    ilac_ad = reader.GetString(1),
                                };
                                ilaclarlist.Add(ilac);
                            }

                        }
                    }
                }
                return ilaclarlist;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void ilacKaydet(int hastaid, DataGridView dataGridView)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(constr);
                {
                    conn.Open();
                    string query = "insert into recete (ilac_id, ilac_adet,hasta_id,recete_tarihi) values (@ilac_id,@ilac_adet,@hasta_id,@recete_tarihi)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                int ilac_id = Convert.ToInt32(row.Cells["ilac_id2"].Value);
                                int ilac_adet = Convert.ToInt32(row.Cells["ilac_adet2"].Value);
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("hasta_id", hastaid);
                                cmd.Parameters.AddWithValue("ilac_id", ilac_id);
                                cmd.Parameters.AddWithValue("ilac_adet", ilac_adet);
                                cmd.Parameters.AddWithValue("recete_tarihi", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public DataTable ilacGetir(int hastaid)
        {
            DataTable dt = new DataTable();
            try
            {
                using MySqlConnection con = new MySqlConnection(constr);
                {
                    con.Open();
                    string query = "select h.ad, h.soyad, i.ilac_ad, r.ilac_adet, r.recete_tarihi" +
                        " from recete r join hastalar h on r.hasta_id=h.hasta_id join ilaclar i on r.ilac_id=i.ilac_id where r.hasta_id=@hasta_id" +
                        " order by recete_tarihi";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@hasta_id", hastaid);
                        cmd.Parameters.AddWithValue("@recete_tarihi", DateTime.Now);
                        using (MySqlDataAdapter da = new MySqlDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            
                            da.Fill(dt);
                        }
                        return dt;
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
