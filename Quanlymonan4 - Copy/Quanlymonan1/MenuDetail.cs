using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlymonan1
{
    public partial class MenuDetail : Form
    {
        private int currentPage = 1; // Trang hiện tại
        private int itemsPerPage = 5; // Số lượng món ăn trên mỗi trang
        private List<Tuple<int, int, string>> menuItems; // Tạo list chứa những món ăn có trong menu
        private List<CheckBox> checkBoxes; // Tạo một list chứa những checkbox từ 1 tới 5
        public MenuDetail()
        {
            InitializeComponent();
            checkBoxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
        }


        private void MenuDetail_Load(object sender, EventArgs e)
        {
            // Load dữ liệu món ăn từ BUS
            LoadMenuItems();

            // Hiển thị món ăn trên trang đầu tiên
            DisplayMenuItems();
            LoadAllFoodsToComboBox();
        }


        private void LoadMenuItems() // Load danh sách những món ăn có trong menu
        {
            // Sử dụng BUS để lấy thông tin món ăn
            BUSMonan busMonan = new BUSMonan();
            int menuId = int.Parse(lblIDMenu.Text); // Lấy IDMenu từ Label
            menuItems = busMonan.GetMenuItems(menuId); //gọi phương thức getmenuitems dưới Bus

            // Số lượng checkbox hiện tại
            int currentCheckBoxCount = checkBoxes.Count; 

            // Số lượng món ăn trên trang hiện tại
            int itemsOnCurrentPage = Math.Min(itemsPerPage, menuItems.Count - (currentPage - 1) * itemsPerPage);

            // Nếu số lượng checkbox hiện tại nhỏ hơn số lượng món ăn trên trang, thêm các checkbox mới
            for (int i = currentCheckBoxCount; i < itemsOnCurrentPage; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = $"Checkbox {i + 1}";
               
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                Controls.Add(checkBox);
                checkBoxes.Add(checkBox);
            }

            // Nếu số lượng checkbox hiện tại lớn hơn số lượng món ăn trên trang, ẩn đi các checkbox không cần thiết
            for (int i = itemsOnCurrentPage; i < currentCheckBoxCount; i++)
            {
                checkBoxes[i].Visible = false;
            }

            // Hiển thị lại checkbox tương ứng với số lượng món ăn trên trang hiện tại
            for (int i = 0; i < itemsOnCurrentPage; i++)
            {
                checkBoxes[i].Visible = true;
            }
        }




        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void DisplayMenuItems() //Hiển thị danh sách món ăn có phân trang
        {
            // Xác định vị trí bắt đầu và kết thúc của danh sách món ăn trên trang hiện tại
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage - 1, menuItems.Count - 1);

            // Hiển thị thông tin món ăn lên các Label
            for (int i = 1; i <= itemsPerPage; i++)
            {
                int index = startIndex + i - 1;

                if (index <= endIndex)
                {
                    // Gọi hàm hiển thị thông tin món ăn cho từng Label (lblTenmonan1 đến lblTenmonan5)
                    DisplayMenuItem(i, menuItems[index]);
                }
                else
                {
                    // Nếu không có đủ món ăn để hiển thị, ẩn các Label không cần thiết
                    HideMenuItemLabel(i);
                }
            }

            // Hiển thị thông tin trang hiện tại và tổng số trang trong lblTongsotrang
            lblTongsotrang.Text = $"{currentPage} of {GetTotalPages()}";
        }

        private void DisplayMenuItem(int labelIndex, Tuple<int, int, string> menuItem)
        {
            // Lấy tên Label dựa trên index
            Label label = Controls.Find($"lblTenmonan{labelIndex}", true).FirstOrDefault() as Label;

            // Hiển thị tên món ăn lên Label
            if (label != null)
            {
                label.Text = menuItem.Item3; // Access the third item (string) in the Tuple
                label.Visible = true;
            }
        }


        private void HideMenuItemLabel(int labelIndex)
        {
            // Ẩn Label không cần thiết
            Label label = Controls.Find($"lblTenmonan{labelIndex}", true).FirstOrDefault() as Label;

            if (label != null)
            {
                label.Visible = false;
            }
        }
        public void SetMenuInfo(int menuId, string menuName)
        {
            lblIDMenu.Text = menuId.ToString();
            lblTenMenu.Text = menuName;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < GetTotalPages())
            {
                currentPage++;
                LoadMenuItems();
                DisplayMenuItems();
             
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadMenuItems();
                DisplayMenuItems();
             
            }
        }
        private int GetTotalPages()
        {
            // Tính tổng số trang dựa trên số lượng món ăn và số lượng món ăn trên mỗi trang
            return (int)Math.Ceiling((double)menuItems.Count / itemsPerPage);
        }
        private void LoadAllFoodsToComboBox()
        {
            BUSMonan busMonan = new BUSMonan();
            List<MonAnDTO> availableFoods = busMonan.GetAvailableFoodData();

            // Set the ComboBox data source
            cbMonan.DataSource = availableFoods;
            cbMonan.DisplayMember = "Tenmonan"; // Display member property
            cbMonan.ValueMember = "IDMonAn"; // Value member property
        }


        private void btnAdd_Click(object sender, EventArgs e)
{
    if (cbMonan.SelectedItem != null)
    {
        MonAnDTO selectedFood = (MonAnDTO)cbMonan.SelectedItem;

        // Kiểm tra xem món ăn đã tồn tại trong danh sách hay không
        if (IsFoodAlreadyAdded(selectedFood.IDMonAn))
        {
            MessageBox.Show("Món ăn đã tồn tại trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Hiển thị hộp thoại xác nhận
        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm món ăn mới?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        // Nếu người dùng chọn "Yes", thực hiện thêm món ăn vào danh sách
        if (result == DialogResult.Yes)
        {
            BUSMonan busMonan = new BUSMonan();
            int menuId = int.Parse(lblIDMenu.Text);

            // Cập nhật ngày cập nhật trong bảng Menu
            busMonan.AddFoodToMenu(menuId, selectedFood.IDMonAn, DateTime.Now);

            // Sau khi thêm, cập nhật lại danh sách menuItems
            LoadMenuItems();

            // Hiển thị lại món ăn trên trang hiện tại
            DisplayMenuItems();
        }
    }
}

        private bool IsFoodAlreadyAdded(int foodId) // Viết hàm kiểm tra xem món ăn đã được add chưa
        {
            // Kiểm tra xem món ăn có tồn tại trong danh sách menuItems hay không
            return menuItems.Any(item => item.Item1 == int.Parse(lblIDMenu.Text) && item.Item2 == foodId);
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có ít nhất một checkbox được chọn hay không
            if (checkBoxes.Any(checkBox => checkBox.Checked))
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn "Yes", thực hiện xóa món ăn khỏi danh sách
                if (result == DialogResult.Yes)
                {
                    BUSMonan busMonan = new BUSMonan();

                    for (int i = 0; i < checkBoxes.Count; i++)
                    {
                        if (checkBoxes[i].Checked)
                        {
                            Tuple<int, int, string> menuItem = menuItems[i];
                            busMonan.DeleteFoodFromMenu(menuItem.Item1, menuItem.Item2);
                        }
                    }

                    // Sau khi xóa, cập nhật lại danh sách menuItems
                    LoadMenuItems();

                    // Hiển thị lại món ăn trên trang hiện tại
                    DisplayMenuItems();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món ăn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
