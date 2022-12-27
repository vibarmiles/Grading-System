using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class BaseModel : IBaseRepository
    {
        string connectionString;
        string tableOrView;
        string key;

        public BaseModel(string connectionString, string tableOrView, string key)
        {
            this.connectionString = connectionString;
            this.tableOrView = tableOrView;
            this.key = key;
        }

        public DataTable View()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableOrView + " ORDER BY " + key, con))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            return dt;
        }
    }
}
