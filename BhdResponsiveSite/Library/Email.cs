using System;
using System.Net;
using System.Net.Mail;
using BhdResponsiveSite.Models;

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

        private void SendMessage(MailMessage mail)
        {
            var mailClient = GetClient();

            try
            {
                mailClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public SmtpClient GetClient()
        {
            var cred = new NetworkCredential("websiteadmin@blackhawkdown.org.uk", "kickintheface");

            return new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Timeout = 20000,
                Credentials = cred
            };
        }


        public string SendAutoResponse(EmailOnlyModel emailVm, string emailType)
        {
            var emailHelper = new Email();
            var client = emailHelper.GetClient();

            dynamic email = new Postal.Email(emailType);
            email.To = emailVm.Email;
            email.From = "BlackHawkDown";
            var service = Postal.Email.CreateEmailService();
            var mailToSend = service.CreateMailMessage(email);

            try
            {
                client.Send(mailToSend);
                return "success";
            }
            catch (Exception ex)
            {
                return "fail: " + ex.Message;
            }
        }
    }
}