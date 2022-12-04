using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;

namespace Grading_System
{
    public partial class ManageSubject : Form
    {
        string id;
        public ManageSubject()
        {
            InitializeComponent();
        }

        private void RefreshTable()
        {
            btnUpdate.Hide();
            tblList.CellClick -= new DataGridViewCellEventHandler(Edit_Click);
            tblList = ForDataGridView.SetDataGridViewFormat(tblList, "Subjects", "[SubjectID]");
            tblList.Columns[1].HeaderText = "Subject ID";
            tblList.Columns[2].HeaderText = "Subject Description";
            tblList.CellClick += new DataGridViewCellEventHandler(Edit_Click);
        }

        private void Edit_Click(object sender, DataGridViewCellEventArgs e)
        {
            this.id = tblList.Rows[e.RowIndex].Cells["SubjectID"].Value.ToString();

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Edit"))
            {
                btnUpdate.Show();
                btnAdd.Text = "Cancel";
                btnAdd.Click -= btnAdd_Click;
                btnAdd.Click += btnCancel_Click;
                string[] row = Database.SelectRow("[Subjects]", "[SubjectName]", "[SubjectID]", this.id);
                txtSubject.Text = row[0].ToString();
            }

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Delete"))
            {
                DialogResult result = InputValidator.ContinueDelete();

                if (result is DialogResult.Yes)
                {
                    Database.Delete("Subjects", "[SubjectID]", this.id);
                    RefreshTable();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                Database.Insert("Subjects", "[SubjectName]", "'" + sub + "'");
                txtSubject.Clear();
                RefreshTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                Database.Update("Subjects", "[SubjectName]='" + sub + "'", "[SubjectID]", this.id);
                txtSubject.Clear();
                RefreshTable();
                btnCancel_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnUpdate.Hide();
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            btnAdd.Click -= btnCancel_Click;
            txtSubject.Clear();
        }

        private void Add_Subject_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            RefreshTable();
        }
    }
}
