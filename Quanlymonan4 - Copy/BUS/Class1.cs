using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSDonHang
    {


        private DALDSDH dALDSDH = new DALDSDH();

        public DataTable DonHang()
        {
            try
            {
                // Gọi hàm từ DAL để lấy dữ liệu
                DataTable dt = dALDSDH.DonHang();

                // Kiểm tra xem dữ liệu có tồn tại không
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Nếu có dữ liệu, trả về DataTable
                    return dt;
                }
                else
                {
                    // Nếu không có dữ liệu, có thể trả về null hoặc DataTable trống tuỳ thuộc vào yêu cầu của bạn.
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                // Có thể log lỗi, thông báo người dùng, hoặc xử lý tùy thuộc vào yêu cầu của bạn.
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<DonHangDTO> TimIDDonHang(string IDDonHang)
        {
            try
            {
                // Gọi phương thức TimDH từ DALDSDH
                List<DonHangDTO> resultList = dALDSDH.TimDH(IDDonHang);

                return resultList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TimIDDonHang: " + ex.Message);
                return null;
            }
        }

        public bool InsertDonHang(DonHangDTO sp)
        {
            if (sp != null)
            {
                sp.IDDonHang = sp.IDDonHang;
                sp.IDKhachhang = sp.IDKhachhang;
                sp.IDKM2 = sp.IDKM2;
                sp.Ngaygiaohang = sp.Ngaygiaohang;
                sp.Trangthaidonhang = sp.Trangthaidonhang;
                sp.Ngatdathang = sp.Ngatdathang;
                sp.Tongdonhang = sp.Tongdonhang;

                return dALDSDH.InsertDonHang(sp);
            }
            else
            {
                Console.WriteLine("Error: InsertDonHang - DonHangDTO is null");
                return false;
            }
        }

        public bool HuyDonHang(int idDonHang)
        {
            try
            {
                return dALDSDH.HuyDonHang(idDonHang);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in HuyDonHang: " + ex.Message);
                return false;
            }
        }
        public DataTable LayDanhSachDonHangBanDau()
        {
            return dALDSDH.LayDanhSachDonHangBanDau();
        }



    }

}