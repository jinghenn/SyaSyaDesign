using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = 1004;

            if (!IsPostBack)
            {
                if (Session["userID"] != null)
                {
                    String strCartCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                    SqlConnection cartCon = new SqlConnection(strCartCon);
                    cartCon.Open();
                    //select data to be bound
                    String strSelectCartItem = "Select Cart.ProductID AS ProductID, Product.product_name AS ProductName, Product.Price AS PRICE, Cart.Quantity AS Quantity, Cart.Quantity * Product.Price AS TotalPrice, Product.URL from Product Product, Cart, [User] u Where Cart.UserID = u.user_id and Cart.UserID=@userID and Product.product_id = Cart.ProductID;";
                    SqlCommand cmdSelectCartItem = new SqlCommand(strSelectCartItem, cartCon);
                    cmdSelectCartItem.Parameters.AddWithValue("@userID", Session["userID"].ToString());

                    cartItemRepeater.DataSource = cmdSelectCartItem.ExecuteReader();
                    cartItemRepeater.DataBind();

                    String strCartTotal = "Select Cart.Quantity * Product.Price AS TotalPrice from Product, Cart, [User] u Where Cart.UserID = u.user_id and Cart.UserID = @userID and Product.product_id = Cart.ProductID; ";
                    SqlCommand cmdCartTotal = new SqlCommand(strCartTotal, cartCon);
                    cmdCartTotal.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                    SqlDataReader dr = cmdCartTotal.ExecuteReader();
                    decimal Total = Convert.ToDecimal(0.0);
                    while (dr.Read())
                    {
                        Total = Total + Convert.ToDecimal(dr["TotalPrice"].ToString());
                    }
                    lblTotalPrice.Text = Convert.ToString(Total);
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
            if (e.CommandName == "delete")
            {
                String strDelCartItem = "DELETE FROM Cart WHERE ProductID=@productId AND UserID=@userID";
                SqlCommand cmdDelCartItem = new SqlCommand(strDelCartItem, cartItemCon);
                cmdDelCartItem.Parameters.AddWithValue("@productId", productId);
                cmdDelCartItem.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                cmdDelCartItem.ExecuteNonQuery();
                cartItemCon.Close();
                Response.Redirect("~/App_Pages/Cart.aspx");
            }
            if (e.CommandName == "minus")
            {
                SqlCommand cmdCheckQtyNve = new SqlCommand("SELECT Quantity FROM Cart WHERE ProductID=@productId AND UserID=@userID", cartItemCon);
                cmdCheckQtyNve.Parameters.AddWithValue("@productId", productId);
                cmdCheckQtyNve.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                int cartItemQty = Convert.ToInt32(cmdCheckQtyNve.ExecuteScalar());
                if (cartItemQty > 1)
                {
                    String strDecreaseCartItem = "UPDATE Cart SET Quantity = Quantity-1 WHERE ProductID=@productId AND UserID=@userID";
                    SqlCommand cmdDecreaseCartItem = new SqlCommand(strDecreaseCartItem, cartItemCon);
                    cmdDecreaseCartItem.Parameters.AddWithValue("@productId", productId);
                    cmdDecreaseCartItem.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                    cmdDecreaseCartItem.ExecuteNonQuery();
                    cartItemCon.Close();

                    int qty = Convert.ToInt32(lblQuantity.Text);
                    qty -= 1;
                    lblQuantity.Text = qty.ToString();
                    Label unitPriceLabel = e.Item.FindControl("lblUnit") as Label;
                    Label subtotalLabel = e.Item.FindControl("lblSubtotal") as Label;
                    subtotalLabel.Text = (Convert.ToDouble(subtotalLabel.Text) - Convert.ToDouble(unitPriceLabel.Text)).ToString();
                    double total = Convert.ToDouble(lblTotalPrice.Text);
                    lblTotalPrice.Text = (total - Convert.ToDouble(unitPriceLabel.Text)).ToString();
                }
                else
                {
                    lblQuantity.Text = "1";
                }

            }
            if (e.CommandName == "plus")
            {
                //check qty in cart
                SqlCommand cmdCheckQty = new SqlCommand("SELECT Quantity FROM Cart WHERE ProductID = @productId AND UserID = @userID", cartItemCon);
                cmdCheckQty.Parameters.AddWithValue("@productId", productId);
                cmdCheckQty.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                int cartItemQty = Convert.ToInt32(cmdCheckQty.ExecuteScalar());
                //check stock qty
                SqlCommand cmdCheckMaxQty = new SqlCommand("SELECT [quantity] FROM Product WHERE product_id = @productId", cartItemCon);
                cmdCheckMaxQty.Parameters.AddWithValue("@productId", productId);
                int stockQty = Convert.ToInt32(cmdCheckMaxQty.ExecuteScalar());
                if (cartItemQty < stockQty)
                {
                    String strIncreaseCartItem = "UPDATE Cart SET Quantity = Quantity+1 WHERE ProductID=@productId AND UserID=@userID";
                    SqlCommand cmdIncreaseCartItem = new SqlCommand(strIncreaseCartItem, cartItemCon);
                    cmdIncreaseCartItem.Parameters.AddWithValue("@productId", productId);
                    cmdIncreaseCartItem.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                    cmdIncreaseCartItem.ExecuteNonQuery();
                    cartItemCon.Close();
                    int qty = Convert.ToInt32(lblQuantity.Text);
                    qty += 1;
                    lblQuantity.Text = qty.ToString();
                    Label unitPriceLabel = e.Item.FindControl("lblUnit") as Label;
                    Label subtotalLabel = e.Item.FindControl("lblSubtotal") as Label;
                    subtotalLabel.Text = (Convert.ToDouble(unitPriceLabel.Text) + Convert.ToDouble(subtotalLabel.Text)).ToString();
                    double total = Convert.ToDouble(lblTotalPrice.Text);
                    lblTotalPrice.Text = (total + Convert.ToDouble(unitPriceLabel.Text)).ToString();
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
            cmdCartItemCount.Parameters.AddWithValue("@userID", Session["userID"].ToString());
            int cartItemCount = Convert.ToInt32(cmdCartItemCount.ExecuteScalar());
            if (cartItemCount > 0)
            {
                //delete item with 0 quantity
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart WHERE Quantity=0 AND UserID=@userID", con);
                cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                cmd.ExecuteNonQuery();
                Response.Redirect("~/App_Pages/Checkout.aspx");
            }
            else
            {
                lblError.Text = "You cannot check out with your cart empty.";
            }

        }
    }
}