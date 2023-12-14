using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoanDTO
    {
        private string tenTaiKhoan;
        private string matKhau;
        private string loaiTaiKhoan;

        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public string LoaiTaiKhoan { get => loaiTaiKhoan; set => loaiTaiKhoan = value; }
    }

    public class ThongtinKhachHangDTO
    {
        private int iDKhachHang;
        private string hoTen;
  
        private string diachi;
        private string hinhanh;
        private string tenTaiKhoan;

        public int IDKhachHang { get => iDKhachHang; set => iDKhachHang = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Hinhanh { get => hinhanh; set => hinhanh = value; }
        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
    }
    public class DiaChiDTO
    {
        private int iDDiaChi;
        private int iDKhachHang;
        private string thanhpho;
        private string quan;
        private string phuong;
        private string duong;

        public int IDDiaChi { get => iDDiaChi; set => iDDiaChi = value; }
        public int IDKhachHang { get => iDKhachHang; set => iDKhachHang = value; }
        public string Thanhpho { get => thanhpho; set => thanhpho = value; }
        public string Quan { get => quan; set => quan = value; }
        public string Phuong { get => phuong; set => phuong = value; }
        public string Duong { get => duong; set => duong = value; }
    }
    public class NhanVienDTO
    {
        private int iDNhanVien;
        private string hoTen;
        private int sDT;
        private string tenTaiKhoan;

        public int IDNhanVien { get => iDNhanVien; set => iDNhanVien = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public int SDT { get => sDT; set => sDT = value; }
        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
    }
    public class QuanLyDTO
    {
        private int iDQuanLy;
        private string hoTen;
        private int sDT;
        private string tenTaiKhoan;

        public int IDQuanLy { get => iDQuanLy; set => iDQuanLy = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public int SDT { get => sDT; set => sDT = value; }
        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
    }
    public class MonAnDTO
    {
        private int iDMonAn;
        private string tenmonan;
        private string mota;
        private decimal gia;
        private string trangThai;
        private string loaimonan;
        private string hinhanh;

        public int IDMonAn { get => iDMonAn; set => iDMonAn = value; }
        public string Tenmonan { get => tenmonan; set => tenmonan = value; }
        public string Mota { get => mota; set => mota = value; }
        public decimal Gia { get => gia; set => gia = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string Loaimonan { get => loaimonan; set => loaimonan = value; }
        public string Hinhanh { get => hinhanh; set => hinhanh = value; }
    }
    public class KhuyenmaigiamgiaDTO
    {
        private int iDKM2;
        private string tenkhuyenmai;
        private string mota;
        private float giatriapdung;

        public int IDKM2 { get => iDKM2; set => iDKM2 = value; }
        public string Tenkhuyenmai { get => tenkhuyenmai; set => tenkhuyenmai = value; }
        public string Mota { get => mota; set => mota = value; }
        public float Giatriapdung { get => giatriapdung; set => giatriapdung = value; }
    }
    public class KhuyenmaitangmonDTO
    {
        private int iDKM1;
        private int iDMonAn;
        private string tenkhuyenmai;
        private string mota;

        public int IDKM1 { get => iDKM1; set => iDKM1 = value; }
        public int IDMonAn { get => iDMonAn; set => iDMonAn = value; }
        public string Tenkhuyenmai { get => tenkhuyenmai; set => tenkhuyenmai = value; }
        public string Mota { get => mota; set => mota = value; }
    }
    public class DonHangDTO
    {
        private int iDDonHang;
        private int iDKhachhang;
        private int iDKM2;
        private DateTime ngatdathang;
        private string trangthaidonhang;
        private DateTime ngaygiaohang;
        private int tongdonhang;

        public int IDDonHang { get => iDDonHang; set => iDDonHang = value; }
        public int IDKhachhang { get => iDKhachhang; set => iDKhachhang = value; }
        public int IDKM2 { get => iDKM2; set => iDKM2 = value; }
        public DateTime Ngatdathang { get => ngatdathang; set => ngatdathang = value; }
        public string Trangthaidonhang { get => trangthaidonhang; set => trangthaidonhang = value; }
        public DateTime Ngaygiaohang { get => ngaygiaohang; set => ngaygiaohang = value; }
        public int Tongdonhang { get => tongdonhang; set => tongdonhang = value; }
    }
    public class ChitietdonhangDTO
    {
        private int iDDonHang;
        private int iDMonAn;
        private int iDKM1;
        private string loaiMonAn;
        private int soluong;

        public int IDDonHang { get => iDDonHang; set => iDDonHang = value; }
        public int IDMonAn { get => iDMonAn; set => iDMonAn = value; }
        public int IDKM1 { get => iDKM1; set => iDKM1 = value; }
        public string LoaiMonAn { get => loaiMonAn; set => loaiMonAn = value; }
        public int Soluong { get => soluong; set => soluong = value; }
    }
    public class MenuDTO
    {
        private int iDMenu;
        private string tenMenu;
        private DateTime ngaycapnhat;

        public int IDMenu { get => iDMenu; set => iDMenu = value; }
        public string TenMenu { get => tenMenu; set => tenMenu = value; }
        public DateTime Ngaycapnhat { get => ngaycapnhat; set => ngaycapnhat = value; }
    }
    public class ChitietmenuDTO
    {
        private int iDMenu;
        private int iDMonAn;

        public int IDMenu { get => iDMenu; set => iDMenu = value; }
        public int IDMonAn { get => iDMonAn; set => iDMonAn = value; }
    }
    public class ChitietgiohangDTO
    {
        private int iDGioHang;
        private int iDMonAn;
        private int iDKhachhang;
        private int soluong;

        public int IDGioHang { get => iDGioHang; set => iDGioHang = value; }
        public int IDMonAn { get => iDMonAn; set => iDMonAn = value; }
        public int IDKhachhang { get => iDKhachhang; set => iDKhachhang = value; }
        public int Soluong { get => soluong; set => soluong = value; }
    }
    public class DanhsachDanhGiaDTO
    {
        private int iDDonHang;
        private int iDKhachhang;
        private int sosao;
        private DateTime ngayDanhGia;

        public int IDDonHang { get => iDDonHang; set => iDDonHang = value; }
        public int IDKhachhang { get => iDKhachhang; set => iDKhachhang = value; }
        public int Sosao { get => sosao; set => sosao = value; }
        public DateTime NgayDanhGia { get => ngayDanhGia; set => ngayDanhGia = value; }
    }

}
