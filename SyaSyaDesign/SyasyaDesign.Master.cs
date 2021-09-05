using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Session["user_id"] != null)
            {
                userProfile.InnerHtml = Session["username"].ToString();
                loginNavLink.Attributes.Add("style", "display:none");
                userProfile.Attributes.Add("style", "display:block");

            }
            else
            {
                loginNavLink.Attributes.Add("style", "display:block");
                userProfile.Attributes.Add("style", "display:none");
            }

            if(Session["userType"] != null && Session["userType"].ToString() == "Manager")
            {
                manageAdminLink.Visible = true;
                manageProductLink.Visible = true;
            }

        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Session.Remove("user_id");
            Session.Remove("userType");
            Response.Redirect("~/Product.aspx");
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();
            var db = new syasyadbEntities();
            var user = db.Users.FirstOrDefault(m => m.username == username);

            db.Users.Remove(user);
            db.SaveChanges();

            Session.Remove("username");
            Session.Remove("user_id");
            Session.Remove("userType");

            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            string scriptKey = "SuccessMessage";
            string url = "Product.aspx";

            javaScript.Append("var userConfirmation = window.confirm('" + "Successfully deleted');\n");
            javaScript.Append("window.location='" + url + "';");

            Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
        }
    }
}