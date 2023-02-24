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
    public partial class assocRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String name = Name.Text;
            String user = Username.Text;
            String pass = Password.Text;
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

            String output = "";
            conn.Open();
            SqlCommand user_exists = conn.CreateCommand();
            user_exists.CommandText = "SELECT dbo.userExists(@username) AS func";
            user_exists.Parameters.AddWithValue("@username", user);
            SqlDataReader rdr = user_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();


            if (output == "True")
            {
                SqlCommand assocRegister = new SqlCommand("addAssociationManager", conn);
                assocRegister.CommandType = System.Data.CommandType.StoredProcedure;
                assocRegister.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;
                assocRegister.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.VarChar)).Value = user;
                assocRegister.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.VarChar)).Value = pass;

                conn.Open();
                assocRegister.ExecuteNonQuery();
                conn.Close();



                Response.Redirect("Login.aspx");
            }

            else
            {
                Label2.Text = "This username already exists";
                return;
            }




        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}