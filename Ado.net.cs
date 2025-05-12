using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;

namespace ex1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source = DESKTOP - 6BHFQS2\\SQLEXPRESS; Initial Catalog = userdb; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection Successful!");

                    //Ensure table exists before creating
                    string sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='studd' AND xtype='U')
                                   CREATE TABLE studd (
                                       id INT PRIMARY KEY,
                                       name VARCHAR(30)
                                   );";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Table 'studd' verified/created successfully.");
                    }

                    // Insert data into the table
                    string sqlins = @"INSERT INTO studd(id, name) VALUES (3, 'Dhanush'), (4, 'Dileep');";
                    using (SqlCommand cmd = new SqlCommand(sqlins, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted {rowsAffected} rows successfully.");
                    }

                    string sqldel = @"delete from studd where id IN (3,4);";
                    using (SqlCommand cmd = new SqlCommand(sqldel, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"deleted {rowsAffected} rows successfully.");
                    }

                    // Display records from the table
                    string sqldis = @"SELECT id, name FROM studd;";
                    using (SqlCommand cmd = new SqlCommand(sqldis, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["id"]}, {reader["name"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No records found.");
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error: " + ex.Message);
                }
            }
        }
    }
}
