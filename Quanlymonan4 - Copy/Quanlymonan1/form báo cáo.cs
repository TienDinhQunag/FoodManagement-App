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
    public partial class DishMenu : Form
    {
        private BUSMonan busMonan;
        private List<MonAnDTO> allFoodData;
        private int currentPage = 1;
        private int itemsPerPage = 6;
        public DishMenu()
        {
            InitializeComponent();
            busMonan = new BUSMonan();
            this.BackColor = Color.FromArgb(219, 224, 243);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                allFoodData = busMonan.GetFoodData();
            }
            else
            {
                allFoodData = busMonan.SearchFoodData(txtSearch.Text);
            }

            currentPage = 1;
            DisplayCurrentPage();
        }







        private void DisplayCurrentPage()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage - 1, allFoodData.Count - 1);

            // Xóa thông tin trước khi sang trang mới
            for (int i = 1; i <= itemsPerPage; i++)
            {
                Label currentLabelMonAn = (Label)this.Controls.Find("lblmonan" + i, true).FirstOrDefault();
                Label currentLabelGia = (Label)this.Controls.Find("lblGia" + i, true).FirstOrDefault();
                PictureBox currentPictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true).FirstOrDefault();
                Button currentBtnDetail = (Button)this.Controls.Find("btnDetail" + i, true).FirstOrDefault();

                if (currentLabelMonAn != null && currentLabelGia != null && currentPictureBox != null && currentBtnDetail != null)
                {
                    currentLabelMonAn.Text = "";
                    currentLabelGia.Text = "";
                    currentPictureBox.Image = null;

                    // Check nếu label có text. Không có thì ẩn nút detail đi
                    if (string.IsNullOrWhiteSpace(currentLabelMonAn.Text) && string.IsNullOrWhiteSpace(currentLabelGia.Text))
                    {
                        currentBtnDetail.Visible = false;
                    }
                    else
                    {
                        currentBtnDetail.Visible = true;
                    }
                }
            }

            // hiển thị label và ảnh từ trang hiện có
            for (int i = startIndex; i <= endIndex; i++)
            {
                int labelIndex = i - startIndex + 1; 

                Label currentLabelMonAn = (Label)this.Controls.Find("lblmonan" + labelIndex, true).FirstOrDefault();
                Label currentLabelGia = (Label)this.Controls.Find("lblGia" + labelIndex, true).FirstOrDefault();
                PictureBox currentPictureBox = (PictureBox)this.Controls.Find("pictureBox" + labelIndex, true).FirstOrDefault();
                Button currentBtnDetail = (Button)this.Controls.Find("btnDetail" + labelIndex, true).FirstOrDefault();

                if (currentLabelMonAn != null && currentLabelGia != null && currentPictureBox != null && currentBtnDetail != null)
                {
                    currentLabelMonAn.Text = allFoodData[i].Tenmonan;
                    currentLabelGia.Text = allFoodData[i].Gia.ToString("C0");

                    // Folder chứa ảnh
                    string imagePath = Path.Combine(@"C:\Users\dinhq\Desktop\UEH\Phat trien ung dung desktop\Dish\", allFoodData[i].Hinhanh);

                    // Thêm định dạng png cho ảnh
                    imagePath = Path.ChangeExtension(imagePath, "png");

                    if (System.IO.File.Exists(imagePath))
                    {
                        currentPictureBox.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        currentPictureBox.Image = null;
                    }

                    // Check label
                    if (string.IsNullOrWhiteSpace(currentLabelMonAn.Text) && string.IsNullOrWhiteSpace(currentLabelGia.Text))
                    {
                        currentBtnDetail.Visible = false;
                    }
                    else
                    {
                        currentBtnDetail.Visible = true;
                    }
                }
            }


            UpdatePaginationControls(); // Hàm để tính số trang 
        }






        private int CalculateTotalPages()
        {
            return (int)Math.Ceiling((double)allFoodData.Count / itemsPerPage); 
            // phân trang bằng cách lấy tổng sản phẩm chia cho sản phẩm trên trang
        }
        private void UpdatePaginationControls()
        {
            lblCurrentPage.Text = $"Page {currentPage} of {CalculateTotalPages()}";
            btnBack.Enabled = (currentPage > 1);
            btnNext.Enabled = (currentPage < CalculateTotalPages());
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < CalculateTotalPages())
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void btnDoubleNext_Click(object sender, EventArgs e)
        {
            int totalPages = CalculateTotalPages();

            if (currentPage + 2 <= totalPages)
            {
                currentPage += 2;
                DisplayCurrentPage();
            }
            else if (currentPage + 1 <= totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }

        private void btnDoubleBack_Click(object sender, EventArgs e)
        {
            if (currentPage - 2 >= 1)
            {
                currentPage -= 2;
                DisplayCurrentPage();
            }
            else if (currentPage - 1 >= 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }



        private void ShowDishDetail(int selectedIndex)
        {
            //Lấy món ăn được chọn từ list
            MonAnDTO selectedFood = allFoodData.ElementAtOrDefault(currentPage * itemsPerPage - itemsPerPage + selectedIndex - 1);


            // gọi hàm bus để lấy thông tin chi tiết món ăn
            MonAnDTO detailedFood = busMonan.GetFoodDetails(selectedFood.IDMonAn);

            // Hiển thị dish detail
            DishDetail dishDetailForm = new DishDetail(detailedFood);
            dishDetailForm.ShowDialog();
        }
        private void btnDetail1_Click(object sender, EventArgs e)
        {
            ShowDishDetail(1);
        }

        private void btnDetail2_Click(object sender, EventArgs e)
        {
            ShowDishDetail(2);
        }

        private void btnDetail3_Click(object sender, EventArgs e)
        {
            ShowDishDetail(3);
        }

        private void btnDetail4_Click(object sender, EventArgs e)
        {
            ShowDishDetail(4);
        }

        private void btnDetail5_Click(object sender, EventArgs e)
        {
            ShowDishDetail(5);
        }

        private void btnDetail6_Click(object sender, EventArgs e)
        {
            ShowDishDetail(6);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    allFoodData = busMonan.GetFoodData();
                }
                else
                {
                    allFoodData = busMonan.SearchFoodData(txtSearch.Text);
                }

                currentPage = 1;
                DisplayCurrentPage();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Xóa nội dung của ô tìm kiếm
            txtSearch.Text = "";

            // Load lại dữ liệu ban đầu
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Mở add form lên
            Add addForm = new Add();
            addForm.ShowDialog();

            //Sau khi đóng form add thì sẽ load lại data món ăn
            LoadData();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuList menuListForm = new MenuList();
            menuListForm.Show();
        }
    }
}
