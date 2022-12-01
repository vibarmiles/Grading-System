using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System
{
    public partial class Add_Subject : Form
    {
        string id;
        public Add_Subject()
        {
            InitializeComponent();
        }

        private void RefreshTable()
        {
            btnUpdate.Hide();
            tblList.Columns.Clear();
            tblList.CellClick -= new DataGridViewCellEventHandler(edit_Click);
            tblList.CellClick += new DataGridViewCellEventHandler(edit_Click);
            tblList.Columns.Insert(0, ForDataGridView.AddButton("Edit"));
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.DataSource = Database.ViewTable("Subjects");
            tblList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns.Insert(tblList.Columns.Count, ForDataGridView.AddButton("Delete"));
            tblList.Columns[tblList.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void edit_Click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataTable dt = Database.ViewTable("Subjects");

            if (ForDataGridView.ButtonColumn_Clicked(dgv, e, "Edit"))
            {
                DataRow index = dt.Rows[e.RowIndex];
                this.id = index[0].ToString();
                btnUpdate.Show();
                btnAdd.Text = "Cancel";
                btnAdd.Click -= btnAdd_Click;
                btnAdd.Click += btnCancel_Click;
                string[] row = Database.SelectRow("[Subjects]", "[SubjectName]", "[SubjectID]", this.id);
                txtSubject.Text = row[0].ToString();
            }

            if (ForDataGridView.ButtonColumn_Clicked(dgv, e, "Delete"))
            {
                DataRow index = dt.Rows[e.RowIndex];
                this.id = index[0].ToString();
                string message = "Are you sure?";
                string caption = "Delete Row";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
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
