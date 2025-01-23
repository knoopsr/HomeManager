using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Security;
using HomeManager.Services.Security;
using HomeManager.View;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using HomeManager.Mail;

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

        private string _login ;
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

        private void Execute_login_Command(object? obj)
        {
            _loginModel = MijnService.GetByLogin(Login, Wachtwoord);

            if (_loginModel != null)
            {
                if (_loginModel.IsLock)
                {
                    MessageBox.Show("Account is locked");
                }
                else if (_loginModel.IsNew)
                {
                    _objHome = obj;
                    clsMessenger.Default.Send<clsLoginModel>(_loginModel);
                    HomeManager.Agenda.Helpers.clsMessenger.Default.Send<clsLoginModel>(_loginModel);
                    _dialogService.ShowNewPassWordView();
                }
                else
                {
                    OpenMainWindow(obj);
                }


                clsMailModel _itemToInsert = new clsMailModel()
                {
                    MailToEmail = "test@test.com",
                    MailToName = _loginModel.VoorNaam,
                    Subject = "Login",
                    Body = "Login: " + _loginModel.Naam + " is ingelogd",
                };
                clsMail.SendEmail(_itemToInsert);
            }
        }

        private void OpenMainWindow(object? obj)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            clsMessenger.Default.Send<clsLoginModel>(_loginModel);

            if (obj is Window winLogin)
            {
                winLogin.Close();
            }
        }

    }
}

