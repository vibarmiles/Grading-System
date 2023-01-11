using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class Class : IStudentClass
    {
        private string connectionString;

        public Class(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDictionary<int[], string> GetList(int id)
        {
            Dictionary<int[], string> classes = new Dictionary<int[], string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT STS.[SubjectID], STS.[TeacherID], S.[SubjectName] FROM Students_Teachers_Subjects STS INNER JOIN Subjects S ON S.[SubjectID]=STS.[SubjectID] WHERE STS.[StudentID]=@id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = id;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int[] ids = { Int32.Parse(reader["SubjectID"].ToString()), Int32.Parse(reader["TeacherID"].ToString()) };
                            classes.Add(ids, reader["SubjectName"].ToString());
                        }
                    }
                }

                con.Close();
            }

            return classes;
        }
    }
}
