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
    public partial class acceptRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void accept_Click(object sender, EventArgs e)
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
            SqlCommand request_exists = conn.CreateCommand();
            request_exists.CommandText = "SELECT dbo.requestExists(@manager_username,@host_name,@guest_name,@start_time) AS func";
            request_exists.Parameters.AddWithValue("@manager_username", Session["username"]);
            request_exists.Parameters.AddWithValue("@host_name", hostc);
            request_exists.Parameters.AddWithValue("@guest_name", guestc);
            request_exists.Parameters.AddWithValue("@start_time", DateTime.Parse(startt));

            SqlDataReader rdr = request_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if(output == "True")
            {
                SqlCommand acceptRequest = new SqlCommand("acceptRequest", conn);
                acceptRequest.CommandType = System.Data.CommandType.StoredProcedure;
                acceptRequest.Parameters.Add(new SqlParameter("@manager_username", System.Data.SqlDbType.VarChar)).Value = Session["username"];
                acceptRequest.Parameters.Add(new SqlParameter("@host_name", System.Data.SqlDbType.VarChar)).Value = hostc;
                acceptRequest.Parameters.Add(new SqlParameter("@guest_name", System.Data.SqlDbType.VarChar)).Value = guestc;
                acceptRequest.Parameters.Add(new SqlParameter("@start", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(startt);

                try
                {
                    conn.Open();
                    acceptRequest.ExecuteNonQuery();
                    conn.Close();
                    Label4.Text = "Request has been accepted successfully!";
                }
                catch(SqlException ex)
                {
                    Label4.Text = ex.Message;
                }
            }
            else
            {
                Label4.Text = "Request doesn't exist";
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("stadHome.aspx");
        }
    }
}