using Grading_System.Classes;
using Grading_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.ChildForms
{
    public partial class ChangeProfileForm : Form
    {
        private string position;
        private string connectionString;
        int id;

        public ChangeProfileForm(int id, string position, string connectionString)
        {
            InitializeComponent();
            this.id = id;
            this.position = position;
            this.connectionString = connectionString;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler((sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnConfirm_Click(sender, e);
                }
            });
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Login login = new Login(new Account(connectionString));
            login.Position = position;
            login.Id = id;
            login.Username = txtUsername.Text;

            if (txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                login.Password = txtConfirmPassword.Text;
                login.ChangeProfile();
                this.DialogResult = DialogResult.OK;
                this.Close();
            } else
            {
                MessageBox.Show("Invalid Input!");
            }
        }
    }
}
