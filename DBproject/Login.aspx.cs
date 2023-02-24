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
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void login(object sender, EventArgs e)
		{
			Label1.Text = " ";
			Label2.Text = " ";
			Label3.Text = " ";

			string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
			SqlConnection conn = new SqlConnection(connStr);

			String user = username.Text;
			String pass = password.Text;

            if (String.IsNullOrWhiteSpace(user))
            {
				Label1.Text = "This field cannot be empty";
				return;
            }
			if (String.IsNullOrWhiteSpace(pass))
			{
				Label2.Text = "This field cannot be empty";
				return;
			}

			SqlCommand login = new SqlCommand("login", conn);
			login.CommandType = System.Data.CommandType.StoredProcedure;
			login.Parameters.Add(new SqlParameter("@username", user));
			login.Parameters.Add(new SqlParameter("@password", pass));

			SqlParameter flag = login.Parameters.Add("@flag", System.Data.SqlDbType.Bit);
			SqlParameter user_type = login.Parameters.Add("@user_type", System.Data.SqlDbType.Int);

			flag.Direction = System.Data.ParameterDirection.Output;
			user_type.Direction = System.Data.ParameterDirection.Output;

			conn.Open();
			login.ExecuteNonQuery();
			conn.Close();

            if(flag.Value.ToString() == "True")
            {
				Session["username"] = user;
				if (user_type.Value.ToString() == "1")
					Response.Redirect("adminHome.aspx");
				else if (user_type.Value.ToString() == "2")
					Response.Redirect("assocHome.aspx");
				else if (user_type.Value.ToString() == "3")
					Response.Redirect("repHome.aspx");
				else if (user_type.Value.ToString() == "4")
					Response.Redirect("stadHome.aspx");
				else if(user_type.Value.ToString() == "5")
					Response.Redirect("fanHome.aspx");

			}

            else
            {
				Label3.Text = "This username or password is invalid";
            }


		}

        protected void back_Click(object sender, EventArgs e)
        {
			Response.Redirect("Main.aspx");
        }
    }
}