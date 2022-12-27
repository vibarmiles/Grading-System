using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Models
{
    public class Section : BaseModel, ISection
    {
        private string connectionString;
        private string name, adviserName;
        private int adviserId, yearlvl;

        public Section(string connectionString) : base(connectionString, "SectionsView", "[SectionID]")
        {
            this.connectionString = connectionString;
        }

        public string Name { get => name; set => name = value; }
        public int AdviserId { get => adviserId; set => adviserId = value; }

        public string AdviserName => adviserName;

        public int YearLevel { get => yearlvl; set => yearlvl = value; }

        public void Add()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Sections ([SectionName],[TeacherID],[YearLevel]) VALUES (@name,@id,@yearlvl)", con))
                    {
                        cmd.Parameters.Add("name", SqlDbType.VarChar);
                        cmd.Parameters["name"].Value = name;
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = adviserId;
                        cmd.Parameters.Add("yearlvl", SqlDbType.Int);
                        cmd.Parameters["yearlvl"].Value = yearlvl;
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Sections WHERE [SectionID] = @id", con))
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

        public IDictionary<int, string> GetAdvisers()
        {
            Dictionary<int, string> advisers = new Dictionary<int, string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TeacherID, Name FROM TeachersView", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            advisers.Add(Int32.Parse(reader["TeacherID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }

                con.Close();
            }

            return advisers;
        }

        public void GetValues(string id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [SectionName], Adviser, YearLevel FROM SectionsView WHERE [SectionID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            DataRow row = dt.Rows[0];
            name = row["SectionName"].ToString();
            adviserName = row["Adviser"].ToString();
            yearlvl = Int32.Parse(row["YearLevel"].ToString());
        }

        public void Update(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Sections SET [SectionName]=@name, [TeacherID]=@adviserId, [YearLevel]=@yearlvl WHERE [SectionID]=@id", con))
                    {
                        cmd.Parameters.Add("name", SqlDbType.VarChar);
                        cmd.Parameters["name"].Value = name;
                        cmd.Parameters.Add("adviserId", SqlDbType.BigInt);
                        cmd.Parameters["adviserId"].Value = adviserId;
                        cmd.Parameters.Add("yearlvl", SqlDbType.Int);
                        cmd.Parameters["yearlvl"].Value = yearlvl;
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
