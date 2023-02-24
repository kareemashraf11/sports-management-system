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
    public partial class addMatch : System.Web.UI.Page
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
            Label5.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String hostc = host.Text;
            String guestc = guest.Text;
            String startt = start.Text;
            String endt = end.Text;

            if (String.IsNullOrWhiteSpace(hostc))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(guestc))
            {
                Label2.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(startt))
            {
                Label3.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(endt))
            {
                Label4.Text = "This field cannot be empty";
                return;
            }

            if (hostc == guestc)
            {
                Label5.Text = "Please choose different clubs";
            }
            else
            {
                String output1 = "";
                conn.Open();
                SqlCommand club_exists = conn.CreateCommand();
                club_exists.CommandText = "SELECT dbo.clubExists(@club_name) AS func";
                club_exists.Parameters.AddWithValue("@club_name", hostc);
                SqlDataReader rdr1 = club_exists.ExecuteReader();
                while (rdr1.Read())
                    output1 = rdr1["func"].ToString();
                conn.Close();

                String output2 = "";
                conn.Open();
                SqlCommand club_exists2 = conn.CreateCommand();
                club_exists2.CommandText = "SELECT dbo.clubExists(@club_name) AS func";
                club_exists2.Parameters.AddWithValue("@club_name", guestc);
                SqlDataReader rdr2 = club_exists2.ExecuteReader();
                while (rdr2.Read())
                    output2 = rdr2["func"].ToString();
                conn.Close();

                if (output1 == "True" && output2 == "True")
                {
                    SqlCommand addMatch = new SqlCommand("addNewMatch", conn);
                    addMatch.CommandType = System.Data.CommandType.StoredProcedure;
                    addMatch.Parameters.Add(new SqlParameter("@host_name", System.Data.SqlDbType.VarChar)).Value = hostc;
                    addMatch.Parameters.Add(new SqlParameter("@guest_name", System.Data.SqlDbType.VarChar)).Value = guestc;
                    addMatch.Parameters.Add(new SqlParameter("@start", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(startt);
                    addMatch.Parameters.Add(new SqlParameter("@end", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(endt);

                    try
                    {
                        conn.Open();
                        addMatch.ExecuteNonQuery();
                        conn.Close();
                        Label5.Text = "Match has been added successfully!";
                        host.Text = "";
                        guest.Text = "";
                        start.Text = "";
                        end.Text = "";
                    }

                    catch (SqlException ex)
                    {
                        Label5.Text = ex.Message;
                    }
                }

                else if (output1 == "False")
                {
                    Label1.Text = "This club doesn't exist";
                }
                else
                {
                    Label2.Text = "This club doesn't exist";
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("assocHome.aspx");
        }
    }
}