using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;

namespace Grading_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        public event EventHandler OnSubmit;

        private void button1_Click(object sender, EventArgs e)
        {
            string username = "'" + txtUsername.Text + "'";
            string password = txtPassword.Text;
            string position = String.Empty;
            string[] result = Database.SelectRow("[Users]", "[Position],[Password]", "[Username]", username);

            if (result[0] is null || result[1] is null)
            {
                MessageBox.Show("Incorrect Username/Password");
                return;
            }

            if (Database.HashPassword(password).Equals(result[1].ToString()))
            {
                //position = result[0].ToString();
                position = "Admin";
                this.OnSubmit?.Invoke(position, EventArgs.Empty);
                this.Close();
            } else
            {
                MessageBox.Show("Incorrect Username/Password");
            }
        }
    }
}
