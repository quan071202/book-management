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
    public partial class FormTim : Form
    {
        public FormTim()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if(txtMS.Text.Trim() != "")
            {              
                string sql = string.Format(@"SELECT * from Sach where MaSach = '{0}'", txtMS.Text);
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql);
                dtgrv.DataSource = dt;
                txtMS.Text = "";
            }
            else if(txtTS.Text.Trim() != ""){
                string sql = string.Format(@"SELECT * from Sach where TenSach LIKE '%{0}%'", txtTS.Text);
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql);
                dtgrv.DataSource = dt;
                txtTS.Text = "";
            }
            else if (txtMNXB.Text.Trim() != "")
            {
                string sql = string.Format(@"SELECT * from Sach where MaNXB = '{0}'", txtMNXB.Text);
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql);
                dtgrv.DataSource = dt;
                txtMNXB.Text = "";
            }
            else if (txtMTG.Text.Trim() != "")
            {
                string sql = string.Format(@"SELECT * from Sach where MaTG = '{0}'", txtMTG.Text);
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql);
                dtgrv.DataSource = dt;
                txtMTG.Text = "";
            }
            else if (txtMTL.Text.Trim() != "")
            {
                string sql = string.Format(@"SELECT * from Sach where MaTL = '{0}'", txtMTL.Text);
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql);
                dtgrv.DataSource = dt;
                txtMTL.Text = "";
            }
            else
            {
                MessageBox.Show("Hãy nhập thông tin sách", "Lỗi");
            }
        }

        private void FormTim_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            string sql1 = @"SELECT * FROM Sach";
            dt1 = Funtion.GetDataToTable(sql1);
            dtgrv.DataSource = dt1;
            dtgrv.Columns[0].HeaderText = "Mã Sách";
            dtgrv.Columns[1].HeaderText = "Tên Sách";
            dtgrv.Columns[2].HeaderText = "Mã Thể Loại";
            dtgrv.Columns[3].HeaderText = "Mã NXB";
            dtgrv.Columns[4].HeaderText = "Mã Tác Giả";
            dtgrv.Columns[5].HeaderText = "Số Lượng Tồn";
            dtgrv.Columns[6].HeaderText = "Giá Bán";
            dtgrv.Columns[0].Width = 100;
            dtgrv.Columns[1].Width = 120;
            dtgrv.Columns[2].Width = 100;
            dtgrv.Columns[3].Width = 100;
            dtgrv.Columns[4].Width = 100;
            dtgrv.Columns[5].Width = 120;
            dtgrv.Columns[6].Width = 120;
        }

        private void FormTim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
