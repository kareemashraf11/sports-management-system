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
    public partial class viewClubMatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            conn.Open();
            SqlCommand matches = conn.CreateCommand();
            matches.CommandText = "SELECT * FROM dbo.upcomingMatchesOfClub(@club_name) AS func";
            matches.Parameters.AddWithValue("@club_name", name);
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[4] { new DataColumn("Host", typeof(string)),
                    new DataColumn("Guest", typeof(string)),
                    new DataColumn("Start",typeof(string)),
                    new DataColumn("End",typeof(string))});

            SqlDataReader rdr2 = matches.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String hostc = rdr2.GetString(rdr2.GetOrdinal("host_name"));
                String guestc = rdr2.GetString(rdr2.GetOrdinal("guest_name"));
                String startt = rdr2.GetDateTime(rdr2.GetOrdinal("start_time")).ToString();
                String endtt = rdr2.GetDateTime(rdr2.GetOrdinal("end_time")).ToString();
                int stad = rdr2.GetOrdinal("stadium_name");
                String stadium = "";
                if (rdr2.IsDBNull(stad))
                    stadium = "Not Decided";
                else
                    stadium = rdr2.GetString(stad);
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

            clubMatches.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("repHome.aspx");
        }
    }
}