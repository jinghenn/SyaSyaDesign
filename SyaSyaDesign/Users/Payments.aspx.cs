using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class Payments : System.Web.UI.Page
    {
        private static int OrderId;
        double Total;

        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            { 
                if (!IsPostBack)
                {
                    OrderId = Int32.Parse(Request.QueryString["id"] ?? "10");

                    HttpCookie orderInfo = new HttpCookie("order");
                    orderInfo["id"] = OrderId.ToString();
                    Response.Cookies.Add(orderInfo);

                    using(var db = new syasyadbEntities())
                    {
                        var data = db.Orders.Find(OrderId);
                        lblFinalTotal.Text = data.Total.ToString();
                        Total = data.Total;
                        Response.Cookies["Total"].Value = data.Total.ToString();
                        lblSubtotal.Text = data.Total.ToString();
                    }
               
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "failalert('Failure','Something wrong.');", true);
            }
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            if (payMethod.Value != "tab-card")
            {
                Session["Bank"] = radioBank.SelectedItem.Value;
                Session["Total"] = Session["Total"] ?? 100.00;
                Response.Redirect("Bank.aspx");
            }
            else
            {
                if (txtcardHolder.Text == "INNI" && txtCardNo1.Text == "4111" && txtCardNo2.Text == "1111" &&
                    txtCardNo3.Text == "1111" && txtCardNo4.Text == "1111" && txtCvv.Text == "123" &&
                    DateTime.Parse(txtMonth.Text) > DateTime.Today)
                {
                    string orderID = "";
                    HttpCookie httpCookie = Request.Cookies["order"];

                    if (httpCookie != null)
                    {
                        orderID = httpCookie["id"];
                    }
                    string content = "Dear Valued Customer, your Order " + orderID + " is comfirmed and placed!"
                        + "\n\n" + "Sub Total : " + Total.ToString()  + "\n" + "Discount  :   0.00"
                        + "\n" + "Total       : " + Total.ToString() + "\n\n" + "We're excited for you to receive your order and will notify you once it is on its way!"
                        + "If you faced any problem, feel free to contact us." + "\n\n" + "Thanks\n\nBest Regards,\nTAYY Art Work";
                    //var result = new Email().SendPaymentEmail(Session["Email"].ToString(), content);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Payment Successfully.');", true);
                    StorePayment(orderID);
                    Session.Remove("CartList");
                    Session.Remove("Email");
                    //Response.Redirect("~/Customer/HistoryDetails.aspx?id=" + orderID);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "failalert('Failure','Payment failure.');", true);
                }
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect("CustomerHomepage.aspx");
        }

        private void StorePayment(string orderID)
        {
            try
            {
                using(var db = new syasyadbEntities())
                {
                    db.Payments.Add(new Payment { OrderID = Int32.Parse(orderID), PaymentDate = DateTime.Now, PaymentMethodID = 1, isPaid = true });

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}