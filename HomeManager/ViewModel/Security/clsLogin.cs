using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.Messages;
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
    public class clsLogin : clsCommonModelPropertiesBase
    {
        clsloginDataService MijnService;
        clsLoginModel _loginModel;
        private clsDialogService _dialogService;
        private object _objHome;


        public ICommand cmdLogin { get; set; }
        public ICommand cmdAnnuleer { get; set; }
        public ICommand cmdClose { get; set; }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();

            }
        }

        private string _wachtwoord;
        public string Wachtwoord
        {
            get { return _wachtwoord; }
            set
            {
                _wachtwoord = value;
                OnPropertyChanged();
            }
        }



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
        private bool CanExecute_close_Command(object? obj)
        {
            return true;
        }

        private void Execute_close_Command(object? obj)
        {
            Application.Current.Shutdown();
        }

        private bool CanExecute_annuleer_Command(object? obj)
        {
            return true;
        }

        private void Execute_annuleer_Command(object? obj)
        {
            Login = string.Empty;
            Wachtwoord = string.Empty;
        }

        private bool CanExecute_login_Command(object? obj)
        {
            return true;
        }

        private async void Execute_login_Command(object? obj)
        {
            _loginModel = MijnService.GetByLogin(Login, Wachtwoord);

            if (_loginModel.AccountID != 0)
            {
                if (_loginModel.IsNew)
                {
                    _objHome = obj;
                    clsMessenger.Default.Send<clsLoginModel>(_loginModel);
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
                    //// account is gelockt en de admin word nu gemaild

                    ObservableCollection<clsEmailAdressenModel> emailAdressen = new ObservableCollection<clsEmailAdressenModel>();
                    clsEmailAdressenDataService emailAdressenService = new clsEmailAdressenDataService();

                    emailAdressen = emailAdressenService.GetAllbyRollName("Admin");

                    List<string> verzondenEmails = new List<string>();
                    foreach (var email in emailAdressen)
                    {

                        clsMailModel mailModel = new clsMailModel
                        {
                            MailToName = "HomeManager Admin",
                            MailFromEmail = "admin@HomeManager.be",
                            MailToEmail = email.Emailadres,
                            Subject = "Gebruiker " + Login + " is geblokkeerd",
                            Body = "Het systeem heeft de gebruiker <b>" + Login + "</b> geblokeerd op " + DateTime.Now + " . <br />"
                            + "De gebruiker heeft te vaak een foutief wachtwoord ingegeven. <br />"
                            + "Gelieve de gebruiker te deblokkeren in het systeem. <br />"
                            + "Dit is een automatisch gegenereerd bericht. <br />"
                            + "Gelieve niet te antwoorden op dit bericht. <br />"
                            + "Met vriendelijke groeten, <br />"
                            + "HomeManager"
                        };

                        bool emailVerzonden = await clsMail.SendEmail(mailModel);

                        if (!emailVerzonden)
                        {
                            MessageBox.Show("Er is een fout opgetreden bij het versturen van de e-mail.");
                        }
                    }
                    ;
                }
                MessageBox.Show(_loginModel.ErrorBoodschap);
            }
        }

        private void OpenMainWindow(object? obj)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();

            clsMessenger.Default.Send<clsLoginModel>(_loginModel);

            if (obj is Window winLogin)
            {
                winLogin.Close();
            }
        }

    }
}

