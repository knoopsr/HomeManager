using HomeManager.Common;

namespace HomeManager.Model.Mail
{
    /// <summary>
    /// Model voor het verzenden van e-mails binnen HomeManager.
    /// Bevat ontvanger, afzender, onderwerp, berichtinhoud en eventuele bijlagen.
    /// </summary>
    public class clsMailModel : clsCommonModelPropertiesBase
    {
        #region Properties

        /// <summary>
        /// Het e-mailadres van de ontvanger.
        /// </summary>
        private string _mailToEmail;
        public string MailToEmail
        {
            get => _mailToEmail;
            set
            {
                _mailToEmail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Het e-mailadres van de afzender.
        /// </summary>
        private string _mailFromEmail;
        public string MailFromEmail
        {
            get => _mailFromEmail;
            set
            {
                _mailFromEmail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// De naam van de ontvanger (voor weergave in de e-mail).
        /// </summary>
        private string _mailToName;
        public string MailToName
        {
            get => _mailToName;
            set
            {
                _mailToName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Het onderwerp van de e-mail.
        /// </summary>
        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// De HTML- of tekstinhoud van de e-mail.
        /// </summary>
        private string _body;
        public string Body
        {
            get => _body;
            set
            {
                _body = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Lijst met bijlagen (bestanden) die aan de e-mail toegevoegd worden.
        /// </summary>
        public List<clsAttachmentModel> Attachments { get; set; } = new List<clsAttachmentModel>();

        #endregion
    }
}
