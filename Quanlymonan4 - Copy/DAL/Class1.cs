using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALDSDH : DBConnect
    {


        // Trong DALDSDH
        public DataTable DonHang()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DonHang", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DonHang: " + ex.Message);
                return null; // Hoặc xử lý ngoại lệ tùy thuộc vào yêu cầu của bạn
            }
            finally
            {
                conn.Close();
            }
        }

        public List<DonHangDTO> LoadDTODonHang(string IDDonHang)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DonHang WHERE IDDonHang = @IDDonHang", conn);
                cmd.Parameters.AddWithValue("@IDDonHang", IDDonHang);

                SqlDataReader reader = cmd.ExecuteReader();
                List<DonHangDTO> list = new List<DonHangDTO>();

                while (reader.Read())
                {
                    DonHangDTO sp = new DonHangDTO();
                    sp.IDDonHang = reader.GetInt32(0);
                    sp.IDKhachhang = reader.GetInt32(1);
                    sp.IDKM2 = reader.GetInt32(2);
                    sp.Ngatdathang = reader.GetDateTime(3);
                    sp.Trangthaidonhang = reader.GetString(4);
                    sp.Ngaygiaohang = reader.GetDateTime(5);
                    sp.Tongdonhang = reader.GetInt32(6);
                    list.Add(sp);
                }

                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LoadDTODonHang: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }


        // Trong DALDSDH
        public List<DonHangDTO> TimDH(string IDDonHang)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DonHang WHERE IDDonHang = @IDDonHang", conn);

                cmd.Parameters.AddWithValue("@IDDonHang", IDDonHang);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<DonHangDTO> resultList = new List<DonHangDTO>();
                foreach (DataRow row in dt.Rows)
                {
                    DonHangDTO sp = new DonHangDTO();
                    sp.IDDonHang = Convert.ToInt32(row["IDDonHang"]);
                    sp.IDKhachhang = Convert.ToInt32(row["IDKhachHang"]);
                    sp.IDKM2 = Convert.ToInt32(row["IDKM2"]);

                    sp.Trangthaidonhang = row["TrangThaiDonHang"].ToString();
                    sp.Ngaygiaohang = Convert.ToDateTime(row["NgayGiaoHang"]);
                    sp.Tongdonhang = Convert.ToInt32(row["TongDonHang"]);
                    resultList.Add(sp);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TimDH: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }


        public bool InsertDonHang(DonHangDTO sp)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Insert into DonHang (IDDonHang, IDKhachHang, IDKM2, Ngaydathang, Trangthaidonhang, Ngaygiaohang, Tongdonhang) values (@IDDonHang, @IDKhachHang, @IDKM2, @Ngaydathang, @Trangthaidonhang, @Ngaygiaohang, @Tongdonhang)", conn);

                cmd.Parameters.Add("@IDDonHang", SqlDbType.Int);
                cmd.Parameters["@IDDonHang"].Value = sp.IDDonHang;
                cmd.Parameters.Add("@IDKhachHang", SqlDbType.Int);
                cmd.Parameters["@IDKhachHang"].Value = sp.IDKhachhang;
                cmd.Parameters.Add("@IDKM2", SqlDbType.Int);
                cmd.Parameters["@IDKM2"].Value = sp.IDKM2;
                cmd.Parameters.Add("@Ngaydathang", SqlDbType.DateTime);
                cmd.Parameters["@Ngaydathang"].Value = sp.Ngatdathang;
                cmd.Parameters.Add("@Trangthaidonhang", SqlDbType.NVarChar);
                cmd.Parameters["@Trangthaidonhang"].Value = sp.Trangthaidonhang;
                cmd.Parameters.Add("@Ngaygiaohang", SqlDbType.DateTime);
                cmd.Parameters["@Ngaygiaohang"].Value = sp.Ngaygiaohang;
                cmd.Parameters.Add("@Tongdonhang", SqlDbType.Int);
                cmd.Parameters["@Tongdonhang"].Value = sp.Tongdonhang;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Console.WriteLine("Index #" + i + "\n" +
                        "Error: " + ex.Errors[i].ToString() + "\n");
                }
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        
        public bool HuyDonHang(int idDonHang)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE DonHang SET TrangThaiDonHang = 'Đã Hủy' WHERE IDDonHang = @IDDonHang", conn);
                cmd.Parameters.AddWithValue("@IDDonHang", idDonHang);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Trả về true nếu có ít nhất một dòng bị ảnh hưởng
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in HuyDonHang DAL: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable LayDanhSachDonHangBanDau()
        {
            try
            {
                conn.Open();
                // Thực hiện truy vấn để lấy danh sách ban đầu từ cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("SELECT * FROM DonHang", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LayDanhSachDonHangBanDau DAL: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }



    }
}


