using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Admins
{
    public partial class AttributesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) StoreTable();
        }

        private void StoreTable()
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    TableAttributeList.DataSource = db.AttributeCategories.ToList();
                    TableAttributeList.DataBind();
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
                    db.AttributeCategories.Add(new AttributeCategory() {
                        Description = txtDescription.Text,
                        IsActive = RadioButton1.Checked,
                        ModifiedBy = Int32.Parse(Session["user_id"].ToString())
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

        protected void TableAttributeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try {
                if(e.CommandName == "Activate" || e.CommandName == "Deactivate")
                {
                    using (var db = new syasyadbEntities())
                    {
                        int id = Int32.Parse(e.CommandArgument.ToString());
                        db.AttributeCategories.Find(id).IsActive = e.CommandName == "Activate";
                        var data = (from row in db.Attributes
                                   where row.CategoryID == id
                                   select row.AttributeID).ToList();
                        data.ForEach(row => db.Attributes.Find(row).IsActive = e.CommandName == "Activate");
                        db.SaveChanges();
                    }
                    StoreTable();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Update Successfully.');", true);
                }
                else if(e.CommandName == "View")
                {
                    Response.Redirect("~/Admins/Attributes.aspx?CategoryID=" + e.CommandArgument.ToString());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "failalert('Failure','Something wrong.');", true);
            }
        }

        protected void TableAttributeList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    TextBox desc = TableAttributeList.Rows[e.RowIndex].FindControl("txtDescription") as TextBox;
                    HiddenField id = TableAttributeList.Rows[e.RowIndex].FindControl("CategoryID") as HiddenField;
                    db.AttributeCategories.Find(Int32.Parse(id.Value.ToString())).Description = desc.Text;
                    db.AttributeCategories.Find(Int32.Parse(id.Value.ToString())).ModifiedBy = Int32.Parse(Session["user_id"].ToString());
                    db.SaveChanges();
                    TableAttributeList.EditIndex = -1;
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

        protected void TableAttributeList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TableAttributeList.EditIndex = -1;
            StoreTable();
        }

        protected void TableAttributeList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TableAttributeList.EditIndex = e.NewEditIndex;
            StoreTable();
        }
    }
}