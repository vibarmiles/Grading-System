using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Models
{
    public class BaseUserModel : BaseModel, IUser
    {
        private string connectionString;
        private string fname, mname, lname, username, gender, position;

        public BaseUserModel(string connectionString, string tableOrView, string key) : base(connectionString, tableOrView, key)
        {
            this.connectionString = connectionString;
        }

        public string Fname { get => fname; set => fname = value; }
        public string Mname { get => mname; set => mname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Username => username;
        public string Gender { get => gender; set => gender = value; }
        public string Position { get => position; set => position = value; }

        protected string HashPassword(string password)
        {
            string hash = String.Empty;

            using (HashAlgorithm sha256 = SHA256.Create())
            {
                foreach (byte b in sha256.ComputeHash(Encoding.UTF8.GetBytes(password)))
                {
                    hash += b.ToString("x2");
                }
            }

            return hash;
        }

        public void Add()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users ([FirstName],[MiddleName],[LastName],[Username],[Password],[Position],[Gender]) VALUES (@fname, @mname, @lname, @username, @password, @position, @gender)", con))
                    {
                        username = lname + fname;
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
                        cmd.Parameters.Add("username", SqlDbType.VarChar);
                        cmd.Parameters["username"].Value = username;
                        cmd.Parameters.Add("password", SqlDbType.VarChar);
                        cmd.Parameters["password"].Value = HashPassword(username);
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

        public void ResetPassword(int id, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Password=@password WHERE UserID=@id)", con))
                    {
                        Console.WriteLine(id + " " + password);
                        cmd.Parameters.Add("password", SqlDbType.VarChar).Value = HashPassword(username);
                        cmd.Parameters.Add("id", SqlDbType.BigInt).Value = id;
                        cmd.ExecuteNonQuery();
                        //Nag sasaing lang ako...
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetValues(string id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [FirstName],[MiddleName],[LastName],[Username],[Gender] FROM Users WHERE [UserID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            DataRow row = dt.Rows[0];
            fname = row["FirstName"].ToString();
            mname = row["MiddleName"].ToString();
            lname = row["LastName"].ToString();
            gender = row["Gender"].ToString();
            username = row["Username"].ToString();
        }

        public void Update(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET [FirstName]=@fname,[MiddleName]=@mname,[LastName]=@lname,[Gender]=@gender WHERE [UserID]=@id", con))
                    {
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
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

        public void Delete(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE [UserID] = @id", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = Int32.Parse(id);
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
