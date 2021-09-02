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
            if (Session["username"] != null)
            {
                Response.Redirect("~/Product.aspx");
            }
            Page.SetFocus(TxtLUsername);
        }

        protected void loginFormBtn_Click(object sender, EventArgs e)
        {
            String strLoginCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
            SqlConnection loginCon = new SqlConnection(strLoginCon);

            loginCon.Open();        // Open connection to database

            String strSelectUser = "Select * from User where username=@username and password=@password";

            SqlCommand cmdSelectUser = new SqlCommand(strSelectUser, loginCon);
            cmdSelectUser.Parameters.AddWithValue("@username", TxtLUsername.Text);
            cmdSelectUser.Parameters.AddWithValue("@password", TxtLPass.Text);
            SqlDataReader dtrUser = cmdSelectUser.ExecuteReader();

            if (dtrUser.HasRows)
            {

                SqlCommand cmdGetUsername = new SqlCommand("Select username from User where username=@username and password=@password", loginCon);
                cmdGetUsername.Parameters.AddWithValue("@username", TxtLUsername.Text);
                cmdGetUsername.Parameters.AddWithValue("@password", TxtLPass.Text);
                String username = Convert.ToString(cmdGetUsername.ExecuteScalar());

                SqlCommand cmdGetPass = new SqlCommand("Select password from User where username=@username and password=@password", loginCon);
                cmdGetPass.Parameters.AddWithValue("@username", TxtLUsername.Text);
                cmdGetPass.Parameters.AddWithValue("@password", TxtLPass.Text);
                String password = Convert.ToString(cmdGetPass.ExecuteScalar());

                if (username.CompareTo(TxtLUsername.Text) == 0 && password.CompareTo(TxtLPass.Text) == 0)
                {
                    String strSelectUserID = "Select user_id from [dbo].[User] where username=@username";
                    SqlCommand cmdSelectUserID = new SqlCommand(strSelectUserID, loginCon);
                    cmdSelectUserID.Parameters.AddWithValue("@username", TxtLUsername.Text);
                    Session["username"] = TxtLUsername.Text;
                    Session["user_id"] = cmdSelectUserID.ExecuteScalar().ToString();
//                    Response.Redirect("Product.aspx");
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
            loginCon.Close();

        }
    }
}
    
