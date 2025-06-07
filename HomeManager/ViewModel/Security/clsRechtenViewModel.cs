using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het beheren van rollen en hun rechten binnen de applicatie.
    /// </summary>
    public class clsRechtenViewModel : clsCommonModelPropertiesBase
    {
        #region Fields

        private bool NewStatus = false;
        private clsPermissionChecker _permissionChecker = new();
        private clsRechtenDataService MijnRechtenService;
        private clsRechtenCatogorieDataService MijnRechtenCatogorieService;
        private clsRollenDataService MijnRollenService;

        #endregion

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdIsChecked { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Verzameling van alle rechten.
        /// </summary>
        public ObservableCollection<clsRechtenModel> MijnRechtenCollectie { get; set; }

        /// <summary>
        /// Verzameling van rechten gecategoriseerd.
        /// </summary>
        public ObservableCollection<clsRechtenCatogorieModel> MijnRechtenCatogorieCollectie
        {
            get => _mijnRechtenCatogorieCollectie;
            set
            {
                _mijnRechtenCatogorieCollectie = value;
                OnPropertyChanged();

                foreach (var item in _mijnRechtenCatogorieCollectie)
                {
                    foreach (var item2 in MijnRechtenCollectie)
                    {
                        if (item.RechtenCatogorieID == item2.RechtenCatogorieID)
                        {
                            item.Rechten.Add(item2);
                        }
                    }
                }
            }
        }
        private ObservableCollection<clsRechtenCatogorieModel> _mijnRechtenCatogorieCollectie;

        /// <summary>
        /// Verzameling van rollen.
        /// </summary>
        private ObservableCollection<clsRollenModel> _mijnRollenCollectie;
        public ObservableCollection<clsRollenModel> MijnRollenCollectie
        {
            get => _mijnRollenCollectie;
            set
            {
                _mijnRollenCollectie = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// De momenteel geselecteerde rol.
        /// </summary>
        public clsRollenModel MijnSelectedItem
        {
            get => _mijnSelectedItem;
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show($"Wilt je {_mijnSelectedItem} opslaan?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }

                _mijnSelectedItem = value;
                OnPropertyChanged();

                if (_mijnSelectedItem != null)
                {
                    _mijnSelectedItem.IsTextBoxEnabled = _mijnSelectedItem.RolName != "Admin";
                    SetCheckedChildren(_mijnSelectedItem.Rechten);
                }
            }
        }
        private clsRollenModel _mijnSelectedItem;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor voor het initialiseren van rechtenbeheer.
        /// </summary>
        public clsRechtenViewModel()
        {
            MijnRechtenService = new clsRechtenDataService();
            MijnRechtenCatogorieService = new clsRechtenCatogorieDataService();
            MijnRollenService = new clsRollenDataService();

            cmdIsChecked = new clsCustomCommand(Execute_cmdIsChecked_Command, CanExecute_cmdIsChecked_Command);
            cmdSave = new clsCustomCommand(Execute_cmdSave_Command, CanExecute_cmdSave_Command);
            cmdNew = new clsCustomCommand(Execute_cmdNew_Command, CanExecute_cmdNew_Command);
            cmdDelete = new clsCustomCommand(Execute_cmdDelete_Command, CanExecute_cmdDelete_Command);
            cmdCancel = new clsCustomCommand(Execute_cmdCancel_Command, CanExecute_cmdCancel_Command);
            cmdClose = new clsCustomCommand(Execute_cmdClose_Command, CanExecute_cmdClose_Command);

            LoadRoles();
            LoadData();
            MijnSelectedItem = MijnRollenService.GetFirst();
        }

        #endregion

        #region Load

        private void LoadData()
        {
            SetCheckedChildren("");
            MijnRollenCollectie = MijnRollenService.GetAll();
        }

        private void LoadRoles()
        {
            MijnRechtenCollectie = MijnRechtenService.GetAll();
            MijnRechtenCatogorieCollectie = MijnRechtenCatogorieService.GetAll();
        }

        #endregion

        #region Command Methods

        private void Execute_cmdClose_Command(object? obj)
        {
            if (obj is MainWindow HomeWindow)
            {
                if (MijnSelectedItem != null && MijnSelectedItem.IsDirty && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show($"{MijnSelectedItem.ToString().ToUpper()} is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                    }
                }

                if (HomeWindow.DataContext is clsHomeVM vm)
                {
                    vm.CurrentViewModel = null;
                }
            }
        }

        private bool CanExecute_cmdClose_Command(object? obj) => true;

        private void Execute_cmdCancel_Command(object? obj)
        {
            MijnSelectedItem = MijnRollenService.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                SetCheckedChildren(MijnSelectedItem.Rechten);
            }
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private bool CanExecute_cmdCancel_Command(object? obj) => NewStatus;

        private void Execute_cmdNew_Command(object? obj)
        {
            var nieuw = new clsRollenModel()
            {
                RolID = 0,
                RolName = string.Empty,
                Rechten = string.Empty
            };
            MijnSelectedItem = nieuw;
            SetCheckedChildren("");
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_cmdNew_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("201"))
            {
                return !NewStatus;
            }
            return false;
        }

        private void Execute_cmdDelete_Command(object? obj)
        {
            if (MessageBox.Show($"Wil je {MijnSelectedItem} verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnRollenService.Delete(MijnSelectedItem))
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

        private bool CanExecute_cmdDelete_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("203"))
            {
                if (MijnSelectedItem != null && MijnSelectedItem.RolName != "Admin" && !NewStatus)
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



        private void Execute_cmdSave_Command(object? obj) => OpslaanCommando();

        private bool CanExecute_cmdSave_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("202"))
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

        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
                bool result = NewStatus ? MijnRollenService.Insert(MijnSelectedItem) : MijnRollenService.Update(MijnSelectedItem);
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
        }

        private void Execute_cmdIsChecked_Command(object? obj)
        {
            if (obj is clsRechtenModel selectedRecht)
            {
                var parent = MijnRechtenCatogorieCollectie.FirstOrDefault(c => c.Rechten.Contains(selectedRecht));

                if (parent != null)
                {
                    parent.IsChecked = parent.Rechten.All(r => r.IsChecked) ? true : parent.Rechten.All(r => !r.IsChecked) ? false : (bool?)null;
                }
            }
            MijnSelectedItem.Rechten = GetAangevinkteChildrenIds();
        }

        private bool CanExecute_cmdIsChecked_Command(object? obj) => true;

        #endregion

        #region Helpers

        /// <summary>
        /// Haalt de IDs op van alle aangevinkte rechten.
        /// </summary>
        public string GetAangevinkteChildrenIds()
        {
            return string.Join("|", MijnRechtenCatogorieCollectie.SelectMany(c => c.Rechten).Where(r => r.IsChecked).Select(r => r.RechtenID));
        }

        /// <summary>
        /// Zet rechten op checked op basis van een string met rechten-IDs.
        /// </summary>
        public void SetCheckedChildren(string idsString)
        {
            foreach (var categorie in MijnRechtenCatogorieCollectie)
            {
                categorie.IsChecked = false;
                foreach (var recht in categorie.Rechten)
                {
                    recht.IsChecked = false;
                }
            }

            var idsToCheck = idsString.Split('|').Where(id => !string.IsNullOrWhiteSpace(id)).Select(int.Parse).ToList();

            foreach (var categorie in MijnRechtenCatogorieCollectie)
            {
                foreach (var recht in categorie.Rechten)
                {
                    recht.IsChecked = idsToCheck.Contains(recht.RechtenID);
                }

                if (categorie.Rechten.All(r => r.IsChecked))
                    categorie.IsChecked = true;
                else if (categorie.Rechten.All(r => !r.IsChecked))
                    categorie.IsChecked = false;
                else
                    categorie.IsChecked = null;
            }
        }

        #endregion
    }
}
