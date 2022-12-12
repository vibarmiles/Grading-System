using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;

namespace Grading_System.ChildForms
{
    public partial class ManageTeacher : ManageObject
    {
        private string id;
        public ManageTeacher() : base("TeachersView", "UserID")
        {
            InitializeComponent();
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            RefreshTable();
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);

        }

        private void RefreshTable()
        {
            SetTableFormat();
            tblList.Columns[1].HeaderText = "User ID";
        }

        protected override void Add(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);
            string user = txtUsername.Text;
            string pos = "Teacher";
            string pass = Database.HashPassword(txtPassword.Text);

            if (fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && txtPassword.Text != "")
            {
                string userValue = "'" + fname + "','" + mname + "','" + lname + "','" + user + "',N'" + pass + "','" + pos + "','" + gender + "'";
                Database.Insert("Users", "[FirstName],[MiddleName],[LastName],[Username],[Password],[Position],[Gender]", userValue);
                string teacherValue = Database.SelectID("Users", "[UserID]", "[Username]", "'" + user + "'").ToString() + ",'" + spec + "'";
                Database.Insert("Teachers", "[UserID], [Specialization]", teacherValue);
                InputValidator.ClearTextBox(panel1);
                RefreshTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Edit()
        {
            string[] row = Database.SelectRow("[Users]", "[FirstName],[MiddleName],[LastName],[Username],[Password],[Gender]", "[UserID]", Id);
            string[] spec = Database.SelectRow("[Teachers]", "[Specialization]", "[UserID]", Id);
            txtFirstName.Text = row[0].ToString();
            txtMiddleName.Text = row[1].ToString();
            txtLastName.Text = row[2].ToString();
            txtSpecialization.Text = spec[0].ToString();
            txtUsername.Text = row[3].ToString();
            cbGender.Text = row[5].ToString();
        }

        protected override void Update(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);
            string user = txtUsername.Text;
            string pos = "Teacher";
            string pass = Database.HashPassword(txtPassword.Text);

            if (fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && txtPassword.Text != "")
            {
                Database.Update("Users", "[FirstName]='" + fname + "',[MiddleName]='"+ mname +"',[LastName]='"+ lname +"',[Username]='"+ user +"',[Password]='"+ pass +"',[Position]='"+ pos +"',[Gender]='"+ gender +"'", "[UserID]", Id);
                Database.Update("Teachers", "[Specialization]='" + spec + "'", "[UserID]", Id);
                RefreshTable();
                Cancel(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            Database.Delete("Teachers", "[UserID]", Id);
            Database.Delete("Users", "[UserID]", Id);
            RefreshTable();
        }
    }
}
