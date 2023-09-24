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
    
    public partial class formMain : Form
    {
        bool isThoat = true;
        public formMain()
        {
            InitializeComponent();
        }

        private void mnuDX_Click(object sender, EventArgs e)
        {
            isThoat = false;
            this.Close();
            FormDN f = new FormDN();
            f.Show();
        }

      
        private void formMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isThoat)
            {
               Application.Exit();
            }


        }
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (isThoat)
                Funtion.Disconcect();
                Application.Exit();
        }

        private void formMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (isThoat)
            {
                if (MessageBox.Show("Bạn muốn thoát chương trình", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            Funtion.Connect();
            string maNV = Funtion.getMaNV();
            if (!maNV.Equals("01"))
            {
                mnuTK.Enabled = false;
            }
            panel1.Visible = true;
            panel5.Visible = false;
            DataTable dt1 = new DataTable();
            string sql1 = @"SELECT * FROM Sach";
            dt1 = Funtion.GetDataToTable(sql1);
            dtgrv.DataSource = dt1;
            dtgrv.Columns[0].HeaderText = "Mã Sách";
            dtgrv.Columns[1].HeaderText = "Tên Sách";
            dtgrv.Columns[2].HeaderText = "Mã Thể Loại";
            dtgrv.Columns[3].HeaderText = "Mã NXB";
            dtgrv.Columns[4].HeaderText = "Mã Tác Giả";
            dtgrv.Columns[5].HeaderText = "Số Lượng Tồn";
            dtgrv.Columns[6].HeaderText = "Giá Bán";
            dtgrv.Columns[0].Width = 100;
            dtgrv.Columns[1].Width = 120;
            dtgrv.Columns[2].Width = 100;
            dtgrv.Columns[3].Width = 100;
            dtgrv.Columns[4].Width = 100;
            dtgrv.Columns[5].Width = 120;
            dtgrv.Columns[6].Width = 120;
        }

        private void mnuPNS_Click(object sender, EventArgs e)
        {
            PhieuNhapSach pns = new PhieuNhapSach();          
            pns.Show();
        }

        private void mnuHDBS_Click(object sender, EventArgs e)
        {
            FormHD formHD = new FormHD();
            formHD.Show();
        }

        private void mnuNNXB_Click(object sender, EventArgs e)
        {
            FormNXB formNXB = new FormNXB();
            formNXB.Show();
        }

        private void mnuNTL_Click(object sender, EventArgs e)
        {
            FormNTL formNTL = new FormNTL();
            formNTL.Show();
        }

        private void mnuNTG_Click(object sender, EventArgs e)
        {
            FormNTG formNTG = new FormNTG();
            formNTG.Show();
        }

        private void mnuTSTTL_Click(object sender, EventArgs e)
        {
            FormTTL formTTL = new FormTTL();
            formTTL.Show();
        }

        private void mnuTSTTG_Click(object sender, EventArgs e)
        {
            FormTTG formTTG = new FormTTG();   
            formTTG.Show();
        }

        private void mnuTSTNXB_Click(object sender, EventArgs e)
        {
            FormTNXB formTNXB = new FormTNXB();
            formTNXB.Show();
        }

        private void mnuTKSBTN_Click(object sender, EventArgs e)
        {
            FormBaoCaoThongKe formBaoCao = new FormBaoCaoThongKe();
            formBaoCao.Show();
        }

        private void mnuDMK_Click(object sender, EventArgs e)
        {
            FormDMK formDMK = new FormDMK();
            formDMK.Show();
        }

        private void mnuTK_Click(object sender, EventArgs e)
        {
            FormTaoTK formTaoTK = new FormTaoTK();
            formTaoTK.Show();
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           panel1.Visible = false;
            panel5.Visible = true;
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;        
            panel5.Visible = false;
        }

        private void mnuTSTTS_Click(object sender, EventArgs e)
        {
            FormTim formTim = new FormTim();
            formTim.Show();
        }

        private void thooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBaoCaoTheoThang formBaoCaoTheoThang = new FormBaoCaoTheoThang();
            formBaoCaoTheoThang.Show();
        }
    }
}
