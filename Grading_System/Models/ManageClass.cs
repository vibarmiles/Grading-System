using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class ManageClass : IClass
    {
        private string connectionString;
        private int teacherId;
        private int subjectId;

        public ManageClass(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public int TeacherID { get => teacherId; set => teacherId = value; }
        public int SubjectID { get => subjectId; set => subjectId = value; }

        public void Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [SubjectID] FROM Teachers_Subjects WHERE [SubjectID]=@subjectId AND [TeacherID]=@teacherId", connection))
                    {
                        cmd.Parameters.Add("teacherId", SqlDbType.BigInt);
                        cmd.Parameters["teacherId"].Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.BigInt);
                        cmd.Parameters["subjectId"].Value = subjectId;

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

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Teachers_Subjects VALUES (@teacherId, @subjectId)", connection))
                    {
                        cmd.Parameters.Add("teacherId", SqlDbType.BigInt);
                        cmd.Parameters["teacherId"].Value = teacherId;
                        cmd.Parameters.Add("subjectId", SqlDbType.BigInt);
                        cmd.Parameters["subjectId"].Value = subjectId;
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            } catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        public void Remove()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Teachers_Subjects WHERE [TeacherID]=@teacherId AND [SubjectID]=@subjectId", connection))
                    {
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

        public DataTable GetList(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("Select S.[SubjectID], S.[SubjectName], T.[TeacherID] FROM Subjects S LEFT JOIN Teachers_Subjects T ON S.[SubjectID]=T.[SubjectID] AND T.[TeacherID]=@id ORDER BY S.[SubjectID]", connection))
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
    }
}
