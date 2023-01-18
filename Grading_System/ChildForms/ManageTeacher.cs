using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;
using Grading_System.Models;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Grading_System.ChildForms
{
    public partial class ManageTeacher : BaseManageObject
    {
        private readonly ITeacher teacher;

        public ManageTeacher(string connectionString) : base(new Teacher(connectionString))
        {
            InitializeComponent();
            teacher = new Teacher(connectionString);
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
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
        }

        private void Add(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string spec = InputValidator.CheckStringTextBox(txtSpecialization.Text);

            if (fname != null && mname != null && lname != null && gender != "" && spec != "")
            {
                dt.Rows.Add(null, lname, fname, mname, gender, lname + fname, spec);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }

            Cancel(sender, e);
        }
        private void Update(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save Changes?", "Update Database", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            while (ids.Count > 0)
            {
                Console.WriteLine("Deleted!");
                teacher.Delete(ids.Dequeue());
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
                string spec = InputValidator.CheckStringTextBox(row["Specialization"].ToString());

                if (fname == null || mname == null || lname == null || gender == "" || spec == "")
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
                string spec = InputValidator.CheckStringTextBox(row["Specialization"].ToString());

                if (fname != null && mname != null && lname != null && gender != "")
                {
                    teacher.Fname = fname;
                    teacher.Mname = mname;
                    teacher.Lname = lname;
                    teacher.Gender = gender;
                    teacher.Position = "Teacher";
                    teacher.Specialization = spec;
                    string id = row["ID"].ToString();
                    Console.WriteLine(id);

                    if (id == "")
                    {
                        Console.WriteLine("Add");
                        teacher.Add();
                    }
                    else
                    {
                        Console.WriteLine("Update");
                        teacher.Update(id);
                    }
                }
            }

            MessageBox.Show("Successfully Updated!");
            ViewTable();
            Cancel(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewTable();
            ids.Clear();
        }

        private void cbReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to set their password to their username?", "Reset Password", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in tblList.SelectedRows)
                {
                    teacher.ResetPassword(Convert.ToInt32(row.Cells["ID"].Value), row.Cells["Username"].Value.ToString());
                }

                MessageBox.Show("Password/s reset");
            }
        }
    }
}
