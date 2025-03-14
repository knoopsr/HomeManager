using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.DataService.Security;
using HomeManager.Model.Security;
namespace HomeManager.ViewModel
{
    public class clsCredentialManagementViewModel : clsCommonModelPropertiesBase
    {
        clsCredentialManagementDataService MijnService;
        clsWachtwoordGroepDataService MijnWachtwoordenGroepService; 

        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdFilter { get; set; }

        private ObservableCollection<clsCredentialManagementModel> _mijnCollectie;
        public ObservableCollection<clsCredentialManagementModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsWachtWoordGroepModel> _mijnWachtwoordGroepCollectie;
        public ObservableCollection<clsWachtWoordGroepModel> MijnWachtwoordGroepCollectie
        {
            get { return _mijnWachtwoordGroepCollectie; }
            set
            {
                _mijnWachtwoordGroepCollectie = value;
                OnPropertyChanged();
            }
        }

        private clsCredentialManagementModel _mijnSelectedItem;
        public clsCredentialManagementModel MijnSelectedItem
        {
            get { return _mijnSelectedItem; }
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }
       
        
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();            
            MijnWachtwoordGroepCollectie = MijnWachtwoordenGroepService.GetAll();
            LaadFilter();
        }

        private void OpslaanCommando()
        {
            if (_mijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

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

        private void OnLoginReceived(clsLoginModel model)
        {
            LoginModel = model;
        }

        private clsLoginModel _loginModel;
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






        private string _filterTekst;
        public string FilterTekst
        {
            get { return _filterTekst; }
            set
            {
                _filterTekst = value;
                OnPropertyChanged(nameof(FilterTekst));
            }
        }
        private ObservableCollection<clsCredentialManagementModel> _gefilterdeCollectie;
        public ObservableCollection<clsCredentialManagementModel> GefilterdeCollectie
        {
            get { return _gefilterdeCollectie; }
            set
            {
                _gefilterdeCollectie = value;
                OnPropertyChanged(nameof(GefilterdeCollectie));
            }
        }
        private void LaadFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterTekst))
            {
                // Als er geen filtertekst is, toon alles
                GefilterdeCollectie = new ObservableCollection<clsCredentialManagementModel>(MijnCollectie);
            }
            else
            {
                // Filter de collectie op basis van FilterTekst
                var gefilterdeItems = MijnCollectie
                    .Where(item =>
                        (item.WachtwoordGroepNaam?.IndexOf(FilterTekst, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (item.WachtwoordNaam?.IndexOf(FilterTekst, StringComparison.OrdinalIgnoreCase) >= 0))
                    .ToList();

                // Update de gefilterde collectie
                GefilterdeCollectie = new ObservableCollection<clsCredentialManagementModel>(gefilterdeItems);
            }
        }




        private void Execute_Filter_Command(object? obj)
        {
            LaadFilter();

        }

        private bool CanExecute_Filter_Command(object? obj)
        {
            return true;
        }

        private void Execute_Save_Command(object? obj)
        {
            OpslaanCommando();
        }

        private bool CanExecute_Save_Command(object? obj)
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

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show(
                "Wil je " + MijnSelectedItem + " verwijderen?",
                "Verwijderen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnSelectedItem != null)
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
        }

        private bool CanExecute_Delete_Command(object? obj)
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

        private void Execute_New_Command(object? obj)
        {
            clsCredentialManagementModel _itemToInsert = new clsCredentialManagementModel()
            {
                WachtwoordID = 0,
                WachtwoordGroepID = 0,
                WachtwoordNaam = string.Empty,
                WachtwoordOmschrijving = string.Empty,
                Login = string.Empty,
                Wachtwoord = string.Empty
            };
            MijnSelectedItem = _itemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return !NewStatus;
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

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return NewStatus;
        }

        private void Execute_Close_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {
                if (MijnSelectedItem != null && MijnSelectedItem.IsDirty == true && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show(
                        MijnSelectedItem.ToString().ToUpper() + " is nog niet opgeslagen, wil je opslaan?",
                        "Opslaan of sluiten?",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                        clsHomeVM vm2 = (clsHomeVM)HomeWindow.DataContext;
                        vm2.CurrentViewModel = null;
                    }
                }
                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_SelectionChangedCommand(object obj)
        {
            return true;
        }

    }
}
