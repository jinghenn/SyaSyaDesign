using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Web.Services;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace SyaSyaDesign
{
    public partial class Attributes : System.Web.UI.Page
    {
        public static int categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CategoryID"] != null) categoryId = Int32.Parse(Request.QueryString["CategoryID"].ToString());
                else Response.Redirect("~/Admins/AttributesList.aspx");

                using (var db = new syasyadbEntities())
                {
                    TableAttribute.Caption = "Category Source :" + db.AttributeCategories.Find(categoryId).Description;
                }

                StoreTable();
            }
        }

        private void StoreTable()
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    TableAttribute.DataSource = (
                        from data in db.Attributes
                        where data.CategoryID == categoryId
                        select data
                        ).ToList();
                    TableAttribute.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    db.Attributes.Add(new Attribute()
                    {
                        Description = txtDescription.Text,
                        CategoryID = categoryId,
                        IsActive = RadioButton1.Checked,
                        ModifiedBy = 1000 //adding after merge with master
                    });
                    db.SaveChanges();
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TableAttribute_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Activate" || e.CommandName == "Deactivate")
                {
                    using (var db = new syasyadbEntities())
                    {
                        db.Attributes.Find(Int32.Parse(e.CommandArgument.ToString())).IsActive = e.CommandName == "Activate";
                        db.SaveChanges();
                    }
                    StoreTable();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Update Successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "failalert('Failure','Something wrong.');", true);
            }
        }

        protected void TableAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    TextBox desc = TableAttribute.Rows[e.RowIndex].FindControl("txtDescription") as TextBox;
                    HiddenField id = TableAttribute.Rows[e.RowIndex].FindControl("AttributeID") as HiddenField;
                    db.Attributes.Find(Int32.Parse(id.Value.ToString())).Description = desc.Text;
                    db.Attributes.Find(Int32.Parse(id.Value.ToString())).ModifiedBy = 1001; //adding after merge with master
                    db.SaveChanges();
                    TableAttribute.EditIndex = -1;
                    StoreTable();
                }
                StoreTable();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Update Successfully.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "failalert('Failure','Something wrong.');", true);
            }
        }

        protected void TableAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TableAttribute.EditIndex = -1;
            StoreTable();
        }

        protected void TableAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TableAttribute.EditIndex = e.NewEditIndex;
            StoreTable();
        }

    }
}