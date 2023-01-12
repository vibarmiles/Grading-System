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

        public IDictionary<int, string> VerifyAccount(string username, string password)
        {
            Dictionary<int, string> account = new Dictionary<int, string>();
            string result = String.Empty;
            int id = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [UserID], [Position] FROM [Users] WHERE [Username] = @username AND [Password] = @password", con))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@username"].Value = username;
                    cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@password"].Value = HashPassword(password);
                    Console.WriteLine(HashPassword(password));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = dr["Position"].ToString();
                            id = Convert.ToInt32(dr["UserID"]);
                        }

                        dr.Close();
                        con.Close();
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [TeacherID] FROM [Teachers] WHERE [UserID] = @id", con))
                {
                    cmd.Parameters.Add("id", System.Data.SqlDbType.BigInt);
                    cmd.Parameters["id"].Value = id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = Convert.ToInt32(dr["TeacherID"]);
                        }

                        dr.Close();
                        con.Close();
                    }
                }
            }

            account.Add(id, result);
            return account;
        }
    }
}
