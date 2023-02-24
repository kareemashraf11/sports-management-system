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
    public partial class deleteClub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void delete_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String name = club.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                Label1.Text = "This field cannot be empty";
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

            if (output == "True")
            {
                SqlCommand deleteClub = new SqlCommand("deleteClub", conn);
                deleteClub.CommandType = System.Data.CommandType.StoredProcedure;
                deleteClub.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;

                conn.Open();
                deleteClub.ExecuteNonQuery();
                conn.Close();

                Label2.Text = "Club has been deleted successfully!";
                club.Text = "";

            }

            else
            {
                Label2.Text = "This club doesn't exist";
                club.Text = "";
                return;
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }
    }
}