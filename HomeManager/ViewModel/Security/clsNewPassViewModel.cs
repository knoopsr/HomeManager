using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Security;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het wijzigen van het wachtwoord van een gebruiker.
    /// </summary>
    public class clsNewPassViewModel : clsCommonModelPropertiesBase
    {
        #region Fields

        private clsloginDataService MijnService;
        private clsLoginModel _loginModel;

        #endregion

        #region Commands

        public ICommand cmdOpslaan { get; set; }
        public ICommand cmdAnnuleer { get; set; }
        public ICommand cmdClose { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Het nieuwe wachtwoord dat de gebruiker invoert.
        /// </summary>
        public string NewPass
        {
            get => _newPass;
            set
            {
                _newPass = value;
                OnPropertyChanged();
            }
        }
        private string _newPass;

        /// <summary>
        /// Bevestiging van het nieuwe wachtwoord.
        /// </summary>
        public string ConfirmPass
        {
            get => _confirmPass;
            set
            {
                _confirmPass = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPass;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor voor het initialiseren van het ViewModel en registreren van de Messenger.
        /// </summary>
        public clsNewPassViewModel()
        {
            MijnService = new clsloginDataService();
            cmdOpslaan = new clsCustomCommand(Execute_Opslaan_Command, CanExecute_Opslaan_Command);
            cmdAnnuleer = new clsCustomCommand(Execute_Annuleer_Command, CanExecute_Annuleer_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateNewPassReceived);
        }

        #endregion

        #region Command Methods

        private bool CanExecute_Close_Command(object? obj) => true;

        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window winNewPassWord)
            {
                winNewPassWord.Close();
            }
        }

        private bool CanExecute_Annuleer_Command(object? obj) => true;

        private void Execute_Annuleer_Command(object? obj)
        {
            ConfirmPass = string.Empty;
            NewPass = string.Empty;
        }

        private bool CanExecute_Opslaan_Command(object? obj)
        {
            return !string.IsNullOrEmpty(NewPass) &&
                   !string.IsNullOrEmpty(ConfirmPass) &&
                   NewPass == ConfirmPass;
        }

        private void Execute_Opslaan_Command(object? obj)
        {
            if (MijnService.UpdatePassWord(_loginModel, NewPass))
            {
                clsMessenger.Default.Send(new clsUpdatePassWordMessages());
            }
            else
            {
                MessageBox.Show(_loginModel.ErrorBoodschap, "Error?");
            }
        }

        #endregion

        #region Messenger Handler

        /// <summary>
        /// Ontvangt het loginmodel wanneer het ViewModel geactiveerd wordt.
        /// </summary>
        private void OnUpdateNewPassReceived(clsLoginModel model)
        {
            ConfirmPass = string.Empty;
            NewPass = string.Empty;
            _loginModel = model;
        }

        #endregion
    }
}
