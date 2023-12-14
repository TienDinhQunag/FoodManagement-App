using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALMonAn
    {
        private string connectionString = "Data Source=LAPTOPNAYCUATIE;Initial Catalog=QLYBANHANG;Integrated Security=True";
        // HÀM ĐỂ LẤY THÔNG TIN MÓN ĂN HIỂN THỊ RA MENU
        public List<MonAnDTO> GetFoodData() //Tạo một list món ăn DTO
        {
            List<MonAnDTO> foodData = new List<MonAnDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT IDMonan, Tenmonan, Mota, Gia, Hinhanh, Loaimonan, Trangthai FROM MonAn";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) // Đọc dữ liệu
                        {
                            MonAnDTO food = new MonAnDTO();
                            food.IDMonAn = Convert.ToInt32(reader["IDMonan"]);
                            food.Tenmonan = reader["Tenmonan"].ToString();
                            food.Mota = reader["Mota"].ToString();
                            food.Gia = Convert.ToDecimal(reader["Gia"]);
                            food.Hinhanh = reader["Hinhanh"].ToString();
                            food.Loaimonan = reader["Loaimonan"].ToString();
                            food.TrangThai = reader["Trangthai"].ToString();

                            foodData.Add(food);
                        }
                    }
                }
            }

            return foodData;
        }
        public List<MonAnDTO> GetAvailableFoodData()
        {
            List<MonAnDTO> availableFoodData = new List<MonAnDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT IDMonan, Tenmonan, Mota, Gia, Hinhanh, Loaimonan, Trangthai FROM MonAn WHERE Trangthai = N'Đang bán'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonAnDTO food = new MonAnDTO();
                            food.IDMonAn = Convert.ToInt32(reader["IDMonan"]);
                            food.Tenmonan = reader["Tenmonan"].ToString();
                            food.Mota = reader["Mota"].ToString();
                            food.Gia = Convert.ToDecimal(reader["Gia"]);
                            food.Hinhanh = reader["Hinhanh"].ToString();
                            food.Loaimonan = reader["Loaimonan"].ToString();
                            food.TrangThai = reader["Trangthai"].ToString();

                            availableFoodData.Add(food);
                        }
                    }
                }
            }

            return availableFoodData;
        }



        // HÀM ĐỂ LẤY CHI TIẾT THÔNG TIN MÓN ĂN
        public MonAnDTO GetFoodDetails(int foodId) 
        {
            MonAnDTO foodDetails = new MonAnDTO();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT IDMonan, Tenmonan, Mota, Gia, Hinhanh, Loaimonan, Trangthai FROM MonAn WHERE IDMonan = {foodId}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foodDetails.IDMonAn = Convert.ToInt32(reader["IDMonan"]);
                            foodDetails.Tenmonan = reader["Tenmonan"].ToString();
                            foodDetails.Mota = reader["Mota"].ToString();
                            foodDetails.Gia = Convert.ToDecimal(reader["Gia"]);
                            foodDetails.Hinhanh = reader["Hinhanh"].ToString();
                            foodDetails.Loaimonan = reader["Loaimonan"].ToString();
                            foodDetails.TrangThai = reader["Trangthai"].ToString();
                        }
                    }
                }
            }

            return foodDetails;
        }

        public void UpdateFoodDetails(MonAnDTO updatedFood) // Hàm để cập nhật thông tin món ăn
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"UPDATE MonAn SET Tenmonan = @Tenmonan, Mota = @Mota, Gia = @Gia, Hinhanh = @Hinhanh, Loaimonan = @Loaimonan, Trangthai = @Trangthai WHERE IDMonan = @IDMonan";

                using (SqlCommand command = new SqlCommand(query, connection)) // Sử dụng SqlCommand
                {
                    command.Parameters.AddWithValue("@IDMonan", updatedFood.IDMonAn);
                    command.Parameters.AddWithValue("@Tenmonan", updatedFood.Tenmonan);
                    command.Parameters.AddWithValue("@Mota", updatedFood.Mota);
                    command.Parameters.AddWithValue("@Gia", updatedFood.Gia);
                    command.Parameters.AddWithValue("@Hinhanh", updatedFood.Hinhanh);
                    command.Parameters.AddWithValue("@Loaimonan", updatedFood.Loaimonan);
                    command.Parameters.AddWithValue("@Trangthai", updatedFood.TrangThai);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<MonAnDTO> SearchFoodData(string keyword) // Hàm để hiển thị theo kết quả search
        {
            List<MonAnDTO> searchResult = new List<MonAnDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT IDMonan, Tenmonan, Mota, Gia, Hinhanh, Loaimonan, Trangthai FROM MonAn WHERE Tenmonan LIKE '%{keyword}%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonAnDTO food = new MonAnDTO();
                            food.IDMonAn = Convert.ToInt32(reader["IDMonan"]);
                            food.Tenmonan = reader["Tenmonan"].ToString();
                            food.Mota = reader["Mota"].ToString();
                            food.Gia = Convert.ToDecimal(reader["Gia"]);
                            food.Hinhanh = reader["Hinhanh"].ToString();
                            food.Loaimonan = reader["Loaimonan"].ToString();
                            food.TrangThai = reader["Trangthai"].ToString();

                            searchResult.Add(food);
                        }
                    }
                }
            }

            return searchResult;
        }
        public void InsertNewFood(MonAnDTO newFood) // hàm để thêm món ăn mới
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO MonAn (Tenmonan, Mota, Gia, Hinhanh, Loaimonan, Trangthai) 
                         VALUES (@Tenmonan, @Mota, @Gia, @Hinhanh, @Loaimonan, @Trangthai)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tenmonan", newFood.Tenmonan);
                    command.Parameters.AddWithValue("@Mota", newFood.Mota);
                    command.Parameters.AddWithValue("@Gia", newFood.Gia);
                    command.Parameters.AddWithValue("@Hinhanh", newFood.Hinhanh);
                    command.Parameters.AddWithValue("@Loaimonan", newFood.Loaimonan);
                    command.Parameters.AddWithValue("@Trangthai", newFood.TrangThai);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetLoaimonanOptions() // hàm để select ra loại món ăn 
        {
            List<string> loaimonanOptions = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT DISTINCT Loaimonan FROM MonAn";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string loaimonan = reader["Loaimonan"].ToString();
                            loaimonanOptions.Add(loaimonan);
                        }
                    }
                }
            }

            return loaimonanOptions;
        }
        public List<MenuDTO> GetMenuData()
        {
            List<MenuDTO> menuData = new List<MenuDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT IDMenu, TenMenu, Ngaycapnhat FROM Menu";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MenuDTO menu = new MenuDTO();
                            menu.IDMenu = Convert.ToInt32(reader["IDMenu"]);
                            menu.TenMenu = reader["TenMenu"].ToString();
                            menu.Ngaycapnhat = Convert.ToDateTime(reader["Ngaycapnhat"]);

                            menuData.Add(menu);
                        }
                    }
                }
            }

            return menuData;
        }

        public List<Tuple<int, int, string>> GetMenuItems(int menuId)
        {
            List<Tuple<int, int, string>> menuItems = new List<Tuple<int, int, string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT ct.IDMenu, ct.IDMonAn, ma.Tenmonan FROM Chitietmenu ct JOIN MonAn ma ON ct.IDMonAn = ma.IDMonan WHERE ct.IDMenu = {menuId}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idMenu = Convert.ToInt32(reader["IDMenu"]);
                            int idMonAn = Convert.ToInt32(reader["IDMonAn"]);
                            string tenMonAn = reader["Tenmonan"].ToString();

                            menuItems.Add(new Tuple<int, int, string>(idMenu, idMonAn, tenMonAn));
                        }
                    }
                }
            }

            return menuItems;

        }
        public void AddFoodToMenu(int menuId, int foodId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Cập nhật ngày cập nhật trong bảng Menu
                string updateMenuQuery = $"UPDATE Menu SET Ngaycapnhat = @NgayCapNhat WHERE IDMenu = @IDMenu";
                using (SqlCommand updateMenuCommand = new SqlCommand(updateMenuQuery, connection))
                {
                    updateMenuCommand.Parameters.AddWithValue("@IDMenu", menuId);
                    updateMenuCommand.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
                    updateMenuCommand.ExecuteNonQuery();
                }

                // Thêm mới món ăn vào chi tiết menu
                string insertChiTietMenuQuery = "INSERT INTO Chitietmenu (IDMenu, IDMonAn) VALUES (@IDMenu, @IDMonAn)";
                using (SqlCommand command = new SqlCommand(insertChiTietMenuQuery, connection))
                {
                    command.Parameters.AddWithValue("@IDMenu", menuId);
                    command.Parameters.AddWithValue("@IDMonAn", foodId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFoodFromMenu(int menuId, int foodId)
        {

            // Cập nhật ngày cập nhật trong bảng Menu

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Thực hiện câu lệnh SQL để xóa món ăn khỏi menu
                string query = $"DELETE FROM Chitietmenu WHERE IDMenu = {menuId} AND IDMonAn = {foodId}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                string updateMenuQuery = $"UPDATE Menu SET Ngaycapnhat = @NgayCapNhat WHERE IDMenu = @IDMenu";
                using (SqlCommand updateMenuCommand = new SqlCommand(updateMenuQuery, connection))
                {
                    updateMenuCommand.Parameters.AddWithValue("@IDMenu", menuId);
                    updateMenuCommand.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
                    updateMenuCommand.ExecuteNonQuery();
                }
            }


        }











    }
}
