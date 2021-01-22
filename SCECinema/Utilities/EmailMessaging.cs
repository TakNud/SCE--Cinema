using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using System.Net;

namespace SCECinema.Utilities
{
    public class EmailMessaging
    {

        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {
            //Create an email client to send the emails     
            var client = new SmtpClient("smtp.gmail.com", 587) {Credentials = new NetworkCredential("almog****@gmail.com", "*****"),EnableSsl = true};

           
            
            //Create an email address object for the sender address     
            MailAddress senderEmail = new MailAddress("SCE-Cinema@gmail.com", "Thank you for Shoping");
            MailMessage mm = new MailMessage
            {
                Subject = "SCE-Cinema - " + emailSubject,
                Sender = senderEmail,
                From = senderEmail
            };
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = emailBody;
            client.Send(mm);
        }

    }
}