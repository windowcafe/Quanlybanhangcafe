using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using QuanLyQuanCafe.DB_layer;

namespace QuanLyQuanCafe.BS_layer
{
    class BsQuanLy
    {
        DBmain db = null;
        public BsQuanLy()
        {
            db = new DBmain();
        }
        public DataSet LayThucDon()
        {
            return db.ExecuteQueryDataSet("select NhomThucUong,BangGia.MaThucUong,TenThucUong,GiaBan,GiaNhap from BangGia,ThucUong where BangGia.MaThucUong = ThucUong.MaThucUong", CommandType.Text);
        }
        public DataSet LayNhomMon()
        {
            return db.ExecuteQueryDataSet("select MaNhomThucUong,MaThucUong,TenNhomThucUong,TenThucUong from NhomThucUong,ThucUong where NhomThucUong.MaNhomThucUong=ThucUong.NhomThucUong", CommandType.Text);
        }
        public DataSet LayBan()
        {
            return db.ExecuteQueryDataSet("select Ban.MaBan,TenBan,LichTrucNhanVien.MaNhanVien,NhanVien.TenNhanVien from Ban,LichTrucNhanVien,NhanVien where Ban.MaBan=LichTrucNhanVien.MaBan and NhanVien.MaNhanVien=LichTrucNhanVien.MaNhanVien", CommandType.Text);
        }
        
    }
}
