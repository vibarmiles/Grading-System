using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Grading_System
{
    internal class Database
    {
        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vibar\\source\\repos\\Grading_System\\Grading_System\\Grading_System.mdf;Integrated Security=True";

        public static void checkConnection()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    Console.WriteLine("Connection Online!");
                }

                con.Close();
            }
        }

        public static int SelectID(string table, string column, string condition, string value)
        {
            int select = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM " + table + " WHERE " + condition + "=" + value, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            select = Int32.Parse(dr.GetSqlValue(0).ToString());
                        }
                    }
                }

                con.Close();
            }

            return select;
        }

        public static string[] SelectRow(string table, string column, string condition, string value)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM " + table + " WHERE " + condition + " = " + value, con))
                {
                    Console.WriteLine(cmd.CommandText);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        string[] result = new string[dr.FieldCount];

                        if (dr.Read())
                        {
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                result[i] = dr[i].ToString();
                            }
                        }

                        dr.Close();
                        con.Close();
                        return result;
                    }
                }
            }
        }

        public static DataTable ViewTable(string table)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM " + table, con))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                }

                con.Close();
            }

            return dt;
        }

        public static void Insert(string table, string column, string value)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + table + " (" + column + ") VALUES (" + value + ")", con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Update(string table, string set, string condition, string value)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE " + table + " SET" + set + " WHERE " + condition + "=" + value, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Delete(string table, string condition, string value)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM " + table + " WHERE " + condition + "=" + value, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string HashPassword(string password)
        {
            string hash = String.Empty; 

            using (HashAlgorithm sha256 = SHA256.Create())
            {
                foreach(byte b in sha256.ComputeHash(Encoding.UTF8.GetBytes(password))) {
                    hash += b.ToString("x2");
                }
            }

            return hash;
        }
    }
}
