using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Repositories
{
    internal class Teacher : ATeacher
    {
        private string connectionString;
        private string fname, mname, lname, username, password, gender, specialization;

        public Teacher(string connectionString) : base(connectionString) 
        {
            this.connectionString = connectionString;
        }

        public override string Fname { get => fname; set => fname = value; }
        public override string Mname { get => mname; set => mname = value; }
        public override string Lname { get => lname; set => lname = value; }
        public override string Username { get => username; set => username = value; }
        public override string Password { set => password = value; }
        public override string Gender { get => gender; set => gender = value; }
        public override string Specialization { get => specialization; set => specialization = value; }

        public override void Add()
        {
            try
            {
                base.AddUser(fname, mname, lname, username, password, "Teacher", gender);

                int select = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [UserID] FROM Users WHERE [Username] = @username", con))
                    {
                        cmd.Parameters.Add("username", SqlDbType.VarChar);
                        cmd.Parameters["username"].Value = username;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                select = Int32.Parse(dr.GetSqlValue(0).ToString());
                            }
                        }
                    }

                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Teachers ([UserID], [Specialization]) VALUES (@id, @specialization)", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = select;
                        cmd.Parameters.Add("specialization", SqlDbType.VarChar);
                        cmd.Parameters["specialization"].Value = specialization;
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

        public override void Delete(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Teachers WHERE [UserID] = @id", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = Int32.Parse(id);
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

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

        public override DataRow GetValues(string id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT U.[FirstName],U.[MiddleName],U.[LastName],U.[Username],U.[Gender],T.[Specialization] FROM Users U INNER JOIN Teachers T ON U.[UserID] = T.[UserID] WHERE U.[UserID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            return dt.Rows[0];
        }

        public override void Update(string id)
        {
            base.UpdateUser(fname, mname, lname, username, password, gender, id);

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Teachers SET [Specialization]=@specialization WHERE [UserID]=@id", con))
                    {
                        cmd.Parameters.Add("specialization", SqlDbType.VarChar);
                        cmd.Parameters["specialization"].Value = specialization;
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
