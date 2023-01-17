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
    public partial class Dashboard : Form
    {
        string position;
        private int id;
        private readonly IObjectList sections;
        private readonly IStudentClass classes;
        private readonly ISectionStudent students;
        private readonly IObjectListTeacher sectionsOfTeacher;
        private readonly IObjectListStudent classesOfTeacher;
        private readonly IGradesExcel grades;
        private IDictionary<int, string> sectionList;
        private IDictionary<int, string> studentList;
        private IDictionary<int[], string> classList;

        public Dashboard(string connectionString, string position, int id)
        {
            this.position = position;
            this.id = id;
            this.sections = new Section(connectionString);
            this.sectionsOfTeacher = new Section(connectionString);
            this.students = new Student(connectionString);
            this.classes = new Class(connectionString);
            this.classesOfTeacher = new Class(connectionString);
            this.grades = new Grades(connectionString);

            InitializeComponent();
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
        }

        private void cbSection_DropDown(object sender, EventArgs e)
        {
            cbSection.Items.Clear();
            tblList.Columns.Clear();

            if (position.Equals("Teacher"))
            {
                sectionList = sectionsOfTeacher.GetTeacherList(id);
            }
            else
            {
                sectionList = sections.GetList();
            }

            foreach (string section in sectionList.Values)
            {
                cbSection.Items.Add(section);
            }
        }

        private void cbSection_SelectedValueChanged(object sender, EventArgs e)
        {
            studentList = students.GetList(sectionList.FirstOrDefault(y => y.Value == cbSection.Text).Key);
            int student = studentList.FirstOrDefault(x => x.Value == studentList.First().Value).Key;
            
            if (position.Equals("Teacher"))
            {
                classList = classesOfTeacher.GetStudentList(student, id);
            }
            else
            {
                classList = classes.GetList(student);
            }

            cbSubject.Items.Clear();

            foreach (string subject in classList.Values)
            {
                cbSubject.Items.Add(subject);
            }
        }

        private void cbSubject_SelectedValueChanged(object sender, EventArgs e)
        {
            tblList.Columns.Clear();
            tblList.DataSource = grades.ExportExcel(sectionList.FirstOrDefault(x => x.Value == cbSection.Text).Key, classList.FirstOrDefault(x => x.Value == cbSubject.Text).Key[1], classList.FirstOrDefault(x => x.Value == cbSubject.Text).Key[0]); ;
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[0].HeaderText = "ID";
        }

        public void RefreshTable()
        {
            cbSection.Items.Clear();
            cbSubject.Items.Clear();
            tblList.Columns.Clear();
        }
    }
}
