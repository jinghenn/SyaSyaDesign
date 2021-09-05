using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class CheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                String strOrderCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                SqlConnection orderCon = new SqlConnection(strOrderCon);

                orderCon.Open();
                String strSelectItem = "SELECT Cart.ProductID,  Product.product_name AS ProductName, Product.Price, Cart.Quantity, Cart.Quantity * Product.Price AS TotalPrice, Product.URL FROM Product, Cart WHERE Product.product_id = Cart.ProductID AND Cart.UserID= @UserID;";
                SqlCommand cmdSelectItem = new SqlCommand(strSelectItem, orderCon);
                cmdSelectItem.Parameters.AddWithValue("@UserID", Session["userID"].ToString());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSelectItem;
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = cmdSelectItem.ExecuteReader();
                Repeater1.DataBind();
                orderCon.Close();

                orderCon.Open();
                String strCartTotal = "Select Cart.Quantity * Product.Price AS TotalPrice from Product, Cart, [User] u Where Cart.UserID = u.user_id and Cart.UserID = @userID and Product.product_id = Cart.ProductID; ";
                SqlCommand cmdCartTotal = new SqlCommand(strCartTotal, orderCon);
                cmdCartTotal.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                SqlDataReader dr = cmdCartTotal.ExecuteReader();
                decimal Total = Convert.ToDecimal(0.0);
                while (dr.Read())
                {
                    Total = Total + Convert.ToDecimal(dr["TotalPrice"].ToString());
                }
                orderCon.Close();

                lblSubtotal.Text = String.Format("{0:0.00}", Total);
                lblTax.Text = String.Format("{0:0.00}", (Convert.ToDouble(lblSubtotal.Text) * 0.06));
                lblTotal.Text = String.Format("RM {0:0.00}", (Convert.ToDouble(lblTax.Text) + Convert.ToDouble(lblSubtotal.Text)));
            }
            else
            {
                Response.Redirect("~/App_Pages/Login.aspx");
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Product.aspx");
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Double total = Convert.ToDouble(Convert.ToDouble(lblTax.Text) + Convert.ToDouble(lblSubtotal.Text));
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString);

                //Insert into Order table
                var newOrder = new Order
                {
                    UserID = Int16.Parse(Session["userID"].ToString()),
                    RecipientName = txtName.Text,
                    EmailAddress = txtEmail.Text,
                    ContactNumber = txtContactNumber.Text,
                    DeliveryAddress = txtDeliveryAddress.Text + " " + txtZipCode.Text + " " + txtCity.Text + " " + ddlState.Text,
                    Date = DateTime.Now.Date,
                    Total = total,
                    Status = "In Progress"
                };
                using (var db = new syasyadbEntities())
                {
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                }

                //get latest OrderID from Order table
                con.Open();
                SqlCommand cmdgetOrderID = new SqlCommand("SELECT OrderID FROM [dbo].[Order] ORDER BY OrderID DESC;", con);
                int orderID = Convert.ToInt32(cmdgetOrderID.ExecuteScalar());
                con.Close();

                //Insert into OrderDetail table
                con.Open();
                SqlCommand cmdCart = new SqlCommand("SELECT Cart.ProductID, Cart.Quantity FROM Cart WHERE Cart.UserID= @UserID;", con);
                cmdCart.Parameters.AddWithValue("@UserID", Session["userID"].ToString());
                SqlDataReader cart = cmdCart.ExecuteReader();
                while (cart.Read())
                {
                    SqlCommand cmdAddOrderDetail = new SqlCommand("INSERT INTO OrderDetail VALUES (@OrderID, @ProductID, @Quantity,null);", con);
                    cmdAddOrderDetail.Parameters.AddWithValue("@OrderID", orderID);
                    cmdAddOrderDetail.Parameters.AddWithValue("@ProductID", cart["ProductID"].ToString());
                    cmdAddOrderDetail.Parameters.AddWithValue("@Quantity", cart["Quantity"].ToString());
                    cmdAddOrderDetail.ExecuteNonQuery();
                }
                cart.Close();
                con.Close();

                //Reduce Stock
                con.Open();
                SqlCommand cmdGetProductCount = new SqlCommand("SELECT * FROM OrderDetail WHERE OrderID = @OrderID", con);
                cmdGetProductCount.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataReader rows = cmdCart.ExecuteReader();
                while (rows.Read())
                {
                    SqlCommand cmdReduceStock = new SqlCommand("UPDATE Product SET quantity = quantity - (SELECT Quantity FROM OrderDetail WHERE OrderID = @OrderID AND ProductID = @ProductID) WHERE product_id = @ProductID", con);
                    cmdReduceStock.Parameters.AddWithValue("@OrderID", orderID);
                    cmdReduceStock.Parameters.AddWithValue("@ProductID", rows["ProductID"].ToString());
                    cmdReduceStock.ExecuteNonQuery();
                }
                rows.Close();
                con.Close();

                //clear cart
                con.Open();
                SqlCommand cmdClearCart = new SqlCommand("DELETE FROM Cart WHERE UserID = @UserID", con);
                cmdClearCart.Parameters.AddWithValue("@UserID", Session["userID"].ToString());
                cmdClearCart.ExecuteNonQuery();
                con.Close();

                //update all product quantity in the cart if exceed maximum amount
                con.Open();
                SqlCommand cmdgetProductInOrder = new SqlCommand("SELECT ProductID FROM OrderDetail WHERE OrderID = @OrderID", con);
                cmdgetProductInOrder.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataReader productInOrder = cmdgetProductInOrder.ExecuteReader();
                int stockQuantity;
                while (productInOrder.Read())
                {
                    //get the stock quantity for a specific product
                    SqlCommand cmdgetquantity = new SqlCommand("SELECT quantity FROM Product WHERE product_id = @ProductID", con);
                    cmdgetquantity.Parameters.AddWithValue("@ProductID", productInOrder["ProductID"].ToString());
                    stockQuantity = Convert.ToInt32(cmdgetquantity.ExecuteScalar());

                    if (stockQuantity == 0)
                    {
                        //remove the specific product from all carts where stock quantity = 0
                        SqlCommand cmdRemoveFromAllCart = new SqlCommand("DELETE FROM Cart WHERE ProductID=@ProductID", con);
                        cmdRemoveFromAllCart.Parameters.AddWithValue("@ProductID", productInOrder["ProductID"].ToString());
                        cmdRemoveFromAllCart.ExecuteNonQuery();
                    }
                    else
                    {
                        //update the cart quantity of the specific product if > stock quantity
                        SqlCommand cmdUpdateAllCart = new SqlCommand("UPDATE Cart SET Quantity = @quantity WHERE ProductID=@ProductID AND Quantity > @quantity", con);
                        cmdUpdateAllCart.Parameters.AddWithValue("@quantity", stockQuantity);
                        cmdUpdateAllCart.Parameters.AddWithValue("@ProductID", productInOrder["ProductID"].ToString());
                        cmdUpdateAllCart.ExecuteNonQuery();
                    }
                }
                productInOrder.Close();
                con.Close();

                string queryString = "~/App_Pages/DigitalReceipt.aspx?OrderID=" + orderID;
                Response.Redirect(queryString);
            }
        }
    }
}