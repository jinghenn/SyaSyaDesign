using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private class ProductDetail
        {
            public int product_id { get; set; }
            public string product_name { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
            public int attribute_id { get; set; }
            public string category_id { get; set; }
            public string category_name { get; set; }


        }

        protected void gvProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var index = gvProduct.EditIndex;
            var row = gvProduct.Rows[index];
            var lblID = (Label)row.FindControl("lblProductID");
            var id = Convert.ToInt32(lblID.Text);
            var txtProductName = (TextBox)row.FindControl("txtProductName");
            var txtQuantity = (TextBox)row.FindControl("txtQuantity");
            var txtPrice = (TextBox)row.FindControl("txtPrice");
            var ddlCategory = (DropDownList)row.FindControl("ddlCategory");

            var db = new syasyadbEntities();
            var product = db.Products.FirstOrDefault(p => p.product_id == id);

            if (product != null)
            {
                product.product_name = txtProductName.Text;
                product.price = Convert.ToDecimal(txtPrice.Text);
                product.quantity = Convert.ToInt32(txtQuantity.Text);
                product.category_id = ddlCategory.SelectedValue;
            }
            db.SaveChanges();
            gvProduct.EditIndex = -1;
            BindData();
        }




        protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProduct.EditIndex = e.NewEditIndex;
            BindData();
        }

        private void BindData()
        {
            var db = new syasyadbEntities();
            var products = db.Products.Select(p => new ProductDetail
            {
                product_id = p.product_id,
                product_name = p.product_name,
                price = p.price,
                quantity = p.quantity,
                attribute_id = p.attribute_id,
                category_id = p.category_id,
                category_name = p.ProductCategory.category_name

            }).ToList();
            gvProduct.DataSource = products;
            gvProduct.DataBind();

        }

        protected void gvProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProduct.EditIndex = -1;
            BindData();
        }


        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
        (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                // Here you will get the Control you need like:
                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                var categoryControl = (HiddenField)e.Row.FindControl("categoryID");

                ddlCategory.SelectedValue = categoryControl.Value;
                ddlCategory.Items.FindByValue(categoryControl.Value).Selected = true;
            }
        }


        protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = gvProduct.Rows[e.RowIndex];

            var lblID = (Label)row.FindControl("lblProductID");
            var id = Convert.ToInt32(lblID.Text);
            var db = new syasyadbEntities();
            var product = db.Products.FirstOrDefault(p => p.product_id == id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            BindData();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var newProduct = new Product
            {
                product_name = txtNewProductName.Text,
                price = Convert.ToDecimal(txtNewPrice.Text),
                quantity = Convert.ToInt32(txtNewQuantity.Text),
                attribute_id = 1,
                category_id = ddlNewCategory.SelectedValue
            };
            db.Products.Add(newProduct);
            db.SaveChanges();
            BindData();
        }



    }

}