using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LAB05.Models; 

namespace LAB05
{
    public partial class Form1 : Form
    {
        StudentContextDB context = new StudentContextDB();

        public Form1()
        {
            InitializeComponent();
        }

        private void frmStudentManagement_Load(object sender, EventArgs e)
        {
            try
            {
                List<Faculty> listFalcultys = context.Faculties.ToList();
                List<Student> listStudent = context.Students.ToList(); 
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtAverageScore.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (txtStudentID.Text.Length != 10)
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự!");
                return false;
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Student dbStudent = context.Students.FirstOrDefault(p => p.StudentID == txtStudentID.Text);
                if (dbStudent != null)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!");
                    return;
                }
                Student s = new Student()
                {
                    StudentID = txtStudentID.Text,
                    FullName = txtFullName.Text,
                    AverageScore = double.Parse(txtAverageScore.Text),
                    FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString())
                };

                context.Students.Add(s);
                context.SaveChanges();
                BindGrid(context.Students.ToList());
                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Student dbUpdate = context.Students.FirstOrDefault(p => p.StudentID == txtStudentID.Text);
                if (dbUpdate != null)
                {
                    dbUpdate.FullName = txtFullName.Text;
                    dbUpdate.AverageScore = double.Parse(txtAverageScore.Text);
                    dbUpdate.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());

                    context.SaveChanges();

                    BindGrid(context.Students.ToList());
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy MSSV cần sửa!");
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student dbDelete = context.Students.FirstOrDefault(p => p.StudentID == txtStudentID.Text);
            if (dbDelete != null)
            {
                DialogResult result = MessageBox.Show($"Bạn có đồng ý xóa sinh viên {dbDelete.FullName} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    context.Students.Remove(dbDelete);
                    context.SaveChanges();

                    BindGrid(context.Students.ToList());
                    MessageBox.Show("Xóa sinh viên thành công!");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy MSSV cần xóa!");
            }
        }
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtFullName.Text = row.Cells[1].Value.ToString();
                cmbFaculty.Text = row.Cells[2].Value.ToString();
                txtAverageScore.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}