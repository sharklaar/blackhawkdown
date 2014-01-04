using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace BHDSite.Library
{
    public class Email
    {
        public void Send(Contact contact)
        {
            MailMessage mail = new MailMessage(
                contact.From,
                "marc@blackhawkdown.org.uk",
                contact.Subject,
                string.Concat(contact.Message, " FROM: ", contact.From));
                        
            NetworkCredential cred = new System.Net.NetworkCredential("websiteadmin@blackhawkdown.org.uk", "kickintheface");           

            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = 20000;
            mailClient.Credentials = cred;
            try
            {
                mailClient.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}