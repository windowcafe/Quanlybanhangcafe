using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using QuanLyQuanCafe.BS_layer;
using System.Data.SqlClient;

namespace QuanLyQuanCafe
{
    public partial class ThanhToan : DevComponents.DotNetBar.RibbonForm
    {
        DataTable dtBanHang = null;
        BSBanHang dbBanHang = new BSBanHang();
        string err;

        public string TenBan;
        public string TongTien;
        public BanHienTai ban;
        public bool XacNhan;

        public ThanhToan()
        {
            InitializeComponent();
            btnXacNhan.Enabled = false;
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.Hide();
            xuLyCapNhatHoaDon();
            BanHang frBanHang = new BanHang();
            XacNhan = true;
        }

        private void xuLyCapNhatHoaDon()
        {
            string MaCa;
            string ThoiGian = ban.ThoiGian;
            char[] charArray = ThoiGian.ToCharArray();
            Array.Reverse(charArray);
            ThoiGian = new string(charArray);
            MaCa = ThoiGian.Substring(0, 2);
            if (MaCa == "MP")
                MaCa = "CT2";
            else
                MaCa = "CT1";
            DataSet ds = dbBanHang.LaySoHoaDon();
            dtBanHang = ds.Tables[0];
            dataGridView1.DataSource = dtBanHang;
            string MaHoaDon ="HĐ"+(Int32.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString())+1);
            dbBanHang.ThemHoaDon(MaHoaDon, ban.btnBan.Text, ban.ThoiGian, MaCa, ref err);
            foreach(DaChon x in ban.lsDaChon)
            {
                dbBanHang.ThemChiTietHoaDon(MaHoaDon, x.btnTenTU.Text, txtGiamGia.Text, x.btnSoLuong.Text, ref err);
            }
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            lbBan.Text = TenBan;
            lbTongTien.Text = TongTien + " VNĐ";
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            if (lbTongTien.Text != "0 VNĐ")
            {
                try
                {
                    if (txtTienKhach.Text != "")
                    {
                        int a = Int32.Parse(txtTienKhach.Text);
                        int b = Int32.Parse(TongTien);
                        if (a >= b)
                        {
                            if (txtGiamGia.Text != "")
                            {
                                int c = Int32.Parse(txtGiamGia.Text);
                                if (c >= 0 && c <= 100)
                                    lbTienThua.Text = (a - b * (100 - c) * 1.0 / 100).ToString() + " VNĐ";
                            }
                            else
                            {
                                lbTienThua.Text = (a - b).ToString() + " VNĐ";
                            }
                        }
                        else
                        {
                            lbTienThua.Text = "...";
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Vui lòng không nhập kí tự vào đây");
                }
                if (lbTienThua.Text == "...")
                    btnXacNhan.Enabled = false;
                else
                    btnXacNhan.Enabled = true;
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            if (lbTongTien.Text != "0 VNĐ")
            {
                try
                {
                    if (txtTienKhach.Text != "")
                    {
                        int a = Int32.Parse(txtTienKhach.Text);
                        int b = Int32.Parse(TongTien);
                        if (a >= b)
                        {
                            if (txtGiamGia.Text != "")
                            {
                                int c = Int32.Parse(txtGiamGia.Text);
                                if (c >= 0 && c <= 100)
                                    lbTienThua.Text = (a - b * (100 - c) * 1.0 / 100).ToString() + " VNĐ";
                                else
                                {
                                    lbTienThua.Text = "...";
                                }
                            }
                            else
                            {
                                lbTienThua.Text = (a - b).ToString() + " VNĐ";
                            }
                        }
                        else
                        {
                            lbTienThua.Text = "...";
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Vui lòng không nhập kí tự vào đây");
                }
                if (lbTienThua.Text == "...")
                    btnXacNhan.Enabled = false;
                else
                    btnXacNhan.Enabled = true;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Hide();
            BanHang frBanHang = new BanHang();
            XacNhan = false ;
        }

    }
}