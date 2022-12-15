using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Grading_System.Classes
{
    public class Account : IAccount
    {
        private string connectionString;

        public Account(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string VerifyAccount(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [Position] FROM [Users] WHERE [Username] = @username AND [Password] = @password", con))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@username"].Value = username;
                    cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@password"].Value = Database.HashPassword(password);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        string result = String.Empty;

                        if (dr.Read())
                        {
                            result = dr["Position"].ToString();
                        }

                        dr.Close();
                        con.Close();
                        return result;
                    }
                }
            }
        }
    }
}
