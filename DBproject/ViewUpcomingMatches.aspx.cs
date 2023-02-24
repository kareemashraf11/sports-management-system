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
    public partial class ViewUpcomingMatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand matches = new SqlCommand("SELECT * FROM allMatches WHERE start_time > @time", conn);
            matches.Parameters.Add("@time", SqlDbType.DateTime).Value = DateTime.Now;
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[4] { new DataColumn("Host", typeof(string)),
                    new DataColumn("Guest", typeof(string)),
                    new DataColumn("Start",typeof(string)),
                    new DataColumn("End",typeof(string))});

            conn.Open();
            SqlDataReader rdr = matches.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String hostc = rdr.GetString(rdr.GetOrdinal("host_name"));
                String guestc = rdr.GetString(rdr.GetOrdinal("guest_name"));
                String startt = rdr.GetDateTime(rdr.GetOrdinal("start_time")).ToString();
                String endtt = rdr.GetDateTime(rdr.GetOrdinal("end_time")).ToString();
                table.Rows.Add(hostc, guestc, startt, endtt);
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

            upcMatches.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("assocHome.aspx");
        }
    }
}