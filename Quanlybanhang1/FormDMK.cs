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
    public partial class FormDMK : Form
    {
        public FormDMK()
        {
            InitializeComponent();
        }

        private void btnDMK_Click(object sender, EventArgs e)
        {
            if (txtMKC.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại !", "Lỗi");
            }
            else if (Funtion.checkMKHT(txtMKC.Text))
            {
                MessageBox.Show("Mật khẩu hiện tại sai !", "Lỗi");
            }
            else if (txtMKM.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới !", "Lỗi");
            }
            else if (!Funtion.checkMKM(txtMKC.Text, txtMKM.Text))
            {
                MessageBox.Show("Nhập mật khẩu mới không hợp lệ", "Lỗi");
            }          
            else if (txtMKM1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu mới !", "Lỗi");
            }          
           else if (!txtMKM.Text.Equals(txtMKM1.Text))
            {
                MessageBox.Show("Xác nhận mật khẩu sai", "Lỗi");
            }
            else
            {
                string sql = string.Format(@"UPDATE TaiKhoan SET MatKhau = '{0}'", txtMKM.Text);
                Funtion.Insert(sql);
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                btnT_Click(sender, e);
            }
           
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
