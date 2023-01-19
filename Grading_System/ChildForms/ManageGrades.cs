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
using System.Security.Cryptography;

namespace Grading_System.ChildForms
{
    public partial class ManageGrades : Form
    {
        private readonly IGradesExcel gradeExcel;
        private readonly IObjectList sections;
        private readonly ISection section;
        private readonly IObjectListTeacher sectionsOfTeacher;
        private readonly ISectionStudent students;
        private readonly IStudentClass classes;
        private readonly IObjectListStudent classesOfTeacher;
        private readonly IGrades grades;
        private IDictionary<int, string> sectionList;
        private IDictionary<int, string> studentList;
        private IDictionary<int[], string> classList;
        private string position;
        private string connectionString;
        private int id;

        public ManageGrades(string connectionString, string position, int id)
        {
            this.connectionString = connectionString;
            gradeExcel = new Grades(connectionString);
            sections = new Section(connectionString);
            section = new Section(connectionString);
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
            cbSection.Items.Clear();

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
            btnExportCard.Enabled = false;
            btnUpdate.Enabled = false;
            btnExportBook.Enabled = false;
            string input = cbSection.Text;
            studentList = students.GetList(sectionList.FirstOrDefault(x => x.Value == input).Key);
            int studentId = studentList.FirstOrDefault(x => x.Value == studentList.First().Value).Key;

            if (position.Equals("Admin"))
            {
                classList = classes.GetList(studentId);
            }
            else
            {
                classList = classesOfTeacher.GetStudentList(studentId, this.id);
            }

            cbStudent.Items.Clear();
            cbSubjects.Items.Clear();

            foreach (string subject in classList.Values)
            {
                cbSubjects.Items.Add(subject);
            }

            tblList.Columns.Clear();
            DataTable dt = new DataTable();
            DataColumn id = new DataColumn("ID");
            id.DataType = typeof(int);
            DataColumn name = new DataColumn("Name");
            dt.Columns.Add(id);
            dt.Columns.Add(name);
            
            foreach (KeyValuePair<int, string> student in studentList)
            {
                dt.Rows.Add(student.Key, student.Value);
            }

            dt.DefaultView.Sort = "ID";
            tblList.DataSource = dt;
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void cbStudent_SelectedValueChanged(object sender, EventArgs e)
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
            DataTable col = grades.GetGrades(student, subject, teacher);

            if (sectionList.FirstOrDefault(x => x.Value == cbSection.Text).Key == section.GetAdvisory(id) || position.Equals("Admin"))
            {
                btnExportCard.Enabled = true;
            }
            else
            {
                btnExportCard.Enabled = false;
            }

            btnExportBook.Enabled = true;
            btnImport.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void cbSubjects_SelectedValueChanged(object sender, EventArgs e)
        {
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
            int section = sectionList.FirstOrDefault(x => x.Value == cbSection.Text).Key;

            cbStudent.Items.Clear();
            tblList.Columns.Clear();
            DataTable dt = grades.GetSectionGrades(section, teacher, subject);

            tblList.DataSource = dt;
            tblList.Columns[0].HeaderText = "ID";
            tblList.Columns[0].Name = "ID";
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[2].HeaderText = "1st Quarter";
            tblList.Columns[3].HeaderText = "2nd Quarter";
            tblList.Columns[4].HeaderText = "3rd Quarter";
            tblList.Columns[5].HeaderText = "4th Quarter";

            foreach (string student in studentList.Values)
            {
                cbStudent.Items.Add(student);
            }

            btnUpdate.Enabled = true;
            btnExportCard.Enabled = false;
            btnExportBook.Enabled = true;
            btnImport.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tblList.Columns.Clear();
            cbSection.Text = String.Empty;
            cbStudent.Text = String.Empty;
            cbSubjects.Text = String.Empty;
            cbSection_DropDown(sender, e);
            cbStudent.Items.Clear();
            cbSubjects.Items.Clear();
            btnExportBook.Enabled = false;
            btnExportCard.Enabled = false;
            btnImport.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save Changes?", "Update Database", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

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

            foreach (DataGridViewRow row in tblList.Rows)
            {
                if (InputValidator.CheckDoubleTextBox(row.Cells["1"].Value.ToString()) > 100 || InputValidator.CheckDoubleTextBox(row.Cells["1"].Value.ToString()) < 0 || InputValidator.CheckDoubleTextBox(row.Cells["2"].Value.ToString()) > 100 || InputValidator.CheckDoubleTextBox(row.Cells["2"].Value.ToString()) < 0 || InputValidator.CheckDoubleTextBox(row.Cells["3"].Value.ToString()) > 100 || InputValidator.CheckDoubleTextBox(row.Cells["3"].Value.ToString()) < 0 || InputValidator.CheckDoubleTextBox(row.Cells["4"].Value.ToString()) > 100 || InputValidator.CheckDoubleTextBox(row.Cells["4"].Value.ToString()) < 0)
                {
                    MessageBox.Show("One of the inputs is invalid!");
                    return;
                }
            }

            foreach (DataGridViewRow row in tblList.Rows)
            {
                grades.Update(Convert.ToInt32(row.Cells["ID"].Value), subject, teacher, 1, InputValidator.CheckDoubleTextBox(row.Cells["1"].Value.ToString()));
                grades.Update(Convert.ToInt32(row.Cells["ID"].Value), subject, teacher, 2, InputValidator.CheckDoubleTextBox(row.Cells["2"].Value.ToString()));
                grades.Update(Convert.ToInt32(row.Cells["ID"].Value), subject, teacher, 3, InputValidator.CheckDoubleTextBox(row.Cells["3"].Value.ToString()));
                grades.Update(Convert.ToInt32(row.Cells["ID"].Value), subject, teacher, 4, InputValidator.CheckDoubleTextBox(row.Cells["4"].Value.ToString()));
            }

            cbSubjects_SelectedValueChanged(sender, e);
            MessageBox.Show("Successfully Updated!");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            IFileImport file;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select File: ";
                dlg.InitialDirectory = @"c:\\";
                dlg.Filter = "Excel Sheet|*.xlsx";
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dlg.FileName + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    System.Windows.Forms.Application.DoEvents();
                    file = new ExcelFile(connectionString, sectionList.FirstOrDefault(x => x.Value == cbSection.Text).Key, classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[1], classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[0]);
                    DataTable dt = file.Import(fileName);
                    if (dt is null)
                    {
                        return;
                    }

                    tblList.DataSource = file.Import(fileName);
                    tblList.Columns["1"].DefaultCellStyle.Format = "N2";
                    tblList.Columns["2"].DefaultCellStyle.Format = "N2";
                    tblList.Columns["3"].DefaultCellStyle.Format = "N2";
                    tblList.Columns["4"].DefaultCellStyle.Format = "N2";
                    tblList.Columns["Average"].DefaultCellStyle.Format = "N2";
                    MessageBox.Show("Data Import Finished!");
                }
            }
        }

