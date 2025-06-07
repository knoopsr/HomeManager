using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.DataService.Security;


namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het beheren van wachtwoordgroepen.
    /// </summary>
    public class clsCredentialGroupViewModel : clsCommonModelPropertiesBase
    {
        #region Fields
        private clsPermissionChecker _permissionChecker = new();
        private clsWachtwoordGroepDataService MijnService;
        private bool NewStatus = false;

        #endregion

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Verzameling van alle wachtwoordgroepen.
        /// </summary>
        public ObservableCollection<clsWachtWoordGroepModel> MijnCollectie
        {
            get => _mijnCollectie;
            set { _mijnCollectie = value; OnPropertyChanged(); }
        }
        private ObservableCollection<clsWachtWoordGroepModel> _mijnCollectie;

        /// <summary>
        /// Geselecteerde wachtwoordgroep.
        /// </summary>
        public clsWachtWoordGroepModel MijnSelectedItem
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
        private clsWachtWoordGroepModel _mijnSelectedItem;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Initialiseert commands en laadt de gegevens.
        /// </summary>
        public clsCredentialGroupViewModel()
        {
            MijnService = new clsWachtwoordGroepDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        #endregion

        #region Data Methods

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

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

        #endregion

        #region Command Methods

        private void Execute_Save_Command(object? obj) => OpslaanCommando();

        private bool CanExecute_Save_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("212"))
            {
                if (MijnSelectedItem != null &&
                MijnSelectedItem.Error == null &&
                MijnSelectedItem.IsDirty == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
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

        private bool CanExecute_Delete_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("213"))
            {
                if (MijnSelectedItem != null)
                {
                    if (NewStatus)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            MijnSelectedItem = new clsWachtWoordGroepModel
            {
                WachtwoordGroepID = 0,
                WachtwoordGroep = string.Empty,
                MyVisibility = (int)Visibility.Hidden
            };
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("211"))
            {
                return !NewStatus;
            }
            return false;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            MijnSelectedItem = MijnService.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
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

        private bool CanExecute_SelectionChangedCommand(object obj) => true;

        #endregion
    }
}
