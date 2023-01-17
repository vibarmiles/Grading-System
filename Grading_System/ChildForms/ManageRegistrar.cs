using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Grading_System.Classes;
using Grading_System.Models;
using System.Xml.Linq;

namespace Grading_System.ChildForms
{
    public partial class ManageRegistrar : BaseManageObject
    {
        private readonly IUser registrar;

        public ManageRegistrar(string connectionString) : base(new Registrar(connectionString))
        {
            InitializeComponent();
            registrar = new Registrar(connectionString);
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

            if (fname != null && mname != null && lname != null && gender != "")
            {
                dt.Rows.Add(null, lname, fname, mname, gender, lname + fname);
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
                registrar.Delete(ids.Dequeue());
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

                if (fname == null || mname == null || lname == null || gender == "")
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

                if (fname != null && mname != null && lname != null && gender != "")
                {
                    registrar.Fname = fname;
                    registrar.Mname = mname;
                    registrar.Lname = lname;
                    registrar.Gender = gender;
                    registrar.Position = "Registrar";
                    string id = row["ID"].ToString();
                    Console.WriteLine(id);

                    if (id == "")
                    {
                        Console.WriteLine("Add");
                        registrar.Add();
                    }
                    else
                    {
                        Console.WriteLine("Update");
                        registrar.Update(id);
                    }
                }
            }

            MessageBox.Show("Successfully Updated!");
            ViewTable();
            Cancel(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ids.Clear();
            ViewTable();
        }
    }
}
