using System;
using System.Text;
using System.Data.SqlClient;
using System.Threading;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "172.30.96.55";    // update me
                builder.UserID = "sa";                  // update me
                builder.Password = "Yukon900";          // update me
                builder.InitialCatalog = "master";

                while(true)
                {
                    try{
                        // Connect to SQL
                        Thread.Sleep(1000);
                        Console.Write("Connecting to SQL Server ... ");
                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            connection.Open();
                            String sql = "SELECT @@servername";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine("Currently connected server name is: {0}", reader.GetString(0));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Lost connection!!  Retrying to connect...");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }            
        }
    }
}