﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NotesMarketPlace.EmailTemplates
{
    public class contactusemail
    {
         public static void ContactUs(string usersubject,string name,string comment)
        {
            var fromEmail = new MailAddress("karathiyanirav2000@gmail.com", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress("karathiyanirav@gmail.com");
            var fromEmailPassword = "Nirav@1109"; // Replace with actual password
            string subject =name +" - "+ usersubject;
            string msg = "Hello,<br/>";
            msg += comment;            
            msg += "<br/><br/>Regards,<br/>";
            msg += name;


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