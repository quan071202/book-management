using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Quanlybanhang1.Class
{

    class Funtion
    {
        public static string ID_USER = "";
        public static OleDbConnection conn;
        public static void Connect()
        {
            string strAdd = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ASUS\Downloads\Data.accdb";
            conn = new OleDbConnection(strAdd);
            if (conn.State != ConnectionState.Open)

                conn.Open();

        }
        public static void Disconcect()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public static void Insert(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        public static void ToExcel(DataGridView dataGridView1, string fileName,string sPN,string TT)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
           
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                      ;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
               
                //đặt tên cho sheet
                worksheet.Name = "Quản lý phiếu nhập";
                worksheet.Cells[1, 4] = "Chi Tiết Phiếu Nhập";             
                worksheet.Cells[2, 1] = "Số phiếu nhập :";
                worksheet.Cells[2, 3] = sPN;
                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[3, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                worksheet.Cells[dataGridView1.RowCount +4, 6] = "Tổng tiền :";
                worksheet.Cells[dataGridView1.RowCount + 4,  8] =  TT;
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }

        public static bool checkSLB(int slb,string a)
        {
            Connect();
            int s = 0;
            string sql = string.Format(@"select SoLuongTon from Sach Where MaSach = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                s = int.Parse(dr["SoLuongTon"].ToString());
            }
           if(slb>s)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
       
        public static bool checkSHD(string a)
        {
            Connect();
            string sql = string.Format(@"select SoHD from HoaDon Where SoHD = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }

        public static DataTable GetDataToTable(string sql)
        {
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }
        public static bool DangNhap(string tk, string mk)
        {
            Connect();
            string sql = string.Format(@"select * from TaiKhoan Where TaiKhoan = '{0}' and MatKhau = '{1}'",tk,mk);
              
            OleDbDataAdapter da = new OleDbDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0 )
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ID_USER = dr["MaNhanVien"].ToString();
                }
                return true; 
            }
            else
            {
                return false;
            }
        }
        public static string getMaNV()
        {
            return ID_USER;
        }
        public static bool checkMS(string a)
        {
            Connect();
            string sql = string.Format(@"select MaSach from Sach Where MaSach = '{0}'", a);
        OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        dap.Fill(dt);
            if (dt.Rows.Count >0)  return false; 
            else
            {
                return true;
            }
        }
        public static bool checkSPN(string a)
        {
            Connect();
            string sql = string.Format(@"select SoPN from PhieuNhap Where SoPN = '{0}'",a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count >0)  return false; 
            else
            {
                return true;
            }
        }
       public static int checkSL(string a)
        {
            Connect();
            int s = 0;
            string sql = string.Format(@"select SoLuongTon from Sach Where MaSach = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               s = int.Parse(dr["SoLuongTon"].ToString());
            }
            return s;
        }
        public static void ToExcelHD(DataGridView dataGridView1, string fileName, string sPN, string TT)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                ;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];

                //đặt tên cho sheet
                worksheet.Name = "Hóa Đơn";
                worksheet.Cells[1, 4] = "Chi Tiết Hóa Đơn";
                worksheet.Cells[2, 1] = "Số Hóa Đơn:";
                worksheet.Cells[2, 3] = sPN;
                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[3, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                worksheet.Cells[dataGridView1.RowCount + 4, 4] = "Tổng tiền :";
                worksheet.Cells[dataGridView1.RowCount + 4, 5] = TT;
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        public static bool checkMTL(string a)
        {
            Connect();
            string sql = string.Format(@"select MaTL from TheLoai Where MaTL = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkMTLtoDelete(string a)
        {
            Connect();
            string sql = string.Format(@"select * from Sach Where MaTL = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkMTG(string a)
        {
            Connect();
            string sql = string.Format(@"select MaTG from TacGia Where MaTG = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkMTGtoDelete(string a)
        {
            Connect();
            string sql = string.Format(@"select * from Sach Where MaTG = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
         public static bool checkPhone(string a)
        {
            if (!IsNumber(a)) { return false; }
            else if(a.Length != 10) { return false; }
            return true;
        }
        public static bool checkMNXB(string a)
        {
            Connect();
            string sql = string.Format(@"select MaNXB from NhaXuatBan Where MaNXB = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkMNXBtoDelete(string a)
        {
            Connect();
            string sql = string.Format(@"select * from Sach Where MaNXB = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static string getMaTL(string a)
        {
            Connect();
            string maTL = "";
            string sql = string.Format(@"select MaTL from TheLoai Where TenTL = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                maTL =dr["MaTL"].ToString();
            }
            return maTL;
        }
        public static string getMaNXB(string a)
        {
            Connect();
            string maNXB = "";
            string sql = string.Format(@"select MaNXB from NhaXuatBan Where TenNXB = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                maNXB = dr["MaNXB"].ToString();
            }
            return maNXB;
        }
        public static string getMaTG(string a)
        {
            Connect();
            string maNXB = "";
            string sql = string.Format(@"select MaTG from TacGia Where TenTG = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                maNXB = dr["MaTG"].ToString();
            }
            return maNXB;
        }
        public static List<string> getSoHDTheoNgay(string a)
        {
            Connect();
            List<string> list = new List<string>();
            string sql = string.Format(@"select SoHD from HoaDon Where NgayBan = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                  list.Add(dr["SoHD"].ToString());
            }
            return list;
        }
        public static List<string> getSoHDTheoThang(string a,string b)
        {
            Connect();
            List<string> list = new List<string>();
            string sql = string.Format(@"select SoHD from HoaDon Where NgayBan LIKE '{0}%{1}'", a,b);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["SoHD"].ToString());
            }
            return list;
        }
        public static bool checkNgay(string ngay)
        {
            Connect();
            string sql = string.Format(@"select * from HoaDon Where NgayBan = '{0}'", ngay);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
       
        public static void ToExcelDoanhThu(DataGridView dataGridView1, string fileName, string Ngay, string DT)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                ;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];

                //đặt tên cho sheet
                worksheet.Name = "Báo Cáo Doanh Thu";
                worksheet.Cells[1, 4] = "Chi Tiết Doanh Thu";
                worksheet.Cells[2, 1] = "Ngày Bán :";
                worksheet.Cells[2, 3] = Ngay;
                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[3, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                worksheet.Cells[dataGridView1.RowCount + 4, 4] = "Tổng doanh thu :";
                worksheet.Cells[dataGridView1.RowCount + 4, 6] = DT;
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        public static void ToExcelDoanhThuThang(DataGridView dataGridView1, string fileName, string Thang, string DT)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                ;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];

                //đặt tên cho sheet
                worksheet.Name = "Báo Cáo Doanh Thu";
                worksheet.Cells[1, 4] = "Chi Tiết Doanh Thu";
                worksheet.Cells[2, 1] = "Tháng :";
                worksheet.Cells[2, 3] = Thang;
                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[3, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                worksheet.Cells[dataGridView1.RowCount + 4, 4] = "Tổng doanh thu :";
                worksheet.Cells[dataGridView1.RowCount + 4, 6] = DT;
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        public static bool checkMKHT(string a)
        {
            Connect();         
            string sql = string.Format(@"select * from TaiKhoan Where MatKhau = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
    public static bool checkMKM(string mkC,string mkM)
        {
            if(mkM.Length < 6)
            {
                return false;
            }
            else if (mkM.Equals(mkC))
            {             
                return false;
            }
            return true;
        }
        public static bool checkMNV(string a)
        {
            Connect();
            string sql = string.Format(@"select * from NhanVien Where MaNhanVien = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkTK(string a)
        {
            Connect();
            string sql = string.Format(@"select * from TaiKhoan Where TaiKhoan = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkMNVCoTK(string a)
        {
            Connect();
            string sql = string.Format(@"select * from TaiKhoan Where MaNhanVien = '{0}'", a);
            OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0) return false;
            else
            {
                return true;
            }
        }
        public static bool checkTaoMK(string a)
        {
            if(a.Length < 6)
            {
                return false;
            }
            return true;
        }
    }
    }