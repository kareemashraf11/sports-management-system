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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";
            Label4.Text = " ";
            Label5.Text = " ";
            Label6.Text = " ";
            Label7.Text = " ";

            string connStr = WebConfigurationManager.ConnectionStrings["myConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String name = Name.Text;
            String user = Username.Text;
            String pass = Password.Text;
            String id = NationalID.Text;
            String phone = Phone.Text;
            String date = Date.Text;
            String address = Address.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                Label1.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(user))
            {
                Label2.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(pass))
            {
                Label3.Text = "This field cannot be empty";
                return;
            }
            if (String.IsNullOrWhiteSpace(id))
            {
                Label4.Text = "This field cannot be empty";
                return;
            }
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i] > '9' || id[i] < '0')
                {
                    Label4.Text = "National ID should consist of digits only";
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(phone))
            {
                Label5.Text = "This field cannot be empty";
                return;
            }
            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] > '9' || phone[i] < '0')
                {
                    Label5.Text = "Phone Number should consist of digits only";
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(date))
            {
                Label6.Text = "Please select a date";
                return;
            }
            if (String.IsNullOrWhiteSpace(address))
            {
                Label7.Text = "This field cannot be empty";
                return;
            }

            
            


            String output = "";
            conn.Open();
            SqlCommand user_exists = conn.CreateCommand();
            user_exists.CommandText = "SELECT dbo.userExists(@username) AS func";
            user_exists.Parameters.AddWithValue("@username", user);
            SqlDataReader rdr = user_exists.ExecuteReader();
            while (rdr.Read())
                output = rdr["func"].ToString();
            conn.Close();

            if (output == "True")
            {
                String output2 = "";
                conn.Open();
                SqlCommand fan_exists = conn.CreateCommand();
                fan_exists.CommandText = "SELECT dbo.fanExists(@id) AS func";
                fan_exists.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr2 = fan_exists.ExecuteReader();
                while (rdr2.Read())
                    output2 = rdr2["func"].ToString();
                conn.Close();

                if (output2 == "False")
                {
                    SqlCommand fanRegister = new SqlCommand("addFan", conn);
                    fanRegister.CommandType = System.Data.CommandType.StoredProcedure;
                    fanRegister.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar)).Value = name;
                    fanRegister.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.VarChar)).Value = user;
                    fanRegister.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.VarChar)).Value = pass;
                    fanRegister.Parameters.Add(new SqlParameter("@national_id", System.Data.SqlDbType.VarChar)).Value = id;
                    fanRegister.Parameters.Add(new SqlParameter("@birth_date", System.Data.SqlDbType.DateTime)).Value = DateTime.Parse(date);
                    fanRegister.Parameters.Add(new SqlParameter("@address", System.Data.SqlDbType.VarChar)).Value = address;
                    fanRegister.Parameters.Add(new SqlParameter("@phone", System.Data.SqlDbType.Int)).Value = phone;
                    conn.Open();
                    fanRegister.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Label4.Text = "This national ID already exists";
                }

            }

            else
            {
                Label2.Text = "This username already exists";
                return;
            }
            
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}