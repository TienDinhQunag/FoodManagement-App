using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlymonan1
{
    public partial class Add : Form
    {

        private BUSMonan busMonan;
        public Add()
        {
            this.BackColor = Color.FromArgb(219, 224, 243);

            InitializeComponent();
            busMonan = new BUSMonan();
            LoadTrangthaiOptions();

            LoadLoaimonanOptions(); 

        }

        private void Add_Load(object sender, EventArgs e)
        {
   
       

        }
        private void LoadTrangthaiOptions()
        {
            // Add Đang bán and Hết bán directly to the combo box
            cbTrangthai.Items.Add("Đang bán");
            cbTrangthai.Items.Add("Hết bán");

            // Optionally, set the default selection
            cbTrangthai.SelectedIndex = 0;
        }

        private void LoadLoaimonanOptions()
        {
            // Gọi method getloaimonanoption từ bus
            var loaimonanOptions = busMonan.GetLoaimonanOptions();


            // thêm loại món ăn vào combobox
            if (loaimonanOptions != null && loaimonanOptions.Count > 0)
            {
                cbLoaimonan.DataSource = loaimonanOptions;
                cbLoaimonan.SelectedIndex = 0;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // thu thập thông tin người dùng
            MonAnDTO newFood = new MonAnDTO
            {
                Tenmonan = txtTenmonan.Text,
                Mota = txtMota.Text,
                Gia = decimal.Parse(txtGia.Text.Trim(), System.Globalization.NumberStyles.AllowDecimalPoint),

                // Đưa đến đường dẫn chứa ảnh
                Hinhanh = Path.Combine(@"C:\Users\dinhq\Desktop\UEH\Phat trien ung dung desktop\Dish\", txtHinhanh.Text.Trim() + ".png"),

                Loaimonan = cbLoaimonan.Text,
                TrangThai = cbTrangthai.Text
            };

            //Hiển thị message box hỏi người dùng có xác nhận thêm mới món ăn không
            DialogResult result = MessageBox.Show("Bạn có muốn thêm mới món ăn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // kiểm tra lựa chọn của người dùng
            if (result == DialogResult.Yes)
            {
               
                busMonan.AddNewFood(newFood);

               
                this.Close();
            }
          
        }


    }


}