        private void btnExportBook_Click(object sender, EventArgs e)
        {
            IFileExport file = null;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Select File: ";
                saveFileDialog.InitialDirectory = @"c:\\";
                saveFileDialog.Filter = "Excel Sheet|*.xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = cbSection.Text + " - " + cbSubjects.Text;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Windows.Forms.Application.DoEvents();
                    file = new ExcelFile(connectionString, sectionList.FirstOrDefault(x => x.Value == cbSection.Text).Key, classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[1], classList.FirstOrDefault(x => x.Value == cbSubjects.Text).Key[0]);
                    file.Name = cbSubjects.Text;
                    file.Export(saveFileDialog.FileName);
                }
            }
        }

        private void btnExportCard_Click(object sender, EventArgs e)
        {
            IFileExport file = null;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Select File: ";
                saveFileDialog.InitialDirectory = @"c:\\";
                saveFileDialog.Filter = "Document|*.docx|PDF|*.pdf";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = cbStudent.Text + " - " + cbSection.Text;
                saveFileDialog.RestoreDirectory = true;

                int studentId = studentList.FirstOrDefault(x => x.Value == cbStudent.Text).Key;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    switch (saveFileDialog.FilterIndex) 
                    {
                        case 1:
                            file = new WordFile(connectionString, studentId, 1);
                            break;
                        case 2:
                            file = new WordFile(connectionString, studentId, 2);
                            break;
                    }

                    file.Export(saveFileDialog.FileName);
                }
            }
        }

        public void RefreshTable()
        {
            cbSection.Items.Clear();
            cbSubjects.Items.Clear();
            cbStudent.Items.Clear();
            tblList.Columns.Clear();
        }
    }
}
