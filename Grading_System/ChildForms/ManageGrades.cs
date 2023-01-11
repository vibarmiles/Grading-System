using Grading_System.Models;
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
    public partial class ManageGrades : Form
    {
        private readonly IObjectList sections;
        private readonly ISectionStudent students;
        private readonly IStudentClass classes;
        private IDictionary<int, string> sectionList;
        private IDictionary<int, string> studentList;
        private IDictionary<int[], string> classList;

        public ManageGrades(string connectionString)
        {
            sections = new Section(connectionString);
            students = new Student(connectionString);
            classes = new Class(connectionString);
            InitializeComponent();
            this.Load += new EventHandler((s, ev) => this.Dock = DockStyle.Fill);
        }

        private void cbSection_DropDown(object sender, EventArgs e)
        {
            sectionList = sections.GetList();
            cbSection.Items.Clear();

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
            string input = cbStudent.Text;
            classList = classes.GetList(studentList.FirstOrDefault(x => x.Value == input).Key);
            cbSubjects.Items.Clear();
            cbSubjects.Text = String.Empty;

            foreach (string subject in classList.Values)
            {
                cbSubjects.Items.Add(subject);
            }
        }

        private void cbSubjects_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
