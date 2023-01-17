using Grading_System.Classes;
using Grading_System.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.ChildForms
{
    public partial class ManageAsstTeacher : BaseManageObject
    {
        private readonly IUser assistantTeacher;

        public ManageAsstTeacher(string connectionString) : base(new AssistantTeacher(connectionString))
        {
            InitializeComponent();
            assistantTeacher = new AssistantTeacher(connectionString);
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
            tblList.Columns[1].Name = "ID";
        }

        protected override void Add(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;

            if (fname != null && mname != null && lname != null && gender != "")
            {
                assistantTeacher.Fname = fname;
                assistantTeacher.Mname = mname;
                assistantTeacher.Lname = lname;
                assistantTeacher.Gender = gender;
                assistantTeacher.Position = "Assistant Teacher";
                assistantTeacher.Add();
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
            assistantTeacher.GetValues(Id);
            txtFirstName.Text = assistantTeacher.Fname;
            txtMiddleName.Text = assistantTeacher.Mname;
            txtLastName.Text = assistantTeacher.Lname;
            cbGender.Text = assistantTeacher.Gender;
        }

        protected override void Update(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;

            if (fname != null && mname != null && lname != null && gender != "")
            {
                assistantTeacher.Fname = fname;
                assistantTeacher.Mname = mname;
                assistantTeacher.Lname = lname;
                assistantTeacher.Gender = gender;
                assistantTeacher.Update(Id);
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
            assistantTeacher.Delete(Id);
            ViewTable();
        }
    }
}
