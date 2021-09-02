using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class SyasyaDesign : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Session["user_id"] != null)
            {
                userProfileLink.InnerHtml = Session["username"].ToString();
                loginNavLink.Attributes.Add("style", "display:none");
                userProfileLink.Attributes.Add("style", "display:block");

            }
            else
            {
                loginNavLink.Attributes.Add("style", "display:block");
                userProfileLink.Attributes.Add("style", "display:none");
            }

        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Session.Remove("user_id");
            Response.Redirect("~/Product.aspx");
        }
    }
}