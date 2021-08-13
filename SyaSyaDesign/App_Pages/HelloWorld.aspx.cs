using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class HelloWorld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_add_user_Click(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var newUser = new User
            {
                username = txtNewUsername.Text,
                isAdmin = true,
                password = "sapnu puas"
            };
            db.Users.Add(newUser);  //use db.{tableName}.Add() to insert new record
            db.SaveChanges();       //must save changes to update database
            Response.Redirect("~/App_Pages/HelloWorld.aspx");
        }
    }
}