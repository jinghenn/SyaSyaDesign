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

            ////how to select all user 
            //var users = db.Users;

            ////use select() to transform the data structure. In this case, the User list is transformed to a list of username
            ////Select() requires a lambda parameter. can replace p with anything (eg, x,y,z,abc,def,...)
            //var usernames = db.Users.Select(p => p.username);

            ////to select a user with id 1001
            //var user = db.Users.FirstOrDefault(u => u.user_id == 1001);

            ////to select admin user
            //var admins = db.Users.Where(u => u.isAdmin);

            ////to remove a user
            //var user_1 = db.Users.FirstOrDefault(u => u.user_id == 1001);
            //db.Users.Remove(user_1);
            //db.SaveChanges();

            ////to bind data into repeater
            //var userList = db.Users.ToList(); //ToList() is required
            
            //rpt_user.DataSource = userList;
            //rpt_user.Databind();
        }
    }
}