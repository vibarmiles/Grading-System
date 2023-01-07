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
    public partial class ManageClassForm : Form
    {
        private readonly IClass teacher;
        private readonly IAssignClass student;
        private readonly IObjectList teachers;
        private readonly IObjectList sections;
        private readonly ISectionStudentList students;
        private IDictionary<int, string> teacherList;
        private IDictionary<int, string> sectionList;
        private IDictionary<int, string> studentList;

        public ManageClassForm(string connectionString)
        {
            teacher = new ManageClass(connectionString);
            teachers = new Teacher(connectionString);
            sections = new Section(connectionString);
            student = new AssignClass(connectionString);
            students = new Student(connectionString);
            InitializeComponent();
            this.Load += new EventHandler((sender, e) => 
            {
                this.Dock = DockStyle.Fill;
                ManageViewTable(0);
                AssignViewTable(0);
            });
        }

        private void ManageViewTable(int id)
        {
            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
            tblSubjectList.Columns.Clear();
            tblSubjectList.Columns.Add(checkbox); 
            DataTable dataTable = teacher.GetList(id);
            tblSubjectList.Columns.Add(dataTable.Columns[0].ColumnName, "ID");
            tblSubjectList.Columns.Add(dataTable.Columns[1].ColumnName, "Description");

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[2].ToString() == "")
                {
                    tblSubjectList.Rows.Add(0, row[0], row[1]);
                }
                else
                {
                    tblSubjectList.Rows.Add(1, row[0], row[1]);
                }
            }

            tblSubjectList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblSubjectList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblSubjectList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void AssignViewTable(int id)
        {
            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
            tblClass.Columns.Clear();
            tblClass.Columns.Add(checkbox);
            DataTable dataTable = student.GetList(id);
            tblClass.Columns.Add(dataTable.Columns[0].ColumnName, dataTable.Columns[0].ColumnName);
            tblClass.Columns.Add(dataTable.Columns[1].ColumnName, "Description");
            tblClass.Columns.Add(dataTable.Columns[2].ColumnName, dataTable.Columns[2].ColumnName);
            tblClass.Columns.Add(dataTable.Columns[3].ColumnName, "Teacher");

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[4].ToString() == "")
                {
                    tblClass.Rows.Add(0, row[0], row[1], row[2], row[3]);
                }
                else
                {
                    tblClass.Rows.Add(1, row[0], row[1], row[2], row[3]);
                }
            }

            tblClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblClass.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblClass.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblClass.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public void RefreshTable()
        {
            ManageViewTable(0);
            AssignViewTable(0);
            cbTeacher.Text = String.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string input = cbTeacher.Text;
            
            if (input != "")
            {
                teacher.TeacherID = teacherList.FirstOrDefault(x => x.Value == input).Key;
            }

            foreach (DataGridViewRow row in tblSubjectList.Rows)
            {
                DataGridViewCheckBoxCell checkbox = row.Cells[0] as DataGridViewCheckBoxCell;
                teacher.SubjectID = Int32.Parse(row.Cells[1].Value.ToString());

                if (Convert.ToBoolean(checkbox.Value) == true)
                {
                    teacher.Add();
                } else if (Convert.ToBoolean(checkbox.Value) == false)
                {
                    teacher.Remove();
                } else
                {
                    MessageBox.Show("An Error has Occured!");
                    return;
                }
            }

            MessageBox.Show("Successfully Updated!");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbTeacher.Text = String.Empty;
            ManageViewTable(0);
            AssignViewTable(0);
        }

        private void cbTeacher_DropDown(object sender, EventArgs e)
        {
            teacherList = teachers.GetList();
            cbTeacher.Items.Clear();

            foreach (string teacher in teacherList.Values)
            {
                cbTeacher.Items.Add(teacher);
            }
        }

        private void cbTeacher_SelectedValueChanged(object sender, EventArgs e)
        {
            string input = cbTeacher.Text;
            ManageViewTable(teacherList.FirstOrDefault(x => x.Value == input).Key);
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

        private void btnClassUpdate_Click(object sender, EventArgs e)
        {
            string input = cbSection.Text;

            if (input != "") 
            {
                studentList = students.GetList(sectionList.FirstOrDefault(x => x.Value == input).Key);
            }

            foreach (int id in studentList.Keys)
            {
                student.StudentID = id;

                foreach (DataGridViewRow row in tblClass.Rows)
                {
                    DataGridViewCheckBoxCell checkbox = row.Cells[0] as DataGridViewCheckBoxCell;
                    student.SubjectID = Int32.Parse(row.Cells[1].Value.ToString());
                    student.TeacherID = Int32.Parse(row.Cells[3].Value.ToString());

                    if (Convert.ToBoolean(checkbox.Value) == true)
                    {
                        Console.WriteLine(student.StudentID);
                        student.Add();
                    }
                    else if (Convert.ToBoolean(checkbox.Value) == false)
                    {
                        student.Remove();
                    }
                    else
                    {
                        MessageBox.Show("An Error has Occured!");
                        return;
                    }
                }

                MessageBox.Show("Successfully Updated!");
            }
        }

        private void cbSection_SelectedValueChanged(object sender, EventArgs e)
        {
            string input = cbSection.Text;
            int section = sectionList.FirstOrDefault(x => x.Value == input).Key;
            studentList = students.GetList(sectionList.FirstOrDefault(x => x.Value == input).Key);
            
            try
            {
                AssignViewTable(studentList.Keys.First());
            } catch (InvalidOperationException)
            {
                AssignViewTable(0);
            }
        }
    }
}
