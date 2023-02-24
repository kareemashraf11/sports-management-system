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
    public partial class sendRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void send_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand info = new SqlCommand("viewClubInfo", conn);
            info.CommandType = System.Data.CommandType.StoredProcedure;
            info.Parameters.Add(new SqlParameter("@rep_username", Session["username"]));

            conn.Open();
            SqlDataReader rdr = info.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            rdr.Read();
            String name = rdr.GetString(rdr.GetOrdinal("name"));
            conn.Close();

            String sname = inName.Text;
            String date = inDate.Text;

            if (String.IsNullOrWhiteSpace(sname))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(date))
            {
                Label2.Text = "Please pick a date";
                return;
            }

            String output = "";
            conn.Open();
            SqlCommand stadium_exists = conn.CreateCommand();
            stadium_exists.CommandText = "SELECT dbo.stadiumExists(@stadium_name) AS func";
            stadium_exists.Parameters.AddWithValue("@stadium_name", sname);
            SqlDataReader rdr2 = stadium_exists.ExecuteReader();
            while (rdr2.Read())
                output = rdr2["func"].ToString();
            conn.Close();

            String output2 = "";
            conn.Open();
            SqlCommand match_exists = conn.CreateCommand();
            match_exists.CommandText = "SELECT dbo.matchExists2(@host,@start) AS func";
            match_exists.Parameters.AddWithValue("@host", name);
            match_exists.Parameters.AddWithValue("@start", DateTime.Parse(date));
            SqlDataReader rdr3 = match_exists.ExecuteReader();
            while (rdr3.Read())
                output2 = rdr3["func"].ToString();
            conn.Close();

            if(output == "True" && output2 == "True")
            {
                SqlCommand sendRequest = new SqlCommand("addHostRequest", conn);
                sendRequest.CommandType = System.Data.CommandType.StoredProcedure;
                sendRequest.Parameters.Add(new SqlParameter("@club_name", System.Data.SqlDbType.VarChar)).Value = name;
                sendRequest.Parameters.Add(new SqlParameter("@stadium_name", System.Data.SqlDbType.VarChar)).Value = sname;
                sendRequest.Parameters.Add(new SqlParameter("@start", System.Data.SqlDbType.DateTime)).Value = date;

                conn.Open();
                sendRequest.ExecuteNonQuery();
                conn.Close();
                Label3.Text = "Request sent successfully!";

            }
            else if(output == "False")
            {
                Label1.Text = "This stadium doesn't exist";
            }
            else
            {
                Label2.Text = "There are no scheduled matches on this date for your club";
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("repHome.aspx");
        }
    }
}