using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Users
{
    public partial class Cart : System.Web.UI.Page
    {
        private int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
                Response.Redirect("~/App_Pages/Login.aspx?returnPage=cart");
            else
            {
                userID = Convert.ToInt32(Session["user_id"].ToString());
                if (!Page.IsPostBack)
                {
                    BindCartProducts();
                }
            }
        }
        private void BindCartProducts()
        {
            var db = new syasyadbEntities();
            var cartItemIDs = db.Carts.Where(c => c.UserID == userID).Select(c => c.ProductID);
            var cartItems = db.Carts.Where(c => c.UserID == userID).Select(c => new CartItemWithImage
            {
                UserID = c.UserID,
                ProductID = c.ProductID,
                ProductName = c.Product.product_name,
                size = c.size,
                sizeDesc = db.Attributes.Where(a => a.AttributeID == c.size).Select(a => a.Description).FirstOrDefault(),
                color = c.color,
                colorDesc = db.Attributes.Where(a => a.AttributeID == c.color).Select(a => a.Description).FirstOrDefault(),
                UnitPrice = c.Product.price,
                TotalPrice = (c.Product.price * c.Quantity),
                quantity = c.Quantity,
                URL = c.Product.URL

            }).ToList();
            cartItemRepeater.DataSource = cartItems;
            cartItemRepeater.DataBind();

            decimal totalPrice = 0;
            foreach(var item in cartItems)
            {
                totalPrice += item.TotalPrice;
            }
            lblTotalPrice.Text = totalPrice.ToString();
        }

        protected void cartItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var db = new syasyadbEntities();
            var argRaw = e.CommandArgument.ToString();
            var argList = argRaw.Split(',');
            var productID = Convert.ToInt32(argList[0]);
            var color = Convert.ToInt32(argList[1]);
            var size = Convert.ToInt32(argList[2]);
            var item = db.Carts.Where(ci => ci.ProductID == productID)
                        .Where(c => c.UserID == userID)
                        .Where(ci => ci.color == color)
                        .Where(ci => ci.size == size)
                        .FirstOrDefault();
            switch (e.CommandName)
            {
                case "delete":
                    db.Carts.Remove(item);
                    db.SaveChanges();
                    break;
                case "minus":
                    if(item.Quantity > 1)
                        item.Quantity -= 1;
                    db.SaveChanges();
                    break;
                case "plus":
                    var max = db.ProductDetails.Where(ci => ci.product_id == productID)
                        .Where(ci => ci.color == color)
                        .Where(ci => ci.size == size)
                        .Select(ci => ci.quantity)
                        .FirstOrDefault();
                    if (item.Quantity < max)
                        item.Quantity += 1;
                    db.SaveChanges();
                    break;
            }
            BindCartProducts();

        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var cartItemIDs = db.Carts.Where(c => c.UserID == userID).Select(c => c.ProductID);
            if (cartItemIDs.Count() > 0)
            {
                lblError.Visible = false;
                Response.Redirect("~/Users/CheckOut.aspx");
            }
            else
                lblError.Text = "You cannot check out with your cart empty.";
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
        private class CartItemWithImage
        {
            public int UserID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int size { get; set; }
            public string sizeDesc { get; set; }
            public int color { get; set; }
            public string colorDesc{ get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public int quantity { get; set; }
            public string URL { get; set; }
        }
    }

}