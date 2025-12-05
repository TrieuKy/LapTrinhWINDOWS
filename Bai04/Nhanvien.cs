using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai04
{
    public partial class Nhanvien : Form
    {
        public delegate void TruyenDuLieu(string msnv, string ten, string luong);
        public TruyenDuLieu Sender;
        public Nhanvien()
        {
            InitializeComponent();
        }
        public Nhanvien(string msnv, string ten, string luong): this()
        {
            txtMSNV.Text = msnv;
            txtTenNV.Text = ten;
            txtLuong.Text = luong;  
            txtMSNV.Enabled = false;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if(Sender != null)
            {
                Sender(txtMSNV.Text, txtTenNV.Text, txtLuong.Text);    
            }
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
