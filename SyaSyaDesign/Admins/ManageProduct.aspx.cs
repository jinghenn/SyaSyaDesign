using SyaSyaDesign;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Admins
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindData();
                BindDropDownList();
                BindAttributes();
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
            var ddlAttribute = (ListBox)row.FindControl("AttributeList");
            List<int> attrSelected = new List<int>();

            using (var db = new syasyadbEntities())
            {
                var product = db.Products.FirstOrDefault(p => p.product_id == id);


                foreach (ListItem listItem in ddlAttribute.Items)
                {
                    if (listItem.Selected) attrSelected.Add(Convert.ToInt32(listItem.Value.ToString()));
                }

                if (product != null)
                {
                    product.product_name = txtProductName.Text;
                    product.price = Convert.ToDecimal(txtPrice.Text);
                    product.category_id = ddlCategory.SelectedValue;
                }
                db.SaveChanges();
            }

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["syasyadbConnection"].ConnectionString))
                {
                    String query = "DELETE FROM PRODUCTATTRIBUTE WHERE PRODUCT_ID = " + lblID.Text + ";";
                    if (attrSelected.Count > 0) query += "INSERT INTO PRODUCTATTRIBUTE VALUES";
                    foreach (var item in attrSelected)
                    {
                        query += "(" + lblID.Text + "," + item.ToString() + "),";
                    }

                    query = query.Remove(query.Length - 1);

                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            gvProduct.EditIndex = -1;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProduct.EditIndex = e.NewEditIndex;

            BindAttributes();

            TextBox txt = (TextBox)gvProduct.Rows[e.NewEditIndex].FindControl("attributeRecord");
            Hidden.Value = txt.Text;

            BindData();
        }

        private void BindData()
        {
            var db = new syasyadbEntities();
            var products = db.Products.Where(p => !p.isDeleted).Select(p => new ProductDetail
            {
                product_id = p.product_id,
                product_name = p.product_name,
                price = p.price,
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
                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                var categoryControl = (HiddenField)e.Row.FindControl("categoryID");

                ddlCategory.SelectedValue = categoryControl.Value;
                ddlCategory.Items.FindByValue(categoryControl.Value).Selected = true;

                ListBox dl = (ListBox)e.Row.FindControl("AttributeList");
                using (var dt = new syasyadbEntities())
                {
                    var result = dt.Attributes.Select(row => row)
                        .Where(element => element.AttributeCategory.AttributeCategoryID != 1 && element.AttributeCategory.AttributeCategoryID != 2).ToList();
                    result.ForEach(row => dl.Items.Add(new ListItem() { Text = row.Description, Value = row.AttributeID.ToString() }));


                    var attGroup = (from attributes in dt.Attributes
                                    join catGroup in dt.AttributeCategories on attributes.CategoryID equals catGroup.AttributeCategoryID
                                    where catGroup.AttributeCategoryID != 1 &
                                    catGroup.AttributeCategoryID != 2
                                    select attributes).ToList();

                    var data = dt.Products.ToList();
                    var rst = (Hidden.Value.Split(',')).ToList();

                    if (rst.FirstOrDefault() != "") rst.ForEach(row => dl.Items.FindByText(row).Selected = true);

                    Label lb = (Label)e.Row.FindControl("lblProductID");
                    if (dl != null)
                    {
                        dl.Items.Clear();
                        attGroup.ForEach(attr => dl.Items.Add(new ListItem(attr.Description, attr.AttributeID.ToString())));
                        var resultList = data.Select(rows => rows).Where(element => element.product_id.ToString() == lb.Text).FirstOrDefault().Attributes.Select(attr => attr.AttributeID.ToString()).ToList();
                        dl.ClearSelection();
                        foreach (var rec in resultList)
                        {
                            if (dl.Items.FindByValue(rec) != null) dl.Items.FindByValue(rec).Selected = true;
                        }
                    }
                }
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
                product.isDeleted = true;
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
                category_id = ddlNewCategory.SelectedValue
            };
            db.Products.Add(newProduct);
            db.SaveChanges();
            BindData();
        }

        protected void BtnAddQty_Click(object sender, EventArgs e)
        {
            using (var db = new syasyadbEntities())
            {
                var size = int.Parse(ddlSizeAdd.SelectedValue);
                var color = int.Parse(DropDownListColor.SelectedValue);
                var prodID = int.Parse(ProdID.Text);

                var data = from row in db.ProductDetails
                           where row.product_id == prodID && row.size == size && row.color == color
                           select row;
                data.FirstOrDefault().quantity += int.Parse(txtQtyAdd.Text);
                db.SaveChanges();
            }
        }

        private void BindDropDownList()
        {
            using (var db = new syasyadbEntities())
            {
                var size = (from attributes in db.Attributes
                            where attributes.CategoryID == 1
                            select attributes).ToList();
                var color = (from attributes in db.Attributes
                             where attributes.CategoryID == 2
                             select attributes).ToList();
                size.ForEach(element => ddlColorList.Items.Add(new ListItem(element.Description, element.AttributeID.ToString())));
                color.ForEach(element => ddlSizeList.Items.Add(new ListItem(element.Description, element.AttributeID.ToString())));
            }

            ddlColorList.SelectedIndex = 0;
            ddlSizeList.SelectedIndex = 0;

        }

        private void BindAttributes()
        {
            using (var db = new syasyadbEntities())
            {
                var data = db.Products.ToList();

                foreach (GridViewRow grdrw in gvProduct.Rows)
                {
                    TextBox lst = (TextBox)grdrw.FindControl("attributeRecord");
                    Label lb = (Label)grdrw.FindControl("lblProductID");
                    Label desc = (Label)grdrw.FindControl("lblProductName");
                    Button btn = (Button)grdrw.FindControl("ModalLink");

                    var result = data.Select(dt => dt).Where(element => element.product_id.ToString() == lb.Text).FirstOrDefault().Attributes.Select(attr => attr.Description.ToString()).ToList();
                    if (result != null) lst.Text = string.Join(",", result.ToArray());

                }



            }
        }

        private void bindingModal(String e)
        {
            var result = e.Split(',');
            ProdID.Text = result[0];
            ProdDesc.Text = result[1];
            using (var db = new syasyadbEntities())
            {
                TableQuantity.DataSource = (from data in db.ProductDetails
                                            where data.product_id.ToString().Equals(ProdID.Text)
                                            select data).ToList();
                TableQuantity.DataBind();
            }
        }

        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow dG = gvProduct.SelectedRow;

            ProdID.Text = (dG.FindControl("lblProductID") as Label).Text;
            ProdDesc.Text = (dG.FindControl("lblProductName") as Label).Text;

            using (var db = new syasyadbEntities())
            {
                var dataList = (from data in db.ProductDetails
                                where data.product_id.ToString().Equals(ProdID.Text)
                                select data).ToList();
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("size", typeof(string)),
                    new DataColumn("color", typeof(string)),
                    new DataColumn("quantity",typeof(string)) });

                dataList.ForEach(element => dt.Rows.Add(element.Attribute1.Description, element.Attribute.Description, element.quantity));

                TableQuantity.DataSource = dt;
                TableQuantity.DataBind();

                ddlSizeAdd.Items.Clear();
                DropDownListColor.Items.Clear();
                dataList.ForEach(element => ddlSizeAdd.Items.Add(new ListItem { Text = element.Attribute1.Description, Value = element.size.ToString() }));
                dataList.ForEach(element => DropDownListColor.Items.Add(new ListItem { Text = element.Attribute.Description, Value = element.color.ToString() }));

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pop", "ShowStatus()", true);
            }
        }

        protected void ButtonNewProdDetails_Click(object sender, EventArgs e)
        {
            using (var db = new syasyadbEntities())
            {
                db.ProductDetails.Add(new SyaSyaDesign.ProductDetail
                {
                    product_id = Int32.Parse(ProdID.Text),
                    color = Int32.Parse(ddlColorList.SelectedValue),
                    size = Int32.Parse(ddlSizeList.SelectedValue),
                    quantity = Int32.Parse(txtnewQty.Text)
                });

                db.SaveChanges();
            }
        }
    }

}