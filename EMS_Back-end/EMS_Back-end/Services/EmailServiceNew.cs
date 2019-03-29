using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace EMS_Back_end.Services
{
    public class EmailServiceNew: IIdentityMessageService
    {
        public static async Task SendEmail(IdentityMessage message, MemoryStream attachedFile = null)
        {
            // Plug in your email service here to send an email. 
            await ConfigSendGridasync(message, attachedFile);
        }

        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email. 
            await ConfigSendGridasync(message);
        }

        private static async Task ConfigSendGridasync(IdentityMessage message, MemoryStream attachedFile = null)
        {
            try
            {
                string fromEmail = "susue0011@gmail.com";
                string fromPassword = "Haku1234";
                MailAddress fromAddress = new MailAddress("susue0011@gmail.com", "Bộ phận Khảo thí");
                MailAddress toAddress = new MailAddress(message.Destination, message.Destination);
                SmtpClient smtp = new SmtpClient { Host = "smtp.gmail.com", Port = 587, EnableSsl = true, DeliveryMethod = SmtpDeliveryMethod.Network, UseDefaultCredentials = false, Credentials = new NetworkCredential(fromEmail, fromPassword) };
                MailMessage messageSend = new MailMessage(fromAddress, toAddress);
                messageSend.Subject = message.Subject;
                messageSend.IsBodyHtml = true;
                messageSend.Body = message.Body;
                if (attachedFile != null)
                {
                    Attachment data = new Attachment(attachedFile, "Data.xlsx");
                    messageSend.Attachments.Add(data);
                }
                smtp.Send(messageSend);
            }
            finally
            {
            }

        }
    }
}
