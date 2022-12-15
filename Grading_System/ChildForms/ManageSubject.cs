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

namespace Grading_System.ChildForms
{
    public partial class ManageSubject : ManageObject
    {
        public ManageSubject() : base("Subjects", "SubjectID")
        {
            InitializeComponent();
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
            RefreshTable();
        }

        private void RefreshTable()
        {
            SetTableFormat();
            tblList.Columns[1].HeaderText = "Subject ID";
            tblList.Columns[2].HeaderText = "Subject Description";
        }

        protected override void Add(object sender, EventArgs e)
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

        protected override void Edit()
        {
            string[] row = Database.SelectRow("[Subjects]", "[SubjectName]", "[SubjectID]", Id);
            txtSubject.Text = row[0].ToString();
        }

        protected override void Update(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                Database.Update("Subjects", "[SubjectName]='" + sub + "'", "[SubjectID]", Id);
                RefreshTable();
                Cancel(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            Database.Delete("Subjects", "[SubjectID]", Id);
            RefreshTable();
        }
    }
}
