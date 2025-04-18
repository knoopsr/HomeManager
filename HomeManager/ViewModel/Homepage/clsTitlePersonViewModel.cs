using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.Model.Homepage;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using HomeManager.Services;
using HomeManager.View;
using HomeManager.View.Exceptions;
using HomeManager.View.Security;
using HomeManager.View.StickyNotes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTitlePersonViewModel : clsCommonModelPropertiesBase
    {

        clsBackupDataService MijnBackupService;
        clsEmailAdressenDataService MijnEmailAdressenService;

        private clsLoginModel _loginModel;

        private bool CanBackup = true;
        private clsDialogService _DialogService;

        public ICommand cmdAfmelden { get; set; }
        public ICommand cmdBackup { get; set; }
        public ICommand cmdUnLockUser { get; set; }
        public ICommand cmdLogs { get; set; }
        public ICommand cmdExceptions { get; set; }

        // Public property die toegankelijk is voor binding
        public clsLoginModel LoginModel
        {
            get { return _loginModel; }
            set
            {
                if (_loginModel != value)
                {
                    _loginModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<clsBackupModel> _mijnBackupCollectie;
        public ObservableCollection<clsBackupModel> MijnBackupCollectie
        {
            get { return _mijnBackupCollectie; }
            set
            {
                _mijnBackupCollectie = value;
                OnPropertyChanged();
            }
        }

        public clsTitlePersonViewModel()
        {
            _DialogService = new clsDialogService();
            MijnBackupService = new clsBackupDataService();
            MijnEmailAdressenService = new clsEmailAdressenDataService();
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateTitlePersonReceived);

            cmdAfmelden = new clsCustomCommand(ExecuteAfmelden, CanExecuteAfmelden);
            cmdBackup = new clsCustomCommand(ExecuteBackup, CanExecuteBackup);
            cmdUnLockUser = new clsCustomCommand(ExecuteUnLockUser, CanExecuteUnLockUser);
            cmdLogs = new clsCustomCommand(ExecuteLogs, CanExecuteLogs);
            cmdExceptions = new clsCustomCommand(ExecuteExceptions, CanExecuteExceptions);
        }

        private bool CanExecuteExceptions(object? obj)
        {
            clsPermissionChecker _permissionChecker = new clsPermissionChecker();
            return _permissionChecker.HasPermission("711"); //RechtenCode: "Ontgrendel computer?"
        }

        private void ExecuteExceptions(object? obj)
        {
            _DialogService.ShowDialog(new ucExceptions(), "Overzicht Exceptions");
        }

        private bool CanExecuteLogs(object? obj)
        {
            clsPermissionChecker _permissionChecker = new clsPermissionChecker();
            return _permissionChecker.HasPermission("711");
        }

        private void ExecuteLogs(object? obj)
        {
            _DialogService.ShowDialog(new ucButtonLogging(), "Overzicht Button Logging");
        }

        private bool CanExecuteUnLockUser(object? obj)
        {
            clsPermissionChecker _permissionChecker = new clsPermissionChecker();
            return _permissionChecker.HasPermission("712");
        }

        private void ExecuteUnLockUser(object? obj)
        {
            _DialogService.ShowDialog(new ucUnlockUser(),"Ontgrendel Gebruiker");
        }

        private bool CanExecuteBackup(object? obj)
        { 
            if (CanBackup)
            {
                clsPermissionChecker _permissionChecker = new clsPermissionChecker();
                return _permissionChecker.HasPermission("710");
            }
            else
            {
                return false;
            }
        }

        private async void ExecuteBackup(object? obj)
        {
            try
            {
                CanBackup= false;
                // Wacht asynchroon op het resultaat van de GetAll() methode
                MijnBackupCollectie = await MijnBackupService.CreateBackup();


                string link ="https://homemanager.knoopsr.be/" + MijnBackupCollectie[0].Path;      

               ObservableCollection<clsEmailAdressenModel> _emailAdressen =  MijnEmailAdressenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);

                if (_emailAdressen.Count == 0)
                {
                    MessageBox.Show("Geen e-mailadressen gevonden voor deze gebruiker." + Environment.NewLine + "Backup is gemaakt: " + link);
                    return;
                }

                List<string> _error = new List<string>();

      
                foreach (clsEmailAdressenModel email in _emailAdressen)
                {
                    clsMailModel mailModel = new clsMailModel
                    {
                        MailToName = clsLoginModel.Instance.VoorNaam,
                        MailToEmail = email.Emailadres,
                        MailFromEmail = "NoReplyBackup@HomeManeger.be",

                        Subject = "Backup Gemaakt",
                        Body = "Backup is gemaakt:\n" + Environment.NewLine + "<a href='" + link + "'>Download Backup</a>"
                    };

                    bool emailVerzonden = await clsMail.SendEmail(mailModel);

                    if (emailVerzonden)
                    {
                        _error.Add("E-mail verzonden naar: " + email.Emailadres);
                    }
                    else
                    {
                        _error.Add("E-mail niet verzonden naar: " + email.Emailadres);
                    }
                }

                string errorMessage = string.Join(Environment.NewLine, _error);
                MessageBox.Show(errorMessage, "Backup E-mail Status", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                // Foutafhandeling voor als de backup mislukt
                MessageBox.Show("Er is een fout opgetreden tijdens het maken van de backup: " + ex.Message);
            }
            finally
            {
                CanBackup = true;
            }
        }

        private bool CanExecuteAfmelden(object? obj)
        {
            return true;
        }

        private void ExecuteAfmelden(object? obj)
        {
            OpenLoginWindow(obj);
        }

        private void OpenLoginWindow(object? obj)
        {

            winLogin _winLogin = new winLogin();
            _winLogin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           _winLogin.txtWachtwoord.Password = "";
            _winLogin.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow) || window.GetType() == typeof(StickyNotesView))
                {
                    window.Close();
                }
            }
        }

        private void OnUpdateTitlePersonReceived(clsLoginModel model)
        {
            LoginModel = clsLoginModel.Instance;

        }
    }
}
