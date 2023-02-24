using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class stadHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void info_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewStadiumInfo.aspx");
        }

        protected void requests_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewReceivedRequests.aspx");
        }

        protected void accept_Click(object sender, EventArgs e)
        {
            Response.Redirect("acceptRequest.aspx");
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            Response.Redirect("rejectRequest.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}