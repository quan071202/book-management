using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Quanlybanhang1.Class;

namespace Quanlybanhang1
{
    public partial class PhieuNhapSach : Form
    {
        public PhieuNhapSach()
        {
            InitializeComponent();
        }

     
        private void PhieuNhapSach_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnThem.Enabled = false;          
            txtSPN.ReadOnly = false;          
            txtNXB.ReadOnly = false;
            txtMS.ReadOnly = true;
            txtMTG.ReadOnly = true;
            txtMTL.ReadOnly = true;
            txtSL.ReadOnly = true;
            txtTS.ReadOnly = true;
            txtGiaNhap.ReadOnly = true;
            btnLPM.Enabled = false;          
           DateTime aDateTime = DateTime.Now;
            txtNgay.Text = aDateTime.ToShortDateString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {           
            if (txtMS.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Mã Sách", "Lỗi");
            }
            if (txtTS.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Tên Sách", "Lỗi");
            }
            if (txtMTL.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Mã Thể Loại", "Lỗi");
            }
            if (txtMTG.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Mã Tác Giả", "Lỗi");
            }
            if (txtSL.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Số Lượng", "Lỗi");
            }
            if (txtGiaNhap.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Giá Nhập", "Lỗi");
            }
            else
            {
                if (!Funtion.checkMS(txtMS.Text))
                {
                    string sql2 = string.Format(@"INSERT INTO ChiTietPhieuNhap(SoPN,MaSach,SoLuongNhap,GiaNhap,MaNhanVien) VALUES('{0}','{1}',{2},{3},'{4}')", txtSPN.Text, txtMS.Text, int.Parse(txtSL.Text), int.Parse(txtGiaNhap.Text), Funtion.getMaNV());
                    int sL;
                    float giaBan = float.Parse(txtGiaNhap.Text);
                    float loiNhuan = (giaBan*3)/10;
                    giaBan += loiNhuan;
                    sL = int.Parse(txtSL.Text) + Funtion.checkSL(txtMS.Text);
                    string sql3 = string.Format(@"UPDATE Sach SET SoLuongTon = {0},GiaBan = {1} WHERE MaSach = '{2}'",sL,giaBan,txtMS.Text);
                    Funtion.Insert(sql2);
                    Funtion.Insert(sql3);
                    btnTai_Click(sender, e);
                    txtMS.Text = "";
                    txtGiaNhap.Text = "";
                    txtMTG.Text = "";
                    txtMTL.Text = "";
                    txtSL.Text = "";
                    txtTS.Text = "";
                    txtMS.Focus();
                    MessageBox.Show("Đã thêm chi tiết phiếu nhập", "Thông Báo");
                }
                else
                {
                    float giaBan = float.Parse(txtGiaNhap.Text);
                    float loiNhuan = (giaBan * 3) / 10;
                    giaBan += loiNhuan;
                    string sql1 = string.Format(@"INSERT INTO ChiTietPhieuNhap(SoPN,MaSach,SoLuongNhap,GiaNhap,MaNhanVien) VALUES('{0}','{1}',{2},{3},'{4}')", txtSPN.Text, txtMS.Text, int.Parse(txtSL.Text), int.Parse(txtGiaNhap.Text), Funtion.getMaNV());
                    string sql = string.Format(@"INSERT INTO Sach(MaSach,TenSach,MaTL,MaNXB,MaTG,SoLuongTon,GiaBan) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6})", txtMS.Text, txtTS.Text, txtMTL.Text, txtNXB.Text, txtMTG.Text, int.Parse(txtSL.Text),giaBan);
                    Funtion.Insert(sql);
                    Funtion.Insert(sql1);
                    btnTai_Click(sender, e);
                    txtMS.Text = "";
                    txtGiaNhap.Text = "";
                    txtMTG.Text = "";
                    txtMTL.Text = "";
                    txtSL.Text = "";
                    txtTS.Text = "";
                    txtMS.Focus();
                    MessageBox.Show("Đã thêm chi tiết phiếu nhập", "Thông Báo");
                }
            }
         
        
        }

