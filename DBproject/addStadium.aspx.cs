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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";
            Label4.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            if (String.IsNullOrWhiteSpace(stadium.Text))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(location.Text))
            {
                Label2.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(capacity.Text))
            {
                Label3.Text = "This field cannot be empty";
                return;
            }
            for (int i = 0; i < capacity.Text.Length; i++)
            {
                if (capacity.Text[i] > '9' || capacity.Text[i] < '0')
                {
                    Label3.Text = "Capacity should consist of digits only";
                    return;
                }
            }



            String name = stadium.Text;
            String loc = location.Text;
            int cap = Int32.Parse(capacity.Text);

            

            String output = "";
            conn.Open();
            SqlCommand stadium_exists = conn.CreateCommand();
            stadium_exists.CommandText = "SELECT dbo.stadiumExists(@stadium_name) AS func";
            stadium_exists.Parameters.AddWithValue("@stadium_name", name);
            SqlDataReader rdr = stadium_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "False")
            {
                SqlCommand addStadium = new SqlCommand("addStadium", conn);
                addStadium.CommandType = System.Data.CommandType.StoredProcedure;
                addStadium.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;
                addStadium.Parameters.Add(new SqlParameter("@location", System.Data.SqlDbType.VarChar)).Value = loc;
                addStadium.Parameters.Add(new SqlParameter("@capacity", System.Data.SqlDbType.Int)).Value = cap;


                conn.Open();
                addStadium.ExecuteNonQuery();
                conn.Close();

                Label4.Text = "Stadium has been added successfully!";
                stadium.Text = "";
                location.Text = "";
                capacity.Text = "";

            }

            else
            {
                Label4.Text = "This club already exists";
                stadium.Text = "";
                location.Text = "";
                capacity.Text = "";
                return;
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }
    }
}