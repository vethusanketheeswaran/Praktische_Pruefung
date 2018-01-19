using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pruefung_Praktisch_Musterloesung.Models
{
    public class Lab4IntrusionLog
    {
        private SqlConnection setUp()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Pruefung_Praktisch_Musterloesung\\Pruefung_Praktisch_Musterloesung\\App_Data\\lab4.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public bool logIntrusion(string ip, string browser, string intrusion_comment)
        {
            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "INSERT INTO [dbo].[Intrusionlog] ([IP], [Browser], [Comment], [CreatedOn]) VALUES('" + ip + "','" + browser + "','" + intrusion_comment + "', getdate())";
            cmd_credentials.Connection = con;

            con.Open();

            int res = cmd_credentials.ExecuteNonQuery();

            con.Close();

            return res > 0;
        }

        public List<List<string>> getAllData()
        {
            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT IP, Browser, Comment, CreatedOn FROM [dbo].[Intrusionlog] ORDER BY CreatedOn DESC";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            List<List<string>> ret = new List<List<string>>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<string> local_entries = new List<string>();

                    var ip = reader.GetValue(0).ToString();
                    var browser = reader.GetValue(1).ToString();
                    var comment = reader.GetValue(2).ToString();
                    var when = reader.GetValue(3).ToString();

                    local_entries.Add(ip);
                    local_entries.Add(browser);
                    local_entries.Add(comment);
                    local_entries.Add(when);

                    ret.Add(local_entries);
                }
            }

            con.Close();

            return ret;
        }
    }
}