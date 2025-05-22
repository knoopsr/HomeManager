using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.Messages;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using HomeManager.Services.Security;
using HomeManager.View;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het beheren van het loginproces binnen de HomeManager applicatie.
    /// </summary>
    public class clsLogin : clsCommonModelPropertiesBase
    {
        #region Fields

        private clsloginDataService MijnService;
        private clsLoginModel _loginModel;
        private clsDialogService _dialogService;
        private object _objHome;

        #endregion

        #region Commands

        public ICommand cmdLogin { get; set; }
        public ICommand cmdAnnuleer { get; set; }
        public ICommand cmdClose { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Ingevoerde gebruikersnaam.
        /// </summary>
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        private string _login;

        /// <summary>
        /// Ingevoerd wachtwoord.
        /// </summary>
        public string Wachtwoord
        {
            get => _wachtwoord;
            set
            {
                _wachtwoord = value;
                OnPropertyChanged();
            }
        }
        private string _wachtwoord;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Initialiseert services en commands.
        /// </summary>
        public clsLogin()
        {
            MijnService = new clsloginDataService();
            _dialogService = new clsDialogService();

            cmdLogin = new clsCustomCommand(Execute_login_Command, CanExecute_login_Command);
            cmdAnnuleer = new clsCustomCommand(Execute_annuleer_Command, CanExecute_annuleer_Command);
            cmdClose = new clsCustomCommand(Execute_close_Command, CanExecute_close_Command);

            clsMessenger.Default.Register<clsUpdatePassWordMessages>(this, OnUpdatePassWord);

            Wachtwoord = string.Empty;
        }

        #endregion

        #region Command Methods

        private bool CanExecute_close_Command(object? obj) => true;

        private void Execute_close_Command(object? obj)
        {
            Application.Current.Shutdown();
        }

        private bool CanExecute_annuleer_Command(object? obj) => true;

        private void Execute_annuleer_Command(object? obj)
        {
            Login = string.Empty;
            Wachtwoord = string.Empty;
        }

        private bool CanExecute_login_Command(object? obj) => true;

        /// <summary>
        /// Voert login uit en opent hoofdvenster of toont wijzig-wachtwoordvenster.
        /// </summary>
        private async void Execute_login_Command(object? obj)
        {
            _loginModel = MijnService.GetByLogin(Login, Wachtwoord);

            if (_loginModel.AccountID != 0)
            {
                if (_loginModel.IsNew)
                {
                    _objHome = obj;
                    clsMessenger.Default.Send(_loginModel);
                    _dialogService.ShowNewPassWordView();
                }
                else
                {
                    OpenMainWindow(obj);
                }
            }
            else
            {
                if (_loginModel.ErrorCode == 163)
                {
                    await NotifyAdminGeblokkeerdeGebruiker();
                }
                MessageBox.Show(_loginModel.ErrorBoodschap);
            }
        }

        /// <summary>
        /// Waarschuwt de admin dat een gebruiker geblokkeerd is.
        /// </summary>
        private async Task NotifyAdminGeblokkeerdeGebruiker()
        {
            var emailAdressenService = new clsEmailAdressenDataService();
            var emailAdressen = emailAdressenService.GetAllbyRollName("Admin");

            foreach (var email in emailAdressen)
            {
                var mailModel = new clsMailModel
                {
                    MailToName = "HomeManager Admin",
                    MailFromEmail = "admin@HomeManager.be",
                    MailToEmail = email.Emailadres,
                    Subject = $"Gebruiker {Login} is geblokkeerd",
                    Body = $"Het systeem heeft de gebruiker <b>{Login}</b> geblokkeerd op {DateTime.Now}.<br />"
                        + "De gebruiker heeft te vaak een foutief wachtwoord ingegeven.<br />"
                        + "Gelieve de gebruiker te deblokkeren in het systeem.<br />"
                        + "Dit is een automatisch gegenereerd bericht.<br />"
                        + "Gelieve niet te antwoorden op dit bericht.<br />"
                        + "Met vriendelijke groeten,<br />HomeManager"
                };

                bool emailVerzonden = await clsMail.SendEmail(mailModel);
                if (!emailVerzonden)
                {
                    MessageBox.Show("Er is een fout opgetreden bij het versturen van de e-mail.");
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Opent het hoofdvenster en sluit het loginvenster.
        /// </summary>
        private void OpenMainWindow(object? obj)
        {
            var mainWindow = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            mainWindow.Show();

            clsMessenger.Default.Send(_loginModel);

            if (obj is Window winLogin)
            {
                winLogin.Close();
            }
        }

        /// <summary>
        /// Handelt de overgang af nadat een wachtwoord gewijzigd is.
        /// </summary>
        private void OnUpdatePassWord(clsUpdatePassWordMessages obj)
        {
            if (obj != null)
            {
                _dialogService.CloseNewPassWordView();
                OpenMainWindow(obj);

                if (_objHome is Window winLogin)
                {
                    winLogin.Close();
                }
            }
        }

        #endregion
    }
}
