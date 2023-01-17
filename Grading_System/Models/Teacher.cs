using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Models
{
    internal class Teacher : BaseUserModel, ITeacher, IObjectList
    {
        private string connectionString;
        private string specialization;

        public Teacher(string connectionString) : base(connectionString, "TeachersView", "[TeacherID]") 
        {
            this.connectionString = connectionString;
        }
        
        public string Specialization { get => specialization; set => specialization = value; }

        public new void Add()
        {
            try
            {
                base.Add();

                int select = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [UserID] FROM Users WHERE [Username] = @username", con))
                    {
                        cmd.Parameters.Add("username", SqlDbType.VarChar);
                        cmd.Parameters["username"].Value = base.Username;

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

        public new void Delete(string id)
        {
            try
            {
                string newId = String.Empty;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Teachers WHERE [TeacherID] = @id", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = Int32.Parse(id);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newId = reader["UserID"].ToString();
                            }
                        }
                    }

                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Sections SET TeacherID=NULL WHERE [TeacherID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Grades WHERE [TeacherID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Students_Teachers_Subjects WHERE [TeacherID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Teachers_Subjects WHERE [TeacherID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Teachers WHERE [TeacherID] = @id", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = Int32.Parse(id);
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                if (newId == "")
                {
                    return;
                }

                base.Delete(newId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public new void GetValues(string id)
        {
            string newId = String.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Teachers WHERE [TeacherID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newId = reader["UserID"].ToString();
                        }
                    }
                }

                con.Close();
            }

            if (newId == "")
            {
                return;
            }

            base.GetValues(newId);

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [Specialization] FROM Teachers WHERE [TeacherID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            DataRow row = dt.Rows[0];
            specialization = row["Specialization"].ToString();
        }

        public new void Update(string id)
        {
            try
            {
                string newId = String.Empty;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Teachers WHERE [TeacherID] = @id", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = Int32.Parse(id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newId = reader["UserID"].ToString();
                            }
                        }
                    }

                    con.Close();
                }

                if (newId == "")
                {
                    return;
                }

                base.Update(newId);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Teachers SET [Specialization]=@specialization WHERE [TeacherID]=@id", con))
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

        public IDictionary<int, string> GetList()
        {
            Dictionary<int, string> advisers = new Dictionary<int, string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TeacherID, (LastName + ', ' + FirstName + ' ' + MiddleName) AS Name FROM TeachersView", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            advisers.Add(Int32.Parse(reader["TeacherID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }

                con.Close();
            }

            return advisers;
        }
    }
}
