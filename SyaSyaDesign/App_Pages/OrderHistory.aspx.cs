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
    public partial class ManageOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = 1004;
            if (Session["userID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["syasyadbConnectionString"].ConnectionString);

                con.Open();
                String strSelectItem = "SELECT OrderID, RecipientName, DeliveryAddress, Date, Total, [Status] FROM [Order] WHERE UserID = @UserID;";
                SqlCommand cmdSelectItem = new SqlCommand(strSelectItem, con);
                cmdSelectItem.Parameters.AddWithValue("@UserID", Session["userID"]);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSelectItem;
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = cmdSelectItem.ExecuteReader();
                Repeater1.DataBind();
                con.Close();
            }
        }
    }
}