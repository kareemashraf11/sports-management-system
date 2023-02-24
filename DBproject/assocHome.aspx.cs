using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class assocHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addMatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("addMatch.aspx");
        }

        protected void deleteMatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteMatch.aspx");
        }

        protected void viewUpcoming_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewUpcomingMatches.aspx");
        }

        protected void viewPlayed_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPlayedMatches.aspx");
        }

        protected void viewNeverPlayed_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewClubsNeverPlayed.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}