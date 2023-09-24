using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanlybanhang1.Class;

namespace Quanlybanhang1
{
    public partial class FormHD : Form
    {
        public FormHD()
        {
            InitializeComponent();
        }

        private void FormHD_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            btnGhiMoi.Enabled = false;
            btnThem.Enabled = false;
            txtSL.Enabled = false;
            btnGhi.Enabled = true;
            DateTime aDateTime = DateTime.Now;
            txtNB.Text = aDateTime.ToShortDateString();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            if(txtSHD.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số hóa đơn cần tìm", "Lỗi");
            }
            else if (Funtion.checkSHD(txtSHD.Text))
            {
                MessageBox.Show("Số hóa đơn không tồn tại", "Lỗi");
            }
            else
            {
                string sql = @"SELECT ChiTietHoaDon.MaSach, Sach.TenSach, ChiTietHoaDon.SoLuongBan, Sach.GiaBan ,(SoLuongBan*GiaBan) AS [Thành Tiền]
                FROM HoaDon INNER JOIN (Sach INNER JOIN ChiTietHoaDon ON Sach.MaSach = ChiTietHoaDon.MaSach) ON HoaDon.SoHD = ChiTietHoaDon.SoHD ";
                string dk = string.Format(@"Where ChiTietHoaDon.SoHD like '{0}' ", txtSHD.Text);
                string sql1 = sql + dk;
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql1);
                dtgrHD.DataSource = dt;
                dtgrHD.Columns[0].HeaderText = "Mã Sách";
                dtgrHD.Columns[1].HeaderText = "Tên Sách";
                dtgrHD.Columns[2].HeaderText = "Số lượng";
                dtgrHD.Columns[3].HeaderText = "Giá Bán";
                dtgrHD.Columns[4].HeaderText = "Thành Tiền";
                dtgrHD.Columns[0].Width = 80;
                dtgrHD.Columns[1].Width = 150;
                dtgrHD.Columns[2].Width = 100;
                dtgrHD.Columns[3].Width = 100;
                dtgrHD.Columns[4].Width = 100;
               
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtSHD.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số hóa đơn cần tìm", "Lỗi");
            }
           else if (!Funtion.checkSHD(txtSHD.Text))
            {
                MessageBox.Show("Số hóa đơn đã tồn tại", "Lỗi");
            }
            else
            {
                string sql1 = string.Format(@"INSERT INTO HoaDon(SoHD,NgayBan) VALUES('{0}','{1}')", txtSHD.Text, txtNB.Text);
                Funtion.Insert(sql1);
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                btnGhiMoi.Enabled = true;
                txtSHD.Enabled = false;
                btnTra.Enabled = false;
                
                btnGhi.Enabled = false;
                MessageBox.Show("Đã ghi hóa đơn", "Thông báo");
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormHD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if(txtMS1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã sách cần tìm", "Lỗi");
            }
            else if (Funtion.checkMS(txtMS1.Text))
            {
                MessageBox.Show("Mã sách không tồn tại", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"SELECT Sach.TenSach, Sach.MaTL, Sach.MaTG, Sach.GiaBan FROM Sach WHERE Sach.MaSach like '{0}'",txtMS1.Text);
                DataTable dt = Funtion.GetDataToTable(sql);           
                    foreach (DataRow dr in dt.Rows)
                    {
                    txtTenSach.Text= dr["TenSach"].ToString();
                    txtTG.Text = dr["MaTG"].ToString();
                    txtTL.Text = dr["MaTL"].ToString();
                    txtGB.Text = dr["GiaBan"].ToString();
                    }
                btnThem.Enabled = true;
                txtMS1.Enabled = false;
                txtSL.Enabled = true;
                                  
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            txtMS1.Text = "";
            txtGB.Text = "";
            txtSL.Text = "";
            txtTG.Text = "";
            txtTL.Text = "";
            txtTenSach.Text = "";
            txtThanhTien.Text = "";
            txtMS1.Focus();
            txtMS1.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtSL.Text.Trim() == "" )
            {
                MessageBox.Show("Hãy nhập số lượng bán", "Lỗi");
            }           
             else if (int.Parse(txtSL.Text) <= 0)
                {
                    MessageBox.Show("Hãy nhập lại số lượng bán", "Lỗi");
                }
            else if (!Funtion.checkSLB(int.Parse(txtSL.Text), txtMS1.Text))
                {
                    MessageBox.Show("Số lượng bán nhiều hơn số lượng sách còn trong kho !", "Lỗi");
                }
                else
                {
                    string maNV = Funtion.getMaNV();
                    string sql = string.Format(@"INSERT INTO ChiTietHoaDon(SoHD,MaSach,SoLuongBan,MaNhanVien) VALUES('{0}','{1}',{2},'{3}')", txtSHD.Text, txtMS1.Text, int.Parse(txtSL.Text), maNV);
                    int soluongTon = Funtion.checkSL(txtMS1.Text) - int.Parse(txtSL.Text);
                    string sql1 = string.Format(@"UPDATE Sach SET SoLuongTon = '{0}' WHERE MaSach = '{1}'", soluongTon, txtMS1.Text);
                    Funtion.Insert(sql);
                    Funtion.Insert(sql1);
                    btnMoi_Click(sender, e);
                    btnTra_Click(sender, e);
                    MessageBox.Show("Đã thêm chi tiết hóa đơn", "Thông báo");
                }          
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if(txtSL.Text.Trim() != "" && Funtion.IsNumber(txtSL.Text))
            {
                txtThanhTien.Text = (int.Parse(txtSL.Text) * float.Parse(txtGB.Text)).ToString();
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgrHD.SelectedRows.Count > 1 || dtgrHD.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtgrHD.CurrentRow.Index;
                    string maSach = dtgrHD.Rows[rowSelected].Cells[0].Value.ToString();
                    int soluong = int.Parse(dtgrHD.Rows[rowSelected].Cells[2].Value.ToString());
                    int a = Funtion.checkSL(maSach) + soluong;
                    string sql = string.Format(@"DELETE FROM ChiTietHoaDon Where SoHD = '{0}' and MaSach = '{1}'", txtSHD.Text, maSach);
                    string sql1 = string.Format(@"UPDATE Sach SET SoLuongTon = {0} where MaSach = '{1}'", a, maSach);
                    Funtion.Insert(sql);
                    Funtion.Insert(sql1);
                    btnTra_Click(sender, e);
                    MessageBox.Show("Đã Xóa", "Thông Báo");
                }
            }
        }

        private void btnGhiMoi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn ghi hóa đơn mới chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {              
                txtMS1.Text = "";
                txtGB.Text = "";
                txtSHD.Text = "";
                txtTenSach.Text = "";
                txtTL.Text = "";
                txtTG.Text = "";
                txtTongTien.Text = "";
                txtThanhTien.Text = "";
                txtSL.Text = "";
                txtSHD.Focus();
                FormHD_Load(sender, e);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Funtion.ToExcelHD(dtgrHD, dialog.FileName, txtSHD.Text, txtTongTien.Text);
            }
        }

        private void btnTT_Click(object sender, EventArgs e)
        {
            if (dtgrHD.Rows.Count == 0)
            {
                MessageBox.Show("Chi tiết phiếu nhập rỗng !", "Lỗi");
            }
            else
            {

                int row = dtgrHD.Rows.Count;
                int tongTien = 0;
                for (int i = 0; i < row - 1; i++)
                {
                    tongTien += int.Parse(dtgrHD.Rows[i].Cells[4].Value.ToString());
                }
                txtTongTien.Text = tongTien.ToString();
                MessageBox.Show("Đã tính tiền", "Thông báo");
            }
        }
    }
}
