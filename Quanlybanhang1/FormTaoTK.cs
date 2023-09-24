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
    public partial class FormTaoTK : Form
    {
        public FormTaoTK()
        {
            InitializeComponent();
        }

        private void FormTaoTK_Load(object sender, EventArgs e)
        {           
            btnLuu.Enabled = false;
            btnCanCle.Enabled = false;
            DataTable dt = new DataTable();
            string sql1 = @"SELECT NhanVien.MaNhanVien, NhanVien.TenNhanVien, NhanVien.GioiTinh, NhanVien.DiaChi, NhanVien.DienThoai, NhanVien.NgaySinh, TaiKhoan.TaiKhoan
                FROM NhanVien INNER JOIN TaiKhoan ON NhanVien.MaNhanVien = TaiKhoan.MaNhanVien where not NhanVien.MaNhanVien = '01'";
            dt = Funtion.GetDataToTable(sql1);
            dtgrvNV.DataSource = dt;
            dtgrvNV.Columns[0].HeaderText = "Mã Nhân Viên";
            dtgrvNV.Columns[1].HeaderText = "Tên Nhân Viên";
            dtgrvNV.Columns[2].HeaderText = "Giới Tính";
            dtgrvNV.Columns[3].HeaderText = "Địa Chỉ";
            dtgrvNV.Columns[4].HeaderText = "Điện Thoại";
            dtgrvNV.Columns[5].HeaderText = "Ngày sinh";
            dtgrvNV.Columns[6].HeaderText = "Tài Khoản";         
            dtgrvNV.Columns[0].Width = 100;
            dtgrvNV.Columns[1].Width = 130;
            dtgrvNV.Columns[2].Width = 100;
            dtgrvNV.Columns[3].Width = 150;
            dtgrvNV.Columns[4].Width = 130;
            dtgrvNV.Columns[5].Width = 130;
            dtgrvNV.Columns[6].Width = 120;
            
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (txtMNV.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên ", "Lỗi");
            }
            else if (!Funtion.checkMNV(txtMNV.Text))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại ", "Lỗi");
            }
            else  if (txtTNV.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên ", "Lỗi");
            }
            else if (txtGT.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập giới tính ", "Lỗi");
            }
            else if (txtDC.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ ", "Lỗi");
            }
            else if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại ", "Lỗi");
            }
            else if (!Funtion.checkPhone(txtPhone.Text))           
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số ", "Lỗi");
            }
            else if (txtNgaySinh.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập ngày sinh ", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"INSERT INTO NhanVien(MaNhanVien,TenNhanVien,GioiTinh,DiaChi,DienThoai,NgaySinh) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", txtMNV.Text, txtTNV.Text, txtGT.Text,txtDC.Text,txtPhone.Text,txtNgaySinh.Text);
                Funtion.Insert(sql);
                FormTaoTK_Load(sender, e);
                MessageBox.Show("Đã thêm nhân viên", "Thông báo");
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrvNV.SelectedRows.Count > 1 || dtgrvNV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrvNV.CurrentRow.Index;
                    string maNV = dtgrvNV.Rows[rowSelected].Cells[0].Value.ToString();
                    string tenTK = dtgrvNV.Rows[rowSelected].Cells[6].Value.ToString();
                    string sql1 = string.Format(@"DELETE FROM TaiKhoan Where TaiKhoan = '{0}'", tenTK);
                    string sql = string.Format(@"DELETE FROM NhanVien Where MaNhanVien = '{0}'", maNV);
                     Funtion.Insert(sql1);
                    Funtion.Insert(sql);
                    FormTaoTK_Load(sender, e);
                    MessageBox.Show("Đã Xóa", "Thông báo");
                   
                }
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (dtgrvNV.SelectedRows.Count > 1 || dtgrvNV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn một hàng để sửa", "Lỗi");
            }
            else
            {
                txtMNV.Enabled = false;
                btnThemNV.Enabled = false;
                btnXoaNV.Enabled = false;
                btnSuaNV.Enabled = false;
                groupBox2.Enabled = false;
                btnLuu.Enabled = true;
                btnCanCle.Enabled = true;
                dtgrvNV.Enabled = false;
                int rowSelected = dtgrvNV.CurrentRow.Index;
                txtMNV.Text = dtgrvNV.Rows[rowSelected].Cells[0].Value.ToString();
                txtTNV.Text = dtgrvNV.Rows[rowSelected].Cells[1].Value.ToString();
                txtGT.Text = dtgrvNV.Rows[rowSelected].Cells[2].Value.ToString();
                txtDC.Text = dtgrvNV.Rows[rowSelected].Cells[3].Value.ToString();
                txtPhone.Text = dtgrvNV.Rows[rowSelected].Cells[4].Value.ToString();
                txtNgaySinh.Text = dtgrvNV.Rows[rowSelected].Cells[5].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                 if (txtTNV.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên ", "Lỗi");
                }
                else if (txtGT.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập giới tính ", "Lỗi");
                }
                else if (txtDC.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ ", "Lỗi");
                }
                else if (txtPhone.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại ", "Lỗi");
                }
                else if (!Funtion.checkPhone(txtPhone.Text))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số ", "Lỗi");
                }
                else if (txtNgaySinh.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập ngày sinh ", "Lỗi");
                }
                else
                {
                    string sql = string.Format(@"UPDATE NhanVien SET MaNhanVien = '{0}' , TenNhanVien = '{1}',GioiTinh = '{2}', DiaChi = '{3}', DienThoai = '{4}', NgaySinh = '{5}' where MaNhanVien = '{6}'", txtMNV.Text, txtTNV.Text, txtGT.Text,txtDC.Text,txtPhone.Text,txtNgaySinh.Text,txtMNV.Text);
                    Funtion.Insert(sql);
                    btnCanCle_Click(sender, e);
                    MessageBox.Show("Đã lưu thông tin", "Thông báo");
                }
            }
        }

        private void btnCanCle_Click(object sender, EventArgs e)
        {
            txtMNV.Text = "";
            txtTNV.Text = "";
            txtGT.Text = "";
            txtPhone.Text = "";
            txtDC.Text = "";
            txtNgaySinh.Text = "";
            txtMNV.Enabled = true;
            btnThemNV.Enabled = true;
            btnXoaNV.Enabled = true;
            btnSuaNV.Enabled = true;
            groupBox2.Enabled = true;
            btnLuu.Enabled = false;
            btnCanCle.Enabled = false;
            dtgrvNV.Enabled = true;
            txtMNV.Focus();
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            if(txtTK.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản ", "Lỗi");
            }
            else if (!Funtion.checkTK(txtTK.Text))
            {
                MessageBox.Show("Tài khoản đã tồn tại ", "Lỗi");
            }
            else  if (txtMK.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu ", "Lỗi");
            }
            else if (!Funtion.checkTaoMK(txtMK.Text))
            {
                MessageBox.Show("Mật khẩu phải dài hơn hoặc bằng 6 ký tự ", "Lỗi");
            }
            else if (txtXNMK.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu ", "Lỗi");
            }
            else if (!txtMK.Text.Equals(txtXNMK.Text))
            {
                MessageBox.Show("Xác nhận mật khẩu sai ", "Lỗi");
            }
            else if (txtMNV1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên ", "Lỗi");
            }
            else if (!Funtion.checkMNVCoTK(txtMNV1.Text))
            {
                MessageBox.Show("Nhân viên này đã có tài khoản ", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"INSERT INTO TaiKhoan(TaiKhoan,MatKhau,MaNhanVien) VALUES('{0}','{1}','{2}')", txtTK.Text, txtMK.Text, txtMNV1.Text);
                Funtion.Insert(sql);
                FormTaoTK_Load(sender, e);
                MessageBox.Show("Đã thêm tài khoản", "Thông báo");
            }
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrvNV.SelectedRows.Count > 1 || dtgrvNV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrvNV.CurrentRow.Index;
                    string TK = dtgrvNV.Rows[rowSelected].Cells[6].Value.ToString();               
                        string sql = string.Format(@"DELETE FROM TaiKhoan Where TaiKhoan = '{0}'", TK);
                        Funtion.Insert(sql);
                        FormTaoTK_Load(sender, e);
                        MessageBox.Show("Đã Xóa Tài Khoản", "Thông báo");                  
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTaoTK_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
