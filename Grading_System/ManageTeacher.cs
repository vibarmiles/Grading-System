using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;

namespace Grading_System
{
    public partial class ManageTeacher : Form
    {
        private string id;
        public ManageTeacher()
        {
            InitializeComponent();
        }

        private void Add_Teacher_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            RefreshTable();
        }
        private void RefreshTable()
        {
            btnUpdate.Hide();
            tblList.CellClick -= new DataGridViewCellEventHandler(Edit_Click);
            tblList = ForDataGridView.SetDataGridViewFormat(tblList, "TeachersView", "[UserID]");
            tblList.Columns[1].HeaderText = "User ID";
            tblList.CellClick += new DataGridViewCellEventHandler(Edit_Click);
        }

        private void Edit_Click(object sender, DataGridViewCellEventArgs e)
        {
            this.id = tblList.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Edit"))
            {
                btnUpdate.Show();
                btnAdd.Text = "Cancel";
                btnAdd.Click -= btnAdd_Click;
                btnAdd.Click += btnCancel_Click;
                string[] row = Database.SelectRow("[Users]", "[FirstName],[MiddleName],[LastName],[Username],[Password],[Gender]", "[UserID]", this.id);
                string[] spec = Database.SelectRow("[Teachers]", "[Specialization]", "[UserID]", this.id);
                txtFirstName.Text = row[0].ToString();
                txtMiddleName.Text = row[1].ToString();
                txtLastName.Text = row[2].ToString();
                txtSpecialization.Text = spec[0].ToString();
                txtUsername.Text = row[3].ToString();
                txtPassword.Text = row[4].ToString();
                cbGender.Text = row[5].ToString();
            }

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Delete"))
            {
                DialogResult result = InputValidator.ContinueDelete();

                if (result is DialogResult.Yes)
                {
                    Database.Delete("Teachers", "[UserID]", this.id);
                    Database.Delete("Users", "[UserID]", this.id);
                    RefreshTable();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnUpdate.Hide();
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            btnAdd.Click -= btnCancel_Click;
            clear();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);
            string user = txtUsername.Text;
            string pos = "Teacher";
            string pass = Database.HashPassword(txtUsername.Text);

            if(fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && pass != "")
            {
                string userValue = "'" + fname + "','" + mname + "','" + lname + "','" + user + "',N'" + pass + "','" + pos + "','" + gender + "'";
                Database.Insert("Users", "[FirstName],[MiddleName],[LastName],[Username],[Password],[Position],[Gender]", userValue);
                string teacherValue = Database.SelectID("Users", "[UserID]", "[Username]", "'" + user + "'").ToString() + ",'" + spec + "'";
                Database.Insert("Teachers", "[UserID], [Specialization]", teacherValue);
                clear();
                RefreshTable();
            } else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        private void clear()
        {
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtSpecialization.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);
            string user = txtUsername.Text;
            string pos = "Teacher";
            string pass = Database.HashPassword(txtPassword.Text);

            if (fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && pass != "")
            {
                Database.Update("Users", "[FirstName]='" + fname + "',[MiddleName]='"+ mname +"',[LastName]='"+ lname +"',[Username]='"+ user +"',[Password]='"+ pass +"',[Position]='"+ pos +"',[Gender]='"+ gender +"'", "[UserID]", this.id);
                Database.Update("Teachers", "[Specialization]='" + spec + "'", "[UserID]", this.id);
                clear();
                RefreshTable();
                btnCancel_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }
    }
}
