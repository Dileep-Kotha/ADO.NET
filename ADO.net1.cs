using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string constring = "Data Source=DESKTOP-6BHFQS2\\SQLEXPRESS;Initial Catalog=userdb;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(constring))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("-->Connection Established:");
                    ////To create Table
                    //string cstr = @"create table employee(id int not null,name varchar(10),age int not null,location varchar(10),salary int not null)";
                    //using (SqlCommand cmd = new SqlCommand(cstr, conn))
                    //{
                    //    cmd.ExecuteNonQuery();
                    //    Console.WriteLine("Table created");
                    //}

                    //To Insert Rows
                    //string istr = @"insert into employee(id,name,age,location,salary) values (1, 'Amit', 28, 'Mumbai', 75000),(2, 'Priya', 32, 'Delhi', 82000),(3, 'Rajesh', 40, 'Bangalore', 95000),(4, 'Sneha', 25, 'Hyderabad', 70000),(5, 'Vikram', 35, 'Ahmedabad', 88000),(6, 'Joshi', 30, 'Pune', 78000),(7, 'Siddharth', 29, 'Jaipur', 77000),(8, 'Ananya', 26, 'Goa', 73000);";
                    //using (SqlCommand cmd1 = new SqlCommand(istr, conn))
                    //{
                    //    int ra = cmd1.ExecuteNonQuery();
                    //    Console.WriteLine($"no.of rows inserted are:{ra}");
                    //}

                    //#####Static insertion
                    //string query = "INSERT INTO employee (id, name, age, location, salary) VALUES (@id, @name, @age, @location, @salary)";

                    //using (SqlCommand cmd = new SqlCommand(query, conn))
                    //{
                    //    cmd.Parameters.AddWithValue("@id", 11);
                    //    cmd.Parameters.AddWithValue("@name", "Karthik");
                    //    cmd.Parameters.AddWithValue("@age", 34);
                    //    cmd.Parameters.AddWithValue("@location", "Chennai");
                    //    cmd.Parameters.AddWithValue("@salary", 82000);
                    //    int r=cmd.ExecuteNonQuery();
                    //    Console.WriteLine($"Record inserted successfully!{r}");
                    //}

                    ////Table Updation
                    //string ustr = @"update employee set salary=salary+4000 where id=4";
                    //using (SqlCommand cmd2 = new SqlCommand(ustr, conn))
                    //{
                    //    int ra = cmd2.ExecuteNonQuery();
                    //    Console.WriteLine($"no.of rows updated are:{ra}");
                    //}

                    string q = @"select id,name,age,location,salary from employee where salary>77000";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();//create SqlDataReader object for reading rows(ExecuteReader() for executing)
                        Console.WriteLine("Employee Table:");
                        Console.WriteLine("id,name,age,location,salary");
                        while (reader.Read()) //for reading one by one record from result set(iterates)
                        {
                            Console.WriteLine($"{reader["id"]}, {reader["name"]}, {reader["age"]}, {reader["location"]}, {reader["salary"]}");
                        }
                    }

                    //Another way to print the dataset using SqlDataAdapter & DataSet

                    //string query = "SELECT id, name, age, location, salary FROM employee";
                    //SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    //DataSet ds = new DataSet();

                    //// Fill the dataset
                    //adapter.Fill(ds, "employee");

                    //// Display the data
                    //foreach (DataRow row in ds.Tables["employee"].Rows)
                    //{
                    //    Console.WriteLine($"{row["id"]}, {row["name"]}, {row["age"]}, {row["location"]}, {row["salary"]}");
                    //}



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }
    }
}
