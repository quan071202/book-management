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
    public partial class FormTTL : Form
    {
        public FormTTL()
        {
            InitializeComponent();
        }

        private void FormTTL_Load(object sender, EventArgs e)
        {
            string sql = @"select TenTL from TheLoai";
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            cboTL.DataSource = dt;
            cboTL.DisplayMember = "TenTL";
            cboTL.ValueMember = "TenTL";
            DataTable dt1  = new DataTable();
            string sql1 = @"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, TheLoai.TenTL
                            FROM TheLoai INNER JOIN Sach ON TheLoai.MaTL = Sach.MaTL";
            dt1 = Funtion.GetDataToTable(sql1);
            dtgrvTL.DataSource = dt1;
            dtgrvTL.Columns[0].HeaderText = "Mã Sách";
            dtgrvTL.Columns[1].HeaderText = "Tên Sách";
            dtgrvTL.Columns[2].HeaderText = "Giá Bán";
            dtgrvTL.Columns[3].HeaderText = "Thể Loại";
            dtgrvTL.Columns[0].Width = 120;
            dtgrvTL.Columns[1].Width = 120;
            dtgrvTL.Columns[2].Width = 130;
            dtgrvTL.Columns[3].Width = 120;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTTL_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {   
            string maTL = Funtion.getMaTL(cboTL.SelectedValue.ToString());
            string sql = string.Format(@"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, TheLoai.TenTL
                                    FROM TheLoai INNER JOIN Sach ON TheLoai.MaTL = Sach.MaTL where TheLoai.MaTL = '{0}'",maTL);
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            dtgrvTL.DataSource = dt;

            }
    }
}
