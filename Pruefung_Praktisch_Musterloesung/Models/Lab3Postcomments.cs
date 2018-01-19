using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pruefung_Praktisch_Musterloesung.Models
{
    public class Lab3Postcomments
    {
        private SqlConnection setUp()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Pruefung_Praktisch_Musterloesung\\Pruefung_Praktisch_Musterloesung\\App_Data\\lab3.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public bool storeComment(int postid, string comment)
        {
            SqlConnection con = this.setUp();

            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "INSERT INTO [dbo].[Comment] ([PostId], [Comment], [CreatedOn]) VALUES('" + postid + "','" + comment + "', getdate())";
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
            cmd_credentials.CommandText = "SELECT Id, Title, Description, Content FROM [dbo].[Post] ORDER BY CreatedOn DESC";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            List<List<string>> ret = new List<List<string>>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<string> local_entries = new List<string>();

                    var postid = reader.GetValue(0).ToString();
                    var title = reader.GetValue(1).ToString();
                    var description = reader.GetValue(2).ToString();
                    var content = reader.GetValue(3).ToString();

                    local_entries.Add(postid);
                    local_entries.Add(title);
                    local_entries.Add(description);
                    local_entries.Add(content);

                    SqlConnection con2 = this.setUp();

                    SqlCommand cmd_comments = new SqlCommand();
                    cmd_comments.CommandText = "SELECT Id, Comment FROM [dbo].[Comment] ORDER BY CreatedOn DESC";
                    cmd_comments.Connection = con2;

                    con2.Open();

                    string comments = "";

                    SqlDataReader reader_comments = cmd_comments.ExecuteReader();
                    if (reader_comments.HasRows)
                    {
                        while (reader_comments.Read())
                        {
                            //List<string> local_comment = new List<string>();
                            //var commentid = reader_comments.GetValue(0).ToString();
                            var comment = reader_comments.GetValue(1).ToString();
                            comments += comment + "<br><br>";
                        }
                    }

                    con2.Close();

                    local_entries.Add(comments);

                    ret.Add(local_entries);
                
                }
            }

            con.Close();

            return ret;
        }
    }
}