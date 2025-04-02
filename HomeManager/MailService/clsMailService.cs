using HomeManager.DataService.Personen;
using HomeManager.Mail;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
