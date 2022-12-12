using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.ChildForms;

namespace Grading_System
{
    public partial class ParentForm : Form
    {
        //Wala pang Admin at Registrar Accounts

        string position;
        ManageRegistrar registrar;
        ManageStudent student;
        ManageTeacher teacher;
        ManageSubject subject;
        public ParentForm()
        {
            InitializeComponent();
            this.Load += btnLogout_Click;
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

                student = new ManageStudent();
                teacher = new ManageTeacher();
                subject = new ManageSubject();

                student.MdiParent = this;
                teacher.MdiParent = this;
                subject.MdiParent = this;

                student.Show();
                teacher.Show();
                subject.Show();
            }

            if (position.Equals("Admin") || position.Equals("Teacher"))
            {
                rbtnGrades.Visible = true;
            }

            if (position.Equals("Admin"))
            {
                rbtnAddRegistrar.Visible = true;
                registrar = new ManageRegistrar();
                registrar.MdiParent = this;
                registrar.Show();
            }

            btnLogout.Visible = true;
        }

        private void btnAddRegistrar_Click(object sender, EventArgs e)
        {
            if (registrar is null)
            {
                return;
            }

            registrar.BringToFront();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (student is null)
            {
                return;
            }

            student.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            registrar = null;
            student = null;
            teacher = null;
            subject = null;
            position = String.Empty;

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
            if (teacher is null)
            {
                return;
            }

            teacher.BringToFront();
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            if (subject is null)
            {
                return;
            }

            subject.BringToFront();
        }

        private void rbtnAddAsstTeacher_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
