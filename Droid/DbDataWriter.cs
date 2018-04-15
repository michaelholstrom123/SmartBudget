using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;


[assembly: Xamarin.Forms.Dependency(typeof(PrismIntro.Droid.DbDataWriter))]

namespace PrismIntro.Droid
{
    public class DbDataWriter : IDbDataWriter
    {
        void IDbDataWriter.WriteData(string command)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(IDbDataWriter.WriteData)}:  Droid");
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

            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(IDbDataWriter.WriteData)}: CONNECTION OPEN");
            MySqlCommand sqlcmd = new MySqlCommand(command, sqlconn);

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(IDbDataWriter.WriteData)}: CONNECTION CLOSED");
        }

    }
}
