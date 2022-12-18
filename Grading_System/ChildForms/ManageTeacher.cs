using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;
using Grading_System.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Grading_System.ChildForms
{
    public partial class ManageTeacher : BaseManageObject
    {
        private readonly ATeacher teacher;

        public ManageTeacher(string connectionString) : base(new Teacher(connectionString), "UserID")
        {
            InitializeComponent();
            teacher = new Teacher(connectionString);
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
            ViewTable();
        }

        protected override void ViewTable()
        {
            base.ViewTable();
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
            string pass = txtPassword.Text;

            if (fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && pass != "")
            {
                teacher.Fname = fname;
                teacher.Mname = mname;
                teacher.Lname = lname;
                teacher.Username = user;
                teacher.Password = pass;
                teacher.Gender = gender;
                teacher.Specialization = spec;
                teacher.Add();
                Cancel(sender, e);
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Edit()
        {
            DataRow row = teacher.GetValues(Id);
            txtFirstName.Text = row["FirstName"].ToString();
            txtMiddleName.Text = row["MiddleName"].ToString();
            txtLastName.Text = row["LastName"].ToString();
            txtSpecialization.Text = row["Specialization"].ToString();
            txtUsername.Text = row["Username"].ToString();
            cbGender.Text = row["Gender"].ToString();
        }

        protected override void Update(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);
            string user = txtUsername.Text;
            string pass = txtPassword.Text;

            if (fname != null && mname != null && lname != null && spec != null && gender != "" && user != "" && pass != "")
            {
                teacher.Fname = fname;
                teacher.Mname = mname;
                teacher.Lname = lname;
                teacher.Username = user;
                teacher.Password = pass;
                teacher.Gender = gender;
                teacher.Specialization = spec;
                teacher.Update(Id);
                ViewTable();
                Cancel(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            teacher.Delete(Id);
            ViewTable();
        }
    }
}
