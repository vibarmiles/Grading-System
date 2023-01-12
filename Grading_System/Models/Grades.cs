using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class Grades : IGrades
    {
        private string connectionString;

        public Grades(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetGrades(int studentId, int subjectId, int teacherId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Grade FROM Grades WHERE [StudentID]=@student AND [TeacherID]=@teacher AND [SubjectID]=@subject", conn))
                {
                    cmd.Parameters.Add("student", SqlDbType.Int).Value = studentId;
                    cmd.Parameters.Add("teacher", SqlDbType.Int).Value = teacherId;
                    cmd.Parameters.Add("subject", SqlDbType.Int).Value = subjectId;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                conn.Close();
            }

            return dt;
        }

        public void Update(int studentId, int subjectId, int teacherId, int quarter, double grade)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE Grades SET [Grade]=@grade WHERE [StudentID]=@student AND [TeacherID]=@teacher AND [SubjectID]=@subject AND [Quarter]=@quarter", conn))
                {
                    cmd.Parameters.Add("student", SqlDbType.Int).Value = studentId;
                    cmd.Parameters.Add("teacher", SqlDbType.Int).Value = teacherId;
                    cmd.Parameters.Add("subject", SqlDbType.Int).Value = subjectId;
                    cmd.Parameters.Add("quarter", SqlDbType.Int).Value = quarter;
                    cmd.Parameters.Add("grade", SqlDbType.Decimal).Value = grade;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public DataTable View(int id, string position, int teacherId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                if (position.Equals("Admin"))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT StudentID, Name, SubjectName, [1], [2], [3], [4], Average FROM GradesPivotedView WHERE [StudentID]=@id", conn))
                    {
                        cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                } else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT StudentID, Name, SubjectName, [1], [2], [3], [4], Average FROM GradesPivotedView WHERE [StudentID]=@id AND [TeacherID]=@teacherId", conn))
                    {
                        cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("teacherId", SqlDbType.Int).Value = teacherId;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

                conn.Close();
            }

            return this.AddRow(dt);
        }

        public DataTable View(int id, string position, int teacherId, int subjectId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (position.Equals("Admin"))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT StudentID, Name, [1], [2], [3], [4], Average FROM GradesPivotedView WHERE [SubjectID]=@subjectId AND [SectionID]=@sectionId", conn))
                    {
                        cmd.Parameters.Add("sectionId", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("subjectId", SqlDbType.Int).Value = subjectId;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT StudentID, Name, [1], [2], [3], [4], Average FROM GradesPivotedView WHERE [SubjectID]=@subjectId AND [TeacherID]=@teacherId AND [SectionID]=@sectionId", conn))
                    {
                        cmd.Parameters.Add("sectionId", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("teacherId", SqlDbType.Int).Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.Int).Value = subjectId;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

                conn.Close();
            }

            return this.AddRow(dt);
        }

        private DataTable AddRow(DataTable dt)
        {
            DataColumn grade = new DataColumn("Grade");
            dt.Columns.Add(grade);

            foreach (DataRow dr in dt.Rows)
            {
                double value = Convert.ToDouble(dr["Average"]);
                if (value >= 98 && value <= 100)
                {
                    dr["Grade"] = 1.00;
                }
                else if (value >= 96 && value < 98)
                {
                    dr["Grade"] = 1.25;
                }
                else if (value >= 93 && value < 96)
                {
                    dr["Grade"] = 1.50;
                }
                else if (value >= 90 && value < 93)
                {
                    dr["Grade"] = 1.75;
                }
                else if (value >= 87 && value < 90)
                {
                    dr["Grade"] = 2.00;
                }
                else if (value >= 84 && value < 87)
                {
                    dr["Grade"] = 2.25;
                }
                else if (value >= 80 && value < 84)
                {
                    dr["Grade"] = 2.50;
                }
                else if (value >= 76 && value < 80)
                {
                    dr["Grade"] = 2.75;
                }
                else if (value >= 75 && value < 76)
                {
                    dr["Grade"] = 3.00;
                }
                else if (value < 75)
                {
                    dr["Grade"] = 5.00;
                }
            }

            return dt;
        }
    }
}
