using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Users
{
    public partial class CheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] != null)
            {
                String strOrderCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                SqlConnection orderCon = new SqlConnection(strOrderCon);

                orderCon.Open();
                String strSelectItem = @"Select Cart.ProductID AS ProductID, Product.product_name AS ProductName, Product.Price AS PRICE, Cart.Quantity AS Quantity, Cart.Quantity * Product.Price AS TotalPrice, Product.URL, size.Description as [Size], color.Description as [Color]
from Product Product, Cart, [User] u, Attribute color, Attribute size
Where Cart.UserID = u.user_id and Cart.UserID=@userID and Product.product_id = Cart.ProductID and color.AttributeID = cart.color and size.AttributeID = cart.size;";
                SqlCommand cmdSelectItem = new SqlCommand(strSelectItem, orderCon);
                cmdSelectItem.Parameters.AddWithValue("@UserID", Session["user_id"].ToString());
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
                cmdCartTotal.Parameters.AddWithValue("@userID", Session["user_id"].ToString());
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
            Response.Redirect("~/Users/Products.aspx");
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
                    UserID = Int16.Parse(Session["user_id"].ToString()),
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
                SqlCommand cmdCart = new SqlCommand("SELECT Cart.ProductID, Cart.Quantity, Cart.Size, Cart.Color FROM Cart WHERE Cart.UserID= @UserID;", con);
                cmdCart.Parameters.AddWithValue("@UserID", Session["user_id"].ToString());
                SqlDataReader cart = cmdCart.ExecuteReader();
                while (cart.Read())
                {
                    SqlCommand cmdAddOrderDetail = new SqlCommand("INSERT INTO OrderDetail VALUES (@OrderID, @ProductID, @Quantity, @color, @size);", con);
                    cmdAddOrderDetail.Parameters.AddWithValue("@OrderID", orderID);
                    cmdAddOrderDetail.Parameters.AddWithValue("@ProductID", cart["ProductID"].ToString());
                    cmdAddOrderDetail.Parameters.AddWithValue("@Quantity", cart["Quantity"].ToString());
                    cmdAddOrderDetail.Parameters.AddWithValue("@Color", cart["Color"].ToString());
                    cmdAddOrderDetail.Parameters.AddWithValue("@Size", cart["Size"].ToString());
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
                    //SqlCommand cmdReduceStock = new SqlCommand("UPDATE ProductDetails SET quantity = quantity - (SELECT Quantity FROM ProductDetails WHERE ProductID = @ProductID AND color = @color AND size = @size) WHERE product_id = @ProductID AND color = @color AND size = @size", con);
                    SqlCommand cmdReduceStock = new SqlCommand("UPDATE ProductDetails SET Quantity = Quantity - @Qty WHERE Product_ID = @ProductID AND color = @color AND size = @size", con);
                    cmdReduceStock.Parameters.AddWithValue("@ProductID", rows["ProductID"].ToString());
                    cmdReduceStock.Parameters.AddWithValue("@Color", Int32.Parse(rows["Color"].ToString()));
                    cmdReduceStock.Parameters.AddWithValue("@Qty", Int32.Parse(rows["Quantity"].ToString()));
                    cmdReduceStock.Parameters.AddWithValue("@Size", Int32.Parse(rows["Size"].ToString()));
                    cmdReduceStock.ExecuteNonQuery();
                }
                rows.Close();
                con.Close();

                //clear cart
                con.Open();
                SqlCommand cmdClearCart = new SqlCommand("DELETE FROM Cart WHERE UserID = @UserID", con);
                cmdClearCart.Parameters.AddWithValue("@UserID", Session["user_id"].ToString());
                cmdClearCart.ExecuteNonQuery();
                con.Close();

                string queryString = "~/Users/Payments.aspx?id=" + orderID;
                Response.Redirect(queryString);
            }
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