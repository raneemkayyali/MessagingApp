/*  Author: Raneem Kayyali 
 *  Project: Messaging Web Application
 *  Date: 11/08/2018
 *  
 *  Summary: This application was made to allow users to 
 *  communicate back and forth via messaging. This page was 
 *  made to allow users to login to the application.
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessagingApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // page load
        }

        // declare variables
        OleDbDataReader rdr2;
        string user;
        string password;
        bool valid;

        private void AuthenticateLogin()
        {
            // set variables to appropriate values
            password = pwdTxt.Text;
            user = userTxt.Text;

            // connect to database to authenticate user credentials
            using (OleDbConnection con = new OleDbConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                con.ConnectionString = constring;
                con.Open();
                string query = "select UserName, Password from Users where UserName = '" + user + "' and Password = '"+ password +"'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = con;
                rdr2 = cmd.ExecuteReader();
                try
                {
                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                            password= rdr2.GetString(0);
                            user = rdr2.GetString(0);
                            valid = true;
                        }
                    }

                    Application["uName"] = user;
                }
                catch (SystemException e)
                {
                    // catches unexpected errors
                    valid = false;
                    InsertLog(e.ToString());
                }
            }
        }

        // inserts error logs into the database
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
                cmd.Parameters.AddWithValue("@usID", OleDbType.VarChar).Value = user;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        // on click, validates user and redirects them to the main page
        protected void loginBtn_Click(object sender, EventArgs e)
        {
            AuthenticateLogin();

            if (valid == true)
            {
                string result = "Logged in " + DateTime.Now;
                InsertLog(result);
                Response.Redirect("Contact.aspx");
            }
            else
            {
                string result = "Invalid Login Attempt " + DateTime.Now;
                InsertLog(result);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Invalid Login');", true);
            }
        }

        // on click, redirects user to page where they can create a new account
        protected void createBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }
    }
}