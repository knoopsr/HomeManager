using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using HomeManager.View.Security;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTitlePersonViewModel : clsCommonModelPropertiesBase
    {

        clsBackupDataService MijnBackupService;

        private clsLoginModel _loginModel;

        private bool CanBackup = true;

        public ICommand cmdAfmelden { get; set; }
        public ICommand cmdBackup { get; set; }

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
            MijnBackupService = new clsBackupDataService();
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateTitlePersonReceived);

            cmdAfmelden = new clsCustomCommand(ExecuteAfmelden, CanExecuteAfmelden);
            cmdBackup = new clsCustomCommand(ExecuteBackup, CanExecuteBackup);
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

                // Toon een bericht als de backup succesvol is gemaakt
                MessageBox.Show("Backup is gemaakt: " + MijnBackupCollectie[0].Path);
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
            _winLogin.txtWachtwoord.Text = string.Empty;
            _winLogin.Show();




            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
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
