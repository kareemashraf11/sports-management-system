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
    public partial class viewAvailableStadiums : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void show_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";

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

            String date = inDate.Text;

            if (String.IsNullOrWhiteSpace(date))
            {
                Label1.Text = "Please pick a date";
                return;
            }

            conn.Open();
            SqlCommand stadiums = conn.CreateCommand();
            stadiums.CommandText = "SELECT * FROM dbo.viewAvailableStadiumsOn(@date) AS func";
            stadiums.Parameters.AddWithValue("@date", DateTime.Parse(date));
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[3] { new DataColumn("Name", typeof(string)),
                    new DataColumn("Location", typeof(string)),
                    new DataColumn("Capacity",typeof(int)),});

            SqlDataReader rdr2 = stadiums.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String sname = rdr2.GetString(rdr2.GetOrdinal("name"));
                String slocation = rdr2.GetString(rdr2.GetOrdinal("location"));
                int scapacity = rdr2.GetInt32(rdr2.GetOrdinal("capacity"));
                table.Rows.Add(sname, slocation, scapacity);
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

            stadiumsl.Text = sb.ToString();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("repHome.aspx");
        }
    }
}