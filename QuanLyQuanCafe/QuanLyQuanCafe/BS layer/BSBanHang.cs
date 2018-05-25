using QuanLyQuanCafe.DB_layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.BS_layer
{
    class BSBanHang
    {
        DBmain db = null;
        public BSBanHang()
        {
            db = new DBmain();
        }
        public DataSet LaySoBan()
        {
            return db.ExecuteQueryDataSet("select count(*) from Ban", CommandType.Text);
        }
        public DataSet LayBan()
        {
            return db.ExecuteQueryDataSet("select * from Ban", CommandType.Text);
        }
        public DataSet LaySoNhomThucUong()
        {
            return db.ExecuteQueryDataSet("select count(*) from NhomThucUong", CommandType.Text);
        }
        public DataSet LayNhomThucUong()
        {
            return db.ExecuteQueryDataSet("select * from NhomThucUong", CommandType.Text);
        }
        public DataSet LaySoThucUong(string strTen)
        {
            return db.ExecuteQueryDataSet("select count(*) from ThucUong,BangGia where NhomThucUong =" +
                "(select MaNhomThucUong from NhomThucUong where TenNhomThucUong = N" + "'"+strTen+ "') and ThucUong.MaThucUong = BangGia.MaThucUong"
                , CommandType.Text);
        }
        public DataSet LayThucUong(string strTen)
        {
            return db.ExecuteQueryDataSet("select * from ThucUong,BangGia where NhomThucUong =" +
                "(select MaNhomThucUong from NhomThucUong where TenNhomThucUong = N" + "'" + strTen + "') and ThucUong.MaThucUong = BangGia.MaThucUong"
                , CommandType.Text);
        }
        public DataSet LayGiaThucUong(string tenTU)
        {
            return db.ExecuteQueryDataSet("select GiaBan,TenThucUong from BangGia,ThucUong " +
                "where TenThucUong = N'"+tenTU+"' and BangGia.MaThucUong = ThucUong.MaThucUong", CommandType.Text);
        }
    }
}
