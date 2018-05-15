using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Tester
{
    class Program
    {


        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Db.Properties.Settings.DbConnectionString"].ConnectionString;
            SqlConnection connection;

            using (connection = new SqlConnection(connectionString))


            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Items", connection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    Console.WriteLine(table.Rows[1][0].ToString());
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.ReadKey();

                }
            }



            //    string connectionString = ConfigurationManager.ConnectionStrings["Db.Properties.Settings.DbConnectionString"].ConnectionString;
            //    SqlConnection connection;

            //    string query = "select * from [Items]";

            //    using (connection = new SqlConnection(connectionString))
            //    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            //    {
            //        DataTable table = new DataTable();
            //        adapter.Fill(table);

            //        StringBuilder s = new StringBuilder();

            //        for (int i = 0, k = table.Rows.Count; i < k; i++)
            //        {                        
            //            string type = table.Rows[2][i].ToString();                      
            //        }

            //    }
            //}

        }
    }
}

