﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var db = new syasyadbEntities();
                var prod_id_str = Request.QueryString["product_id"] ?? "0";
                var prod_id = Convert.ToInt32(prod_id_str);
                var product = db.Products.FirstOrDefault(p => p.product_id == prod_id);
                if(product == null)
                {
                    Response.Redirect("~/ProductNotFound.aspx");
                }
                lblProductName.Text = product.product_name;
                lblPrice.Text = $"RM {product.price}";
            }
        }
    }
}