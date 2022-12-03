using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System
{
    public partial class Main_Form : Form
    {
        //Wala pang Admin at Registrar Accounts

        string position = String.Empty;
        Add_Registrar add_Registrar = null;
        Add_Student add_Student = null;
        Add_Teacher add_Teacher = null;
        Add_Subject add_Subject = null;
        public Main_Form()
        {
            InitializeComponent();
            this.Load += btnLogout_Click;
            Database.checkConnection();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblPosition.Text = position;

            if (position.Equals("Admin") || position.Equals("Registrar"))
            {
                rbtnAddAsstTeacher.Visible = true;
                rbtnAddStudent.Visible = true;
                rbtnAddSubject.Visible = true;
                rbtnAddTeacher.Visible = true;
                rbtnAddSection.Visible = true;

                add_Student = new Add_Student();
                add_Teacher = new Add_Teacher();
                add_Subject = new Add_Subject();

                add_Student.MdiParent = this;
                add_Teacher.MdiParent = this;
                add_Subject.MdiParent = this;

                add_Student.Show();
                add_Teacher.Show();
                add_Subject.Show();
            }

            if (position.Equals("Admin") || position.Equals("Teacher"))
            {
                rbtnGrades.Visible = true;
            }

            if (position.Equals("Admin"))
            {
                rbtnAddRegistrar.Visible = true;
                add_Registrar = new Add_Registrar();
                add_Registrar.MdiParent = this;
                add_Registrar.Show();
            }

            btnLogout.Visible = true;
        }

        private void btnAddRegistrar_Click(object sender, EventArgs e)
        {
            add_Registrar.BringToFront();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            add_Student.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            add_Registrar = null;
            add_Student = null;
            add_Teacher = null;
            add_Subject = null;

            Login login = new Login();
            login.MdiParent = this;

            rbtnAddAsstTeacher.Visible = false;
            btnLogout.Visible = false;
            rbtnAddStudent.Visible = false;
            rbtnGrades.Visible = false;
            rbtnAddTeacher.Visible = false;
            rbtnAddSubject.Visible = false;
            rbtnAddSection.Visible = false;
            rbtnAddRegistrar.Visible = false;
            lblPosition.Text = "Login";

            login.Show();
            login.BringToFront();
            login.OnSubmit += new EventHandler((s, eArgs) => this.position = (string)s);
            login.FormClosed += Login_FormClosed;
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            add_Teacher.BringToFront();
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            add_Subject.BringToFront();
        }

        private void rbtnAddAsstTeacher_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
