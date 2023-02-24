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
    public partial class WebForm2 : System.Web.UI.Page
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
            String stadium = Stadium.Text;

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
            if (String.IsNullOrWhiteSpace(stadium))
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

            String output2 = "";
            conn.Open();
            SqlCommand stadium_exists = conn.CreateCommand();
            stadium_exists.CommandText = "SELECT dbo.stadiumExists(@stadium_name) AS func";
            stadium_exists.Parameters.AddWithValue("@stadium_name", stadium);
            SqlDataReader rdr2 = stadium_exists.ExecuteReader();
            while (rdr2.Read())
                output2 = rdr2["func"].ToString();
            conn.Close();

            String output3 = "";
            conn.Open();
            SqlCommand hasMgr = conn.CreateCommand();
            hasMgr.CommandText = "SELECT dbo.hasManager(@stad_name) AS func";
            hasMgr.Parameters.AddWithValue("@stad_name", stadium);
            SqlDataReader rdr3 = hasMgr.ExecuteReader();
            while (rdr3.Read())
                output3 = rdr3["func"].ToString();
            conn.Close();


            if (output1 == "True" && output2 == "True" && output3 == "False")
            {
                SqlCommand stadRegister = new SqlCommand("addStadiumManager", conn);
                stadRegister.CommandType = System.Data.CommandType.StoredProcedure;
                stadRegister.Parameters.Add(new SqlParameter("@manager_name", System.Data.SqlDbType.VarChar)).Value = name;
                stadRegister.Parameters.Add(new SqlParameter("@stadium_name", System.Data.SqlDbType.VarChar)).Value = stadium;
                stadRegister.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.VarChar)).Value = user;
                stadRegister.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.VarChar)).Value = pass;

                conn.Open();
                stadRegister.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("Login.aspx");
            }

            else if(output1 == "False")
            {
                Label2.Text = "This username already exists";
                return;
            }
            else if(output3 == "True")
            {
                Label4.Text = "This stadium already has a manager";
            }
            else
            {
                Label4.Text = "This stadium doesn't exist";
                return;
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}