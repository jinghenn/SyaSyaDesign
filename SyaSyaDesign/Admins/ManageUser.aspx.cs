using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Admins
{
    public partial class ManageUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userNo"] = "0";
            if (!Page.IsPostBack)
            {
                var db = new syasyadbEntities();
                var user = db.Users.Where(m => m.userType == "Admin"|| m.userType == "Manager").OrderBy(m => m.username);
                rptUserList.DataSource = user.ToList();
                rptUserList.DataBind();

                
            }
            
        }

        protected void rptUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            // determines which position in the outer layer repeater in the repeater (AlternatingItemTemplate, FooterTemplate,

            //HeaderTemplate，，ItemTemplate，SeparatorTemplate）
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlCat = (DropDownList)e.Item.FindControl("ddlCat");
                Label lblNo = (Label)e.Item.FindControl("lblNo");
                string userType = DataBinder.Eval(e.Item.DataItem, "userType").ToString();

                int userNo = Convert.ToInt32(Session["userNo"].ToString()) + 1;
                Session["userNo"] = userNo;
                lblNo.Text = Convert.ToString(userNo);

                ddlCat.SelectedValue = userType;

            }
        }

        public void successMsg(string msg)
        {
            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            string scriptKey = "SuccessMessage";
            string url = "ManageUser.aspx";

            javaScript.Append("var userConfirmation = window.confirm('" + "Successfully " + msg + "');\n");
            javaScript.Append("window.location='" + url + "';");

            ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
        }

        protected void rptUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string username = e.CommandArgument.ToString();         
            TextBox txtUsername = (TextBox)e.Item.FindControl("txtUsername");
            TextBox txtEmail = (TextBox)e.Item.FindControl("txtEmail");

            Label lblUserType = (Label)e.Item.FindControl("lblUserType");
            DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCat");

            LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
            LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");

            var db = new syasyadbEntities();

            if (e.CommandName == "edit")
            {
                ddlCategory.Visible = true;
                lblUserType.Visible = false;

                txtUsername.Enabled = true;
                txtUsername.BorderStyle = BorderStyle.Inset;
                txtUsername.BackColor = Color.White;

                txtEmail.Enabled = true;
                txtEmail.BorderStyle = BorderStyle.Inset;
                txtEmail.BackColor = Color.White;

                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnDelete.Visible = true;
            }
            if (e.CommandName == "delete")
            {
                var dtrUser = db.Users.Where(m => m.userType == "Manager");
                
                if (dtrUser.Count()>1)
                {
                    var user = db.Users.FirstOrDefault(m => m.username == username);
                    db.Users.Remove(user);
                    db.SaveChanges();

                    if (Session["username"].ToString() == username)
                    {
                        Session.Remove("username");
                        Session.Remove("user_id");
                        Session.Remove("userType");
                    }

                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    successMsg("deleted");

                }
                else
                {
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "SuccessMessage";
                    string url = "ManageUser.aspx";

                    javaScript.Append("var userConfirmation = window.confirm('" + "At least one manager is needed.');\n");
                    javaScript.Append("window.location='" + url + "';");

                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                }
                                                                                                 
            }
            if (e.CommandName == "save")
            {
                if (Page.IsValid)
                {
                    var dtrUser = db.Users.Where(m => m.username == txtUsername.Text);

                    if (!dtrUser.Any() || username == txtUsername.Text)
                    {
                        var user = db.Users.FirstOrDefault(m => m.username == username);

                        if (user != null)
                        {
                            user.username = txtUsername.Text;
                            user.Email = txtEmail.Text;
                            user.userType = ddlCategory.SelectedValue;
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        System.Text.StringBuilder javaScript1 = new System.Text.StringBuilder();
                        string scriptKey = "SuccessMessage";

                        javaScript1.Append("var userConfirmation = window.confirm('" + "Username already exist." + "');\n");
                        javaScript1.Append("window.location='ManageUser.aspx';");

                        ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript1.ToString(), true);
                    }
                   

                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    successMsg("updated");

                }

            }
            if (e.CommandName == "cancel")
            {
                
                Response.Redirect("ManageUser.aspx");

            }
        }

        protected void addAdminFormSubmitClicked(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = formUsername.Text;
                string userPassword = formUserPassword.Text;
                string userType = formddlUserType.SelectedValue;
                string email = formEmail.Text;

                var db = new syasyadbEntities();
                var dtrUser = db.Users.Where(m => m.username == username);


                if (dtrUser.Any())
                {
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "SuccessMessage";

                    javaScript.Append("var userConfirmation = window.confirm('" + "Username already exist." + "');\n");
                    javaScript.Append("window.location='ManageUser.aspx';");

                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);                  

                }
                else 
                {                    

                    // insert

                    db.Users.Add(new User { username = username, password = userPassword, Email = email, userType = userType });
                    db.SaveChanges();

                    successMsg("added");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["userNo"] = "0";

            var db = new syasyadbEntities();
            var user = db.Users.Where(m => m.username.Contains(txtSearch.Text));
            rptUserList.DataSource = user.ToList();
            rptUserList.DataBind();

            
        }

    }
}