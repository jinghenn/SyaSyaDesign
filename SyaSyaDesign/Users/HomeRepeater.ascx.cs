using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace SyaSyaDesign.Users
{
    public partial class HomeRepeater : System.Web.UI.UserControl
    {
        public string displayType = "Trending";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String strProductCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                SqlConnection productCon = new SqlConnection(strProductCon);

                productCon.Open();
                SqlCommand cmdGetURL;
                heading.Text = displayType;

                switch (displayType)
                {
                    case "Hot Selling":
                        //Hot Selling
                        cmdGetURL = new SqlCommand("SELECT Product.product_id, Product.URL, Product.product_name, Product.price FROM Product WHERE product_id IN (SELECT TOP 5 OrderDetail.ProductID AS TotalQuantity FROM OrderDetail GROUP BY OrderDetail.ProductID ORDER BY SUM(OrderDetail.Quantity) DESC)", productCon);
                        break;

                    case "New Artwork":
                        //New
                        cmdGetURL = new SqlCommand("SELECT Product.product_id, Product.URL, Product.product_name, Product.price FROM Product WHERE product_id IN (SELECT TOP 5 product_id FROM Product ORDER BY product_id DESC)", productCon);
                        break;

                    default:
                        heading.CssClass = "h1 text-light mb4";
                        cmdGetURL = new SqlCommand("SELECT Product.product_id, Product.URL, Product.product_name, Product.price FROM Product WHERE product_id IN (SELECT TOP 5 OrderDetail.ProductID AS TotalQuantity FROM OrderDetail INNER JOIN [Order] on (OrderDetail.OrderID = [Order].OrderID) WHERE [Order].[Date] > (GETDATE() - 7) GROUP BY OrderDetail.ProductID ORDER BY SUM(OrderDetail.Quantity) DESC)", productCon);
                        break;
                }

                rpt.DataSource = cmdGetURL.ExecuteReader();
                rpt.DataBind();
                productCon.Close();
            }
        }
        protected void SlideImg_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lnkRowSelection = (ImageButton)sender;
            //Get the id from command argumen tof linkbutton
            string product_id = lnkRowSelection.CommandArgument.ToString();

            Response.Redirect("~/Users/Detail.aspx?product_id=" + product_id);
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