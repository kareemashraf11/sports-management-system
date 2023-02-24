using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void assoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("assocRegister.aspx");
        }

        protected void rep_Click(object sender, EventArgs e)
        {
            Response.Redirect("repRegister.aspx");
        }

        protected void stad_Click(object sender, EventArgs e)
        {
            Response.Redirect("stadRegister.aspx");
        }

        protected void fan_Click(object sender, EventArgs e)
        {
            Response.Redirect("fanRegister.aspx");
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}