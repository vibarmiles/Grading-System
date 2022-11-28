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
        Add_Registrar add_Registrar = new Add_Registrar();
        Add_Student add_Student = new Add_Student();
        Add_Teacher add_Teacher = new Add_Teacher();
        Add_Subject add_Subject = new Add_Subject();
        public Main_Form()
        {
            InitializeComponent();
            add_Registrar.MdiParent = this;
            add_Student.MdiParent = this;
            add_Teacher.MdiParent = this;
            add_Subject.MdiParent = this;
            this.Load += btnLogout_Click;
            add_Registrar.Show();
            add_Student.Show();
            add_Teacher.Show();
            add_Subject.Show();
            Database.checkConnection();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            rbtnAddAsstTeacher.Visible = true;
            btnLogout.Visible = true;
            rbtnGrades.Visible = true;
            rbtnAddStudent.Visible = true;
            rbtnAddTeacher.Visible = true;
            rbtnAddSubject.Visible = true;
            rbtnAddSection.Visible = true;
            rbtnAddRegistrar.Visible = true;
            lblPosition.Text = "Admin";
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
