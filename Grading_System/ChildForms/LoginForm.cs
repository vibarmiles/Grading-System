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
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vibar\\source\\repos\\Grading_System\\Grading_System\\Grading_System.mdf;User ID=LoginChecker;Password=LoginChecker";
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
            IDictionary<int, string> account = login.GetAccount();

            if (account.Values.First() != "")
            {
                if (account.Values.Last().Equals("Yes"))
                {
                    ChangeProfileForm profile = new ChangeProfileForm(account.Keys.First(), account.Values.First(), connectionString);
                    if (profile.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("The Username and Password needs to be changed on the First Time Login");
                        return;
                    }
                }

                this.OnSubmit?.Invoke(account, EventArgs.Empty);
                this.Close();
            } else
            {
                MessageBox.Show("Incorrect Username/Password");
            }
        }
    }
}
