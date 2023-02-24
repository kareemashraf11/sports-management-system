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
    public partial class deleteMatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         //   start.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
         //   end.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

        }

        protected void delete_Click(object sender, EventArgs e)
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

                    String output = "";
                    conn.Open();
                    SqlCommand match_exists = conn.CreateCommand();
                    match_exists.CommandText = "SELECT dbo.matchExists(@host,@guest,@start,@end) AS func";
                    match_exists.Parameters.AddWithValue("@host", hostc);
                    match_exists.Parameters.AddWithValue("@guest", guestc);
                    match_exists.Parameters.AddWithValue("@start", DateTime.Parse(startt));
                    match_exists.Parameters.AddWithValue("@end", DateTime.Parse(endt));
                    SqlDataReader rdr = match_exists.ExecuteReader();
                    while (rdr.Read())
                        output = rdr["func"].ToString();
                    conn.Close();

                    if (output == "True")
                    {
                        SqlCommand deleteMatch = new SqlCommand("deleteMatch", conn);
                        deleteMatch.CommandType = System.Data.CommandType.StoredProcedure;
                        deleteMatch.Parameters.Add(new SqlParameter("@host_name", System.Data.SqlDbType.VarChar)).Value = hostc;
                        deleteMatch.Parameters.Add(new SqlParameter("@guest_name", System.Data.SqlDbType.VarChar)).Value = guestc;
                        deleteMatch.Parameters.Add(new SqlParameter("@start", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(startt);
                        deleteMatch.Parameters.Add(new SqlParameter("@end", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(endt);

                        conn.Open();
                        deleteMatch.ExecuteNonQuery();
                        conn.Close();

                        Label5.Text = "Match has been deleted successfully!";
                        host.Text = "";
                        guest.Text = "";
                        start.Text = "";
                        end.Text = "";
                    }

                    else
                    {
                        Label5.Text = "This match doesn't exist";

                        host.Text = "";
                        guest.Text = "";
                        start.Text = "";
                        end.Text = "";
                        return;
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