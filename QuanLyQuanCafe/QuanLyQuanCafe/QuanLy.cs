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
using System.Data.Sql;

namespace QuanLyQuanCafe
{
    public partial class QuanLy : DevComponents.DotNetBar.RibbonForm
    {
        DataTable dtThucDon = null;
        DataTable dtBan = null;
        DataTable dtNhomMon = null;
        BsQuanLy dbQuanLy = new BsQuanLy();
        
        public QuanLy()
        {
            InitializeComponent();
        }
        void LoadComboBox()
        {
            comboBoxEx1.DataSource = dtThucDon;
            comboBoxEx1.DisplayMember = "GiaBan";
            comboBoxEx1.ValueMember = "GiaBan";
        }
        void LoadData()
        {
            try
            {
                dtThucDon = new DataTable();
               
                DataSet ds = dbQuanLy.LayThucDon();
                dtThucDon = ds.Tables[0];
                dtgvThucDon.DataSource = dtThucDon;
                dtgvThucDon.Columns[2].Width = 100;
                dtgvThucDon.Columns[3].Width = 194;
                dtgvThucDon.Columns[4].Width = 194;

                DataSet dss = dbQuanLy.LayNhomMon();
                dtNhomMon = dss.Tables[0];
                dtgvNhomMon.DataSource = dtNhomMon;
                dtgvNhomMon.Columns[2].Width = 163;
                dtgvNhomMon.Columns[0].Width = 164;
                dtgvNhomMon.Columns[1].Width = 164;
                dtgvNhomMon.Columns[3].Width = 195;

                DataSet dsBan = dbQuanLy.LayBan();
                dtBan = dsBan.Tables[0];
                dtgvBan.DataSource = dtBan;
                dtgvBan.Columns[2].Width = 163;
                dtgvBan.Columns[0].Width = 164;
                dtgvBan.Columns[1].Width = 164;
                dtgvBan.Columns[3].Width = 195;
                

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table  Lỗi rồi!!!");
            }
        }
        private void QuanLy_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBox();
            buttonX1_Click(sender, e);
            lbBan.Visible = false;
            txbBan.Visible = false;


            lbLoai.Visible = false;
            txbLoai.Visible = false;

            lbMon.Visible = true;
            txbTimMon.Visible = true;

            comboBoxEx1.Visible = true;
            lbGiaBan.Visible = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            dtgvThucDon.Show();
            dtgvNhomMon.Hide();
            dtgvBan.Hide();

            lbBan.Visible = false;
            txbBan.Visible = false;

            lbLoai.Visible = false;
            txbLoai.Visible = false;

            lbMon.Visible = true;
            txbTimMon.Visible = true;

            comboBoxEx1.Visible = true;
            lbGiaBan.Visible = true;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            dtgvNhomMon.Show();
            dtgvThucDon.Hide();
            dtgvBan.Hide();

            lbBan.Visible = false;
            txbBan.Visible = false;

            lbLoai.Visible = true;
            txbLoai.Visible = true;

            lbMon.Visible = false;
            txbTimMon.Visible = false;

            comboBoxEx1.Visible = false;
            lbGiaBan.Visible = false;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            dtgvBan.Show();
            dtgvNhomMon.Hide();
            dtgvThucDon.Hide();

            lbBan.Visible = true;
            txbBan.Visible = true;

            lbLoai.Visible =false;
            txbLoai.Visible = false;


            lbMon.Visible = false;
            txbTimMon.Visible = false;

            comboBoxEx1.Visible = false;
            lbGiaBan.Visible = false;
     
        }

        private void txbTimMon_TextChanged(object sender, EventArgs e)
        {
          //dtgvThucDon.DataSource
            dtThucDon.DefaultView.RowFilter = string.Format("TenThucUong LIKE '%{0}%'",txbTimMon.Text);
            dtgvThucDon.DataSource = dtThucDon;
        }

        private void txbLoai_TextChanged(object sender, EventArgs e)
        {
            dtNhomMon.DefaultView.RowFilter = string.Format("TenNhomThucUong LIKE '%{0}%'", txbLoai.Text);
            dtgvNhomMon.DataSource = dtNhomMon;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxEx1.Text =="")
            {
                dtThucDon.DefaultView.RowFilter = string.Format("TenThucUong LIKE '%{0}%'", comboBoxEx1.Text);
                dtgvThucDon.DataSource = dtThucDon;
            }
        }

        private void txbBan_TextChanged(object sender, EventArgs e)
        {
            dtBan.DefaultView.RowFilter = string.Format("MaBan LIKE '%{0}%'", txbBan.Text);
            dtgvBan.DataSource = dtBan;
        }
    }
}