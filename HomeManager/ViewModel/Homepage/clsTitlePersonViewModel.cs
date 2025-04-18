using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.Model.Homepage;
using HomeManager.Model.Mail;
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
        #region FIELDS
        clsBackupDataService MijnBackupService;
        private clsLoginModel _loginModel;
        private bool CanBackup = true;
        private clsDialogService _DialogService;
        private ObservableCollection<clsBackupModel> _mijnBackupCollectie;
        #endregion

        #region PROPERTIES
        public ICommand cmdAfmelden { get; set; }
        public ICommand cmdBackup { get; set; }
        public ICommand cmdUnLockUser { get; set; }
        public ICommand cmdLogs { get; set; }
        public ICommand cmdExceptions { get; set; }
        public ICommand cmdExceptionsMail { get; set; }

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

        public ObservableCollection<clsBackupModel> MijnBackupCollectie
        {
            get { return _mijnBackupCollectie; }
            set
            {
                _mijnBackupCollectie = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CONSTRUCTOR
        public clsTitlePersonViewModel()
        {
            _DialogService = new clsDialogService();
            MijnBackupService = new clsBackupDataService();
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateTitlePersonReceived);

            cmdAfmelden = new clsCustomCommand(ExecuteAfmelden, CanExecuteAfmelden);
            cmdBackup = new clsCustomCommand(ExecuteBackup, CanExecuteBackup);
            cmdUnLockUser = new clsCustomCommand(ExecuteUnLockUser, CanExecuteUnLockUser);
            cmdLogs = new clsCustomCommand(ExecuteLogs, CanExecuteLogs);
            cmdExceptions = new clsCustomCommand(ExecuteExceptions, CanExecuteExceptions);
            cmdExceptionsMail = new clsCustomCommand(ExecuteExceptionsMail, CanExecuteExceptionsMail);
        }
        #endregion

        #region METHODS
        private bool HasPermission(string permissionCode)
        {
            //710 RechtenCode: "??"
            //711 RechtenCode: "Ontgrendel computer?"
            //712 RechtenCode: "??"

            clsPermissionChecker _permissionChecker = new clsPermissionChecker();
            return _permissionChecker.HasPermission(permissionCode);
        }

        private void OnUpdateTitlePersonReceived(clsLoginModel model)
        {
            LoginModel = clsLoginModel.Instance;
        }

        private async void ExecuteBackup(object? obj)
        {
            try
            {
                CanBackup = false;
                // Wacht asynchroon op het resultaat van de GetAll() methode
                MijnBackupCollectie = await MijnBackupService.CreateBackup();


                string link = "https://homemanager.knoopsr.be/" + MijnBackupCollectie[0].Path;


                clsMailModel mailModel = new clsMailModel
                {
                    MailToName = clsLoginModel.Instance.VoorNaam,
                    MailToEmail = "johndoe@example.com",
                    Subject = "Backup Gemaakt",
                    Body = "Backup is gemaakt:\n" + Environment.NewLine + "<a href='" + link + "'>Download Backup</a>"
                };

                bool emailVerzonden = await clsMail.SendEmail(mailModel);

                if (emailVerzonden)
                {
                    MessageBox.Show("E-mail succesvol verzonden naar " + mailModel.MailToEmail);
                }
                else
                {
                    MessageBox.Show("Er is een fout opgetreden tijdens het verzenden van de e-mail.");
                }

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
        #endregion

        #region COMMANDS
        private bool CanExecuteExceptionsMail(object? obj) => HasPermission("711");
        private bool CanExecuteExceptions(object? obj) => HasPermission("711");
        private bool CanExecuteLogs(object? obj) => HasPermission("711");
        private bool CanExecuteUnLockUser(object? obj) => HasPermission("712");
        private bool CanExecuteAfmelden(object? obj) => true;
        private bool CanExecuteBackup(object? obj)
        {
            if (CanBackup) return HasPermission("710");
            else return false;
        }

        private void ExecuteExceptionsMail(object? obj)
        {
            _DialogService.ShowDialog(new ucExceptionsMail(), "Exception emails beheren");
        }

        private void ExecuteExceptions(object? obj)
        {
            _DialogService.ShowDialog(new ucExceptions(), "Overzicht Exceptions");
        }

        private void ExecuteLogs(object? obj)
        {
            _DialogService.ShowDialog(new ucButtonLogging(), "Overzicht Button Logging");
        }

        private void ExecuteUnLockUser(object? obj)
        {
            _DialogService.ShowDialog(new ucUnlockUser(),"Ontgrendel Gebruiker");
        }

        private void ExecuteAfmelden(object? obj)
        {
            OpenLoginWindow(obj);
        }
        #endregion
    }
}
