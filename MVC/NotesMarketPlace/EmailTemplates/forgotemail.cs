using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NotesMarketPlace.EmailTemplates
{
    public class forgotemail
    {

        public static void SendOtpToEmail(Users u, string  otp)
        {
            var fromEmail = new MailAddress("supportedemail", "Notes Marketplace"); //system email
            var toEmail = new MailAddress(u.EmailID);
            var fromEmailPassword = "password"; //actual password
            string subject = "New Temporary Password has been created for you";
            string msg = "Hello " + u.FirstName + " " + u.LastName + "<br/>";
            msg += "We have generated a new password for you <br/>";
            msg += "Password: " + otp;
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
