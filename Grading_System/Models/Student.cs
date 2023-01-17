using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grading_System.Models
{
    internal class Student : BaseModel, IStudent, IObjectList, ISectionStudent
    {
        string connectionString;
        string fname, mname, lname, gender, lrn, sectionName;
        DateTime enrollmentDate;
        int sectionID;

        public Student(string connectionString) : base(connectionString, "StudentsView", "[StudentID]")
        {
            this.connectionString = connectionString;
        }

        public string FName { get => fname; set => fname = value; }
        public string MName { get => mname; set => mname = value; }
        public string LName { get => lname; set => lname = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime EnrollmentDate { get => enrollmentDate; set => enrollmentDate = value; }
        public string LRN { get => lrn; set => lrn = value; }

        public string SectionName => sectionName;

        public int SectionID { get => sectionID; set => sectionID = value; }

        public void Add()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Students ([SectionID],[FirstName],[MiddleName],[LastName],[Gender],[EnrollmentDate],[LRN]) VALUES (@id,@fname,@mname,@lname,@gender,@date,@lrn)", con))
                    {
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = sectionID;
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
                        cmd.Parameters.Add("gender", SqlDbType.VarChar);
                        cmd.Parameters["gender"].Value = gender;
                        cmd.Parameters.Add("lrn", SqlDbType.VarChar);
                        cmd.Parameters["lrn"].Value = lrn;
                        cmd.Parameters.Add("date", SqlDbType.Date);
                        cmd.Parameters["date"].Value = enrollmentDate;
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Grades WHERE [StudentID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Students_Teachers_Subjects WHERE [StudentID] = @id", con))
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE [StudentID] = @id", con))
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

        public IDictionary<int, string> GetList()
        {
            Dictionary<int, string> students = new Dictionary<int, string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT StudentID, Name FROM StudentsView", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(Int32.Parse(reader["StudentID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }

                con.Close();
            }

            return students;
        }

        public IDictionary<int, string> GetList(int id)
        {
            Dictionary<int, string> students = new Dictionary<int, string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT S.StudentID, (SV.LastName + ', ' + SV.FirstName + ' ' + SV.MiddleName) AS Name FROM StudentsView SV INNER JOIN Students S ON S.[StudentID]=SV.[StudentID] WHERE S.[SectionID]=@id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = id;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(Int32.Parse(reader["StudentID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }

                con.Close();
            }

            return students;
        }

        public void GetValues(string id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT StudentsView.[SectionName],Students.[FirstName],Students.[MiddleName],Students.[LastName],Students.[EnrollmentDate],Students.[LRN],Students.[Gender] FROM Students INNER JOIN StudentsView ON Students.StudentID=StudentsView.StudentID WHERE Students.[StudentID] = @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = Int32.Parse(id);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            DataRow row = dt.Rows[0];
            sectionName = row["SectionName"].ToString();
            fname = row["FirstName"].ToString();
            mname = row["MiddleName"].ToString();
            lname = row["LastName"].ToString();
            gender = row["Gender"].ToString();
            lrn = row["LRN"].ToString();
            enrollmentDate = Convert.ToDateTime(row["EnrollmentDate"]);
        }

        public void Update(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Students SET [SectionID]=@sectionId,[FirstName]=@fname,[MiddleName]=@mname,[LastName]=@lname,[Gender]=@gender,[LRN]=@lrn,[EnrollmentDate]=@date WHERE [StudentID]=@id", con))
                    {
                        Console.WriteLine(cmd.CommandText);
                        cmd.Parameters.Add("sectionId", SqlDbType.BigInt);
                        cmd.Parameters["sectionId"].Value = sectionID;
                        cmd.Parameters.Add("fname", SqlDbType.VarChar);
                        cmd.Parameters["fname"].Value = fname;
                        cmd.Parameters.Add("mname", SqlDbType.VarChar);
                        cmd.Parameters["mname"].Value = mname;
                        cmd.Parameters.Add("lname", SqlDbType.VarChar);
                        cmd.Parameters["lname"].Value = lname;
                        cmd.Parameters.Add("gender", SqlDbType.VarChar);
                        cmd.Parameters["gender"].Value = gender;
                        cmd.Parameters.Add("lrn", SqlDbType.VarChar);
                        cmd.Parameters["lrn"].Value = lrn;
                        cmd.Parameters.Add("date", SqlDbType.Date);
                        cmd.Parameters["date"].Value = enrollmentDate;
                        cmd.Parameters.Add("id", SqlDbType.BigInt);
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
