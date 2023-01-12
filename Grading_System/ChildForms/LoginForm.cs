using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grading_System.Models;
using Grading_System.Classes;

namespace Grading_System.ChildForms
{
    public partial class LoginForm : Form
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vibar\\source\\repos\\Grading_System\\Grading_System\\Grading_System.mdf;Integrated Security=True";
        
        public LoginForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler((sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button1_Click(sender, e);
                }
            });
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        public event EventHandler OnSubmit;

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Login login = new Login(new Account(connectionString));
            login.Username = username;
            login.Password = password;
            IDictionary<int, string> position = login.GetAccount();

            if (position.Count > 0)
            {
                this.OnSubmit?.Invoke(position, EventArgs.Empty);
                this.Close();
            } else
            {
                MessageBox.Show("Incorrect Username/Password");
            }
        }
    }
}
