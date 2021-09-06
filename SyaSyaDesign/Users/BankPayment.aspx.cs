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
            Response.Redirect("Products.aspx");
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
                SentEmail();
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
            HttpCookie httpCookie = Request.Cookies["order"];
            var orderID = "1001";
            if (httpCookie != null)
            {
                orderID = httpCookie["id"];
            }
            string content = "Dear Valued Customer, your Order " + orderID + " is comfirmed and placed!" +
                       "\n\n" + "We're excited for you to receive your order and will notify you once it is on its way!"
                       + "If you faced any problem, feel free to contact us." + "\n\n" + "Thanks\n\nBest Regards,\nSyaSyaDesign";

            string email = Session["userEmail"].ToString();
            new Email().SendEmail(email, content, "Payment Successful");

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('Success','Payment Successfully.');", true);
            Response.Redirect(String.Format("~/Users/PurchaseSummary.aspx?OrderID={0}", orderID));
        }

        private void StorePayment()
        {
            try
            {
                using (var db = new syasyadbEntities())
                {
                    HttpCookie httpCookie = Request.Cookies["order"];
                    var id = Int32.Parse(httpCookie["id"]);
                    db.Payments.Add(new Payment() { OrderID = id, PaymentDate = DateTime.Now, PaymentMethodID = 1, isPaid = true });
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