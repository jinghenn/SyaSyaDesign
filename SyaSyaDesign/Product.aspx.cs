using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SyaSyaDesign;
namespace SyaSyaDesign.App_Pages
{
    public partial class Product : System.Web.UI.Page
    {
        private const string LATEST = "0";
        private const string POPULAR = "1";
        private const string LOW_TO_HIGH = "2";
        private const string HIGH_TO_LOW = "3";
        private string category_id;
        private string sort;
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var categories = db.ProductCategories.ToList();
            category_id = Request.QueryString["category"] ?? categories[0].category_id; //display first category's item if no category is selected
            sort = Request.QueryString["sort"] ?? LATEST;
            
            if (!Page.IsPostBack)
            {
                ddlSort.SelectedValue = sort;
                //Display available product category in side bar
                rpt_product_category.DataSource = categories;
                rpt_product_category.DataBind();
                //Get product items for the current category
                var productItems = SortProduct();
                rpt_product_item.DataSource = productItems;
                rpt_product_item.DataBind();
                //Display "no product" label if no product in list
                var isNoProduct = productItems.Count == 0;
                lblNoProduct.Visible = isNoProduct;
                //Change the category title
                var category_title = db.ProductCategories.FirstOrDefault(pc => pc.category_id == category_id).category_name;
                categoryTitle.InnerText = category_title;

            }
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ddlSort.SelectedValue;
            Response.Redirect($"~/Product.aspx?category={category_id}&sort={value}");
        }
        protected List<SyaSyaDesign.Product> SortProduct()
        {
            var db = new syasyadbEntities();
            var products = new List<SyaSyaDesign.Product>();
            switch (sort)
            {
                case LOW_TO_HIGH:
                    products = db.Products.Where(p => p.category_id == category_id).OrderBy(p => p.price).ToList();
                    break;
                case HIGH_TO_LOW:
                    products = db.Products.Where(p => p.category_id == category_id).OrderByDescending(p => p.price).ToList();
                    break;
                case POPULAR:
                    //TODO: change the function to return popular item
                    products = db.Products.Where(p => p.category_id == category_id).OrderByDescending(p => p.product_id).ToList();
                    break;
                default:
                    products = db.Products.Where(p => p.category_id == category_id).OrderByDescending(p => p.product_id).ToList();       
                    break;
            }
            return products;
        }
    }
}