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
    public partial class BanHang : DevComponents.DotNetBar.RibbonForm
    {
        DataTable dtBanHang = null;
        BSBanHang dbBanHang = new BSBanHang();
        public List<BanHienTai> lsBan = new List<BanHienTai>();
        List<ButtonX> lsNhomTU = new List<ButtonX>();
        List<ButtonX> lsbtnMenu = new List<ButtonX>();
        public int viTriBan = 0;


        public BanHang()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            timer1.Start();
            picBanHang.Visible = true;
            flayoutBan.Controls.Clear();
            try
            {
                dtBanHang = new DataTable();
                dtBanHang.Clear();

                DataSet ds = dbBanHang.LaySoBan();
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;
                int n = Int32.Parse( dataGridViewX1.Rows[0].Cells[0].Value.ToString());

                ds = dbBanHang.LayBan();
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;

                for (int i = 0; i < n; i++)
                {
                    BanHienTai bht = new BanHienTai();
                    bht.btnBan.Tag = i;
                    bht.btnBan.Click += new EventHandler(btnBan_click);
                    bht.btnBan.Text = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    lsBan.Add(bht);
                    flayoutBan.Controls.Add(bht.btnBan);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Ban. Lỗi rồi!!!");
            }
        }
        public ButtonX TaoNhomTU(int i)
        {
            ButtonX btn = new ButtonX();
            btn.Size = new Size(flayoutNhomThucUong.Width, 83);
            btn.ColorTable = new eButtonColor();
            Random r = new Random();
            btn.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            btn.Font = new Font("SketchFlow Print", 12, FontStyle.Bold);
            btn.Tag = i;
            btn.Click += new EventHandler(btNhomTU_click);
            return btn;
        }
        private void BanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
        }

        private void btnBan_click(object sender,EventArgs e)
        {
            picBanHang.Visible = false;
            picMenu.Visible = true;
            lbBan.Text = ((ButtonX)sender).Text;
            viTriBan = (int)((ButtonX)sender).Tag;
            loadDatBan();
        }

        public void loadDatBan()
        {
            BanHienTai bht = lsBan[viTriBan];
            lbGioDen.Text = bht.ThoiGian;
            flayoutDaGoi.Controls.Clear();
            flayoutMenu.Controls.Clear();
            foreach(DaChon x in bht.lsDaChon)
            {
                flayoutDaGoi.Controls.Add(x.btnTenTU);
                flayoutDaGoi.Controls.Add(x.btnSoLuong);
                flayoutDaGoi.Controls.Add(x.btnXoaTU);
            }
            if(bht.serve == true)
            {
                btnHuy.Visible = true;
                btnDatCho.Enabled = false;
                btnGoiMon.Enabled = true;
                btnThanhToan.Enabled = true;
                lbTrangThai.Text = "Đang phục vụ";
            }
            else
            {
                btnHuy.Visible = false;
                btnDatCho.Enabled = true;
                btnGoiMon.Enabled = false;
                btnThanhToan.Enabled = false;
                lbTrangThai.Text = "Trống";
            }
        }

        public void TinhTien()
        {
            lsBan[viTriBan].serve = false;
            lsBan[viTriBan].btnBan.BackColor = Color.Lime;
            loadDatBan();
        }

        private void btNhomTU_click(object sender,EventArgs e)
        {
            lsbtnMenu.Clear();
            flayoutMenu.Controls.Clear();

            Label lbTenMenu = new Label();
            lbTenMenu.Size = new Size(flayoutMenu.Width, 36);
            lbTenMenu.TextAlign = ContentAlignment.MiddleCenter;
            lbTenMenu.Font = new Font("Palatino Linotype", 14, FontStyle.Italic);
            lbTenMenu.Text = ((ButtonX)sender).Text;
            lbTenMenu.BackColor = ((ButtonX)sender).BackColor;
            flayoutMenu.Controls.Add(lbTenMenu);


            try
            {
                dtBanHang = new DataTable();
                dtBanHang.Clear();

                DataSet ds = dbBanHang.LaySoThucUong(((ButtonX)sender).Text);
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;
                int n = Int32.Parse(dataGridViewX1.Rows[0].Cells[0].Value.ToString());

                ds = dbBanHang.LayThucUong(((ButtonX)sender).Text);
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;

                for (int i = 0; i < n; i++)
                {
                    ButtonX btnTU = new ButtonX();
                    btnTU = taoMenu(i);
                    string tenMon = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    string giaMon = dataGridViewX1.Rows[i].Cells[4].Value.ToString();
                    btnTU.Text = "<b>" + tenMon + "</b><br/> " + giaMon + "VNĐ/Ly";
                    lsNhomTU.Add(btnTU);
                    flayoutMenu.Controls.Add(btnTU);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table ThucUong. Lỗi rồi!!!");
            }
        }

        public ButtonX taoMenu(int i)
        {
            ButtonX btn = new ButtonX();
            btn.Size = new Size(112,51);
            btn.ColorTable = new eButtonColor();
            btn.BackColor = Color.Orange;
            btn.Font = new Font("Palatino Linotype", 12, FontStyle.Regular);
            btn.Tag = i;
            btn.Click += new EventHandler(btnTU_Click);
            return btn;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString();
        }

        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            lsNhomTU.Clear();
            picMenu.Visible = false;
            flayoutNhomThucUong.Controls.Clear();
            try
            {
                dtBanHang = new DataTable();
                dtBanHang.Clear();

                DataSet ds = dbBanHang.LaySoNhomThucUong();
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;
                int n = Int32.Parse(dataGridViewX1.Rows[0].Cells[0].Value.ToString());

                ds = dbBanHang.LayNhomThucUong();
                dtBanHang = ds.Tables[0];
                dataGridViewX1.DataSource = dtBanHang;

                for (int i = 0; i < n; i++)
                {
                    ButtonX btnNhomTU = new ButtonX();
                    btnNhomTU = TaoNhomTU(i);
                    btnNhomTU.Text = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    lsNhomTU.Add(btnNhomTU);
                    flayoutNhomThucUong.Controls.Add(btnNhomTU);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table THANHPHO. Lỗi rồi!!!");
            }
        }

        private void btnTU_Click(object sender, EventArgs e)
        {
            bool co = false;
            int t = lsBan[viTriBan].lsDaChon.Count;
            try
            {
                int i = (int)((ButtonX)sender).Tag;
                lsBan[viTriBan].TongTien += Int32.Parse(dataGridViewX1.Rows[i].Cells[4].Value.ToString());
                foreach(DaChon x in lsBan[viTriBan].lsDaChon)
                {
                    if (x.btnTenTU.Text == dataGridViewX1.Rows[i].Cells[1].Value.ToString())
                    {
                        int s = Int32.Parse(x.btnSoLuong.Text);
                        s++;
                        x.btnSoLuong.Text = s.ToString();
                        co = true;
                    }
                }
                if(co == false)
                {
                    DaChon daChon = new DaChon();
                    daChon.btnXoaTU.Click += new EventHandler(btnXoaTU_Click);
                    daChon.btnTenTU.Text = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    flayoutDaGoi.Controls.Add(daChon.btnTenTU);
                    flayoutDaGoi.Controls.Add(daChon.btnSoLuong);
                    flayoutDaGoi.Controls.Add(daChon.btnXoaTU);
                    daChon.btnSoLuong.Tag = daChon.btnTenTU.Tag = daChon.btnXoaTU.Tag = t;
                    lsBan[viTriBan].lsDaChon.Add(daChon);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("không hiểu thị được đã chọn món. Lỗi rồi!!!");
            }
        }
        private void btnXoaTU_Click(object sender, EventArgs e)
        {
            List<DaChon> lsDaChon = lsBan[viTriBan].lsDaChon;
            int t = (int)((ButtonX)sender).Tag;
         
            try
            {
                dtBanHang = new DataTable();
                dtBanHang.Clear();

                DataSet ds = dbBanHang.LayGiaThucUong(lsDaChon[t].btnTenTU.Text);
                dtBanHang = ds.Tables[0];
                dataGridViewX2.DataSource = dtBanHang;
                lsBan[viTriBan].TongTien -= Int32.Parse(dataGridViewX2.Rows[0].Cells[0].Value.ToString()) * Int32.Parse(lsDaChon[t].btnSoLuong.Text);
            }
            catch (SqlException)
            {
                MessageBox.Show("không thể tính tổng tiền. Lỗi rồi!!!");
            }

            lsDaChon.RemoveAt(t);
            flayoutDaGoi.Controls.Clear();
            for (int i = 0; i < lsDaChon.Count; i++)
            {
                lsDaChon[i].btnTenTU.Tag = i;
                lsDaChon[i].btnSoLuong.Tag = i;
                lsDaChon[i].btnXoaTU.Tag = i;
                flayoutDaGoi.Controls.Add(lsDaChon[i].btnTenTU);
                flayoutDaGoi.Controls.Add(lsDaChon[i].btnSoLuong);
                flayoutDaGoi.Controls.Add(lsDaChon[i].btnXoaTU);
            }
        }

        private void btnDatCho_Click(object sender, EventArgs e)
        {
            lsBan[viTriBan].btnBan.BackColor = Color.Red;
            lbGioDen.Text = lbTime.Text;
            lsBan[viTriBan].ThoiGian = lbTime.Text;
            lsBan[viTriBan].serve = true;
            lbTrangThai.Text = "Đang phục vụ";
            btnHuy.Visible = true;
            btnGoiMon.Enabled = true;
            btnThanhToan.Enabled = true;
            btnDatCho.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Visible = false;
            btnGoiMon.Enabled = false;
            btnThanhToan.Enabled = false;
            lsBan[viTriBan].serve = false;
            lsBan[viTriBan].lsDaChon.Clear();
            flayoutDaGoi.Controls.Clear();
            btnDatCho.Enabled = true;
            lsBan[viTriBan].btnBan.BackColor = Color.Lime;
            lbGioDen.Text = "...";
            lbTrangThai.Text = "Trống";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            ThanhToan frThanhToan = new ThanhToan();
            frThanhToan.TongTien = lsBan[viTriBan].TongTien.ToString();
            frThanhToan.TenBan = lbBan.Text;
            frThanhToan.vitri = viTriBan;
            frThanhToan.lsBan = lsBan;
            picBanHang.Visible = true;
            frThanhToan.ShowDialog();
        }
    }


    public class DaChon
    {
        public ButtonX btnTenTU { get; set; }
        public ButtonX btnSoLuong { get; set; }
        public ButtonX btnXoaTU { get; set; }
        public DaChon()
        {
            btnTenTU = new ButtonX();
            btnTenTU.Size = new Size(120, 33);
            btnTenTU.ColorTable = new eButtonColor();
            btnTenTU.BackColor = Color.Olive;
            btnTenTU.Font = new Font("Palatino Linotype", 12, FontStyle.Bold);
            btnTenTU.TextColor = Color.Black;
            btnTenTU.Enabled = false;

            btnSoLuong = new ButtonX();
            btnSoLuong.Size = new Size(51, 33);
            btnSoLuong.ColorTable = new eButtonColor();
            btnSoLuong.BackColor = Color.Olive;
            btnSoLuong.Font = new Font("Palatino Linotype", 12, FontStyle.Bold);
            btnSoLuong.TextColor = Color.Black;
            btnSoLuong.Text = "1";
            btnSoLuong.Enabled = false;

            btnXoaTU = new ButtonX();
            btnXoaTU.Size = new Size(39, 33);
            btnXoaTU.ColorTable = new eButtonColor();
            btnXoaTU.BackColor = Color.Olive;
            btnXoaTU.Symbol = "\uf00d";
            btnXoaTU.SymbolColor = Color.MidnightBlue;
        }
    }

    public class BanHienTai
    {
        public string ThoiGian { get; set; }
        public List<DaChon> lsDaChon { get; set; }
        public bool serve { get; set; }
        public ButtonX btnBan { get; set; }
        public int TongTien { get; set; }

        public BanHienTai()
        {
            ThoiGian = "...";
            TongTien = 0;

            lsDaChon = new List<DaChon>();
            serve = false;

            btnBan = new ButtonX();
            btnBan.Size = new Size(128, 83);
            btnBan.Image = Properties.Resources.table;
            btnBan.ImageFixedSize = new Size(50, 50);
            btnBan.ColorTable = new eButtonColor();
            btnBan.BackColor = Color.Lime;
            btnBan.ImagePosition = eImagePosition.Left;
            btnBan.Font = new Font("SketchFlow Print", 12, FontStyle.Bold);
        }
    }
}