        private void btnTai_Click(object sender, EventArgs e)
        {
            if(txtSPN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số phiếu nhập", "Lỗi");
            }
           
            else
            {
                string sql = @"SELECT ChiTietPhieuNhap.MaSach, Sach.TenSach, Sach.MaTL, Sach.MaTG, Sach.MaNXB, ChiTietPhieuNhap.SoLuongNhap, ChiTietPhieuNhap.GiaNhap, (SoLuongNhap*GiaNhap) AS [Thành tiền]
                FROM Sach INNER JOIN (PhieuNhap INNER JOIN ChiTietPhieuNhap ON PhieuNhap.SoPN = ChiTietPhieuNhap.SoPN) ON Sach.MaSach = ChiTietPhieuNhap.MaSach ";
                string dk = string.Format(@"Where ChiTietPhieuNhap.SoPN like '{0}' ",txtSPN.Text);
                string sql1 = sql + dk;
                DataTable dt = new DataTable();
                dt = Funtion.GetDataToTable(sql1);
                dtGRV.DataSource = dt;
                dtGRV.Columns[0].HeaderText = "Mã Sách";
                dtGRV.Columns[1].HeaderText = "Tên Sách";
                dtGRV.Columns[2].HeaderText = "Mã Thể Loại";
                dtGRV.Columns[3].HeaderText = "Mã Tác Giả";
                dtGRV.Columns[4].HeaderText = "Mã NXB";
                dtGRV.Columns[5].HeaderText = "Số Lượng";
                dtGRV.Columns[6].HeaderText = "Giá Nhập";
                dtGRV.Columns[7].HeaderText = "Thành Tiền";             
                dtGRV.Columns[0].Width = 80;
                dtGRV.Columns[1].Width = 150;
                dtGRV.Columns[2].Width = 100;
                dtGRV.Columns[3].Width = 100;
                dtGRV.Columns[4].Width = 100;
                dtGRV.Columns[5].Width = 100;
                dtGRV.Columns[6].Width = 100;
                dtGRV.Columns[7].Width = 120;              
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if(txtSPN.Text.Trim() == "" || txtNXB.Text.Trim() == "" )
            {
                MessageBox.Show("Hãy nhập đủ số phiếu nhập  và nhà xuất bản !", "Lỗi");
            }
            if (!Funtion.checkSPN(txtSPN.Text))
            {
                MessageBox.Show("Số phiếu nhập bị trùng vui lòng nhập lại", "lỗi");
            }
            else
            {
                string sql1 = string.Format(@"INSERT INTO PhieuNhap(SoPN,NgayNhap,MaNXB) VALUES('{0}','{1}','{2}')", txtSPN.Text, txtNgay.Text, txtNXB.Text);
                Funtion.Insert(sql1);
                txtSPN.ReadOnly = true;
               
                txtNXB.ReadOnly = true;
                btnThem.Enabled = true;
                
                btnXoa.Enabled = true;
                txtMS.ReadOnly = false;
                txtMTG.ReadOnly = false;
                txtMTL.ReadOnly = false;
                txtSL.ReadOnly = false;
                txtTS.ReadOnly = false;
                txtGiaNhap.ReadOnly = false;
                btnLPM.Enabled = true;
               
                dtGRV.Columns.Clear();
                dtGRV.Rows.Clear();
                MessageBox.Show("Đã ghi phiếu nhập","Thông Báo");
            }
        
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn chắc chắn muốn xóa chứ","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtGRV.SelectedRows.Count > 1 || dtGRV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một hàng để xóa", "Lỗi");
                }
                else
                {
                    int rowSelected = dtGRV.CurrentRow.Index;
                    string maSach = dtGRV.Rows[rowSelected].Cells[0].Value.ToString();
                    int soluong = int.Parse(dtGRV.Rows[rowSelected].Cells[5].Value.ToString());
                    int a = Funtion.checkSL(maSach) - soluong;
                    if (a > 0)
                    {
                        string sql = string.Format(@"DELETE FROM ChiTietPhieuNhap Where SoPN = '{0}' and MaSach = '{1}'", txtSPN.Text, maSach);
                        string sql1 = string.Format(@"UPDATE Sach SET SoLuongTon = {0} where MaSach = '{1}'", a, maSach);
                        Funtion.Insert(sql);
                        Funtion.Insert(sql1);
                        btnTai_Click(sender, e);
                        MessageBox.Show("Đã Xóa", "Thông Báo");
                    }
                    else
                    {
                        string sql2 = string.Format(@"DELETE FROM ChiTietPhieuNhap Where SoPN = '{0}' and MaSach = '{1}'", txtSPN.Text, maSach);
                        string sql3 = string.Format(@"DELETE FROM Sach Where MaSach = '{0}'", maSach);
                        Funtion.Insert(sql2); 
                        Funtion.Insert(sql3);
                        btnTai_Click(sender, e);
                        MessageBox.Show("Đã Xóa", "Thông Báo");
                    }
                }
            }          
     
        }

       

        private void btnThoat_Click(object sender, EventArgs e)
        {         
          this.Close();
        }

        private void PhieuNhapSach_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnLPM_Click(object sender, EventArgs e)
        {
            dtGRV.Columns.Clear();
            dtGRV.Rows.Clear();
            txtMS.Text = "";
            txtGiaNhap.Text = "";
            txtMTG.Text = "";
            txtMTL.Text = "";
            txtSL.Text = "";
            txtTS.Text = "";
            txtSPN.Text = "";
            txtNXB.Text = "";
            txtSPN.Focus();
            PhieuNhapSach_Load(sender, e);          
        }

        private void btnTT_Click(object sender, EventArgs e)
        {
            if(dtGRV.Rows.Count == 0)
            {
                MessageBox.Show("Chi tiết phiếu nhập rỗng !", "Lỗi");
            }
            else
            {
               
                int row = dtGRV.Rows.Count;
                int tongTien = 0;
                for(int i = 0; i < row-1; i++)
                {
                    tongTien +=  int.Parse(dtGRV.Rows[i].Cells[7].Value.ToString());
                }
                txtTT.Text = tongTien.ToString();
                MessageBox.Show("Đã tính tiền", "Thông báo");
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
                Funtion.ToExcel(dtGRV,dialog.FileName,txtSPN.Text,txtTT.Text);
            }            
       }
   
    
    }
    }
    

