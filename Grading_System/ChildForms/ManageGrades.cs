using Grading_System.Models;
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
    public partial class ManageGrades : Form
    {
        private readonly IObjectList sections;
        private readonly IObjectListTeacher sectionsOfTeacher;
        private readonly ISectionStudent students;
        private readonly IStudentClass classes;
        private readonly IObjectListStudent classesOfTeacher;
        private readonly IGrades grades;
        private IDictionary<int, string> sectionList;
        private IDictionary<int, string> studentList;
        private IDictionary<int[], string> classList;
        private string position;
        private int id;

        public ManageGrades(string connectionString, string position, int id)
        {
            sections = new Section(connectionString);
            sectionsOfTeacher = new Section(connectionString);
            students = new Student(connectionString);
            classes = new Class(connectionString);
            grades = new Grades(connectionString);
            classesOfTeacher = new Class(connectionString);
            this.position = position;
            this.id = id;
            InitializeComponent();
            this.Load += new EventHandler((s, ev) => this.Dock = DockStyle.Fill);
        }

        private void cbSection_DropDown(object sender, EventArgs e)
        {
            if (position.Equals("Admin"))
            {
                sectionList = sections.GetList();
            } else
            {
                sectionList = sectionsOfTeacher.GetTeacherList(id);
            }
            cbSection.Items.Clear();
            tblList.Columns.Clear();

            foreach (string section in sectionList.Values)
            {
                cbSection.Items.Add(section);
            }
        }

        private void cbSection_SelectedValueChanged(object sender, EventArgs e)
        {
            string input = cbSection.Text;
            studentList = students.GetList(sectionList.FirstOrDefault(x => x.Value == input).Key);
            cbStudent.Items.Clear();
            cbStudent.Text = String.Empty;
            cbSubjects.Text = String.Empty;

            foreach (string student in studentList.Values)
            {
                cbStudent.Items.Add(student);
            }
        }

        private void cbStudent_SelectedValueChanged(object sender, EventArgs e)
        {
            tblList.Columns.Clear();
            string input = cbStudent.Text;
            int student = studentList.FirstOrDefault(x => x.Value == input).Key;
            if (position.Equals("Admin"))
            {
                classList = classes.GetList(student);
            } else
            {
                classList = classesOfTeacher.GetStudentList(student, id);
            }
            cbSubjects.Items.Clear();
            cbSubjects.Text = String.Empty;
            tblList.DataSource = grades.View(student, position, id);
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[0].HeaderText = "ID";

            foreach (string subject in classList.Values)
            {
                cbSubjects.Items.Add(subject);
            }
        }

        private void cbSubjects_SelectedValueChanged(object sender, EventArgs e)
        {
            int student = studentList.FirstOrDefault(x => x.Value == cbStudent.Text).Key;
            int teacher = 0;
            if (position.Equals("Admin"))
            {
                teacher = classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[1];
            } else
            {
                teacher = id;
            }
            int subject = classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[0];
            DataTable col = grades.GetGrades(student, subject, teacher);
            txtQuarter1.Text = col.Rows[0][0].ToString();
            txtQuarter2.Text = col.Rows[1][0].ToString();
            txtQuarter3.Text = col.Rows[2][0].ToString();
            txtQuarter4.Text = col.Rows[3][0].ToString();
            btnUpdate.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tblList.Columns.Clear();
            txtQuarter1.Clear();
            txtQuarter2.Clear();
            txtQuarter3.Clear();
            txtQuarter4.Clear();
            cbSection.Text = String.Empty;
            cbStudent.Text = String.Empty;
            cbSubjects.Text = String.Empty;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int student = studentList.FirstOrDefault(x => x.Value == cbStudent.Text).Key;
            int teacher = 0;
            if (position.Equals("Admin"))
            {
                teacher = classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[1];
            }
            else
            {
                teacher = id;
            }
            int subject = classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[0];
            double prelim = InputValidator.CheckDoubleTextBox(txtQuarter1.Text);
            double midterm = InputValidator.CheckDoubleTextBox(txtQuarter2.Text);
            double prefinal = InputValidator.CheckDoubleTextBox(txtQuarter3.Text);
            double final = InputValidator.CheckDoubleTextBox(txtQuarter4.Text);

            if (prelim < 100 && midterm < 100 && prefinal < 100 && final < 100 && prelim >= 0 && midterm >= 0 && prefinal >= 0 && final >= 0)
            {
                grades.Update(student, subject, teacher, 1, prelim);
                grades.Update(student, subject, teacher, 2, midterm);
                grades.Update(student, subject, teacher, 3, prefinal);
                grades.Update(student, subject, teacher, 4, final);
                MessageBox.Show("Successfully Updated!");
                cbSubjects.Text = String.Empty;
                txtQuarter1.Clear();
                txtQuarter2.Clear();
                txtQuarter3.Clear();
                txtQuarter4.Clear();
                btnUpdate.Enabled = false;
                cbStudent_SelectedValueChanged(sender, e);
            } else
            {
                MessageBox.Show("Invalid Input!");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }
    }
}
