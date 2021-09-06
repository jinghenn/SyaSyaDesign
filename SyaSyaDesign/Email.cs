using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SyaSyaDesign
{
    public class Email
    {
        public bool SendEmail(String email, String mailbody, String subject)
        {
            try
            {
                string to = email; //To address    
                string from = "sya266696@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                message.Subject = subject;
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("sya266696@gmail.com", "syasya12345");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}