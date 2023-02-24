using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class viewReceivedRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();
            SqlCommand req = conn.CreateCommand();
            req.CommandText = "SELECT * FROM dbo.allRequestsPerManager(@manager_username) AS func";
            req.Parameters.AddWithValue("@manager_username", Session["username"]);
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[6] { new DataColumn("Club Representative", typeof(string)),
                    new DataColumn("Host", typeof(string)),
                    new DataColumn("Guest", typeof(string)),
                    new DataColumn("Start",typeof(string)),
                    new DataColumn("End",typeof(string)),
                    new DataColumn("Status", typeof(string))});

            SqlDataReader rdr = req.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String rep = rdr.GetString(rdr.GetOrdinal("rep_name"));
                String hostc = rdr.GetString(rdr.GetOrdinal("host_name"));
                String guestc = rdr.GetString(rdr.GetOrdinal("guest_name"));
                String startt = rdr.GetDateTime(rdr.GetOrdinal("start_time")).ToString();
                String endtt = rdr.GetDateTime(rdr.GetOrdinal("end_time")).ToString();
                int stat = rdr.GetOrdinal("status");
                String status = "";
                if (rdr.IsDBNull(stat))
                    status = "Pending";
                else
                    status = rdr.GetBoolean(stat) ? "Accepted" : "Rejected";
                table.Rows.Add(rep,hostc, guestc, startt, endtt, status);

            }
            conn.Close();

            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

            sb.Append("<tr>");
            foreach (DataColumn col in table.Columns)
            {
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + col.ColumnName + "</th>");
            }
            sb.Append("</tr>");


            foreach (DataRow row in table.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn col in table.Columns)
                {
                    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[col.ColumnName].ToString() + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            requests.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("stadHome.aspx");
        }
    }
}