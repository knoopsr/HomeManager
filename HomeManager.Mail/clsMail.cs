using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentEmail.Core.Interfaces; // <- nodig voor ISender
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Mail;
using Microsoft.CodeAnalysis.Emit;

namespace HomeManager.Mail
{
    public class clsMail
    {
        /// <summary>
        /// Verstuur een e-mail
        /// </summary>
        /// <param name="MailModel">Het model met e-mailinformatie</param>
        /// <returns>Geeft `true` terug als de e-mail succesvol is verzonden, anders `false`</returns>
        public static async Task<bool> SendEmail(clsMailModel MailModel)
        {
            try
            {
                var sender = new SmtpSender(() => new SmtpClient("localhost")
                {
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 25
                });

                StringBuilder htmlBody = new StringBuilder();
                htmlBody.AppendLine("<html>");
                htmlBody.AppendLine("<head>");
                htmlBody.AppendLine("<title>HomeManager</title>");
                htmlBody.AppendLine("</head>");
                htmlBody.AppendLine("<body style='background-color: #d3d3d3; font-family: Arial, sans-serif; color: #00223e; padding: 20px;'>");
                htmlBody.AppendLine("  <div style='background-color: #024f87; padding: 20px; border-radius: 8px; max-width: 600px; margin: auto;'>");
                htmlBody.AppendLine("    <h1 style='color: LightGray;'>HomeManager</h1>");
                htmlBody.AppendLine("    <p style='color: LightGray; font-size: 14px;'>Hi " + MailModel.MailToName + ",</p>");
                htmlBody.AppendLine("    <p style='color: LightGray; font-size: 14px;'>" + MailModel.Body + "</p>");
                htmlBody.AppendLine("  </div>");
                htmlBody.AppendLine("  <div style='color: Gray; font-size: 12px; text-align: center; margin-top: 20px;'>");
                htmlBody.AppendLine("    <p>Thank you for using HomeManager!</p>");
                htmlBody.AppendLine("  </div>");
                htmlBody.AppendLine("</body>");
                htmlBody.AppendLine("</html>");

                Email.DefaultSender = sender;

                var email = Email
                    .From(MailModel.MailFromEmail)
                    .To(MailModel.MailToEmail, MailModel.MailToName)
                    .Subject(MailModel.Subject)
                    .Body(htmlBody.ToString(), isHtml: true);




                // **Bijlagen toevoegen vóór het verzenden**
                if (MailModel.Attachments != null && MailModel.Attachments.Count > 0)
                {
                    foreach (var attachment in MailModel.Attachments)
                    {
                        email.Attach(new FluentEmail.Core.Models.Attachment
                        {
                            Data = new MemoryStream(attachment.FileData),  // Zet byte[] om naar een MemoryStream
                            ContentType = attachment.ContentType,
                            Filename = attachment.FileName
                        });

                    }
                }


                // **Verstuur de e-mail**
                var result = await email.SendAsync();

                if (result.Successful)
                {
                    return true;
                }
                else
                {
                    
                    Console.WriteLine("Fouten bij het versturen van de e-mail: " + string.Join(", ", result.ErrorMessages));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Er is een fout opgetreden bij het versturen van de e-mail: " + ex.Message);
                return false;
            }
        }

    }
}
