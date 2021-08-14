using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign.App_Pages
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var db = new syasyadbEntities();
                var categories = db.ProductCategories.ToList();
                rpt_product_category.DataSource = categories;
                rpt_product_category.DataBind();

                var category_id = Request.QueryString["category"] ?? categories[0].category_id; //display first category's item if no category is selected
                var product_items = db.Products.Where(p => p.category_id == category_id).ToList();  //convert to list after select.
                rpt_product_item.DataSource = product_items;
                rpt_product_item.DataBind();
            }
        }


    }
}