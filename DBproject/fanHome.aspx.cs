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
    public partial class fanHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewAvailable_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewAvailableMatchesToBook.aspx");

        }

        protected void purchase_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand info = new SqlCommand("viewFanInfo", conn);
            info.CommandType = System.Data.CommandType.StoredProcedure;
            info.Parameters.Add(new SqlParameter("@fan_username", Session["username"]));

            conn.Open();
            SqlDataReader rdr = info.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            rdr.Read();
            if (rdr.GetBoolean(rdr.GetOrdinal("status")))
                Response.Redirect("purchaseTicket.aspx");
            else
                Label1.Text = "You are currently blocked";

            conn.Close();

        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}