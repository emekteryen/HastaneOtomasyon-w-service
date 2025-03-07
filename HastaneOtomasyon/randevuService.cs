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
    public interface IrandevuService
    {
        DataTable randevuGetir(int hastaid);
    }
    public class randevuService : IrandevuService
    {
        private static string constr = "server=localhost;database=hastane;uid=root;pwd=;";
        public DataTable randevuGetir(int hastaid)
        {
            DataTable dt = new DataTable();
            try
            {
                using MySqlConnection conn = new MySqlConnection(constr);
                {
                    conn.Open();
                    string query = "select d.ad,d.soyad,r.randevu_tarihi,b.bolum_adi,d.ad from randevular r" +
                        " join doktorlar d on" +
                        " d.doktor_id=r.doktor_id join bolumler b on b.bolum_id=r.bolum_id where r.hasta_id=@hasta_id and " +
                        "r.randevu_tarihi >@randevu_tarihi order by randevu_tarihi";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@hasta_id", hastaid);
                        cmd.Parameters.AddWithValue("@randevu_tarihi", DateTime.Now);
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
        public void randevual(int bolum, int doktor_id,int hastaid,DateTime randevusaat)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query2 = "select * from randevular where randevu_tarihi=@randevu_tarihi and doktor_id=@doktor_id";
                    using (MySqlCommand cmnd = new MySqlCommand(query2, con))
                    {
                        cmnd.Parameters.AddWithValue("@randevu_tarihi", randevusaat);
                        cmnd.Parameters.AddWithValue("@doktor_id", doktor_id);
                        using MySqlDataReader reader = cmnd.ExecuteReader();
                        if (reader.Read()) { MessageBox.Show(randevusaat + " tarihinde zaten randevu verdiniz! Başka tarih seçin."); return; }
                    }

                    string query = "INSERT INTO randevular (bolum_id, doktor_id, hasta_id, randevu_tarihi) " +
                                   "VALUES (@bolum_id, @doktor_id, @hasta_id, @randevu_tarihi)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@bolum_id", bolum);
                        cmd.Parameters.AddWithValue("@doktor_id", doktor_id);
                        cmd.Parameters.AddWithValue("@hasta_id", hastaid);
                        cmd.Parameters.AddWithValue("@randevu_tarihi", randevusaat);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Randevu başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Randevu eklenirken hata oluştu.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void randevuSilme(DateTime tarih)
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    con.Open();
                    string query = "delete from randevular where randevu_tarihi=@randevu_tarihi";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@randevu_tarihi", tarih);
                        int sonuc = cmd.ExecuteNonQuery(); // Komutu çalıştır

                        if (sonuc > 0)
                            MessageBox.Show("Randevu başarıyla silindi.");
                        else
                            MessageBox.Show("Silinecek randevu bulunamadı.");
                    }
                }
                //dataGridView1.Rows.Clear();

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
