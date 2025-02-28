using HomeManager.Common;
using HomeManager.DataService.Logging;
using HomeManager.Helpers;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeManager.ViewModel.Logging
{
    public class clsButtonLoggingViewModel : clsCommonModelPropertiesBase
    {
        clsButtonLoggingDataService MijnService;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }


        private ObservableCollection<clsButtonLoggingModel> _mijnCollectie;
        public ObservableCollection<clsButtonLoggingModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsButtonLoggingModel> _mijnGefilterdeCollectie;
        public ObservableCollection<clsButtonLoggingModel> MijnGefilterdeCollectie
        {
            get { return _mijnGefilterdeCollectie; }
            set
            {
                _mijnGefilterdeCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsAccountModel> _mijnAccounten;
        public ObservableCollection<clsAccountModel> MijnAccounten
        {
            get { return _mijnAccounten; }
            set
            {
                _mijnAccounten = value;
                OnPropertyChanged();
            }
        }



        private clsAccountModel _selectedAccount;
        public clsAccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();

                FilterData();
            }
        }







        public clsButtonLoggingViewModel()
        {
            MijnService = new clsButtonLoggingDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();

            // Unieke accounts ophalen
            var uniekeAccounts = MijnCollectie
                .Select(x => new clsAccountModel { AccountID = x.AccountId, AccountName = x.AccountName })
                .DistinctBy(x => x.AccountID) // Alleen unieke AccountID's
                .ToList();

            // ❗ Voeg een speciale "Alle Accounts" optie toe
            var alleAccountsOptie = new clsAccountModel { AccountID = 0, AccountName = "-- Alle Accounts --" };
            uniekeAccounts.Insert(0, alleAccountsOptie);

            // Koppel de lijst aan de ObservableCollection
            MijnAccounten = new ObservableCollection<clsAccountModel>(uniekeAccounts);

            // ❗ Standaard "-- Alle Accounts --" selecteren
            SelectedAccount = alleAccountsOptie;
        }



        private void FilterData()
        {
            if (SelectedAccount != null)
            {
                if (SelectedAccount.AccountID == 0) // ❗ Controleer op AccountID = 0
                {
                    MijnCollectie = MijnService.GetAll();
                }
                else
                {
                    MijnCollectie = MijnService.GetAllByAccountId(SelectedAccount.AccountID);
                }
            }
        }



        #region Command Methods

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private void Execute_Close_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {
                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return true;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return false;
        }

        private void Execute_Delete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            return false;
        }

        private void Execute_Save_Command(object? obj)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
