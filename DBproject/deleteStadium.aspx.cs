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
    public partial class deleteStadium : System.Web.UI.Page
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

            String name = stadium.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }

            String output = "";
            conn.Open();
            SqlCommand stadium_exists = conn.CreateCommand();
            stadium_exists.CommandText = "SELECT dbo.stadiumExists(@stadium_name) AS func";
            stadium_exists.Parameters.AddWithValue("@stadium_name", name);
            SqlDataReader rdr = stadium_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "True")
            {
                SqlCommand deleteStadium = new SqlCommand("deleteStadium", conn);
                deleteStadium.CommandType = System.Data.CommandType.StoredProcedure;
                deleteStadium.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;

                conn.Open();
                deleteStadium.ExecuteNonQuery();
                conn.Close();

                Label2.Text = "Stadium has been deleted successfully!";
                stadium.Text = "";

            }

            else
            {
                Label2.Text = "This stadium doesn't exist";
                stadium.Text = "";
                return;
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }
    }
}