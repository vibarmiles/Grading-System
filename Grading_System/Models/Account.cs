using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
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

        public void ChangeProfile(string position, int id, string username, string password)
        {
            int userId = id;

            if (position.Equals("Teacher"))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [TeacherID] FROM [Teachers] WHERE [UserID]=@id", con))
                    {
                        cmd.Parameters.Add("id", System.Data.SqlDbType.BigInt);
                        cmd.Parameters["id"].Value = id;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                userId = Convert.ToInt32(dr["TeacherID"]);
                            }

                            dr.Close();
                        }
                    }

                    con.Close();
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET [Username]=@username, [Password]=@password, [FirstLogin]=1 WHERE [UserID]=@id", conn))
                    {
                        cmd.Parameters.Add("id", System.Data.SqlDbType.BigInt).Value = userId;
                        cmd.Parameters.Add("username", System.Data.SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("password", System.Data.SqlDbType.VarChar).Value = HashPassword(password);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated!");
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetUsername(int id, string position)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT U.Username FROM Users U LEFT JOIN Teachers T ON U.UserID=T.UserID WHERE (T.TeacherID=@id AND @position='Teacher') OR (U.UserID=@id AND @position!='Teacher')", conn))
                {
                    cmd.Parameters.Add("id", System.Data.SqlDbType.BigInt).Value = id;
                    cmd.Parameters.Add("position", System.Data.SqlDbType.VarChar).Value = position;
                    Console.WriteLine(id + " " + position);
                    return (string)cmd.ExecuteScalar();
                }

                conn.Close();
            }
        }

        public IDictionary<int, string> VerifyAccount(string username, string password)
        {
            Dictionary<int, string> account = new Dictionary<int, string>();
            string result = String.Empty;
            int id = 0;
            string login = "Yes";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT [UserID], [Position], [FirstLogin] FROM [Users] WHERE [Username] = @username AND [Password] = @password", con))
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

                            if (Convert.ToInt32(dr["FirstLogin"]) == 1)
                            {
                                login = "No";
                            }
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

            Console.WriteLine(id);
            account.Add(id, result);
            account.Add(-1, login);
            return account;
        }
    }
}
