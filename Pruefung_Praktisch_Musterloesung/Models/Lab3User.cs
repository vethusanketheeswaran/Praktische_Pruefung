using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pruefung_Praktisch_Musterloesung.Models
{
    public class Lab3User
    {
        private SqlConnection setUp()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Pruefung_Praktisch_Musterloesung\\Pruefung_Praktisch_Musterloesung\\App_Data\\lab3.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public bool checkCredentials(string username, string password)
        {
            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT id FROM [dbo].[User] WHERE Username = '" + username + "' AND Password = '" + password + "'";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            bool ret = reader.HasRows;

            con.Close();

            return ret;
        }
    }
}