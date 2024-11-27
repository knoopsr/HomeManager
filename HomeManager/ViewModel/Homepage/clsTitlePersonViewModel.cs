using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.View.Security;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTitlePersonViewModel : clsCommonModelPropertiesBase
    {
        private clsLoginModel _loginModel;
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



        public clsTitlePersonViewModel()
        {
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateTitlePersonReceived);

            cmdAfmelden = new clsCustomCommand(ExecuteAfmelden, CanExecuteAfmelden);
            cmdBackup = new clsCustomCommand(ExecuteBackup, CanExecuteBackup);
        }

        private bool CanExecuteBackup(object? obj)
        {
            clsPermissionChecker _permissionChecker = new clsPermissionChecker();      
            return _permissionChecker.HasPermission("710"); 
        }

        private void ExecuteBackup(object? obj)
        {
            throw new NotImplementedException();
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
