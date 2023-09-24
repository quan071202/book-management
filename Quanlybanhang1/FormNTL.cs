using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanlybanhang1.Class;

namespace Quanlybanhang1
{
    public partial class FormNTL : Form
    {
        public FormNTL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTL.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã thể loại", "Lỗi");
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên thể loại", "Lỗi");
            }
            else if (!Funtion.checkMTL(txtTL.Text))
            {
                MessageBox.Show("Mã thể loại đã tồn tại", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"INSERT INTO TheLoai(MaTL,TenTL) VALUES('{0}','{1}')", txtTL.Text, txtTen.Text);
                Funtion.Insert(sql);
                FormNTL_Load(sender, e);
                MessageBox.Show("Đã thêm thể loại", "Thông báo");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrvTL.SelectedRows.Count > 1 || dtgrvTL.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrvTL.CurrentRow.Index;
                    string maTL = dtgrvTL.Rows[rowSelected].Cells[0].Value.ToString();
                    if (Funtion.checkMTLtoDelete(maTL))
                    {
                        string sql = string.Format(@"DELETE FROM TheLoai Where MaTL = '{0}'",maTL);
                        Funtion.Insert(sql);
                        FormNTL_Load(sender, e);
                        MessageBox.Show("Đã Xóa", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa thể loại này", "Lỗi");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtgrvTL.SelectedRows.Count > 1 || dtgrvTL.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn một hàng để sửa", "Lỗi");
            }
            else
            {
                txtTL.Enabled = false;
                panel3.Enabled = true;
                panel2.Enabled = false;
                dtgrvTL.Enabled = false;
                int rowSelected = dtgrvTL.CurrentRow.Index;
                txtTL.Text = dtgrvTL.Rows[rowSelected].Cells[0].Value.ToString();
                txtTen.Text = dtgrvTL.Rows[rowSelected].Cells[1].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormNTL_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            DataTable dt = new DataTable();
            string sql = @"Select * from TheLoai";
            dt = Funtion.GetDataToTable(sql);
            dtgrvTL.DataSource = dt;
            dtgrvTL.Columns[0].HeaderText = "Mã Thể Loại";
            dtgrvTL.Columns[1].HeaderText = "Tên Thể Loại";
            dtgrvTL.Columns[0].Width = 100;
            dtgrvTL.Columns[1].Width = 140;
        }

        private void btnCanCle_Click(object sender, EventArgs e)
        {
            txtTen.Text = "";
            txtTL.Text = "";
            txtTL.Enabled = true;
            txtTL.Focus();
            panel2.Enabled = true;
            dtgrvTL.Enabled = true;
            panel3.Enabled = false;
            FormNTL_Load(sender, e);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {             
                if (txtTen.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên thể loại", "Lỗi");
                }
                else
                {
                    string sql = string.Format(@"UPDATE TheLoai SET TenTL = '{0}' where MaTL = '{1}'", txtTen.Text,txtTL.Text );
                     Funtion.Insert(sql);
                    btnCanCle_Click(sender, e);
                    MessageBox.Show("Đã lưu thông tin", "Thông báo");
                }
            }

        }

        private void FormNTL_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
