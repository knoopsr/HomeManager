using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using HomeManager.Model.Mail;
using Attachment = FluentEmail.Core.Models.Attachment;

namespace HomeManager.Mail
{
    /// <summary>
    /// Klasse voor het verzenden van e-mails via SMTP.
    /// </summary>
    public class clsMail
    {
        #region Public Methods

        /// <summary>
        /// Verstuurt een e-mail met de opgegeven gegevens in het mailmodel.
        /// </summary>
        /// <param name="MailModel">Het e-mailmodel met verzendgegevens en inhoud.</param>
        /// <returns><c>true</c> als de e-mail succesvol werd verzonden; anders <c>false</c>.</returns>
        public static async Task<bool> SendEmail(clsMailModel MailModel)
        {
            try
            {
                #region Configure Sender

                var sender = new SmtpSender(() => new SmtpClient("localhost")
                {
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 25
                });

                Email.DefaultSender = sender;

                #endregion

                #region Opbouw HTML-body

                var htmlBody = new StringBuilder();
                htmlBody.AppendLine("<html>");
                htmlBody.AppendLine("<head><title>HomeManager</title></head>");
                htmlBody.AppendLine("<body style='background-color: #d3d3d3; font-family: Arial, sans-serif; color: #00223e; padding: 20px;'>");
                htmlBody.AppendLine("<div style='background-color: #024f87; padding: 20px; border-radius: 8px; max-width: 600px; margin: auto;'>");
                htmlBody.AppendLine($"<h1 style='color: LightGray;'>HomeManager</h1>");
                htmlBody.AppendLine($"<p style='color: LightGray; font-size: 14px;'>Hi {MailModel.MailToName},</p>");
                htmlBody.AppendLine($"<p style='color: LightGray; font-size: 14px;'>{MailModel.Body}</p>");
                htmlBody.AppendLine("</div>");
                htmlBody.AppendLine("<div style='color: Gray; font-size: 12px; text-align: center; margin-top: 20px;'>");
                htmlBody.AppendLine("<p>Thank you for using HomeManager!</p>");
                htmlBody.AppendLine("</div></body></html>");

                #endregion

                #region Samenstellen e-mail

                var email = Email
                    .From(MailModel.MailFromEmail)
                    .To(MailModel.MailToEmail, MailModel.MailToName)
                    .Subject(MailModel.Subject)
                    .Body(htmlBody.ToString(), isHtml: true);

                if (MailModel.Attachments != null && MailModel.Attachments.Count > 0)
                {
                    foreach (var attachment in MailModel.Attachments)
                    {
                        email.Attach(new Attachment
                        {
                            Data = new MemoryStream(attachment.FileData),
                            ContentType = attachment.ContentType,
                            Filename = attachment.FileName
                        });
                    }
                }

                #endregion

                #region Verzenden

                var result = await email.SendAsync();
                if (!result.Successful)
                {
                    Debug.WriteLine("Fout bij verzenden: " + string.Join(", ", result.ErrorMessages));
                }
                return result.Successful;

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception bij e-mailverzending: " + ex.Message);
                return false;
            }
        }

        #endregion
    }
}
