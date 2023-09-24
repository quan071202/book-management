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
    public partial class FormNTG : Form
    {
        public FormNTG()
        {
            InitializeComponent();
        }

        

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormNTG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMTG.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã tác giả", "Lỗi");
            }
            else if (txtTenTG.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên tác giả", "Lỗi");
            }
            else if (txtLL.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin liên lạc", "Lỗi");
            }
            else if (!Funtion.checkMTG(txtMTG.Text))
            {
                MessageBox.Show("Mã tác giả đã tồn tại", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"INSERT INTO TacGia(MaTG,TenTG,LienLac) VALUES('{0}','{1}','{2}')", txtMTG.Text, txtTenTG.Text,txtLL.Text);
                Funtion.Insert(sql);
               FormNTG_Load(sender, e);
                MessageBox.Show("Đã thêm tác giả", "Thông báo");
            }
        }

        private void FormNTG_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            DataTable dt = new DataTable();
            string sql = @"Select * from TacGia";
            dt = Funtion.GetDataToTable(sql);
            dtgrvTG.DataSource = dt;
            dtgrvTG.Columns[0].HeaderText = "Mã Tác Giả";
            dtgrvTG.Columns[1].HeaderText = "Tên Tác Giả";
            dtgrvTG.Columns[2].HeaderText = "Liên Lạc";
            dtgrvTG.Columns[0].Width = 100;
            dtgrvTG.Columns[1].Width = 100;
            dtgrvTG.Columns[2].Width = 160;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgrvTG.SelectedRows.Count > 1 || dtgrvTG.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn một hàng để sửa", "Lỗi");
            }
            else
            {
                
                txtMTG.Enabled = false;
                panel3.Enabled = true;
                panel2.Enabled = false;
                dtgrvTG.Enabled = false;
                int rowSelected = dtgrvTG.CurrentRow.Index;
                txtMTG.Text = dtgrvTG.Rows[rowSelected].Cells[0].Value.ToString();
                txtTenTG.Text = dtgrvTG.Rows[rowSelected].Cells[1].Value.ToString();
                txtLL.Text = dtgrvTG.Rows[rowSelected].Cells[2].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtTenTG.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên tác giả", "Lỗi");
                }
                if (txtLL.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập thông tin liên lạc", "Lỗi");
                }
                else
                {
                    string sql = string.Format(@"UPDATE TacGia SET TenTG = '{0}' , LienLac = '{1}' where MaTG = '{2}'", txtTenTG.Text, txtLL.Text,txtMTG);
                    Funtion.Insert(sql);
                    btnCanCle_Click(sender, e);
                    MessageBox.Show("Đã lưu thông tin", "Thông báo");
                }
            }
        }

        private void btnCanCle_Click(object sender, EventArgs e)
        {
            txtMTG.Text = "";
            txtTenTG.Text = "";
            txtLL.Text = "";
            txtMTG.Enabled = true;
            panel3.Enabled = false;
            panel2.Enabled = true;
            dtgrvTG.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrvTG.SelectedRows.Count > 1 || dtgrvTG.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrvTG.CurrentRow.Index;
                    string maTG = dtgrvTG.Rows[rowSelected].Cells[0].Value.ToString();
                    if (Funtion.checkMTGtoDelete(maTG))
                    {
                        string sql = string.Format(@"DELETE FROM TacGia Where MaTG = '{0}'", maTG);
                        Funtion.Insert(sql);
                        FormNTG_Load(sender, e);
                        MessageBox.Show("Đã Xóa", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tác giả này", "Lỗi");
                    }
                }
            }
        }
    }
}
