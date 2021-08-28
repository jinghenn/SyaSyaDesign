using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class PurchaseSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["OrderID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString);

                lblOrderId.Text = Request.QueryString["OrderID"];

                using (var db = new syasyadbEntities())
                {
                    Order order = db.Orders.Find(Int16.Parse(lblOrderId.Text));
                    lblOrderTime.Text = order.Date.ToString("dd-MM-yyyy");
                    lblEstimatedArriveTime.Text = order.Date.AddDays(7).ToString("dd-MM-yyyy");
                    lblDeliveryAddress.Text = order.DeliveryAddress;
                    lblStatus.Text = order.Status;
                    if(order.Status.Equals("In Progress"))
                    {
                        lblTrackNo.Text = "123456789";
                    }
                }

                con.Open();
                String strSelectItem = "SELECT OrderDetail.ProductID, Product.product_name AS ProductName, Product.Price, OrderDetail.Quantity, OrderDetail.Quantity * Product.Price AS TotalPrice FROM OrderDetail INNER JOIN Product ON(OrderDetail.ProductID = Product.product_id) WHERE OrderID = @OrderID;";
                SqlCommand cmdSelectItem = new SqlCommand(strSelectItem, con);
                cmdSelectItem.Parameters.AddWithValue("@OrderID", Request.QueryString["OrderID"]);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSelectItem;
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = cmdSelectItem.ExecuteReader();
                Repeater1.DataBind();
                con.Close();

                con.Open();
                String strGetTotal = "SELECT OrderDetail.Quantity * Product.Price AS TotalPrice FROM OrderDetail INNER JOIN Product ON(OrderDetail.ProductID = Product.product_id) WHERE OrderID = @OrderID;";
                SqlCommand cmdGetTotal = new SqlCommand(strGetTotal, con);
                cmdGetTotal.Parameters.AddWithValue("@OrderID", Request.QueryString["OrderID"]);
                double Total = 0.0;
                SqlDataReader reader = cmdGetTotal.ExecuteReader();
                while (reader.Read())
                {
                    Total = Total + Convert.ToDouble(reader["TotalPrice"].ToString());
                }
                con.Close();

                lblSubtotal.Text = String.Format("{0:0.00}", Total);
                lblTax.Text = String.Format("{0:0.00}", (Convert.ToDouble(lblSubtotal.Text) * 0.06));
                lblTotal.Text = String.Format("RM {0:0.00}", (Convert.ToDouble(lblTax.Text) + Convert.ToDouble(lblSubtotal.Text)));
            }
            else
            {
                Response.Redirect("~/App_Pages/OrderHistory.aspx");
            }
        }
    }
}