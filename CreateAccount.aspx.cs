/*  Author: Raneem Kayyali 
 *  Project: Messaging Web Application
 *  Date: 11/08/2018
 *  
 *  Summary: This application was made to allow users to 
 *  communicate back and forth via messaging. This page was 
 *  made to allow new users to create an account.
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
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // page load
        }

        // declare variables
        OleDbDataReader rdr2;
        string user;
        string password;
        bool found;
        bool valid;

        private void ValidateLogin()
        {
            // set values of variables
            password = pwdTxt.Text;
            user = userTxt.Text;

            // validates user credentials by matching them to the values in the database
            using (OleDbConnection con = new OleDbConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                con.ConnectionString = constring;
                con.Open();
                string query = "select UserName, Password from Users where UserName = '" + user + "' and Password = '" + password + "'";
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
                            password = rdr2.GetString(0);
                            user = rdr2.GetString(0);
                            found = true;
                        }
                    }
                }
                catch (SystemException e)
                {
                    found = false;
                    // logs any caught errors
                    InsertLog(e.ToString());
                }
            }
        }

        // checks if user already exists in the database
        private void CheckExists()
        {
            ValidateLogin();

            if (found == true)
            {
                string result = "Attempt to re-create account " + DateTime.Now;
                InsertLog(result);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('You already have an account.');", true);
            }
            else
            {
                found = false;
            }
        }

        // matches password entered with confirm password field
        private void ValidatePassword()
        {
            string confirmPWD = confirmTxt.Text;

            if (confirmPWD != password)
            {
                string result = "Password Mismatch Attempt " + DateTime.Now;
                InsertLog(result);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Passwords do not match.');", true);
            }
            else
            {
                valid = true;
            }
        }

        // inserts logged errors into the database
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

        // inserts new user information into the database
        private void InsertNewUser()
        {
            using (OleDbConnection con = new OleDbConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\offlineDB.accdb;Persist Security Info=True";
                con.ConnectionString = constring;
                con.Open();
                try
                {
                    string query = "INSERT into Users (UserName, [Password]) VALUES (@user, @pwd)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@user", OleDbType.VarChar).Value = user;
                    cmd.Parameters.AddWithValue("@pwd", OleDbType.VarChar).Value = password;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (SystemException e)
                {
                    InsertLog(e.ToString());
                }
            }
        }

        // on click, validates all fields and redirects user back to login page
        protected void createBtn_Click(object sender, EventArgs e)
        {
            CheckExists();
            ValidatePassword();

            if (valid == true)
            {
                InsertNewUser();
                string result = "New account created " + DateTime.Now;
                InsertLog(result);
                Response.Redirect("Default.aspx");
            }
        }
    }
}