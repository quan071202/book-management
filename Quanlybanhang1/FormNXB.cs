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
    public partial class FormNXB : Form
    {
        public FormNXB()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMNXB.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhà xuất bản", "Lỗi");
            }
            else if (txtTenNXB.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên nhà xuất bản", "Lỗi");
            }
            else if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ liên lạc", "Lỗi");
            }
            else if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại", "Lỗi");
            }
            else if (!Funtion.checkMNXB(txtMNXB.Text))
            {
                MessageBox.Show("Mã nhà xuất bản đã tồn tại", "Lỗi");
            }
            else if (!Funtion.checkPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"INSERT INTO NhaXuatBan(MaNXB,TenNXB,DiaChiNXB,DienThoai) VALUES('{0}','{1}','{2}','{3}')", txtMNXB.Text, txtTenNXB.Text, txtDiaChi.Text,txtPhone.Text);
                Funtion.Insert(sql);
                FormNXB_Load(sender, e);
                MessageBox.Show("Đã thêm nhà xuất bản", "Thông báo");
            }
        }

        private void FormNXB_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            DataTable dt = new DataTable();
            string sql = @"Select * from NhaXuatBan";
            dt = Funtion.GetDataToTable(sql);
            dtgrvNXB.DataSource = dt;
            dtgrvNXB.Columns[0].HeaderText = "Mã NXB";
            dtgrvNXB.Columns[1].HeaderText = "Tên NXB";
            dtgrvNXB.Columns[2].HeaderText = "Địa chỉ";
            dtgrvNXB.Columns[3].HeaderText = "Số điện thoại";
            dtgrvNXB.Columns[0].Width = 120;
            dtgrvNXB.Columns[1].Width = 120;
            dtgrvNXB.Columns[2].Width = 130;
            dtgrvNXB.Columns[3].Width = 120;
        }

        private void FormNXB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgrvNXB.SelectedRows.Count > 1 || dtgrvNXB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn một hàng để sửa", "Lỗi");
            }
            else
            {
                txtMNXB.Enabled = false;
                panel3.Enabled = true;
                panel2.Enabled = false;
                dtgrvNXB.Enabled = false;
                int rowSelected = dtgrvNXB.CurrentRow.Index;
                txtMNXB.Text = dtgrvNXB.Rows[rowSelected].Cells[0].Value.ToString();
                txtTenNXB.Text = dtgrvNXB.Rows[rowSelected].Cells[1].Value.ToString();
                txtDiaChi.Text = dtgrvNXB.Rows[rowSelected].Cells[2].Value.ToString();
                txtPhone.Text = dtgrvNXB.Rows[rowSelected].Cells[3].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtTenNXB.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên nhà xuất bản", "Lỗi");
                }
               else if (txtDiaChi.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ liên lạc", "Lỗi");
                }
                else if (txtPhone.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại", "Lỗi");
                }
                else if (!Funtion.checkPhone(txtPhone.Text))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi");
                }
                else
                {
                    string sql = string.Format(@"UPDATE NhaXuatBan SET TenNXB = '{0}' ,DiaChiNXB = '{1}', DienThoai = '{2}' where MaNXB = '{3}'", txtTenNXB.Text, txtDiaChi.Text, txtPhone.Text,txtMNXB.Text);
                    Funtion.Insert(sql);
                    btnCanCle_Click(sender, e);
                    MessageBox.Show("Đã lưu thông tin", "Thông báo");
                }
            }
        }

        private void btnCanCle_Click(object sender, EventArgs e)
        {
            txtMNXB.Text = "";
            txtTenNXB.Text = "";
            txtDiaChi.Text = "";
            txtPhone.Text = "";
            txtMNXB.Enabled = true;
            panel3.Enabled = false;
            panel2.Enabled = true;
            dtgrvNXB.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrvNXB.SelectedRows.Count > 1 || dtgrvNXB.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrvNXB.CurrentRow.Index;
                    string maNXB = dtgrvNXB.Rows[rowSelected].Cells[0].Value.ToString();
                    if (Funtion.checkMNXBtoDelete(maNXB))
                    {
                        string sql = string.Format(@"DELETE FROM NhaXuatBan Where MaNXB = '{0}'", maNXB);
                        Funtion.Insert(sql);
                        FormNXB_Load(sender, e);
                        MessageBox.Show("Đã Xóa", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa nhà xuất bản này", "Lỗi");
                    }
                }
            }
        }
    }
}
