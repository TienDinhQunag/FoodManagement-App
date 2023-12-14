using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlymonan1
{

    public partial class DishDetail : Form
    {

        private BUSMonan busMonan;
        private MonAnDTO detailedFood;

        // Tạo một hàm dựng MonAnDTO
        public DishDetail(MonAnDTO detailedFood)
        {
            this.BackColor = Color.FromArgb(219, 224, 243);
            InitializeComponent();
            busMonan = new BUSMonan();
            this.detailedFood = detailedFood;
            LoadLoaimonanOptions();
            InitializeForm();


 
        }
        private void InitializeForm()
        {
            // Thêm 2 trạng thái cho combo box trạng thái

            cbTrangthai.Items.Add("Đang bán");
            cbTrangthai.Items.Add("Hết bán");
            // Dùng 'detailedFood' để fill dữ liệu 
            txtTenmonan.Text = detailedFood.Tenmonan;
            txtMota.Text = detailedFood.Mota;
            txtGia.Text = detailedFood.Gia.ToString("C0");
            cbTrangthai.Text = detailedFood.TrangThai;
            cbLoaimonan.Text = detailedFood.Loaimonan;
            txtHinhanh.Text = detailedFood.Hinhanh;

            //Đường dẫn đến folder ảnh
            string imagePath = $"C:\\Users\\dinhq\\Desktop\\UEH\\Phat trien ung dung desktop\\Dish\\{detailedFood.Hinhanh}.png";
            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    pictureBoxDishDetail.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Nếu ảnh không tồn tại thì xử lý thế nào
                    pictureBoxDishDetail.Image = null;
                }
            }
            catch (Exception ex)
            {
                // Kiểm tra lỗi
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
        }

        private void DishDetail_Load(object sender, EventArgs e)
        {

        }
        private void LoadLoaimonanOptions()
        {
            // Gọi method getloaimonanoption từ busw
            var loaimonanOptions = busMonan.GetLoaimonanOptions();

            // thêm loại món ăn vào combobox
            if (loaimonanOptions != null && loaimonanOptions.Count > 0)
            {
                cbLoaimonan.DataSource = loaimonanOptions;

            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                // Chuyển định dạng chuẩn cho txtGia
                if (!decimal.TryParse(txtGia.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal gia))
                {
                    MessageBox.Show("Please enter a valid numeric value for Gia.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật giá trị theo thông tin được nhập trên textBox
                detailedFood.Tenmonan = txtTenmonan.Text;
                detailedFood.Mota = txtMota.Text;
                detailedFood.Gia = gia;
                detailedFood.TrangThai = cbTrangthai.Text;
                detailedFood.Loaimonan = cbLoaimonan.Text;
                detailedFood.Hinhanh = txtHinhanh.Text;

                // Hỏi Người dùng có muốn lưu hay không
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Tạo Object busMonan
                    BUSMonan busMonan = new BUSMonan();

                    // Gọi method của bus
                    busMonan.UpdateFoodDetails(detailedFood);

                    // Nếu yes thì show box update thành công
                    MessageBox.Show("Food details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Nếu no thì sẽ hiện thị thông báo thông tin chưa được lưu
                    MessageBox.Show("Changes not saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
