using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.MailService;
using HomeManager.Model.Security;
using HomeManager.View.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Security
{
    /// <summary>
    /// ViewModel voor het beheren en unlocken van gelokte gebruikersaccounts.
    /// </summary>
    public class clsUnLockViewModel : clsCommonModelPropertiesBase
    {

        #region Constructor

        /// <summary>
        /// Constructor voor het initialiseren van commando’s en data.
        /// </summary>
        public clsUnLockViewModel()
        {
            MijnService = new clsLockedAccountDataService();
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
        }

        #endregion

        #region Fields

        private clsLockedAccountDataService MijnService;
        private bool _isDirtyLocal = false;

        #endregion

        #region Properties

        /// <summary>
        /// Bevat alle gelokte accounts.
        /// </summary>
        public ObservableCollection<clsLockedAccountModel> MijnCollectie
        {
            get => _mijnCollectie;
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<clsLockedAccountModel> _mijnCollectie;

        /// <summary>
        /// De geselecteerde (gelokte) gebruikers om te ontgrendelen.
        /// </summary>
        public ObservableCollection<clsLockedAccountModel> SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<clsLockedAccountModel> _selectedItem;

        #endregion

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdUnLockUser { get; set; }


        /// <summary>
        /// Bepaalt of de Save-command mag worden uitgevoerd.
        /// </summary>
        private bool CanExecute_Save_Command(object? obj)
        {
            _isDirtyLocal = SelectedItem.Any(item => item.IsDirty);
            return _isDirtyLocal;
        }
        private bool CanExecute_Close_Command(object? obj) => true;
        private bool CanExecute_Cancel_Command(object? obj) => _isDirtyLocal;
        private bool CanExecute_Delete_Command(object? obj) => false;
        private bool CanExecute_New_Command(object? obj) => false;

        private void Execute_Cancel_Command(object? obj)
        {
            foreach (var item in MijnCollectie)
            {
                item.IsSelected = false;
                item.IsDirty = false;
            }
            _isDirtyLocal = false;
        }
        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window win)
            {
                win.Close();
            }
        }
        private void Execute_New_Command(object? obj) => throw new NotImplementedException();
        private void Execute_Delete_Command(object? obj) => throw new NotImplementedException();

        /// <summary>
        /// Ontgrendelt geselecteerde gebruikers en verzendt nieuwe wachtwoorden per e-mail.
        /// </summary>
        private async void Execute_Save_Command(object? obj)
        {
            DataTable inputTable = new DataTable();
            inputTable.Columns.Add("AccountID", typeof(int));
            inputTable.Columns.Add("Wachtwoord", typeof(string));

            PasswordGenerator generator = new PasswordGenerator();

            foreach (var item in SelectedItem.Where(i => i.IsSelected))
            {
                item.Account.Wachtwoord = generator.GeneratePassword(8);
                inputTable.Rows.Add(item.Account.AccountID, item.Account.Wachtwoord);
            }

            var model = new clsLockedAccountModel
            {
                SelectedItemsList = new ObservableCollection<(int AccountID, string Wachtwoord)>(
                    inputTable.AsEnumerable().Select(row => (
                        row.Field<int>("AccountID"),
                        row.Field<string>("Wachtwoord")
                    ))
                )
            };

            if (MijnService.UnLockUsers(model))
            {
                _isDirtyLocal = false;
                List<string> verzondenEmails = null;

                foreach (var item in SelectedItem.Where(i => i.IsSelected))
                {
                    var mailService = new clsMailService();
                    verzondenEmails = await mailService.SendNewPassToPerson(item.Account, item.Persoon);
                }

                if (verzondenEmails != null)
                {
                    MessageBox.Show("E-mail succesvol verzonden naar:\n" + string.Join(Environment.NewLine, verzondenEmails));
                }

                LoadData();
            }
            else
            {
                MessageBox.Show(model.ErrorBoodschap, "Error");
            }
        }


        #endregion

        #region Load

        /// <summary>
        /// Laadt alle gelokte accounts in het ViewModel.
        /// </summary>
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            SelectedItem = new ObservableCollection<clsLockedAccountModel>();
        }

        #endregion

    }
}
