using HomeManager.DataService.Personen;
using HomeManager.Mail;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeManager.MailService
{
    public class clsMailService
    {
        public async Task<List<string>> SendNewPassToPerson(clsAccountModel accountModel, clsPersoonModel persoonModel)
        {
            // Haal e-mailadressen op
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

            if (!isDetailed) // Normal - User mail
            {
                clsMailModel mailModel = new clsMailModel()
                {
                    MailToName = "Gebruiker",
                    Subject = $"Unhandled exception caught, {DateTime.Now.ToString()}",
                    MailToEmail = emailModel.Emailadres,
                    MailFromEmail = "NoReply@HomeManager.be",
                    Body = mailBody.ToString()
                };

                bool isSuccessful = await clsMail.SendEmail(mailModel);
                if (!isSuccessful)
                {
                    MessageBox.Show($"SendExceptionToMailAddress unsuccesfully tried to send normal mail to {emailModel.Emailadres}");
                }
            }
            else // Detailed - Developer mail
            {
                mailBody.AppendLine($"Module: {exception.Module}");
                mailBody.AppendLine($"Source: {exception.Source}");
                mailBody.AppendLine($"TargetSite: {exception.TargetSite}");
                mailBody.AppendLine($"ExceptionMessage: {exception.ExceptionMessage}");
                mailBody.AppendLine($"InnerExceptionMessage: {exception.InnerExceptionMessage}");
                mailBody.AppendLine($"StackTrace: {exception.StackTrace}");
                mailBody.AppendLine($"DotNetAssembly: {exception.DotNetAssembly}");

                clsMailModel mailModel = new clsMailModel()
                {
                    MailToName = "Developer",
                    Subject = $"Unhandled exception caught, {DateTime.Now.ToString()}",
                    MailToEmail = emailModel.Emailadres,
                    MailFromEmail = "NoReply@HomeManager.be",
                    Body = mailBody.ToString()
                };

                bool isSuccessful = await clsMail.SendEmail(mailModel);
                if (!isSuccessful)
                {
                    MessageBox.Show($"SendExceptionToMailAddress unsuccesfully tried to send detailed mail to {emailModel.Emailadres}");
                }
            }
        }
    }
}
