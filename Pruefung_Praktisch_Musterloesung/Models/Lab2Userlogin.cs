using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pruefung_Praktisch_Musterloesung.Models
{
    public class Lab2Userlogin
    {
        private SqlConnection setUp()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Pruefung_Praktisch_Musterloesung\\Pruefung_Praktisch_Musterloesung\\App_Data\\lab2.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public bool checkCredentials(string username, string password)
        {
            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT id FROM [dbo].[Userlogin] WHERE Username = '" + username + "' AND Password = '" + password + "'";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            bool ret = reader.HasRows;

            con.Close();

            return ret;
        }

        public bool storeSessionInfos(string username, string password, string sessionid)
        {
            if (string.IsNullOrEmpty(sessionid)) return false;

            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "UPDATE [dbo].[Userlogin] SET SessionID = '" + sessionid + "'  WHERE Username = '" + username + "' AND Password = '" + password + "' ";
            cmd_credentials.Connection = con;

            con.Open();

            int res = cmd_credentials.ExecuteNonQuery();

            con.Close();

            return res > 0;
        }

        public bool checkSessionInfos(string sessionid)
        {
            if (string.IsNullOrEmpty(sessionid)) return false;

            SqlConnection con = this.setUp();
            
            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT id FROM [dbo].[Userlogin] WHERE SessionID = '" + sessionid + "'";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            bool ret = reader.HasRows;

            con.Close();

            return ret;
        }
    }
}