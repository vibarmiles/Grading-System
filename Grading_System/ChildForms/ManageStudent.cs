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

            tblList.KeyDown += new KeyEventHandler((sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewRow row in tblList.SelectedRows)
                    {
                        ids.Enqueue(row.Cells["ID"].Value.ToString());
                        Console.WriteLine(row.Cells["ID"].Value.ToString());
                    }
                }
            });
        }

        protected override void ViewTable()
        {
            base.ViewTable();
            tblList.Columns[1].HeaderText = "Last Name";
            tblList.Columns[2].HeaderText = "First Name";
            tblList.Columns[3].HeaderText = "Middle Name";
            tblList.Columns[6].HeaderText = "Section";
            tblList.Columns[7].HeaderText = "Enrollment Date";
            tblList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void Add(object sender, EventArgs e)
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
                dt.Rows.Add(null, lname, fname, mname, lrn, gender, section, date);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }

            Cancel(sender, e);
        }

        private void Update(object sender, EventArgs e)
        {
            while (ids.Count > 0)
            {
                Console.WriteLine("Deleted!");
                student.Delete(ids.Dequeue());
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string fname = InputValidator.CheckStringTextBox(row["FirstName"].ToString());
                string mname = InputValidator.CheckStringTextBox(row["MiddleName"].ToString());
                string lname = InputValidator.CheckStringTextBox(row["LastName"].ToString());
                string gender = row["Gender"].ToString();
                string section = row["SectionName"].ToString();
                string lrn = InputValidator.CheckIntTextBox(row["LRN"].ToString()).ToString();
                DateTime date;
                try
                {
                    date = Convert.ToDateTime(row["EnrollmentDate"]);
                } catch (Exception)
                {
                    MessageBox.Show("Invalid Table Input!");
                    return;
                }

                if (fname == null || mname == null || lname == null || section == "" || gender == "" || lrn == "0" && date == null)
                {
                    MessageBox.Show("Invalid Table Input!");
                    return;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string fname = InputValidator.CheckStringTextBox(row["FirstName"].ToString());
                string mname = InputValidator.CheckStringTextBox(row["MiddleName"].ToString());
                string lname = InputValidator.CheckStringTextBox(row["LastName"].ToString());
                string gender = row["Gender"].ToString();
                string section = row["SectionName"].ToString();
                string lrn = InputValidator.CheckIntTextBox(row["LRN"].ToString()).ToString();
                DateTime date = Convert.ToDateTime(row["EnrollmentDate"]);

                if (fname != null && mname != null && lname != null && section != "" && gender != "" && lrn != "0" && date != null)
                {
                    student.FName = fname;
                    student.MName = mname;
                    student.LName = lname;
                    student.Gender = gender;
                    student.SectionID = sections.FirstOrDefault(x => x.Value == section).Key;
                    student.LRN = lrn;
                    student.EnrollmentDate = date;

                    string id = row["ID"].ToString();
                    Console.WriteLine(id);

                    if (id == "")
                    {
                        Console.WriteLine("Add");
                        student.Add();
                    }
                    else
                    {
                        Console.WriteLine("Update");
                        student.Update(id);
                    }
                }
            }

            ViewTable();
            Cancel(sender, e);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewTable();
            ids.Clear();
        }
    }
}
