using Quanlybanhang1.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlybanhang1
{
    public partial class FormTNXB : Form
    {
        public FormTNXB()
        {
            InitializeComponent();
        }

        private void FormTNXB_Load(object sender, EventArgs e)
        {
            string sql = @"select TenNXB from NhaXuatBan";
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            cboNXB.DataSource = dt;
            cboNXB.DisplayMember = "TenNXB";
            cboNXB.ValueMember = "TenNXB";
            DataTable dt1 = new DataTable();
            string sql1 = @"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, NhaXuatBan.TenNXB
                        FROM NhaXuatBan INNER JOIN Sach ON NhaXuatBan.MaNXB = Sach.MaNXB";
            dt1 = Funtion.GetDataToTable(sql1);
            dtgrvNXB.DataSource = dt1;
            dtgrvNXB.Columns[0].HeaderText = "Mã Sách";
            dtgrvNXB.Columns[1].HeaderText = "Tên Sách";
            dtgrvNXB.Columns[2].HeaderText = "Giá Bán";
            dtgrvNXB.Columns[3].HeaderText = "Nhà Xuất Bản";
            dtgrvNXB.Columns[0].Width = 100;
            dtgrvNXB.Columns[1].Width = 120;
            dtgrvNXB.Columns[2].Width = 130;
            dtgrvNXB.Columns[3].Width = 150;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maNXB = Funtion.getMaNXB(cboNXB.SelectedValue.ToString());
            string sql = string.Format(@"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, NhaXuatBan.TenNXB
                        FROM NhaXuatBan INNER JOIN Sach ON NhaXuatBan.MaNXB = Sach.MaNXB where NhaXuatBan.MaNXB = '{0}'", maNXB);
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            dtgrvNXB.DataSource = dt;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTNXB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
