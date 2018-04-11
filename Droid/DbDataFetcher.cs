﻿using System;
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

        public String GetData(string command)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GetData)} Droid");

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
            MySqlCommand sqlcmd = new MySqlCommand(command, sqlconn);
            String result = sqlcmd.ExecuteScalar().ToString();
            sqlconn.Close();

            return result;

        }
    }
}