using Portal_v1._0._1.Identity;
using Portal_v1._0._1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Portal_v1._0._1.Controllers
{
    public class MailController
    {
        public void MailGonderAsync(string mesaj, string key)
        {
           
            if (!String.IsNullOrEmpty(key))
            {

                var message = new MailMessage();
                if (key == "izin")
                {
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["PortalIzinMailAdres"]));
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["KalderaMailAdres"]));
                }
                else if (key == "masraf")
                {
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["PortalMasrafMailAdres"]));
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["KalderaMailAdres"]));
                }
                else
                {
                    message.To.Add(new MailAddress(key));
                }
                message.From = new MailAddress("kaldera.portal@gmail.com");  // replace with valid value
                message.Subject = "Portal Mesajı";
                message.Body = mesaj;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "kaldera.portal@gmail.com",
                        Password = "kalderasoft123"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
        }
    }
}