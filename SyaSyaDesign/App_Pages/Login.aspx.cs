using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SyaSyaDesign.App_Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void loginFormBtn_Click(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var dtrUser = db.Users.Where(m => m.username == TxtLUsername.Text).Where(m => m.password == TxtLPass.Text);

            if (dtrUser.Any())
            {
                var user = db.Users.FirstOrDefault(m => m.username == TxtLUsername.Text && m.password == TxtLPass.Text);
                String username = Convert.ToString(user.username);
                String password = Convert.ToString(user.password);
                String user_id = Convert.ToString(user.user_id);
                String userType = Convert.ToString(user.userType);

                if (username.CompareTo(TxtLUsername.Text) == 0 && password.CompareTo(TxtLPass.Text) == 0)
                {

                    Session["username"] = TxtLUsername.Text;
                    Session["user_id"] = user_id;
                    Session["userType"] = userType;
                    Response.Redirect("~/Product.aspx");
                }
                else
                {
                    lblLoginFail.Text = "Invalid Username or Password";
                }
            }
            else
            {
                lblLoginFail.Text = "Invalid Username or Password";
            }


        }
    }
}
    
