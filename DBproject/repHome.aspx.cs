using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class repHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void info_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewClubInfo.aspx");
        }

        protected void matches_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewClubMatches.aspx");
        }

        protected void stadiums_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewAvailableStadiums.aspx");
        }

        protected void request_Click(object sender, EventArgs e)
        {
            Response.Redirect("sendRequest.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}