using HomeManager.DataService.Personen;
using HomeManager.Mail;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace HomeManager.MailService
{
    /// <summary>
    /// Verantwoordelijk voor het versturen van e-mails binnen HomeManager.
    /// Ondersteunt zowel wachtwoordcommunicatie als exceptionmeldingen.
    /// </summary>
    public class clsMailService
    {
        /// <summary>
        /// Stuurt een e-mail met nieuwe logingegevens naar alle gekoppelde e-mailadressen van een persoon.
        /// </summary>
        /// <param name="accountModel">Het account met de login- en wachtwoordinformatie.</param>
        /// <param name="persoonModel">De persoon aan wie de e-mail gericht is.</param>
        /// <returns>Een lijst van e-mailadressen waarnaar succesvol werd verzonden.</returns>
        public async Task<List<string>> SendNewPassToPerson(clsAccountModel accountModel, clsPersoonModel persoonModel)
        {
            clsEmailAdressenDataService emailAdressenService = new clsEmailAdressenDataService();
            ObservableCollection<clsEmailAdressenModel> emailAdressen = emailAdressenService.GetByPersoonID(persoonModel.PersoonID);

            List<string> verzondenEmails = new List<string>();

            foreach (var email in emailAdressen)
            {
                clsMailModel mailModel = new clsMailModel
                {
                    MailToName = persoonModel.ToString(),
                    MailFromEmail = "admin@HomeManager.be",
                    MailToEmail = email.Emailadres,
                    Subject = "Login gegevens",
                    Body = $"U kan de volgende gegevens gebruiken om in te loggen:<br />" +
                           $"Login: {accountModel.Login}<br />" +
                           $"Wachtwoord: {accountModel.Wachtwoord}<br />" +
                           $"Gelieve dit wachtwoord te wijzigen na de eerste keer inloggen."
                };

                bool emailVerzonden = await clsMail.SendEmail(mailModel);
                if (emailVerzonden)
                {
                    verzondenEmails.Add(email.Emailadres);
                }
            }

            return verzondenEmails;
        }

        /// <summary>
        /// Stuurt een exceptionmelding naar een opgegeven e-mailadres.
        /// </summary>
        /// <param name="emailModel">Het e-mailadresmodel waarnaar de melding moet worden verzonden.</param>
        /// <param name="exception">De exceptiongegevens.</param>
        /// <param name="isDetailed">
        /// Indien <c>true</c>, wordt een uitgebreide e-mail verzonden (voor ontwikkelaars). 
        /// Indien <c>false</c>, wordt een beknopte e-mail verzonden (voor eindgebruikers).
        /// </param>
        public async void SendExceptionToMailAddress(clsEmailAdressenModel emailModel, clsExceptionsModel exception, bool isDetailed = false)
        {
            if (emailModel == null)
            {
                MessageBox.Show("No emailAddress was found by SendExceptionToMailAddress.");
                return;
            }

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine($"Exception ID: {exception.ExceptionID}");
            mailBody.AppendLine($"Account ID: {exception.AccountID}");
            mailBody.AppendLine($"AccountName: {exception.AccountName}");
            mailBody.AppendLine($"ExceptionName: {exception.ExceptionName}");

            if (!isDetailed) // User mail
            {
                clsMailModel mailModel = new clsMailModel
                {
                    MailToName = "Gebruiker",
                    Subject = $"Unhandled exception caught, {DateTime.Now}",
                    MailToEmail = emailModel.Emailadres,
                    MailFromEmail = "NoReply@HomeManager.be",
                    Body = mailBody.ToString()
                };

                bool isSuccessful = await clsMail.SendEmail(mailModel);
                if (!isSuccessful)
                {
                    MessageBox.Show($"SendExceptionToMailAddress unsuccessfully tried to send normal mail to {emailModel.Emailadres}");
                }
            }
            else // Developer mail
            {
                mailBody.AppendLine($"Module: {exception.Module}");
                mailBody.AppendLine($"Source: {exception.Source}");
                mailBody.AppendLine($"TargetSite: {exception.TargetSite}");
                mailBody.AppendLine($"ExceptionMessage: {exception.ExceptionMessage}");
                mailBody.AppendLine($"InnerExceptionMessage: {exception.InnerExceptionMessage}");
                mailBody.AppendLine($"StackTrace: {exception.StackTrace}");
                mailBody.AppendLine($"DotNetAssembly: {exception.DotNetAssembly}");

                clsMailModel mailModel = new clsMailModel
                {
                    MailToName = "Developer",
                    Subject = $"Unhandled exception caught, {DateTime.Now}",
                    MailToEmail = emailModel.Emailadres,
                    MailFromEmail = "NoReply@HomeManager.be",
                    Body = mailBody.ToString()
                };

                bool isSuccessful = await clsMail.SendEmail(mailModel);
                if (!isSuccessful)
                {
                    MessageBox.Show($"SendExceptionToMailAddress unsuccessfully tried to send detailed mail to {emailModel.Emailadres}");
                }
            }
        }
    }
}
