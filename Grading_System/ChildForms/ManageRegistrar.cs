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
            tblList.Columns[1].HeaderText = "User ID";
            tblList.Columns[1].Name = "ID";
        }

        protected override void Add(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string user = txtUsername.Text;
            string pass = txtPassword.Text;

            if (fname != null && mname != null && lname != null && gender != "" && user != "" && pass != "")
            {
                registrar.Fname = fname;
                registrar.Mname = mname;
                registrar.Lname = lname;
                registrar.Username = user;
                registrar.Password = pass;
                registrar.Gender = gender;
                registrar.Position = "Registrar";
                registrar.Add();
                Cancel(sender, e);
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Edit()
        {
            registrar.GetValues(Id);
            txtFirstName.Text = registrar.Fname;
            txtMiddleName.Text = registrar.Mname;
            txtLastName.Text = registrar.Lname;
            txtUsername.Text = registrar.Username;
            cbGender.Text = registrar.Gender;
        }

        protected override void Update(object sender, EventArgs e)
        {
            string fname = InputValidator.CheckStringTextBox(txtFirstName.Text);
            string mname = InputValidator.CheckStringTextBox(txtMiddleName.Text);
            string lname = InputValidator.CheckStringTextBox(txtLastName.Text);
            string gender = cbGender.Text;
            string user = txtUsername.Text;
            string pass = txtPassword.Text;

            if (fname != null && mname != null && lname != null && gender != "" && user != "" && pass != "")
            {
                registrar.Fname = fname;
                registrar.Mname = mname;
                registrar.Lname = lname;
                registrar.Username = user;
                registrar.Password = pass;
                registrar.Gender = gender;
                registrar.Update(Id);
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
            registrar.Delete(Id);
            ViewTable();
        }
    }
}
