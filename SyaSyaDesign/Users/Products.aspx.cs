using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class Products : System.Web.UI.Page
    {
        private const string LATEST = "0";
        private const string POPULAR = "1";
        private const string LOW_TO_HIGH = "2";
        private const string HIGH_TO_LOW = "3";
        private string cat_id;
        private string sort;
        private string keyword;
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new syasyadbEntities();
            var categories = db.ProductCategories.ToList();
            cat_id = Request.QueryString["category"] ?? categories[0].category_id; //display first category's item if no category is selected
            sort = Request.QueryString["sort"] ?? LATEST;
            keyword = Request.QueryString["search"] ?? "";
            if (!Page.IsPostBack)
            {
                ddlSort.SelectedValue = sort;
                searchBox.Value = keyword;
                //Display available product category in side bar
                rpt_product_category.DataSource = categories;
                rpt_product_category.DataBind();
                //Get product items for the current category
                //var productItems = SortProduct();
                var productItems = GetProduct();
                rpt_product_item.DataSource = productItems;
                rpt_product_item.DataBind();
                //Display "no product" label if no product in list
                var isNoProduct = productItems.Count == 0;
                lblNoProduct.Visible = isNoProduct;
                //Change the category title
                var category_title = db.ProductCategories.FirstOrDefault(pc => pc.category_id == cat_id).category_name;
                categoryTitle.InnerText = category_title;

            }
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ddlSort.SelectedValue;
            var q = keyword == "" ? "" : $"&search={keyword}";
            Response.Redirect($"~/Products.aspx?category={cat_id}&sort={value}{q}");
        }
        protected List<Product> GetProduct()
        {
            var db = new syasyadbEntities();
            var products = new List<Product>();
            if (keyword != "")
            {
                var temp = "%" + keyword + "%";
                products = db.Products.Where(p => DbFunctions.Like(p.product_name, temp)).ToList();
            }
            else
            {
                products = db.Products.Where(p => p.category_id == cat_id).ToList();
            }
            switch (sort)
            {
                case LOW_TO_HIGH:
                    products = products.OrderBy(p => p.price).ToList();
                    break;
                case HIGH_TO_LOW:
                    products = products.OrderByDescending(p => p.price).ToList();
                    break;
                case POPULAR:
                    //TODO: change the function to return popular item
                    products = products.OrderByDescending(p => p.product_id).ToList();
                    break;
                default:
                    products = products.OrderByDescending(p => p.product_id).ToList();
                    break;
            }
            return products;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var rawValue = searchBox.Value;
            var kw = rawValue == "" ? "" : $"&search={rawValue}";
            Response.Redirect($"~/Products.aspx?category={cat_id}&sort={sort}{kw}");

        }
    }
}