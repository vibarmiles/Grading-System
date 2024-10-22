﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.ChildForms;
using Grading_System.Classes;
using Grading_System.Models;

namespace Grading_System
{
    public partial class ParentForm : Form
    {
        private string userId = String.Empty;
        string connectionString;
        private string position;
        private int id;
        private Dictionary<int, string> account;
        private ManageRegistrar registrar;
        private ManageStudent student;
        private ManageTeacher teacher;
        private ManageSubject subject;
        private ManageAsstTeacher asstTeacher;
        private ManageSection section;
        private ManageClassForm classForm;
        private ManageGrades grades;
        private Dashboard dashboard;

        public ParentForm()
        {
            InitializeComponent();
            this.Load += btnLogout_Click;
        }

        private void Login_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (account is null)
            {
                return;
            }

            position = account.Values.First();
            id = account.Keys.First();

            switch (position)
            {
                case "Admin":
                    userId = "SuperAdmin";
                    break;
                case "Assistant Teacher":
                    userId = "AssistantTeacher";
                    break;
                case "Teacher":
                    userId = "Teacher";
                    break;
                case "Registrar":
                    userId = "Registrar";
                    break;
            }

            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vibar\\source\\repos\\Grading_System\\Grading_System\\Grading_System.mdf;User ID=" + userId + ";Password=" + userId;
            Console.WriteLine(connectionString);

            lblPosition.Text = position;

            if (position.Equals("Admin") || position.Equals("Registrar"))
            {
                rbtnAddAsstTeacher.Visible = true;
                rbtnAddStudent.Visible = true;
                rbtnAddSubject.Visible = true;
                rbtnAddTeacher.Visible = true;
                rbtnAddSection.Visible = true;
                rbtnClass.Visible = true;

                student = new ManageStudent(connectionString);
                teacher = new ManageTeacher(connectionString, position);
                subject = new ManageSubject(connectionString);
                asstTeacher = new ManageAsstTeacher(connectionString, position);
                section = new ManageSection(connectionString);
                classForm = new ManageClassForm(connectionString);

                student.MdiParent = this;
                teacher.MdiParent = this;
                subject.MdiParent = this;
                asstTeacher.MdiParent = this;
                section.MdiParent = this;
                classForm.MdiParent = this;

                student.Show();
                teacher.Show();
                subject.Show();
                asstTeacher.Show();
                section.Show();
                classForm.Show();
            }

            if (position.Equals("Admin") || position.Equals("Teacher"))
            {
                rbtnGrades.Visible = true;
                grades = new ManageGrades(connectionString, position, id);
                grades.MdiParent = this;
                grades.Show();
            }

            if (position.Equals("Admin"))
            {
                rbtnAddRegistrar.Visible = true;
                registrar = new ManageRegistrar(connectionString);
                registrar.MdiParent = this;
                registrar.Show();
                rbtnGrades.Checked = true;
            }

            rbtnDashboard.Visible = true;
            btnLogout.Visible = true;
            rbtnChangeProfile.Visible = true;

            dashboard = new Dashboard(connectionString, position, id);
            dashboard.MdiParent = this;
            dashboard.Show();
            rbtnDashboard.Checked = true;
            dashboard.BringToFront();
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

            student.RefreshTable();
            student.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
            }

            registrar = null;
            student = null;
            teacher = null;
            subject = null;
            asstTeacher = null;
            section = null;
            classForm = null;
            grades = null;
            dashboard = null;
            position = String.Empty;
            userId = String.Empty;
            id = 0;

            LoginForm login = new LoginForm();
            login.MdiParent = this;

            rbtnAddAsstTeacher.Visible = false;
            btnLogout.Visible = false;
            rbtnAddStudent.Visible = false;
            rbtnGrades.Visible = false;
            rbtnAddTeacher.Visible = false;
            rbtnAddSubject.Visible = false;
            rbtnAddSection.Visible = false;
            rbtnAddRegistrar.Visible = false;
            rbtnClass.Visible = false;
            rbtnDashboard.Visible = false;
            rbtnChangeProfile.Visible = false;
            lblPosition.Text = "Login";

            login.Show();
            login.BringToFront();
            login.OnSubmit += new EventHandler((s, eArgs) => this.account = (Dictionary<int, string>)s);
            login.FormClosing += Login_FormClosed;
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            if (grades is null)
            {
                return;
            }

            grades.RefreshTable();
            grades.BringToFront();
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

        private void rbtnAddAsstTeacher_Click(object sender, EventArgs e)
        {
            if (asstTeacher is null)
            {
                return;
            }

            asstTeacher.BringToFront();
        }

        private void rbtnAddSection_Click(object sender, EventArgs e)
        {
            if (section is null)
            {
                return;
            }

            section.RefreshTable();
            section.BringToFront();
        }

        private void rbtnClass_Click(object sender, EventArgs e)
        {
            if (classForm is null)
            {
                return;
            }

            classForm.RefreshTable();
            classForm.BringToFront();
        }

        private void rbtnDashboard_Click(object sender, EventArgs e)
        {
            if (dashboard is null)
            {
                return;
            }

            dashboard.RefreshTable();
            dashboard.BringToFront();
        }

        private void rbtnChangeProfile_Click(object sender, EventArgs e)
        {
            ChangeProfileForm profile = new ChangeProfileForm(account.Keys.First(), account.Values.First(), connectionString);
            if (profile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }
    }
}
