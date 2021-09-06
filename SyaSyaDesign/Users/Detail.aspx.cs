using System;
using System.Collections.Generic;
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
            var prod_id_str = Request.QueryString["product_id"] ?? "0";
            var prod_id = Convert.ToInt32(prod_id_str);
            product = db.Products.FirstOrDefault(p => p.product_id == prod_id);
            if (!Page.IsPostBack)
            {
                
                
                if(product == null)
                {
                    Response.Redirect("~/ProductNotFound.aspx");
                }
                SqlDataSource1.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                SqlDataSource2.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                SqlDataSource3.SelectParameters["prod"].DefaultValue = prod_id.ToString();
                hiddenProd.Value = prod_id.ToString();
                lblProductName.Text = product.product_name;
                lblPrice.Text = $"RM {product.price}";
                txtQuantity.Attributes.Add("readonly", "readonly");
            }
        }

        protected void btn_add_cart_Click(object sender, EventArgs e)
        {
            var user_id = Session["user_id"];
            if (user_id != null)
            {
                using (var db = new syasyadbEntities())
                {
                    var sizeID = db.Attributes.ToList().Select(dt => dt).Where(element => element.Description == hiddenID.Value);
                    var colorID = db.Attributes.ToList().Select(dt => dt).Where(element => element.Description == hiddenColor.Value);
                    db.Carts.Add(new Cart
                    {
                        size = sizeID.Select(dt => dt.AttributeID).FirstOrDefault(),
                        Quantity = Convert.ToInt32(txtQuantity.Text),
                        color = colorID.Select(dt => dt.AttributeID).FirstOrDefault(),
                        ProductID = Int32.Parse(hiddenProd.Value),
                        UserID = Int32.Parse(Session["user_id"].ToString())
                    });
                    db.SaveChanges();
                    btn_add_cart.Text = "Added To Cart";
                    btn_add_cart.Enabled = false;
                }
            }
            else
            {
                var loginUrl = "~/App_Pages/Login.aspx";
                Response.Redirect($"{loginUrl}?returnPage=product&id={product.product_id}");
            }
        }

        protected void BtnSizeActive_CheckedChanged(object sender, EventArgs e)
        {
            hiddenIndex.Value = (sender as RadioButton).Text;
           
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

            if(value-1==1) btnMinus.Enabled = false;
        }
    }
}