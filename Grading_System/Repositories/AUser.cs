using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Repositories
{
    public abstract class AUser : AObject
    {
        private string connectionString;

        public AUser(string connectionString, string tableOrView) : base(connectionString, tableOrView, "[UserID]")
        {
            this.connectionString = connectionString;
        }

        public abstract string Fname { set; get; }
        public abstract string Mname { set; get; }
        public abstract string Lname { set; get; }
        public abstract string Username { set; get; }
        public abstract string Password { set; }
        public abstract string Gender { set; get; }

        protected void AddUser(string fname, string mname, string lname, string username, string password, string position, string gender)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users ([FirstName],[MiddleName],[LastName],[Username],[Password],[Position],[Gender]) VALUES (@fname, @mname, @lname, @username, @password, @position, @gender)", con))
                    {
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
                        cmd.Parameters.Add("username", SqlDbType.VarChar);
                        cmd.Parameters["username"].Value = username;
                        cmd.Parameters.Add("password", SqlDbType.VarChar);
                        cmd.Parameters["password"].Value = HashPassword(password);
                        cmd.Parameters.Add("position", SqlDbType.VarChar);
                        cmd.Parameters["position"].Value = position;
                        cmd.Parameters.Add("gender", SqlDbType.VarChar);
                        cmd.Parameters["gender"].Value = gender;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void UpdateUser(string fname, string mname, string lname, string username, string password, string gender, string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET [FirstName]=@fname,[MiddleName]=@mname,[LastName]=@lname,[Username]=@username,[Password]=@password,[Gender]=@gender WHERE [UserID]=@id", con))
                    {
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
                        cmd.Parameters.Add("username", SqlDbType.VarChar);
                        cmd.Parameters["username"].Value = username;
                        cmd.Parameters.Add("password", SqlDbType.VarChar);
                        cmd.Parameters["password"].Value = HashPassword(password);
                        cmd.Parameters.Add("gender", SqlDbType.VarChar);
                        cmd.Parameters["gender"].Value = gender;
                        cmd.Parameters.Add("id", SqlDbType.VarChar);
                        cmd.Parameters["id"].Value = id;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
