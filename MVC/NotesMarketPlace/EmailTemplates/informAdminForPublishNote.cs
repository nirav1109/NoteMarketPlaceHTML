using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NotesMarketPlace.EmailTemplates
{
    public class informAdminForPublishNote
    {
        public static void sellerRequestToAdmin(string sellerName, string notetitle)
        {
            var fromEmail = new MailAddress("supportedEmail", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress("AdminEmail");
            var fromEmailPassword = "Password"; // Replace with actual password
            string subject = sellerName + " sent his note for review";
            string msg = "Hello Admin,<br/>";
            msg += "We want to inform you that," + sellerName + " sent his note " + notetitle + " for review.Please look at the notes and take required actions.";
            msg += "<br/><br/>Regards,<br/> Notes MarketPlace";



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = msg,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}