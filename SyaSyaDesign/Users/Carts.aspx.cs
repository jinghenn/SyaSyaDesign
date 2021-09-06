using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Linq;

namespace SyaSyaDesign.Users
{
    public class CartList
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string URL { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public CartList(int v1, string v2, decimal v3, int v4, decimal v5, string v6, string v7, string v8)
        {
            ProductID = v1;
            ProductName = v2;
            Price = v3;
            Quantity = v4;
            TotalPrice = v5;
            URL = v6;
            Size = v7;
            Color = v8;
        }
    }

    public partial class Carts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Session["userID"] = 1004;
            if (!IsPostBack)
            {
                if (Session["user_id"] != null)
                {
                    var test = Session["user_id"].ToString();
                    String strCartCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                    SqlConnection cartCon = new SqlConnection(strCartCon);
                    cartCon.Open();
                    //select data to be bound
                    String strSelectCartItem = @"Select Cart.ProductID AS ProductID, Product.product_name AS ProductName, Product.Price AS PRICE, Cart.Quantity AS Quantity, Cart.Quantity * Product.Price AS TotalPrice, Product.URL, size.Description as [Size], color.Description as [Color]
from Product Product, Cart, [User] u, Attribute color, Attribute size
Where Cart.UserID = u.user_id and Cart.UserID=@userID and Product.product_id = Cart.ProductID and color.AttributeID = cart.color and size.AttributeID = cart.size;";
                    SqlCommand cmdSelectCartItem = new SqlCommand(strSelectCartItem, cartCon);
                    cmdSelectCartItem.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                    SqlDataReader reader = cmdSelectCartItem.ExecuteReader();
                    List<CartList> cartList = new List<CartList>();

                    while (reader.Read())
                    {
                        cartList.Add(new CartList(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
                    }

                    cartItemRepeater.DataSource = cartList;
                    cartItemRepeater.DataBind();

                    String strCartTotal = "Select sum(Cart.Quantity * Product.Price) AS TotalPrice from Product, Cart, [User] u Where Cart.UserID = u.user_id and Cart.UserID = @userID and Product.product_id = Cart.ProductID; ";
                    SqlCommand cmdCartTotal = new SqlCommand(strCartTotal, cartCon);
                    cmdCartTotal.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                    object dr = cmdCartTotal.ExecuteScalar();
                    lblTotalPrice.Text = (!(dr is System.DBNull)) ? String.Format("{0:0.00}", Double.Parse(dr.ToString())) : "0.00";
                    cartCon.Close();
                }
                else
                {
                    Response.Redirect("~/App_Pages/Login.aspx");
                }

            }

        }

        protected void cartItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String strCartItemCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
            SqlConnection cartItemCon = new SqlConnection(strCartItemCon);
            cartItemCon.Open();
            String productId = e.CommandArgument.ToString();
            Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
            Label lblColor = (Label)e.Item.FindControl("LabelColor");
            Label lblSize = (Label)e.Item.FindControl("LabelSize"); 
            Label unitPriceLabel = e.Item.FindControl("lblUnit") as Label;
            Label subtotalLabel = e.Item.FindControl("lblSubtotal") as Label;
            int cartItemQty = Convert.ToInt32(lblQuantity.Text);
            int colorID;
            int sizeID;

            using (var db = new syasyadbEntities())
            {
                colorID = (from data in db.Attributes
                          where data.Description == lblColor.Text
                          select data.AttributeID).FirstOrDefault();
                sizeID = (from data in db.Attributes
                          where data.Description == lblSize.Text
                          select data.AttributeID).FirstOrDefault();
            }

            if (e.CommandName == "delete")
            {
                String strDelCartItem = "DELETE FROM Cart WHERE ProductID=@productId AND UserID=@userID AND Size = @Size AND Color = @Color";
                SqlCommand cmdDelCartItem = new SqlCommand(strDelCartItem, cartItemCon);
                cmdDelCartItem.Parameters.AddWithValue("@productId", productId);
                cmdDelCartItem.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                cmdDelCartItem.Parameters.AddWithValue("@Color", colorID);
                cmdDelCartItem.Parameters.AddWithValue("@Size", sizeID);
                cmdDelCartItem.ExecuteNonQuery();
                cartItemCon.Close();
                Response.Redirect("~/Users/Carts.aspx");
            }
            else if (e.CommandName == "minus")
            {
                if (cartItemQty > 1)
                {
                    String strDecreaseCartItem = "UPDATE Cart SET Quantity = Quantity-1 WHERE ProductID=@productId AND UserID=@userID AND Size = @Size AND Color = @Color";
                    SqlCommand cmdDecreaseCartItem = new SqlCommand(strDecreaseCartItem, cartItemCon);
                    cmdDecreaseCartItem.Parameters.AddWithValue("@productId", productId);
                    cmdDecreaseCartItem.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                    cmdDecreaseCartItem.Parameters.AddWithValue("@Color", colorID);
                    cmdDecreaseCartItem.Parameters.AddWithValue("@Size", sizeID);
                    cmdDecreaseCartItem.ExecuteNonQuery();
                    cartItemCon.Close();

                    int qty = Convert.ToInt32(lblQuantity.Text) - 1;
                    lblQuantity.Text = qty.ToString();
                    subtotalLabel.Text = String.Format("{0:0.00}", ((Convert.ToDouble(subtotalLabel.Text) - Convert.ToDouble(unitPriceLabel.Text)).ToString()));
                    double total = Convert.ToDouble(lblTotalPrice.Text);
                    lblTotalPrice.Text = String.Format("{0:0.00}", (total - Convert.ToDouble(unitPriceLabel.Text)).ToString());
                }
                else
                {
                    lblQuantity.Text = "1";
                }
            }
            else if (e.CommandName == "plus")
            {
                //check stock qty
                SqlCommand cmdCheckMaxQty = new SqlCommand("SELECT [quantity] FROM ProductDetails WHERE product_id = @productId AND Size = @Size AND Color = @Color", cartItemCon);
                cmdCheckMaxQty.Parameters.AddWithValue("@productId", productId);
                cmdCheckMaxQty.Parameters.AddWithValue("@Color", colorID);
                cmdCheckMaxQty.Parameters.AddWithValue("@Size", sizeID);
                int stockQty = Convert.ToInt32(cmdCheckMaxQty.ExecuteScalar());
                if (cartItemQty < stockQty)
                {
                    String strIncreaseCartItem = "UPDATE Cart SET Quantity = Quantity+1 WHERE ProductID=@productId AND UserID=@userID AND Size = @Size AND Color = @Color";
                    SqlCommand cmdIncreaseCartItem = new SqlCommand(strIncreaseCartItem, cartItemCon);
                    cmdIncreaseCartItem.Parameters.AddWithValue("@productId", productId);
                    cmdIncreaseCartItem.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                    cmdIncreaseCartItem.Parameters.AddWithValue("@Color", colorID);
                    cmdIncreaseCartItem.Parameters.AddWithValue("@Size", sizeID);
                    cmdIncreaseCartItem.ExecuteNonQuery();
                    cartItemCon.Close();
                    int qty = Convert.ToInt32(lblQuantity.Text) + 1;
                    lblQuantity.Text = qty.ToString();
                    
                    subtotalLabel.Text = String.Format("{0:0.00}", ((Convert.ToDouble(unitPriceLabel.Text) + Convert.ToDouble(subtotalLabel.Text)).ToString()));
                    double total = Convert.ToDouble(lblTotalPrice.Text);
                    lblTotalPrice.Text = String.Format("{0:0.00}", (total + Convert.ToDouble(unitPriceLabel.Text)).ToString());
                }
                else
                {
                    lblQuantity.Text = stockQty.ToString();
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString);
            con.Open();
            //check if the cart is empty
            SqlCommand cmdCartItemCount = new SqlCommand("SELECT COUNT(ProductID) FROM Cart WHERE UserID=@userID AND Quantity > 0", con);
            cmdCartItemCount.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
            int cartItemCount = Convert.ToInt32(cmdCartItemCount.ExecuteScalar());
            if (cartItemCount > 0)
            {
                //delete item with 0 quantity
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart WHERE Quantity=0 AND UserID=@userID", con);
                cmd.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Users/Checkout.aspx");
            }
            else
            {
                lblError.Text = "You cannot check out with your cart empty.";
            }

        }
    }
}