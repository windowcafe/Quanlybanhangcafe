using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace QuanLyQuanCafe
{
    public partial class ThanhToan : DevComponents.DotNetBar.RibbonForm
    {
        public ThanhToan()
        {
            InitializeComponent();
        }
        public string TenBan;
        public string TongTien;
        public int vitri = 0;
        public List<BanHienTai> lsBan;
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.Hide();
            BanHang frBanHang = new BanHang();
            frBanHang.lsBan = lsBan;
            frBanHang.viTriBan = vitri;
            frBanHang.TinhTien();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            lbBan.Text = TenBan;
            lbTongTien.Text = TongTien + " VNĐ";
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTienKhach.Text != "")
                {
                    int a = Int32.Parse(txtTienKhach.Text);
                    int b = Int32.Parse(TongTien);
                    if (a >= b)
                    {
                        if (txtGiamGia.Text == "")
                            lbTienThua.Text = (a - b).ToString() + " VNĐ";
                        else
                        {
                            int c = Int32.Parse(txtGiamGia.Text);
                            lbTienThua.Text = ((a - b) * (100 - c) * 1.0 / 100).ToString() + " VNĐ";
                        }
                    }
                }
                else
                {
                    lbTienThua.Text = "...";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng không nhập kí tự vào đây");
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGiamGia.Text != "" && txtTienKhach.Text != "")
                {
                    int a = Int32.Parse(txtTienKhach.Text);
                    int c = Int32.Parse(txtGiamGia.Text);
                    int b = Int32.Parse(TongTien);
                    if(c>=0 && c<=100)
                    {
                        lbTienThua.Text = ((a - b) * (100-c) * 1.0 / 100).ToString()+ " VNĐ";
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng không nhập kí tự vào đây");
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}