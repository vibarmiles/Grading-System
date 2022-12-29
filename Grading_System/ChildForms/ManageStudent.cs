using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Grading_System.Classes;
using Grading_System.Models;

namespace Grading_System.ChildForms
{
    public partial class ManageStudent : BaseManageObject
    {
        private readonly IStudent student;
        private readonly IObjectList section;
        private IDictionary<int, string> sections;

        public ManageStudent(string connectionString) : base(new Student(connectionString))
        {
            InitializeComponent();
            student = new Student(connectionString);
            section = new Section(connectionString);
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) =>
            {
                this.Dock = DockStyle.Fill;
                cbSection_DropDown(sender, e);
                dtEnrollmentDate.MaxDate = DateTime.Now;
                dtEnrollmentDate.Value = DateTime.Now.Date;
            });
            ViewTable();
        }

        protected override void ViewTable()
        {
            base.ViewTable();
            tblList.Columns[1].HeaderText = "Student ID";
            tblList.Columns[1].Name = "ID";
        }

        protected override void Add(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string section = cbSection.Text;
            string lrn = InputValidator.CheckIntTextBox(txtLRN.Text).ToString();
            DateTime date = dtEnrollmentDate.Value;

            if (fname != null && mname != null && lname != null && section != "" && gender != "" && lrn != "0" && date != null)
            {
                student.FName = fname;
                student.MName = mname;
                student.LName = lname;
                student.Gender = gender;
                student.SectionID = sections.FirstOrDefault(x => x.Value == section).Key;
                student.LRN = lrn;
                student.EnrollmentDate = date;
                student.Add();
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
            student.GetValues(Id);
            txtFirstName.Text = student.FName;
            txtMiddleName.Text = student.MName;
            txtLastName.Text = student.LName;
            txtLRN.Text = student.LRN;
            cbGender.Text = student.Gender;
            cbSection.Text = student.SectionName;
            dtEnrollmentDate.Value = Convert.ToDateTime(student.EnrollmentDate);
        }

        protected override void Update(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string section = cbSection.Text;
            string lrn = InputValidator.CheckIntTextBox(txtLRN.Text).ToString();
            DateTime date = dtEnrollmentDate.Value;

            if (fname != null && mname != null && lname != null && section != "" && gender != "" && lrn != "0" && date != null)
            {
                student.FName = fname;
                student.MName = mname;
                student.LName = lname;
                student.Gender = gender;
                student.SectionID = sections.FirstOrDefault(x => x.Value == section).Key;
                student.LRN = lrn;
                student.EnrollmentDate = date;
                student.Update(Id);
                Cancel(sender, e);
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            student.Delete(Id);
            ViewTable();
        }

        public void RefreshTable()
        {
            ViewTable();
        }

        private void cbSection_DropDown(object sender, EventArgs e)
        {
            sections = section.GetList();
            cbSection.Items.Clear();

            foreach (string section in sections.Values)
            {
                cbSection.Items.Add(section);
            }
        }
    }
}
