using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.DataService.Security;
using HomeManager.MailService;
using HomeManager.Helpers;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor het beheren van gebruikersaccounts binnen HomeManager.
    /// Ondersteunt CRUD-acties en het versturen van logingegevens via e-mail.
    /// </summary>
    public class clsAccountViewModel : clsCommonModelPropertiesBase
    {
        #region Velden & Services
        
        private clsPermissionChecker _permissionChecker = new();
        private readonly clsAccountDataService MijnService;
        private readonly clsPersoonDataService MijnPersoonService;
        private bool NewStatus = false;

        #endregion

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        #endregion

        #region ObservableCollections
        private ObservableCollection<clsAccountModel> _mijncollectie;
        public ObservableCollection<clsAccountModel> MijnCollectie
        {
            get => _mijncollectie;
            set
            {
                _mijncollectie = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<clsPersoonModel> MijnPersoonCollectie { get; set; }

        #endregion

        #region SelectedItems

        private clsAccountModel _mijnSelectedItem;
        /// <summary>
        /// Het geselecteerde account in de lijst.
        /// </summary>
        public clsAccountModel MijnSelectedItem
        {
            get => _mijnSelectedItem;
            set
            {
                if (value != null && _mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                {
                    if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                        LoadData();
                    }
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private clsPersoonModel _mijnSelectedPersoonItem;
        /// <summary>
        /// De geselecteerde persoon die aan een nieuw account gekoppeld wordt.
        /// </summary>
        public clsPersoonModel MijnSelectedPersoonItem
        {
            get => _mijnSelectedPersoonItem;
            set
            {
                _mijnSelectedPersoonItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public clsAccountViewModel()
        {
            MijnService = new clsAccountDataService();
            MijnPersoonService = new clsPersoonDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
            MijnPersoonCollectie = MijnPersoonService.GetAllApplicationUser();
        }

        #endregion

        #region Data Handling

        /// <summary>
        /// Laadt alle accounts uit de dataservice.
        /// </summary>
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        /// <summary>
        /// Voert de opslaglogica uit voor een nieuw of bestaand account.
        /// </summary>
        private async void OpslaanCommando()
        {
            if (_mijnSelectedItem == null) return;

            if (NewStatus)
            {
                if (MijnService.Insert(_mijnSelectedItem))
                {
                    NewStatus = false;
                    MijnSelectedItem.IsDirty = false;
                    MijnSelectedItem.MyVisibility = (int)Visibility.Visible;

                    var mailService = new clsMailService();
                    var verzondenEmails = await mailService.SendNewPassToPerson(_mijnSelectedItem, _mijnSelectedPersoonItem);

                    MessageBox.Show("E-mail succesvol verzonden naar:\n" + string.Join("\n", verzondenEmails));
                    LoadData();
                }
                else
                {
                    MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error");
                }
            }
            else
            {
                if (MijnService.Update(_mijnSelectedItem))
                {
                    NewStatus = false;
                    MijnSelectedItem.IsDirty = false;
                    MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                    LoadData();
                }
                else
                {
                    MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error");
                }
            }
        }

        #endregion

        #region Command Logic

        private void Execute_Save_Command(object? obj) => OpslaanCommando();
        private bool CanExecute_Save_Command(object? obj){
            if (_permissionChecker.HasPermission("232"))
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
            if (MijnSelectedItem != null && MessageBox.Show(
                $"Wil je {MijnSelectedItem} verwijderen?",
                "Verwijderen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnService.Delete(MijnSelectedItem))
                {
                    NewStatus = false;
                    LoadData();
                }
                else
                {
                    MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error");
                }
            }
        }

        private bool CanExecute_Delete_Command(object obj)
        {
            if (_permissionChecker.HasPermission("233"))
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
            var ongekoppeldePersonen = MijnPersoonCollectie
                .Where(p => !MijnCollectie.Any(a => a.PersoonID == p.PersoonID))
                .ToList();

            MijnPersoonCollectie.Clear();
            ongekoppeldePersonen.ForEach(p => MijnPersoonCollectie.Add(p));

            PasswordGenerator generator = new PasswordGenerator();
            MijnSelectedItem = new clsAccountModel
            {
                AccountID = 0,
                RolID = 0,
                Wachtwoord = generator.GeneratePassword(8),
                Login = string.Empty,
                PersoonID = 0,
                IsNew = true,
                IsLock = false,
                CountFailLogins = 0,
                MyVisibility = (int)Visibility.Hidden
            };

            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj)
        {
            if (_permissionChecker.HasPermission("231"))
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
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private bool CanExecute_Cancel_Command(object? obj) => NewStatus;

        private void Execute_Close_Command(object? obj)
        {
            if (obj is MainWindow homeWindow)
            {
                if (MijnSelectedItem != null && MijnSelectedItem.IsDirty && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show(
                        $"{MijnSelectedItem} is nog niet opgeslagen. Wil je opslaan?",
                        "Opslaan of sluiten?",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                    }
                }

                if (homeWindow.DataContext is clsHomeVM vm)
                    vm.CurrentViewModel = null;
            }
        }

        private bool CanExecute_Close_Command(object? obj) => true;

        #endregion
    }
}
