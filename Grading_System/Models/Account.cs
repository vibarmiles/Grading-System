using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Grading_System.Classes;

namespace Grading_System.Models
{
    public class Account : IAccount
    {
        private string connectionString;

        public Account(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string HashPassword(string password)
        {
            string hash = String.Empty;

            using (HashAlgorithm sha256 = SHA256.Create())
            {
                foreach (byte b in sha256.ComputeHash(Encoding.UTF8.GetBytes(password)))
                {
                    hash += b.ToString("x2");
                }
            }

            return hash;
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
                    cmd.Parameters["@password"].Value = HashPassword(password);
                    Console.WriteLine(HashPassword(password));

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
