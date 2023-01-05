using Grading_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Grading_System.ChildForms
{
    public partial class ManageClassForm : Form
    {
        private readonly IClass teacher;
        private readonly IObjectList teachers;
        private readonly IClass student;
        private IDictionary<int, string> teacherList;

        public ManageClassForm(string connectionString)
        {
            teacher = new ManageClass(connectionString);
            teachers = new Teacher(connectionString);
            InitializeComponent();
            this.Load += new EventHandler((sender, e) => 
            {
                this.Dock = DockStyle.Fill;
                ViewTable(0);
            });
        }

        private void ViewTable(int id)
        {
            tblSubjectList.Columns.Clear();
            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
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

        public void RefreshTable()
        {
            ViewTable(0);
            cbTeacher.Text = String.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string input = cbTeacher.Text;
            teacher.TeacherID = teacherList.FirstOrDefault(x => x.Value == input).Key;
            
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
            ViewTable(0);
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
            ViewTable(teacherList.FirstOrDefault(x => x.Value == input).Key);
        }
    }
}
