using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class Grades : IGrades, IBaseRepository
    {
        private string connectionString;

        public Grades(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void GetGrades(int studentId, int subjectId, int teacherId)
        {

        }

        public DataTable View()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("", conn))

                conn.Close();
            }

            return dt;
        }
    }
}
