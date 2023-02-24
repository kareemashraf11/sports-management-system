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
    public partial class viewStadiumInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand info = new SqlCommand("viewStadiumInfo", conn);
            info.CommandType = System.Data.CommandType.StoredProcedure;
            info.Parameters.Add(new SqlParameter("@manager_username", Session["username"]));

            conn.Open();
            SqlDataReader rdr = info.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            rdr.Read();
            int id = rdr.GetInt32(rdr.GetOrdinal("stadium_id"));
            String name = rdr.GetString(rdr.GetOrdinal("name"));
            String location = rdr.GetString(rdr.GetOrdinal("location"));
            int capacity = rdr.GetInt32(rdr.GetOrdinal("capacity"));
            bool status = rdr.GetBoolean(rdr.GetOrdinal("status"));

            conn.Close();
            sid.Text = id.ToString();
            sname.Text = name;
            slocation.Text = location;
            scapacity.Text = capacity.ToString();
            sstatus.Text = status ? "Available" : "Not Available";
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("stadHome.aspx");
        }
    }
}