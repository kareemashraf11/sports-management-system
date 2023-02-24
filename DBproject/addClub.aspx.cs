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
    public partial class addClub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String name = club.Text;
            String loc = location.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(loc))
            {
                Label2.Text = "This field cannot be empty";
                return;
            }

            String output = "";
            conn.Open();
            SqlCommand club_exists = conn.CreateCommand();
            club_exists.CommandText = "SELECT dbo.clubExists(@club_name) AS func";
            club_exists.Parameters.AddWithValue("@club_name", name);
            SqlDataReader rdr = club_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "False")
            {
                SqlCommand addClub = new SqlCommand("addClub", conn);
                addClub.CommandType = System.Data.CommandType.StoredProcedure;
                addClub.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;
                addClub.Parameters.Add(new SqlParameter("@location", System.Data.SqlDbType.VarChar)).Value = loc;

                conn.Open();
                addClub.ExecuteNonQuery();
                conn.Close();

                Label3.Text = "Club has been added successfully!";
                club.Text = "";
                location.Text = "";

            }

            else
            {
                Label3.Text = "This club already exists";
                club.Text = "";
                location.Text = "";
                return;
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }
    }
}