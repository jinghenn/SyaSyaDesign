using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace SyaSyaDesign.App_Pages
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        static String validationCode;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void forgetPasssFormBtn_Click(object sender, EventArgs e)
        {
            String txtvalidation = TxtRValidationCode.Text;

            if (txtvalidation.CompareTo(validationCode) == 0)
            {
                var db = new syasyadbEntities();
                var dtrUser = db.Users.Where(m => m.username == TxtRUsername.Text);

                if (dtrUser.Any())
                {
                    var user = db.Users.FirstOrDefault(m => m.username == TxtRUsername.Text);

                    if (user != null)
                    {
                        user.password = TxtRPass.Text;
                        db.SaveChanges();
                    }



                }

                validationCode = "";
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                string scriptKey = "SuccessMessage";
                javaScript.Append("var userConfirmation = window.confirm('" + "Successfully reset password." + "');\n");
                javaScript.Append("window.location='login.aspx';");

                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            }
            else
            {
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                string scriptKey = "ErrorMessage";

                javaScript.Append("var userConfirmation = window.confirm('" + "Wrong validation code." + "');\n");

                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            }

        }

        protected void getVerificationCode_Click(object sender, EventArgs e)
        {
            String email;
            var db = new syasyadbEntities();
            var dtrUser = db.Users.Where(m => m.username == TxtRUsername.Text);

               

            if (dtrUser.Any() && TxtRPass.Text == TxtRConfirmPass.Text && TxtRUsername.Text != "")
            {

                var user = db.Users.FirstOrDefault(m => m.username == TxtRUsername.Text);
                email = Convert.ToString(user.Email);
                
                Random rnd = new Random();
                int v = rnd.Next(1000, 9999);
                validationCode = v.ToString();

                string mailbody = "Your validation code to reset password is " + validationCode;

                try
                {
                    new Email().SendEmail(email, mailbody, "Validation Code");
                    getCodeMsg.Text = "A validation code is successfully sent to your email, please check.";
                }

                catch (Exception ex)
                {
                    throw ex;
                }


            }
            else if (!dtrUser.Any())
            {
               
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                string scriptKey = "ErrorMessage";

                javaScript.Append("var userConfirmation = window.confirm('" + "Username does not exist." + "');\n");

                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            }

            //else
            //  {
            //      System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            //      string scriptKey = "ErrorMessage";
            //
            //    javaScript.Append("var userConfirmation = window.confirm('" + "Confirm password does not match." + "');\n");
            //
            //  ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            //}
        }

        protected void conPassValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TxtRPass.Text != TxtRConfirmPass.Text)
            {
                args.IsValid = false;
            }
        }
    }
}