using System;
using System.Net;
using System.Net.Mail;
using BHDSite.Library;
using Postal;

namespace BhdResponsiveSite.Library
{
    public class Email
    {
        public void Send(Contact contact)
        {
            var mail = new MailMessage(
                contact.From,
                "marc@blackhawkdown.org.uk",
                contact.Subject,
                string.Concat(contact.Message, " FROM: ", contact.From));
                        
            SendMessage(mail);
        }

        public void SendAutoResponse(Contact contact)
        {
            var mail = new MailMessage(
                "websiteadmin@blackhawkdown.org.uk",
                contact.From,
                "Your free tracks from BlackHawkDown",
                contact.Message
                );

            SendMessage(mail);
        }

        private void SendMessage(MailMessage mail)
        {
            var cred = new NetworkCredential("websiteadmin@blackhawkdown.org.uk", "kickintheface");

            var mailClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Timeout = 20000,
                Credentials = cred
            };
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