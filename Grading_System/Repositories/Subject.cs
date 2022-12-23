using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grading_System.Repositories
{
    internal class Subject : BaseRepository, IHasObject
    {
        private string connectionString;
        private string name;

        public Subject(string connectionString) : base(connectionString, "Subjects", "SubjectID")
        {
            this.connectionString = connectionString;
        }

        public string Name { get => name; set => name = value; }

        public void Add()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Subjects ([SubjectName]) VALUES (@name)", con))
                    {
                        cmd.Parameters.Add("name", SqlDbType.VarChar);
                        cmd.Parameters["name"].Value = name;
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Subjects WHERE [SubjectID] = @id", con))
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

        public void GetValues(string id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [SubjectName] FROM Subjects WHERE [SubjectID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            DataRow row = dt.Rows[0];
            name = row["SubjectName"].ToString();
        }

        public void Update(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Subjects SET [SubjectName]=@name WHERE [SubjectID]=@id", con))
                    {
                        cmd.Parameters.Add("name", SqlDbType.VarChar);
                        cmd.Parameters["name"].Value = name;
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
