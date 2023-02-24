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
    public partial class purchaseTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void purchase_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";
            Label4.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String hostc = host.Text;
            String guestc = guest.Text;
            String startt = start.Text;

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
                Label3.Text = "Please pick a date";
                return;
            }

            String output = "";
            conn.Open();
            SqlCommand ticket_exists = conn.CreateCommand();
            ticket_exists.CommandText = "SELECT dbo.ticketExists(@host,@guest,@start) AS func";
            ticket_exists.Parameters.AddWithValue("@host", hostc);
            ticket_exists.Parameters.AddWithValue("@guest", guestc);
            ticket_exists.Parameters.AddWithValue("@start", DateTime.Parse(startt));

            SqlDataReader rdr = ticket_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "True")
            {
                SqlCommand info = new SqlCommand("viewFanInfo", conn);
                info.CommandType = System.Data.CommandType.StoredProcedure;
                info.Parameters.Add(new SqlParameter("@fan_username", Session["username"]));

                conn.Open();
                SqlDataReader rdr2 = info.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                rdr2.Read();
                String id = rdr2.GetString(rdr2.GetOrdinal("national_id"));
                conn.Close();

                SqlCommand purchaseTicket = new SqlCommand("purchaseTicket", conn);
                purchaseTicket.CommandType = System.Data.CommandType.StoredProcedure;
                purchaseTicket.Parameters.Add(new SqlParameter("@national_id", System.Data.SqlDbType.VarChar)).Value = id;
                purchaseTicket.Parameters.Add(new SqlParameter("@host_name", System.Data.SqlDbType.VarChar)).Value = hostc;
                purchaseTicket.Parameters.Add(new SqlParameter("@guest_name", System.Data.SqlDbType.VarChar)).Value = guestc;
                purchaseTicket.Parameters.Add(new SqlParameter("@start", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(startt);


                conn.Open();
                purchaseTicket.ExecuteNonQuery();
                conn.Close();

                Label4.Text = "Ticket has been purchased successfully!";
            }
            else
            {
                Label4.Text = "No tickets are available for this match";
            }

        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("fanHome.aspx");
        }
    }
}