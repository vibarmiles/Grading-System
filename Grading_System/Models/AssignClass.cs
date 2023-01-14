using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grading_System.Models
{
    internal class AssignClass : IAssignClass
    {
        private string connectionString;
        private int teacherId;
        private int subjectId;
        private int studentId;
        private int sectionId;

        public AssignClass(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int StudentID { get => studentId; set => studentId = value; }
        public int SectionID { get => sectionId; set => sectionId = value; }
        public int TeacherID { get => teacherId; set => teacherId = value; }
        public int SubjectID { get => subjectId; set => subjectId = value; }

        public void Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [SubjectID] FROM Students_Teachers_Subjects WHERE [SubjectID]=@subjectId AND [TeacherID]=@teacherId AND [StudentID]=@studentId", connection))
                    {
                        cmd.Parameters.Add("teacherId", SqlDbType.BigInt);
                        cmd.Parameters["teacherId"].Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.BigInt);
                        cmd.Parameters["subjectId"].Value = subjectId;
                        cmd.Parameters.Add("studentId", SqlDbType.BigInt);
                        cmd.Parameters["studentId"].Value = studentId;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("Return!");
                                return;
                            }
                        }
                    }

                    connection.Close();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Students_Teachers_Subjects VALUES (@studentId, @sectionId, @teacherId, @subjectId, @year); INSERT INTO Grades Values (@studentId, @sectionId, @teacherId, @subjectId, 1, 0); INSERT INTO Grades Values (@studentId, @sectionId, @teacherId, @subjectId, 2, 0); INSERT INTO Grades Values (@studentId, @sectionId, @teacherId, @subjectId, 3, 0); INSERT INTO Grades Values (@studentId, @sectionId, @teacherId, @subjectId, 4, 0)", connection))
                    {
                        cmd.Parameters.Add("studentId", SqlDbType.BigInt);
                        cmd.Parameters["studentId"].Value = studentId;
                        cmd.Parameters.Add("sectionId", SqlDbType.BigInt).Value = sectionId;
                        cmd.Parameters.Add("teacherId", SqlDbType.BigInt);
                        cmd.Parameters["teacherId"].Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.BigInt);
                        cmd.Parameters["subjectId"].Value = subjectId;
                        cmd.Parameters.Add("year", SqlDbType.VarChar);
                        cmd.Parameters["year"].Value = (DateTime.Now.Year + "-" + (DateTime.Now.Year + 1)).ToString();
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DataTable GetList(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT ST.[SubjectID], S.[SubjectName], T.[TeacherID], T.[Name], STS.[StudentID] FROM Teachers_Subjects ST INNER JOIN TeachersView T ON T.[TeacherID] = ST.[TeacherID] INNER JOIN Subjects S ON ST.[SubjectID]= S.[SubjectID] LEFT JOIN Students_Teachers_Subjects STS ON STS.[SubjectID]=ST.[SubjectID] AND STS.[TeacherID]=ST.[TeacherID] AND STS.[StudentID]=@id ORDER BY S.[SubjectID]", connection))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = id;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                connection.Close();
            }

            return dt;
        }

        public void Remove()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Grades WHERE [TeacherID]=@teacherId AND [SubjectID]=@subjectId AND [StudentID]=@studentId AND [SectionID]=@sectionID; DELETE FROM Students_Teachers_Subjects WHERE [TeacherID]=@teacherId AND [SubjectID]=@subjectId AND [StudentID]=@studentId AND [SectionID]=@sectionID", connection))
                    {
                        cmd.Parameters.Add("studentId", SqlDbType.BigInt);
                        cmd.Parameters["studentId"].Value = studentId;
                        cmd.Parameters.Add("teacherId", SqlDbType.BigInt);
                        cmd.Parameters["teacherId"].Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.BigInt);
                        cmd.Parameters["subjectId"].Value = subjectId;
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
