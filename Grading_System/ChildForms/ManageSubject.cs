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
using Grading_System.Repositories;

namespace Grading_System.ChildForms
{
    public partial class ManageSubject : BaseManageObject
    {
        private readonly AHasObject subject;

        public ManageSubject(string connectionString) : base(new Subject(connectionString), "SubjectID")
        {
            InitializeComponent();
            subject = new Subject(connectionString);
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
            ViewTable();
        }

        protected override void ViewTable()
        {
            base.ViewTable();
            tblList.Columns[1].HeaderText = "Subject ID";
            tblList.Columns[2].HeaderText = "Subject Description";
        }

        protected override void Add(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                subject.Name = sub;
                subject.Add();
                txtSubject.Clear();
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Edit()
        {
            subject.GetValues(Id);
            txtSubject.Text = subject.Name;
        }

        protected override void Update(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                subject.Name = sub;
                subject.Update(Id);
                ViewTable();
                Cancel(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            subject.Delete(Id);
            ViewTable();
        }
    }
}
