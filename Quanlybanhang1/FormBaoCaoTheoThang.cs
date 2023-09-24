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
    public partial class FormBaoCaoTheoThang : Form
    {
        public FormBaoCaoTheoThang()
        {
            InitializeComponent();
        }

        private void FormBaoCaoTheoThang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtThang.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tháng cần tìm", "Lỗi");
            }
            
            else
             {               
                string []a = txtThang.Text.Split('/');
                string b = a[0];
                string c = a[1];
                List<string> list = new List<string>();
                list = Funtion.getSoHDTheoThang(b,c);
                DataTable dt = new DataTable();
                string sql = @"SELECT Sach.MaSach, Sach.TenSach, ChiTietPhieuNhap.GiaNhap, Sach.GiaBan, ChiTietHoaDon.SoLuongBan, ((GiaBan-GiaNhap)*SoLuongBan) AS [Doanh Thu]
            FROM (Sach INNER JOIN (HoaDon INNER JOIN ChiTietHoaDon ON HoaDon.SoHD = ChiTietHoaDon.SoHD) ON Sach.MaSach = ChiTietHoaDon.MaSach) INNER JOIN ChiTietPhieuNhap ON Sach.MaSach = ChiTietPhieuNhap.MaSach";
                string dk = "";
                int count = 0;
                foreach (string item in list)
                {
                    if (count == 0)
                    {
                        dk += string.Format(@" Where HoaDon.SoHD = '{0}' ", item);
                        count++;
                    }
                    else
                    {
                        dk += string.Format(@" OR HoaDon.SoHD = '{0}' ", item);
                    }
                }
                sql = sql + dk;
                dt = Funtion.GetDataToTable(sql);
                dtgrvDT.DataSource = dt;
                dtgrvDT.Columns[0].HeaderText = "Mã Sách";
                dtgrvDT.Columns[1].HeaderText = "Tên Sách";
                dtgrvDT.Columns[2].HeaderText = "Giá Nhập";
                dtgrvDT.Columns[3].HeaderText = "Giá Bán";
                dtgrvDT.Columns[4].HeaderText = "Số Lượng Bán";
                dtgrvDT.Columns[5].HeaderText = "Doanh Thu";
                dtgrvDT.Columns[0].Width = 100;
                dtgrvDT.Columns[1].Width = 120;
                dtgrvDT.Columns[2].Width = 120;
                dtgrvDT.Columns[3].Width = 120;
                dtgrvDT.Columns[4].Width = 100;
                dtgrvDT.Columns[5].Width = 150;
                if (dtgrvDT.Rows.Count == 0)
                {
                    MessageBox.Show("Chi tiết doanh thu rỗng !", "Lỗi");
                }
                else
                {

                    int row = dtgrvDT.Rows.Count;
                    int tongTien = 0;
                    for (int i = 0; i < row - 1; i++)
                    {
                        tongTien += int.Parse(dtgrvDT.Rows[i].Cells[5].Value.ToString());
                    }
                    txtTDT.Text = tongTien.ToString();
                }
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
                Funtion.ToExcelDoanhThuThang(dtgrvDT, dialog.FileName, txtThang.Text, txtTDT.Text);
            }
        }

        private void FormBaoCaoTheoThang_Load(object sender, EventArgs e)
        {
            DateTime aDateTime = DateTime.Now;
            String []a = aDateTime.ToShortDateString().Split('/');
            string b = a[0];
            string c = a[1];
            string d = a[2];
            b = b +'/'+ d;
            txtThang.Text = b;  
        }
    }
}
