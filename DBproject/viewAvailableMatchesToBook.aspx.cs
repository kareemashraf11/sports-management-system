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
    public partial class viewAvailableMatchesToBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void show_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String date = inDate.Text;

            if (String.IsNullOrWhiteSpace(date))
            {
                Label1.Text = "Please pick a date";
                return;
            }

            conn.Open();
            SqlCommand matches = conn.CreateCommand();
            matches.CommandText = "SELECT * FROM dbo.availableMatchesToAttend(@date) AS func";
            matches.Parameters.AddWithValue("@date", DateTime.Parse(date));
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[4] { new DataColumn("Host", typeof(string)),
                    new DataColumn("Guest", typeof(string)),
                    new DataColumn("Stadium",typeof(string)),
                    new DataColumn("Location",typeof(string))});

            SqlDataReader rdr = matches.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String hostc = rdr.GetString(rdr.GetOrdinal("host"));
                String guestc = rdr.GetString(rdr.GetOrdinal("guest"));
                String stadiumn = rdr.GetString(rdr.GetOrdinal("stadium_name"));
                String stadiuml = rdr.GetString(rdr.GetOrdinal("stadium_location"));
                table.Rows.Add(hostc, guestc, stadiumn, stadiuml);
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

            availableMatches.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("fanHome.aspx");
        }
    }
}