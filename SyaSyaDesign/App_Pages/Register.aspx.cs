using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerFormBtn_Click(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var dtrUser = db.Users.Where(m => m.username == TxtRUsername.Text);


            if (dtrUser.Any())
            {
                lblRegisterOk.Text = "Username already exist.";

            }
            else if (TxtRPass.Text == TxtRConfirmPass.Text)
            {

                lblRegisterOk.Text = "";

                // insert
               
                db.Users.Add(new User { username = TxtRUsername.Text, password = TxtRPass.Text, Email = TxtREmail.Text, userType = "Customer" });
                db.SaveChanges();
                
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                string scriptKey = "SuccessMessage";

                javaScript.Append("var userConfirmation = window.confirm('" + "Successfully registerd." + "');\n");
                javaScript.Append("window.location='login.aspx';");

                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);

            }

          

        }

        protected void cvRConfirmPass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TxtRPass.Text != TxtRConfirmPass.Text)
            {
                args.IsValid = false;
            }
        }
    }
}