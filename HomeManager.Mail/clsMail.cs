using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Security;

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

                // HTML-template maken als string
                StringBuilder htmlBody = new StringBuilder();
                htmlBody.AppendLine("<html>");
                htmlBody.AppendLine("<head>");
                htmlBody.AppendLine("<title>HomeManager</title>");
                htmlBody.AppendLine("</head>");
                htmlBody.AppendLine("<body style='background-color: #d3d3d3; font-family: Arial, sans-serif; color: #00223e; padding: 20px;'>"); // Achtergrondkleur uit applicatie
                htmlBody.AppendLine("  <div style='background-color: #024f87; padding: 20px; border-radius: 8px; max-width: 600px; margin: auto;'>"); // Container met witte achtergrond
                htmlBody.AppendLine("    <h1 style='color: LightGray;'>HomeManager</h1>"); // Titel met applicatiekleur voor titels
                htmlBody.AppendLine("    <p style='color: LightGray; font-size: 14px;'>Hi " + MailModel.MailToName + ",</p>"); // Paragraaf tekstkleur
                htmlBody.AppendLine("    <p style='color: LightGray; font-size: 14px;'>" + MailModel.Body + "</p>"); // Paragraaf met inhoud
                htmlBody.AppendLine("  </div>");
                htmlBody.AppendLine("  <div style='color: Gray; font-size: 12px; text-align: center; margin-top: 20px;'>"); // Footer met grijze kleur
                htmlBody.AppendLine("    <p>Thank you for using HomeManager!</p>");
                htmlBody.AppendLine("  </div>");
                htmlBody.AppendLine("</body>");
                htmlBody.AppendLine("</html>");

                Email.DefaultSender = sender;

                // Verzend de e-mail zonder een template renderer
                var email = await Email
                    .From(MailModel.MailFromEmail)
                    .To(MailModel.MailToEmail, MailModel.MailToName)
                    .Subject(MailModel.Subject)                    
                    .Body(htmlBody.ToString(), isHtml: true)
                    .SendAsync();

                if (email.Successful)
                {
                    return true; // E-mail is succesvol verzonden
                }
                else
                {
                    Console.WriteLine("Fouten bij het versturen van de e-mail: " + string.Join(", ", email.ErrorMessages));
                    return false; // E-mail is niet succesvol verzonden
                }
            }
            catch (Exception ex)
            {
                // Log de foutmelding of voer foutafhandeling uit
                Console.WriteLine("Er is een fout opgetreden bij het versturen van de e-mail: " + ex.Message);
                return false;
            }
        }
    }
}
