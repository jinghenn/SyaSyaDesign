using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.Users
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String strProductCon = ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString;
                SqlConnection productCon = new SqlConnection(strProductCon);

                productCon.Open();
                SqlCommand cmdGetURL = new SqlCommand("SELECT Product.product_id, Product.URL, Product. product_name FROM Product WHERE product_id IN (SELECT TOP 5 OrderDetail.ProductID AS TotalQuantity FROM OrderDetail GROUP BY OrderDetail.ProductID ORDER BY SUM(OrderDetail.Quantity) DESC)", productCon);

                carouselRepeater.DataSource = cmdGetURL.ExecuteReader();
                carouselRepeater.DataBind();
                productCon.Close();
                HtmlGenericControl div = (HtmlGenericControl)carouselRepeater.Items[0].FindControl("carouselItem");
                div.Attributes.Add("class", "carousel-item active");
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