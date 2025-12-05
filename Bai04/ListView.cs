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
    public partial class ListView : Form
    {
        public ListView()
        {
            InitializeComponent();
        }
        private void ThemNV(string msnv, string ten, string luong)
        {
            dgvNhanVien.Rows.Add(msnv, ten, luong);
        }
        private void SuaNV(string msnv, string ten, string luong)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                dgvNhanVien.CurrentRow.Cells[0].Value = msnv;
                dgvNhanVien.CurrentRow.Cells[1].Value = ten;
                dgvNhanVien.CurrentRow.Cells[2].Value = luong;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Nhanvien frm = new Nhanvien();
            frm.Sender = new Nhanvien.TruyenDuLieu(ThemNV);
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null && !dgvNhanVien.CurrentRow.IsNewRow)
            {
                string ma = Convert.ToString(dgvNhanVien.CurrentRow.Cells[0].Value);
                string ten = Convert.ToString(dgvNhanVien.CurrentRow.Cells[1].Value);
                string luong = Convert.ToString(dgvNhanVien.CurrentRow.Cells[2].Value);
                Nhanvien frm = new Nhanvien(ma, ten, luong);
                frm.Sender = new Nhanvien.TruyenDuLieu(SuaNV);

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng dữ liệu hợp lệ để sửa!");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null && !dgvNhanVien.CurrentRow.IsNewRow)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhân viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (result == DialogResult.Yes)
                {
                    dgvNhanVien.Rows.Remove(dgvNhanVien.CurrentRow);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng dữ liệu cần xóa!", "Thông báo");
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
