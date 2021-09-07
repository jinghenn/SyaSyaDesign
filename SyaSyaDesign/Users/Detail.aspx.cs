using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class Detail : System.Web.UI.Page
    {
        private Product product;
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var prod_id_str = Request.QueryString["product_id"];
            if (prod_id_str == null)
                Response.Redirect("~/Users/Products.aspx", true);

            var prod_id = Convert.ToInt32(prod_id_str);
            product = db.Products.FirstOrDefault(p => p.product_id == prod_id);
            if (!Page.IsPostBack)
            {
                if (product == null)
                    Response.Redirect("~/ProductNotFound.aspx");

                SqlDataSource1.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                //SqlDataSource2.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                //SqlDataSource3.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                //hiddenProd.Value = prod_id.ToString();
                imgProduct.ImageUrl = GetImage(product.URL);
                lblProductName.Text = product.product_name;
                lblPrice.Text = $"RM {product.price}";
                txtQuantity.Attributes.Add("readonly", "readonly");
                BindColorAttr();
            }
            lblAlreadyInCart.Visible = false;
        }

        protected void btn_add_cart_Click(object sender, EventArgs e)
        {
            var user_id = Session["user_id"];
            if (user_id != null)
            {
                using (var db = new syasyadbEntities())
                {
                    var sizeId = Convert.ToInt32(rblSize.SelectedValue);
                    var colorId = Convert.ToInt32(rblColor.SelectedValue);
                    var newCartItem = new Cart
                    {
                        size = sizeId,
                        color = colorId,
                        Quantity = Convert.ToInt32(txtQuantity.Text),
                        ProductID = product.product_id,
                        UserID = Int32.Parse(Session["user_id"].ToString())
                    };
                    try
                    {
                        db.Carts.Add(newCartItem);
                        db.SaveChanges();
                    }
                    catch
                    {
                        lblAlreadyInCart.Visible = true;
                    }

                }
            }
            else
            {
                var loginUrl = "~/App_Pages/Login.aspx";
                Response.Redirect($"{loginUrl}?returnPage=product&id={product.product_id}");
            }
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
            var value = Int32.Parse(txtQuantity.Text.ToString());
            if (value < 10) txtQuantity.Text = (value + 1).ToString();
            else btnPlus.Enabled = false;
            btnMinus.Enabled = true;
        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            var value = Int32.Parse(txtQuantity.Text.ToString());
            if (value < 1) txtQuantity.Text = (value - 1).ToString();

            if (value - 1 == 1) btnMinus.Enabled = false;
        }

        private void BindColorAttr()
        {
            var db = new syasyadbEntities();
            var attrColorIDs = db.ProductDetails.Where(pd => pd.product_id == product.product_id)
                .Select(pd => pd.color).Distinct();
            var attrColor = db.Attributes.Where(attr => attrColorIDs.Contains(attr.AttributeID))
                .Select(attr => new
                {
                    AttributeID = attr.AttributeID,
                    Description = attr.Description
                }).ToList();

            rblColor.DataSource = attrColor;
            rblColor.DataValueField = "AttributeID";
            rblColor.DataTextField = "Description";
            rblColor.DataBind();
            if(rblColor.Items.Count > 0)
                rblColor.Items[0].Selected = true;
            BindSizeAttr();
        }
        private void BindSizeAttr()
        {
            var db = new syasyadbEntities();
            var colorID = Convert.ToInt32(rblColor.SelectedValue);
            var availableSizeIDs = db.ProductDetails.Where(pd => pd.product_id == product.product_id)
                .Where(pd => pd.color == colorID)
                .Select(pd => pd.size);
            var attrSize = db.Attributes.Where(pd => availableSizeIDs.Contains(pd.AttributeID))
                .Select(attr => new
                {
                    AttributeID = attr.AttributeID,
                    Description = attr.Description
                }).ToList();

            rblSize.DataSource = attrSize;
            rblSize.DataValueField = "AttributeID";
            rblSize.DataTextField = "Description";
            rblSize.DataBind();
            if (rblSize.Items.Count > 0)
                rblSize.Items[0].Selected = true;
            CheckAvailability();

        }

        protected void rblColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSizeAttr();
        }

        protected void rblSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAvailability();
        }


        private void CheckAvailability()
        {
            var color = Convert.ToInt32(rblColor.SelectedValue);
            var size = Convert.ToInt32(rblSize.SelectedValue);
            var db = new syasyadbEntities();
            var prod = db.ProductDetails.Where(pd => pd.product_id == product.product_id)
                .Where(pd => pd.color == color)
                .Where(pd => pd.size == size)
                .FirstOrDefault();

            EnableAddCart();
            if (prod == null)
                DisableAddCart();
            if (prod.quantity == 0)
                DisableAddCart();

        }
        private void DisableAddCart()
        {
            var css = "btn-dark mx-auto flex-grow-1 mb-3 py-3 rounded-1 fw-bold";
            btn_add_cart.Enabled = false;
            btn_add_cart.Text = "Out of Stock";
            btn_add_cart.CssClass = $"{css} disabled";
        }
        private void EnableAddCart()
        {
            var css = "btn-dark mx-auto flex-grow-1 mb-3 py-3 rounded-1 fw-bold";
            btn_add_cart.Enabled = true;
            btn_add_cart.Text = "Add To Cart";
            btn_add_cart.CssClass = $"{css} enabled";
        }

        public string GetImage(string name)
        {
            //* var imgPath = HttpContext.Current.Server.MapPath("~/Product_Images/");
            var imgPath = "/Product_Images/";
            var imgFile = HttpContext.Current.Server.MapPath($"~/Product_Images/{name}");
            if (String.IsNullOrEmpty(name) || !File.Exists(imgFile))
                return $"{imgPath}empty.png";

            return $"{imgPath}{name}";
        }
    }
}