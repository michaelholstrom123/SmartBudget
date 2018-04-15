using System;
using System.Collections.Generic;
using PrismIntro.Droid;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(DbDataFetcher))]
namespace PrismIntro.Droid

{
    public class DbDataFetcher : IDbDataFetcher
    {
        public DbDataFetcher() { }

        public List<string> GetData(string command)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GetData)} DROID");

            List<string> result = new List<string>();

            MySqlConnection sqlconn;
            string connsqlstring = string.Format("Server=csusm.c0uo1rgt9ctn.us-west-2.rds.amazonaws.com;Port=3306;database=S(G)_E4J;User Id=cs441;password=csusmcs441;charset=utf8");
            sqlconn = new MySqlConnection(connsqlstring);
            try
            {
                sqlconn.Open();
            }

            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Debug.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Debug.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }

            Debug.WriteLine($"**** CONNECTION OPEN");
            MySqlCommand sqlcmd = new MySqlCommand(command, sqlconn);
            MySqlDataReader reader = sqlcmd.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                result.Add(reader[reader.GetName(i)].ToString());
                Debug.WriteLine($"**** {result[i]}");
                i = i + 1;
            }

            reader.Close();
            sqlconn.Close();

            Debug.WriteLine($"**** CONNECTION CLOSED");

            return result;
        }
    }
}