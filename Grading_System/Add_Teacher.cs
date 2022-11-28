using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace Grading_System
{
    public partial class Add_Teacher : Form
    {
        private string id;
        public Add_Teacher()
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
            tblList.Columns.Clear();
            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            edit.Name = "Edit";
            edit.Text = "Edit";
            edit.UseColumnTextForButtonValue = true;
            tblList.CellClick -= new DataGridViewCellEventHandler(edit_Click);
            tblList.CellClick += new DataGridViewCellEventHandler(edit_Click);
            tblList.Columns.Insert(0, edit);
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.DataSource = Database.ViewTable("TeachersView");
            tblList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.Name = "Delete";
            delete.Text = "Delete";
            delete.UseColumnTextForButtonValue = true;
            tblList.Columns.Insert(tblList.Columns.Count, delete);
            tblList.Columns[tblList.Columns.Count-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void edit_Click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataTable dt = Database.ViewTable("TeachersView");

            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && tblList.Columns[e.ColumnIndex].Name == "Edit" && e.RowIndex >= 0)
            {
                DataRow index = dt.Rows[e.RowIndex];
                this.id = index[0].ToString();
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

            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && tblList.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
            {
                DataRow index = dt.Rows[e.RowIndex];
                this.id = index[0].ToString();
                string message = "Are you sure?";
                string caption = "Delete Row";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
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
            string pass = Database.HashPassword(txtUsername.Text);

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
