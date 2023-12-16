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
    public partial class qldh : Form
    {

        BUSDonHang bUSDonHang = new BUSDonHang();
        private DataTable dtDanhSachBanDau;


        public qldh()
        {
            InitializeComponent();
            dtDanhSachBanDau = bUSDonHang.DonHang();
            LoadDS();
        }
        public void LoadDS()
        {
            dataGridViewOrders.DataSource = bUSDonHang.DonHang();
            // Lấy danh sách đơn hàng từ BUS
            DataTable dtDonHang = bUSDonHang.DonHang();


            // Kiểm tra xem danh sách có dữ liệu không
            if (dtDonHang != null)
            {
                // Lưu lại danh sách ban đầu khi load
                dtDanhSachBanDau = dtDonHang.Copy();
                dataGridViewOrders.DataSource = dtDonHang;
            }
            else
            {
                // Hoặc xử lý khác tùy thuộc vào yêu cầu của bạn khi dtDonHang không có dữ liệu.
                MessageBox.Show("Danh sách đơn hàng không tồn tại hoặc trống.");
            }
            if (dtDonHang != null && dtDonHang.Rows.Count > 0)
            {
                // Loại bỏ các đơn hàng đã hủy
                DataRow[] rowsToKeep = dtDonHang.Select("TrangThaiDonHang <> 'Hủy'");
                DataTable dtRemaining = rowsToKeep.CopyToDataTable();

                // Gán danh sách đơn hàng còn lại vào dataGridViewOrders
                dataGridViewOrders.DataSource = dtRemaining;
            }
            else
            {
                // Nếu không có dữ liệu, đặt DataSource thành null hoặc xử lý tùy thuộc vào yêu cầu của bạn.
                dataGridViewOrders.DataSource = null;
            }





        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị IDDonHang từ TextBox txtSearch
            string IDDonHang = txtTimkiem.Text.Trim();

            // Kiểm tra xem IDDonHang có giá trị không trống
            if (!string.IsNullOrEmpty(IDDonHang))
            {
                // Gọi phương thức TimIDDonHang từ BUSDonHang và truyền IDDonHang
                List<DonHangDTO> resultList = bUSDonHang.TimIDDonHang(IDDonHang);

                // Kiểm tra xem có kết quả nào hay không
                if (resultList != null && resultList.Count > 0)
                {
                    // Hiển thị kết quả trong DataGridView
                    dataGridViewOrders.DataSource = resultList;
                }
                else
                {
                    // Hiển thị thông báo nếu không tìm thấy đơn hàng
                    MessageBox.Show("Không tìm thấy đơn hàng với IDDonHang: " + IDDonHang);
                }
            }
            else
            {
                // Hiển thị thông báo nếu IDDonHang trống
                MessageBox.Show("Vui lòng nhập IDDonHang để tìm kiếm.");
            }
            // qlbh.Designer.cs
            // ...
            this.btnSearch.Click += new System.EventHandler(this.btnTimkiem_Click);
            // ...
        }

        

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Lấy ID đơn hàng được chọn từ DataGridView


            // Lấy ID đơn hàng được chọn từ DataGridView
            BUSDonHang busDonHang = new BUSDonHang();

            // Lấy ID đơn hàng từ TextBox
            if (int.TryParse(txtTimkiem.Text, out int idDonHang))
            {
                // Kiểm tra xem có đơn hàng được chọn không
                if (idDonHang != -1)
                {
                    // Gọi phương thức huỷ đơn hàng từ BUS
                    bool result = busDonHang.HuyDonHang(idDonHang);

                    // Kiểm tra và thông báo kết quả
                    if (result)
                    {
                        MessageBox.Show("Hủy đơn hàng thành công!");
                        // Cập nhật lại danh sách đơn hàng (nếu cần)
                        LoadDS();
                    }
                    else
                    {
                        MessageBox.Show("Không thể hủy đơn hàng. Vui lòng thử lại.");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần hủy.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập IDDonHang hợp lệ.");
            }

            // Gọi hàm làm mới danh sách
           
        }
        public void ResetDanhSach()
        {
            try
            {
                // Lấy lại danh sách đơn hàng ban đầu từ BUS
                DataTable dtDonHangBanDau = bUSDonHang.LayDanhSachDonHangBanDau();

                if (dtDonHangBanDau != null)
                {
                    // Cập nhật lại DataSource của DataGridView
                    dataGridViewOrders.DataSource = dtDonHangBanDau.Copy();

                    // Cập nhật lại DataTable dtDanhSachBanDau
                    dtDanhSachBanDau = dtDonHangBanDau.Copy();
                }
                else
                {
                    MessageBox.Show("Không thể lấy lại danh sách ban đầu từ cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ResetDanhSach: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DonHangDTO spNew = new DonHangDTO();

                if (int.TryParse(txtIDDonHang.Text, out int idDonHang) &&
                    int.TryParse(txtKhachHang.Text, out int idKhachHang) &&
                    int.TryParse(txtIDKM2.Text, out int idKM2) &&
                    DateTime.TryParse(txtNdh.Text, out DateTime ngayDatHang) &&
                    DateTime.TryParse(txtNgh.Text, out DateTime ngayGiaoHang) &&
                    int.TryParse(txtTdh.Text, out int tongDonHang))
                {
                    spNew.IDDonHang = idDonHang;
                    spNew.IDKhachhang = idKhachHang;
                    spNew.IDKM2 = idKM2;
                    spNew.Ngatdathang = ngayDatHang;
                    spNew.Trangthaidonhang = txtTtdh.Text;
                    spNew.Ngaygiaohang = ngayGiaoHang;
                    spNew.Tongdonhang = tongDonHang;

                    if (bUSDonHang.InsertDonHang(spNew))
                    {
                        MessageBox.Show($"Them moi {spNew.IDDonHang} thành công.");
                        LoadDS();
                    }
                    else
                    {
                        MessageBox.Show($"Them moi {spNew.IDDonHang} THẤT BẠI.");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại giá trị nhập vào. Có thể có lỗi định dạng hoặc giá trị không hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ResetDanhSach();
        }

        private void qldh_Load(object sender, EventArgs e)
        {
            dtDanhSachBanDau = bUSDonHang.DonHang();

            LoadDS();

            // Thêm sự kiện click cho nút "Load"
           
        }
    }
    }






