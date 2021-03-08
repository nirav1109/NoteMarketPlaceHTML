using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using NotesMarketPlace.Models;
namespace NotesMarketPlace.EmailTemplates
{
    public class emailVerification
    {
        public static void SendVerifyLinkEmail(Users objUser, string activationlink)
        {
            var fromEmail = new MailAddress("supportedemail", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress(objUser.EmailID);
            var fromEmailPassword = "Password"; // Replace with actual password
            string subject = "Notes Marketplace - Email Verification";
            string msg = "Hello " + objUser.FirstName + " " + objUser.LastName + "<br/>";
            msg += "<br/>Thank you for signing up with us.Please click on below link to verify your email address and to do login.<br/>";
            msg += "<a href='" + activationlink + "'> Click To VerifyEmail</a>";
            msg += "<br/><br/>Regards,<br/>";
            msg += "Notes Marketplace";

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
