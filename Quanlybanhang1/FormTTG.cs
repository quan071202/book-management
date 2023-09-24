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
    public partial class FormTTG : Form
    {
        public FormTTG()
        {
            InitializeComponent();
        }

        private void FormTTG_Load(object sender, EventArgs e)
        {
            string sql = @"select TenTG from TacGia";
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            cboTG.DataSource = dt;
            cboTG.DisplayMember = "TenTG";
            cboTG.ValueMember = "TenTG";
            DataTable dt1 = new DataTable();
            string sql1 = @"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, TacGia.TenTG
                            FROM TacGia INNER JOIN Sach ON TacGia.MaTG = Sach.MaTG";
            dt1 = Funtion.GetDataToTable(sql1);
            dtgrvTG.DataSource = dt1;
            dtgrvTG.Columns[0].HeaderText = "Mã Sách";
            dtgrvTG.Columns[1].HeaderText = "Tên Sách";
            dtgrvTG.Columns[2].HeaderText = "Giá Bán";
            dtgrvTG.Columns[3].HeaderText = "Tác Giả";
            dtgrvTG.Columns[0].Width = 100;
            dtgrvTG.Columns[1].Width = 120;
            dtgrvTG.Columns[2].Width = 130;
            dtgrvTG.Columns[3].Width = 150;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maTG = Funtion.getMaTG(cboTG.SelectedValue.ToString());
            string sql = string.Format(@"SELECT Sach.MaSach, Sach.TenSach, Sach.GiaBan, TacGia.TenTG
                            FROM TacGia INNER JOIN Sach ON TacGia.MaTG = Sach.MaTG where TacGia.MaTG = '{0}'", maTG);
            DataTable dt = new DataTable();
            dt = Funtion.GetDataToTable(sql);
            dtgrvTG.DataSource = dt;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTTG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
