using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBProject
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addClub_Click(object sender, EventArgs e)
        {
            Response.Redirect("addClub.aspx");
        }

        protected void deleteClub_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteClub.aspx");
        }

        protected void addStadium_Click(object sender, EventArgs e)
        {
            Response.Redirect("addStadium.aspx");
        }

        protected void deleteStadium_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteStadium.aspx");
        }

        protected void blockFan_Click(object sender, EventArgs e)
        {
            Response.Redirect("blockFan.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}