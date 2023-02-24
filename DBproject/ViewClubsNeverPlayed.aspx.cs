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
    public partial class ViewClubsNeverPlayed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand never = new SqlCommand("SELECT * FROM clubsNeverMatched", conn);
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[2] {
                new DataColumn("First Club", typeof(string)),
                new DataColumn("Second Club", typeof(string))
            });

            conn.Open();
            SqlDataReader rdr = never.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String first = rdr.GetString(rdr.GetOrdinal("first_club"));
                String second = rdr.GetString(rdr.GetOrdinal("second_club"));
                table.Rows.Add(first,second);
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

            neverPlayed.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("assocHome.aspx");
        }
    }
}