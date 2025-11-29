using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi03
{
    public partial class Bai03 : Form
    {
        public Bai03()
        {
            InitializeComponent();
            this.Text = "ListView Demo";
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvSinhVien.SelectedItems[0];
                txtLastName.Text = selectedItem.Text;
                txtFirstName.Text = selectedItem.SubItems[1].Text;
                txtPhone.Text = selectedItem.SubItems[2].Text;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            ListViewItem item = new ListViewItem(txtLastName.Text);
            item.SubItems.Add(txtFirstName.Text);
            item.SubItems.Add(txtPhone.Text);
            lvSinhVien.Items.Add(item);
            txtLastName.Clear();
            txtFirstName.Clear();
            txtPhone.Clear();
            txtLastName.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
 }

