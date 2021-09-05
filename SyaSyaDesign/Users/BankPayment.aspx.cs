using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SyaSyaDesign
{
    public partial class BankPayment : System.Web.UI.Page
    {
        int OrderId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BankName.Text = Session["Bank"].ToString();
                HttpCookie httpCookie = Request.Cookies["order"];

                if (httpCookie != null)
                {
                    OrderId = Int32.Parse(httpCookie["id"]);
                }
                Amount.Text = Session["Total"].ToString().Substring(2);
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Product.aspx");
        }

        protected void PAC_Click(object sender, EventArgs e)
        {
            AcceptBtn.Enabled = true;
            PAC.Visible = true;
        }

        protected void Accept_Click(object sender, EventArgs e)
        {
            if (TxtPAC.Text == "123456")
            {
                StorePayment();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PopupMsg", "failalert('Error','Invalid PAC No.')", true);
            }
        }

        private void SentEmail()
        {

            if (Request.Cookies["orderID"] != null)
            {
                OrderId = Request.Cookies["order"].Value == null ? 0 : Int32.Parse(Request.Cookies["order"].Value);
            }
            //string content = "Dear Valued Customer, your Order " + orderID + " is comfirmed and placed!"
            //    + "\n\n" + "Sub Total : " + Session["Subtotal"].ToString() + "\n" + "Discount  : " + Session["Discount"].ToString()
            //    + "\n" + "Total       : " + Session["Total"].ToString() + "\n\n" + "We're excited for you to receive your order and will notify you once it is on its way!"
            //    + "If you faced any problem, feel free to contact us." + "\n\n" + "Thanks\n\nBest Regards,\nTAYY Art Work";
            //var result = new Email().SendPaymentEmail(Session["Email"].ToString(), content);
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Payment Successfully.')", true);

            //Session.Remove("Email");
            //Response.Redirect("~/Customer/HistoryDetails.aspx?id=" + OrderId);
        }

        private void StorePayment()
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    var newObj = new Payment();
                    db.Payments.Add(new Payment() { OrderID = Int32.Parse(Request.Cookies["order"].Value), PaymentDate = DateTime.Now, PaymentMethodID = 1, isPaid = true });
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