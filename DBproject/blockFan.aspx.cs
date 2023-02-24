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
    public partial class blockFan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void block_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String nationalid = id.Text;

            if (String.IsNullOrWhiteSpace(nationalid))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }

            for (int i = 0; i < nationalid.Length; i++)
            {
                if (nationalid[i] > '9' || nationalid[i] < '0')
                {
                    Label1.Text = "National ID should consist of digits only";
                    return;
                }
            }

            String output = "";
            conn.Open();
            SqlCommand fan_exists = conn.CreateCommand();
            fan_exists.CommandText = "SELECT dbo.fanExists(@id) AS func";
            fan_exists.Parameters.AddWithValue("@id", nationalid);
            SqlDataReader rdr = fan_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "True")
            {
                String output2 = "";
                conn.Open();
                SqlCommand fanStatus = conn.CreateCommand();
                fanStatus.CommandText = "SELECT dbo.fanStatus(@id) AS func";
                fanStatus.Parameters.AddWithValue("@id", nationalid);
                SqlDataReader rdr2 = fanStatus.ExecuteReader();
                while (rdr2.Read())
                    output2 = rdr2["func"].ToString();
                conn.Close();

                if (output2 == "True")
                {
                    SqlCommand blockFan = new SqlCommand("blockFan", conn);
                    blockFan.CommandType = System.Data.CommandType.StoredProcedure;
                    blockFan.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.VarChar)).Value = nationalid;

                    conn.Open();
                    blockFan.ExecuteNonQuery();
                    conn.Close();

                    Label2.Text = "Fan has been blocked successfully!";
                    id.Text = "";
                }

                else
                {
                    Label2.Text = "This fan is already blocked!";
                }
            }

            else
            {
                Label1.Text = "This fan doesn't exist";
                id.Text = "";
                return;
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }
    }
}