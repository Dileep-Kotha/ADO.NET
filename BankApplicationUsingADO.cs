using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankusingADOl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int accno;
            string name,str;
            string pin;
            string display,dep,wdraw;
            string addacc;
            float amount;
            string cmstr1,query;
            int af,up,ch;


            int id,did,wid,sid;
            string un, pass,vpin,spin,show;
            float sal,dam,wam;

            
            string constring ="Data Source=DESKTOP-6BHFQS2\\SQLEXPRESS;Initial Catalog=userdb;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using(SqlConnection conn = new SqlConnection(constring))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("connection established");
                   cmstr1= @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'bank')
BEGIN
    CREATE TABLE bank(accno int primary key, name varchar(20), pin varchar(8), amount float);
END;";
                    using (SqlCommand comm = new SqlCommand(cmstr1,conn))
                    {
                        comm.ExecuteNonQuery();
                        Console.WriteLine("created");
                    }

                    do
                    {
                        Console.WriteLine("Bank Application:\n 1.Create Account\n2.Deposit\n3.Withdraw\n4.Display All Accounts\n5.Show");
                        Console.WriteLine("Enter the choice");
                        ch = Convert.ToInt32(Console.ReadLine());
                        switch(ch)
                        {
                            case 1:
                                Console.WriteLine("enter the accno(3-digit)");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter name");
                                un = Console.ReadLine();
                                Console.WriteLine("create password(4-digit) like 2233");
                                pass = Console.ReadLine();
                                Console.WriteLine("deposit basic amount");
                                sal = Convert.ToInt64(Console.ReadLine());

                                addacc = $"insert into bank values({id},'{un}','{pass}',{sal});";

                                using (SqlCommand comm = new SqlCommand(addacc, conn))
                                {
                                    af = comm.ExecuteNonQuery();
                                    Console.WriteLine($"Added account:{af}");
                                    Console.WriteLine($"{id} created");
                                }
                                break;

                            case 2://deposit
                                Console.WriteLine("enter your accno:");
                                did = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the pin");
                                vpin=Console.ReadLine();
                                query = $"SELECT COUNT(*) FROM bank WHERE accno = {did} AND pin = {vpin}";
                                using (SqlCommand cm = new SqlCommand(query, conn))
                                {
                                    int c=(int)cm.ExecuteScalar();
                                    if(c>0)
                                    {
                                        Console.WriteLine("Credentials verified");
                                        Console.WriteLine("enter the amount to deposit");
                                        dam = Convert.ToInt64(Console.ReadLine());

                                        dep = $"update bank set amount=amount+{dam} where accno={did};";
                                        using (SqlCommand comm = new SqlCommand(dep, conn))
                                        {
                                            up = comm.ExecuteNonQuery();
                                            Console.WriteLine($"{up} account updated");
                                        }
                                        //after deposit
                                        Console.WriteLine("After deposit");
                                        display = $"select * from bank where accno={did};";
                                        using (SqlCommand comm = new SqlCommand(display, conn))
                                        {
                                            Console.WriteLine("Accno    Name   Pin   Amount");
                                            SqlDataReader r = comm.ExecuteReader();
                                            while (r.Read())
                                            {
                                                Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                                            }
                                            r.Close();

                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("invalid credentials");

                                    }

                                }
                                
                                break;

                            case 3:
                                Console.WriteLine("enter your accno:");
                                wid = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the pin:");
                                vpin=Console.ReadLine() ;
                                query = $"SELECT COUNT(*) FROM bank WHERE accno = {wid} AND pin = {vpin}";
                                using(SqlCommand cm= new SqlCommand(query, conn))
                                {
                                    int c=(int)cm.ExecuteScalar();
                                    if(c>0)
                                    {
                                        Console.WriteLine("Credentials Verified");
                                        Console.WriteLine("enter the amount to withdraw");
                                        wam = Convert.ToInt64(Console.ReadLine());

                                        wdraw = $"update bank set amount=amount-{wam} where accno={wid};";
                                        using (SqlCommand comm = new SqlCommand(wdraw, conn))
                                        {
                                            up = comm.ExecuteNonQuery();
                                            Console.WriteLine($"{up} account updated");
                                        }
                                        //after withdrawal

                                        Console.WriteLine("After Withdrawal:");
                                        display = $"select * from bank where accno={wid};";
                                        using (SqlCommand comm = new SqlCommand(display, conn))
                                        {
                                            Console.WriteLine("Accno    Name   Pin   Amount");
                                            SqlDataReader r = comm.ExecuteReader();
                                            while (r.Read())
                                            {
                                                Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                                            }
                                            r.Close();

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Credentials");

                                    }
                                } 
                                break;

                            case 4:
                                display = @"select * from bank;";
                                using (SqlCommand comm = new SqlCommand(display, conn))
                                {
                                    SqlDataReader r = comm.ExecuteReader();
                                    Console.WriteLine("Accno    Name   Pin   Amount");
                                    while (r.Read())
                                    {
                                        Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                                    }
                                    r.Close();
                                }
                                break;
                            case 5:  
                                    Console.WriteLine("enter your accno:");
                                    sid = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter the pin");
                                    spin = Console.ReadLine();
                                    show = $"select * from bank where accno={sid} and pin={spin}";
                                    using(SqlCommand cm=new SqlCommand(show,conn))
                                    {
                                      SqlDataReader r = cm.ExecuteReader();
                                        while(r.Read())
                                        {
                                         Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");
                                        }
                                        r.Close();
                                    }
                                    break;

                            default:Console.WriteLine("Select Valid  choice");
                                break;
                        }
                        Console.WriteLine("enter y to continue:");
                        str=Console.ReadLine();
                    } while (str=="y");









                    //creating new account
                    //addacc = @"insert into bank values(100,'dileep','2222',20500);";
                    //Console.WriteLine("enter the accno(3-digit)");
                    //id = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine("enter name");
                    //un=Console.ReadLine();
                    //Console.WriteLine("create password(4-digit) like 2233");
                    //pass = Console.ReadLine();
                    //Console.WriteLine("deposit basic amount");
                    //sal = Convert.ToInt64(Console.ReadLine());

                    //addacc = $"insert into bank values({id},'{un}','{pass}',{sal});";

                    //using (SqlCommand comm = new SqlCommand(addacc, conn))
                    //{
                    //    af=comm.ExecuteNonQuery();
                    //    Console.WriteLine($"Added account:{af}");
                    //}



                    //display
                    //display = @"select * from bank;";
                    //using (SqlCommand comm = new SqlCommand(display,conn))
                    //{
                    //    SqlDataReader r = comm.ExecuteReader();
                    //    Console.WriteLine("Accno    Name   Pin   Amount");
                    //    while (r.Read())
                    //    {                           
                    //        Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                    //    }
                    //    r.Close();
                    //}

                    
                    //deposit
                    //Console.WriteLine("enter your accno:");
                    //did=Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine("enter the amount to deposit");
                    //dam = Convert.ToInt64(Console.ReadLine());

                    //dep = $"update bank set amount=amount+{dam} where accno={did};";
                    //using (SqlCommand comm = new SqlCommand(dep,conn))
                    //{
                    //    up = comm.ExecuteNonQuery();
                    //    Console.WriteLine($"{up} account updated");
                    //}
                    ////after deposit
                    //Console.WriteLine("After deposit");
                    //display = $"select * from bank where accno={did};";
                    //using (SqlCommand comm = new SqlCommand(display, conn))
                    //{
                    //    Console.WriteLine("Accno    Name   Pin   Amount");
                    //    SqlDataReader r = comm.ExecuteReader();
                    //    while (r.Read())
                    //    {     
                    //        Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                    //    }
                    //    r.Close();

                    //}



                    //withdrawal
                    //Console.WriteLine("enter your accno:");
                    //wid = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine("enter the amount to withdraw");
                    //wam = Convert.ToInt64(Console.ReadLine());

                    //wdraw = $"update bank set amount=amount-{wam} where accno={wid};";
                    //using (SqlCommand comm = new SqlCommand(wdraw, conn))
                    //{
                    //    up = comm.ExecuteNonQuery();
                    //    Console.WriteLine($"{up} account updated");
                    //}
                    //after withdrawal

                    //Console.WriteLine("After Withdrawal:");
                    //display = $"select * from bank where accno={wid};";
                    //using (SqlCommand comm = new SqlCommand(display, conn))
                    //{
                    //    Console.WriteLine("Accno    Name   Pin   Amount");
                    //    SqlDataReader r = comm.ExecuteReader();
                    //    while (r.Read())
                    //    {
                    //        Console.WriteLine($"{r["accno"]}, {r["name"]}, {r["pin"]}, {r["amount"]}");

                    //    }
                    //    r.Close();

                    //}












                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                finally
                {
                    Console.WriteLine("finally executed");
                    Console.WriteLine("Thank you");
                    conn.Close();
                }
            }
        }
    }
}   
