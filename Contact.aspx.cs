/*  Author: Raneem Kayyali 
 *  Project: Messaging Web Application
 *  Date: 11/08/2018
 *  
 *  Summary: This application was made to allow users to 
 *  communicate back and forth via messaging. Users have
 *  the ability to view all activity regarding their account.
 *  Users have the ability to block other users, message them,
 *  and view the conversation history.
 * 
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessagingApp
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ensures that list is populated only once
            if (!IsPostBack)
            {
                GetFriendsList();
            }

            // tries to assign username variable to label and removes user's own name
            // from friends list
            try
            {
                userName.Text = Application["uName"].ToString();
                friendsList.Items.Remove(Application["uName"].ToString());
            }
            catch
            {
                // redirects user to login page if username variable is null
                Response.Redirect("Default.aspx");
            }
        }

        // declare variables 
        OleDbDataReader rdr2;
        int userID;

        // inserts error logs into database 
        private void InsertLog(string res)
        {
            using (OleDbConnection con = new OleDbConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                con.ConnectionString = constring;
                con.Open();
                string query = "INSERT into Log ([Result], [UserID]) VALUES (@result, @usID)";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@result", OleDbType.VarChar).Value = res;
                cmd.Parameters.AddWithValue("@usID", OleDbType.VarChar).Value = userID;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        // populates friends list
        private void GetFriendsList()
        {
            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "select UserName as Friends from Users where UserName <> '"+ Application["uName"].ToString() +"'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    rdr2 = cmd.ExecuteReader();
                    dg.DataSource = rdr2;
                    dg.DataBind();
                    con.Close();
                    con.Dispose();
                }
                catch (SystemException e)
                {
                    // catches errors and inserts them into database
                    InsertLog(e.ToString());
                }
            }

            // populates friends dropdown list
            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "select UserName as Friends from Users";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    rdr2 = cmd.ExecuteReader();

                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                            friendsList.Items.Add(rdr2[0].ToString());
                        }
                    }
                }
                catch (SystemException e)
                {
                    // catches errors and inserts them into database
                    InsertLog(e.ToString());
                }
            }

        }

        // connects to database to get conversation history
        private void GetMessages()
        {
            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "select [From] + ':', Message from Messages where [From] = '"+ userName.Text + "' OR [To] = '" + userName.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    rdr2 = cmd.ExecuteReader();

                    dg2.DataSource = rdr2;
                    dg2.DataBind();
                    con.Close();
                    con.Dispose();
                }
                catch (SystemException e)
                {
                    InsertLog(e.ToString());
                }
            }
        }

        // connects to the database to get unique user ID
        private void GetUserID()
        {
            string user = userName.Text;

            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "select ID from Users where UserName = '"+ user +"'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    rdr2 = cmd.ExecuteReader();

                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                           userID = rdr2.GetInt32(0);
                        }
                    }
                }
                catch (SystemException e)
                {
                    InsertLog(e.ToString());
                }
            }
        }

        // connects to database and gets activity history
        private void GetActivityLog()
        {
            string user = userName.Text;

            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "select Result from Log where UserID = '"+ user +"'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    rdr2 = cmd.ExecuteReader();

                    dg1.DataSource = rdr2;
                    dg1.DataBind();
                    con.Close();
                    con.Dispose();
                }
                catch (SystemException e)
                {
                    InsertLog(e.ToString());
                }
            }
        }

        // inserts messages into the database
        private void SubmitMessage()
        {
            string message = messageTxt.Text;
            string recepient = friendsList.Text;
            string sender = userName.Text;

            using (OleDbConnection con = new OleDbConnection())
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                    con.ConnectionString = constring;
                    con.Open();
                    string query = "INSERT into Messages ([Message], [To], [From], [UserID]) VALUES (@msg, @to, @frm, @uID)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@msg", OleDbType.VarChar).Value = message;
                    cmd.Parameters.AddWithValue("@to", OleDbType.VarChar).Value = recepient;
                    cmd.Parameters.AddWithValue("@frm", OleDbType.VarChar).Value = sender;
                    cmd.Parameters.AddWithValue("@uID", OleDbType.VarChar).Value = userID.ToString();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (SystemException e)
                {
                    InsertLog(e.ToString());
                }
            }
        }

        // on click, sends message
        protected void sendBtn_Click(object sender, EventArgs e)
        {
            GetUserID();
            SubmitMessage();
            string result = "Message sent successfully to " + friendsList.Text + DateTime.Now;
            InsertLog(result);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Message sent successfully!');", true);
        }

        // on click gets activity and displays it to the user
        protected void actBtn_Click(object sender, EventArgs e)
        {
            GetActivityLog();
            activity.Visible = true;
        }

        // on click, gets user conversation history 
        protected void viewBtn_Click(object sender, EventArgs e)
        {
            if (hist.Visible == false)
            {
                hist.Visible = true;
                viewBtn.Text = "Hide Conversation History";
            }
            else
            {
                hist.Visible = false;
                viewBtn.Text = "View Entire Conversation";
            }
            GetMessages();
        }

        // on click, selected user from the list is blocked
        protected void blockBtn_Click(object sender, EventArgs e)
        {
            dg.SelectedRow.Visible = false;
            friendsList.Items.Remove(dg.SelectedRow.Cells[1].Text);
        }
    }
}