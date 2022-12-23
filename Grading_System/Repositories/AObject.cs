using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Repositories
{
    public abstract class AObject
    {
        private string connectionString;
        private string tableOrView;
        private string key;

        public AObject(string connectionString, string tableOrView, string key)
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

        public abstract void Add();
        public abstract void GetValues(string id);
        public abstract void Update(string id);
        public abstract void Delete(string id);
    }
}
