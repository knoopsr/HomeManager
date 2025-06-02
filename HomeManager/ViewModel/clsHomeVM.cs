using HomeManager.Common;
using HomeManager.DataService.Logging;
using HomeManager.Helpers;
using HomeManager.Model.Logging;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using HomeManager.View.StickyNotes;
using HomeManager.ViewModel.Homepage;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    /// <summary>
    /// ViewModel voor de hoofdstructuur van HomeManager. Beheert navigatie, expander-menu's, en logging.
    /// </summary>
    public class clsHomeVM : clsBindableBase
    {
        #region Services & Velden

        private readonly clsButtonLoggingDataService MijnLoggingService;
        private static StickyNotesView stickyNotesView;

        #endregion

        #region Constructor

        public clsHomeVM()
        {
            NavCommand = new clsRelayCommand<string>(OnNav);
            cmdMenu = new clsCustomCommand(Execute_cmdMenu_Command, CanExecute_cmdMenu_Command);
            cmdCloseAplication = new clsCustomCommand(Execute_cmdCloseAplication_Command, CanExecute_cmdCloseAplication_Command);

            MijnLoggingService = new clsButtonLoggingDataService();
            clsMessenger.Default.Register<clsPersoonModel>(this, OnNewPersonenReceive);

            FavorietVensterVM = new clsFavorieteVensterViewModel
            {
                OpenVensterAction = OnNav
            };
        }

        #endregion

        #region Commands

        public ICommand cmdMenu { get; }
        public ICommand cmdCloseAplication { get; }
        public clsRelayCommand<string> NavCommand { get; private set; }

        #endregion

        #region Navigatie & ViewModels

        private clsBindableBase _currentViewModel;
        /// <summary>
        /// Huidig geladen ViewModel dat in de ContentControl weergegeven wordt.
        /// </summary>
        public clsBindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        /// <summary>
        /// ViewModel dat de favorietenvensters beheert op de startpagina.
        /// </summary>
        public clsFavorieteVensterViewModel FavorietVensterVM { get; set; }

        #endregion

        #region UI-Properties (hamburgermenu + expanderstatus)

        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set { _isMenuVisible = value; OnPropertyChange(); }
        }
        private bool _isMenuVisible = true;

        public bool IsPersonenExpanderMenu
        {
            get => _isPersonenExpanderMenu;
            set { _isPersonenExpanderMenu = value; OnPropertyChange(); }
        }
        private bool _isPersonenExpanderMenu;

        public bool IsBudgetExpanderMenu
        {
            get => _isBudgetExpanderMenu;
            set { _isBudgetExpanderMenu = value; OnPropertyChange(); }
        }
        private bool _isBudgetExpanderMenu;

        public bool IsTodoExpanderMenu
        {
            get => _isTodoExpanderMenu;
            set { _isTodoExpanderMenu = value; OnPropertyChange(); }
        }
        private bool _isTodoExpanderMenu;

        public bool IsSecurityExpanderMenu
        {
            get => _isSecurityExpanderMenu;
            set { _isSecurityExpanderMenu = value; OnPropertyChange(); }
        }
        private bool _isSecurityExpanderMenu;

        public bool IsStickyNotesExpanderMenu
        {
            get => _isStickyNotesExpanderMenu;
            set { _isStickyNotesExpanderMenu = value; OnPropertyChange(); }
        }
        private bool _isStickyNotesExpanderMenu;

        #endregion

        #region Command Handlers

        private void Execute_cmdMenu_Command(object? obj)
        {
            IsMenuVisible = !IsMenuVisible;

            if (!IsMenuVisible)
            {
                IsPersonenExpanderMenu = false;
                IsBudgetExpanderMenu = false;
                IsTodoExpanderMenu = false;
                IsSecurityExpanderMenu = false;
                IsStickyNotesExpanderMenu = false;
            }
        }

        private bool CanExecute_cmdMenu_Command(object? obj) => true;

        private void Execute_cmdCloseAplication_Command(object? obj)
        {
            var result = MessageBox.Show("Wilt u de applicatie sluiten?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private bool CanExecute_cmdCloseAplication_Command(object? obj) => true;

        #endregion

        #region Navigatie Logica

        /// <summary>
        /// Verwerkt de navigatie naar een ander ViewModel obv string identifier.
        /// </summary>
        private void OnNav(string destination)
        {
            // Logging
            MijnLoggingService.Insert(new clsButtonLoggingModel
            {
                AccountId = clsLoginModel.Instance.AccountID,
                ActionName = "MenuKnop",
                ActionTarget = destination
            });

            // Controleer permissie
            clsPermissionChecker permissionChecker = new clsPermissionChecker();
            if (!permissionChecker.PermissionViewmodel(destination))
            {
                MessageBox.Show("U heeft geen toegang tot deze pagina.");
                return;
            }

            // Zoek het ViewModel type
            var type = GetType().Assembly.GetTypes().FirstOrDefault(t => t.Name == destination);
            if (type != null && Activator.CreateInstance(type) is clsBindableBase vmInstance)
            {
                CurrentViewModel = vmInstance;
            }
        }

        #endregion

        #region Messenger Event

        private void OnNewPersonenReceive(clsPersoonModel persoon)
        {
            if (persoon != null && persoon.PersoonID == 0)
            {
                OnNav("clsPersoonViewModel");
            }
        }

        #endregion
    }
}
