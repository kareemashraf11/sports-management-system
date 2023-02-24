using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";
            Label4.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String name = Name.Text;
            String user = Username.Text;
            String pass = Password.Text;
            String club = Club.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(user))
            {
                Label2.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(pass))
            {
                Label3.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(club))
            {
                Label4.Text = "This field cannot be empty";
                return;
            }

            String output1 = "";
            conn.Open();
            SqlCommand user_exists = conn.CreateCommand();
            user_exists.CommandText = "SELECT dbo.userExists(@username) AS func";
            user_exists.Parameters.AddWithValue("@username", user);
            SqlDataReader rdr = user_exists.ExecuteReader();
            while (rdr.Read())
                output1 = rdr["func"].ToString();
            conn.Close();

            String output2 ="";
            conn.Open();
            SqlCommand club_exists = conn.CreateCommand();
            club_exists.CommandText = "SELECT dbo.clubExists(@club_name) AS func";
            club_exists.Parameters.AddWithValue("@club_name", club);
            SqlDataReader rdr2 = club_exists.ExecuteReader();
            while (rdr2.Read())
                output2 = rdr2["func"].ToString();
            conn.Close();

            String output3 = "";
            conn.Open();
            SqlCommand hasRep = conn.CreateCommand();
            hasRep.CommandText = "SELECT dbo.hasRep(@club_name) AS func";
            hasRep.Parameters.AddWithValue("@club_name", club);
            SqlDataReader rdr3 = hasRep.ExecuteReader();
            while (rdr3.Read())
                output3 = rdr3["func"].ToString();
            conn.Close();


            if (output1 == "True" && output2 == "True" && output3 == "False")
            {
                SqlCommand repRegister = new SqlCommand("addRepresentative", conn);
                repRegister.CommandType = System.Data.CommandType.StoredProcedure;
                repRegister.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;
                repRegister.Parameters.Add(new SqlParameter("@club_name", System.Data.SqlDbType.VarChar)).Value = club;
                repRegister.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.VarChar)).Value = user;
                repRegister.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.VarChar)).Value = pass;

                conn.Open();
                repRegister.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("Login.aspx");
            }

            else if (output1 == "False")
            {
                Label2.Text = "This username already exists";
                return;
            }
            else if(output3 == "True")
            {
                Label4.Text = "This club already has a representative";
            }

            else
            {
                Label4.Text = "This club doesn't exist";
                return;
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}