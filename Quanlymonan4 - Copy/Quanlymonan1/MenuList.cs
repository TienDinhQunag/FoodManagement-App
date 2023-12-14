using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlymonan1
{
    public partial class MenuList : Form
    {
        private List<MenuDTO> menuData;
        private int itemsPerPage = 5;
        private int currentPage = 1;

        public MenuList()
        {
            InitializeComponent();

        }
        BUSMonan busMenu = new BUSMonan();
        private void ClearLabels()
        {
            // Build label names dynamically and clear text
            for (int i = 1; i <= itemsPerPage; i++)
            {
                string idLabelName = $"lblID{i}";
                string menuNameLabelName = $"lblMenuName{i}";
                string dateLabelName = $"lblDate{i}";

                Label idLabel = Controls.Find(idLabelName, true).FirstOrDefault() as Label;
                Label menuNameLabel = Controls.Find(menuNameLabelName, true).FirstOrDefault() as Label;
                Label dateLabel = Controls.Find(dateLabelName, true).FirstOrDefault() as Label;

                idLabel.Text = "";
                menuNameLabel.Text = "";
                dateLabel.Text = "";
            }
        }
        private void LoadMenuData()
        {
          
            // Lấy danh sách menu từ BUS
            List<MenuDTO> menuData = busMenu.GetMenuData();

            // Tính tổng số trang
            int totalPages = (int)Math.Ceiling((double)menuData.Count / itemsPerPage);

            // Kiểm tra trang hiện tại để đảm bảo nó không vượt quá tổng số trang
            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            // Hiển thị số trang hiện tại và tổng số trang
            lblSotrang.Text = $"Trang {currentPage} / {totalPages}";

            // Lấy sublist của menuData cho trang hiện tại
            List<MenuDTO> currentPageData = menuData
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            // Hiển thị dữ liệu trong các label tương ứng
            for (int i = 0; i < currentPageData.Count; i++)
            {
                Label lblID = Controls.Find($"lblID{i + 1}", true).FirstOrDefault() as Label;
                Label lblMenuName = Controls.Find($"lblMenuName{i + 1}", true).FirstOrDefault() as Label;
                Label lblDate = Controls.Find($"lblDate{i + 1}", true).FirstOrDefault() as Label;

                if (lblID != null && lblMenuName != null && lblDate != null)
                {
                    lblID.Text = currentPageData[i].IDMenu.ToString();
                    lblMenuName.Text = currentPageData[i].TenMenu;
                    lblDate.Text = currentPageData[i].Ngaycapnhat.ToString("dd/MM/yyyy");
                }
            }
        }


        private void MenuList_Load(object sender, EventArgs e)
        {
            LoadMenuData();
            
        }

       

      




        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadMenuData();
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)busMenu.GetMenuData().Count / itemsPerPage);

            if (currentPage < totalPages)
            {
                currentPage++;
                LoadMenuData();
            }
        }

        private void btnDoubleNext_Click(object sender, EventArgs e)
        {
            // Di chuyển đến trang sau 2 trang nếu có
            int totalPages = (int)Math.Ceiling((double)busMenu.GetMenuData().Count / itemsPerPage);

            if (currentPage + 1 < totalPages)
            {
                currentPage += 2;
                LoadMenuData();
            }
        }

        private void btnDoubleBack_Click(object sender, EventArgs e)
        {
            // Di chuyển đến trang trước 2 trang nếu có
            if (currentPage - 2 > 0)
            {
                currentPage -= 2;
                LoadMenuData();
            }
        }

        private void btnDetail1_Click(object sender, EventArgs e)
        {
            ShowMenuDetail(1);
        }

        private void btnDetail2_Click(object sender, EventArgs e)
        {
            ShowMenuDetail(2);
        }

        private void btnDetail3_Click(object sender, EventArgs e)
        {
            ShowMenuDetail(3);
        }

        private void btnDetail4_Click(object sender, EventArgs e)
        {
            ShowMenuDetail(4);
        }

        private void btnDetail5_Click(object sender, EventArgs e)
        {
            ShowMenuDetail(5);
        }
        private void ShowMenuDetail(int buttonNumber)
        {
            // Lấy ID và tên của Menu từ dữ liệu tương ứng với số thứ tự của nút
            int menuIndex = (currentPage - 1) * itemsPerPage + buttonNumber - 1;

            if (menuIndex >= 0 && menuIndex < busMenu.GetMenuData().Count)
            {
                int menuId = busMenu.GetMenuData()[menuIndex].IDMenu;
                string menuName = busMenu.GetMenuData()[menuIndex].TenMenu;

                // Tạo và hiển thị form MenuDetail và truyền tham số
                MenuDetail menuDetailForm = new MenuDetail();
                menuDetailForm.SetMenuInfo(menuId, menuName);
                menuDetailForm.ShowDialog();
            }
        }

        private void MenuList_Load_1(object sender, EventArgs e)
        {
            LoadMenuData();
        }
    }
}
