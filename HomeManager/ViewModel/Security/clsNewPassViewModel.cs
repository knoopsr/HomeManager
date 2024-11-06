using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Security;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsNewPassViewModel : clsCommonModelPropertiesBase
    {
        clsloginDataService MijnService;
        clsLoginModel _loginModel;

        public ICommand cmdOpslaan { get; set; }
        public ICommand cmdAnnuleer { get; set; }
        public ICommand cmdClose { get; set; }

  
        private string _newPass;
        public string NewPass
        {
            get { return _newPass; }
            set
            {
                _newPass = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPass;
        public string ConfirmPass
        {
            get { return _confirmPass; }
            set
            {
                _confirmPass = value;
                OnPropertyChanged();
            }
        }


        public clsNewPassViewModel()
        {
            MijnService = new clsloginDataService();
            cmdOpslaan = new clsCustomCommand(Execute_Opslaan_Command, CanExecute_Opslaan_Command);
            cmdAnnuleer = new clsCustomCommand(Execute_Annuleer_Command, CanExecute_Annuleer_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateNewPassReceived);
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window winNewPassWord)
            {
                winNewPassWord.Close();
            }
        }

        private bool CanExecute_Annuleer_Command(object? obj)
        {
            return true;
        }

        private void Execute_Annuleer_Command(object? obj)
        {
          ConfirmPass = string.Empty;
            NewPass = string.Empty;
        }

        private bool CanExecute_Opslaan_Command(object? obj)
        {
            if (NewPass != null && ConfirmPass != null && NewPass == ConfirmPass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute_Opslaan_Command(object? obj)
        {
            if (MijnService.UpdatePassWord(_loginModel, NewPass))
            {
                clsMessenger.Default.Send<clsUpdatePassWordMessages>(new clsUpdatePassWordMessages());
            }
            else
            {
                MessageBox.Show(_loginModel.ErrorBoodschap, "Error?");
            }

        }

        private void OnUpdateNewPassReceived(clsLoginModel model)
        {
            _loginModel = model;
        }
    }
}
