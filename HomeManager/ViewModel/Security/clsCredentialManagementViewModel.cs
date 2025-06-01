using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.DataService.Security;
using HomeManager.Model.Security;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het beheren van wachtwoorden en wachtwoordgroepen.
    /// </summary>
    public class clsCredentialManagementViewModel : clsCommonModelPropertiesBase
    {
        #region Fields

        private clsCredentialManagementDataService MijnService;
        private clsWachtwoordGroepDataService MijnWachtwoordenGroepService;
        private bool NewStatus = false;

        #endregion

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdFilter { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Alle opgeslagen credentials.
        /// </summary>
        public ObservableCollection<clsCredentialManagementModel> MijnCollectie
        {
            get => _mijnCollectie;
            set { _mijnCollectie = value; OnPropertyChanged(); }
        }
        private ObservableCollection<clsCredentialManagementModel> _mijnCollectie;

        /// <summary>
        /// Alle wachtwoordgroepen.
        /// </summary>
        public ObservableCollection<clsWachtWoordGroepModel> MijnWachtwoordGroepCollectie
        {
            get => _mijnWachtwoordGroepCollectie;
            set { _mijnWachtwoordGroepCollectie = value; OnPropertyChanged(); }
        }
        private ObservableCollection<clsWachtWoordGroepModel> _mijnWachtwoordGroepCollectie;

        /// <summary>
        /// Geselecteerde credential.
        /// </summary>
        public clsCredentialManagementModel MijnSelectedItem
        {
            get => _mijnSelectedItem;
            set
            {
                if (value != null && _mijnSelectedItem?.IsDirty == true)
                {
                    if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                        LoadData();
                    }
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }
        private clsCredentialManagementModel _mijnSelectedItem;

        /// <summary>
        /// Ingelogde gebruiker.
        /// </summary>
        public clsLoginModel LoginModel
        {
            get => _loginModel;
            set { _loginModel = value; OnPropertyChanged(); }
        }
        private clsLoginModel _loginModel;

        /// <summary>
        /// Tekst waarmee gefilterd wordt.
        /// </summary>
        public string FilterTekst
        {
            get => _filterTekst;
            set
            {
                _filterTekst = value;
                OnPropertyChanged();
                LaadFilter();
            }
        }
        private string _filterTekst;

        /// <summary>
        /// Gefilterde lijst van credentials.
        /// </summary>
        public ObservableCollection<clsCredentialManagementModel> GefilterdeCollectie
        {
            get => _gefilterdeCollectie;
            set { _gefilterdeCollectie = value; OnPropertyChanged(); }
        }
        private ObservableCollection<clsCredentialManagementModel> _gefilterdeCollectie;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Initialiseert services, commando's en laadt data.
        /// </summary>
        public clsCredentialManagementViewModel()
        {
            MijnService = new clsCredentialManagementDataService();
            MijnWachtwoordenGroepService = new clsWachtwoordGroepDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdFilter = new clsCustomCommand(Execute_Filter_Command, CanExecute_Filter_Command);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
            GefilterdeCollectie = new ObservableCollection<clsCredentialManagementModel>(MijnCollectie);

            clsMessenger.Default.Register<clsLoginModel>(this, OnLoginReceived);
        }

        #endregion

        #region Data Loading & Filtering

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            MijnWachtwoordGroepCollectie = MijnWachtwoordenGroepService.GetAll();
            LaadFilter();
        }

        private void LaadFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterTekst))
            {
                GefilterdeCollectie = new ObservableCollection<clsCredentialManagementModel>(MijnCollectie);
            }
            else
            {
                var gefilterdeItems = MijnCollectie.Where(item =>
                    (item.WachtwoordGroepNaam?.IndexOf(FilterTekst, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (item.WachtwoordNaam?.IndexOf(FilterTekst, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();

                GefilterdeCollectie = new ObservableCollection<clsCredentialManagementModel>(gefilterdeItems);
            }
        }

        #endregion

        #region Command Methods

        private void Execute_Save_Command(object? obj) => OpslaanCommando();

        private bool CanExecute_Save_Command(object? obj) => MijnSelectedItem?.IsDirty == true && MijnSelectedItem.Error == null;

        private void OpslaanCommando()
        {
            if (MijnSelectedItem == null) return;

            bool result = NewStatus ? MijnService.Insert(MijnSelectedItem) : MijnService.Update(MijnSelectedItem);
            if (result)
            {
                MijnSelectedItem.IsDirty = false;
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                NewStatus = false;
                LoadData();
            }
            else
            {
                MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
            }
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show($"Wil je {MijnSelectedItem} verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnService.Delete(MijnSelectedItem))
                {
                    NewStatus = false;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                }
            }
        }

        private bool CanExecute_Delete_Command(object? obj) => MijnSelectedItem != null && !NewStatus;

        private void Execute_New_Command(object? obj)
        {
            MijnSelectedItem = new clsCredentialManagementModel
            {
                WachtwoordID = 0,
                WachtwoordGroepID = 0,
                WachtwoordNaam = string.Empty,
                WachtwoordOmschrijving = string.Empty,
                Login = string.Empty,
                Wachtwoord = string.Empty,
                MyVisibility = (int)Visibility.Hidden
            };
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj) => !NewStatus;

        private void Execute_Cancel_Command(object? obj)
        {
            MijnSelectedItem = MijnService.GetFirst();
            MijnSelectedItem.MijnSelectedIndex = 0;
            MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private bool CanExecute_Cancel_Command(object? obj) => NewStatus;

        private void Execute_Close_Command(object? obj)
        {
            if (obj is MainWindow HomeWindow)
            {
                if (MijnSelectedItem?.IsDirty == true && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show($"{MijnSelectedItem.ToString().ToUpper()} is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                        ((clsHomeVM)HomeWindow.DataContext).CurrentViewModel = null;
                    }
                }
                ((clsHomeVM)HomeWindow.DataContext).CurrentViewModel = null;
            }
        }

        private bool CanExecute_Close_Command(object? obj) => true;

        private void Execute_Filter_Command(object? obj) => FilterTekst = string.Empty;

        private bool CanExecute_Filter_Command(object? obj) => true;

        #endregion

        #region Helpers

        private void OnLoginReceived(clsLoginModel model) => LoginModel = model;
     
        #endregion
    }
}
