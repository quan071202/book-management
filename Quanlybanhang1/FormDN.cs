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
    public partial class FormDN : Form
    {
        public FormDN()
        {
            InitializeComponent();
        }

        

        private void btnDN_Click(object sender, EventArgs e)
        {       
                if (Funtion.DangNhap(txtTK.Text,txtMK.Text))
                {
                    formMain f = new formMain();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu ", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
    }
}
