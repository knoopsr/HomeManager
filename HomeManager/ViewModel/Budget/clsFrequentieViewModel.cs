using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Budget;
using HomeManager.DataService.Budget;
using HomeManager.Messages;
using HomeManager.Services;
using System.Runtime.CompilerServices;
using HomeManager.Messages;


namespace HomeManager.ViewModel
{
    public class clsFrequentieViewModel : clsCommonModelPropertiesBase
    {
        private clsPermissionChecker _permissionChecker = new();
        clsFrequentieDataService MijnService;

        

        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdFilter { get; set; }
        
        private ObservableCollection<clsFrequentieModel> _MijnCollectie;
        public ObservableCollection<clsFrequentieModel> MijnCollectie
        {
            get
            {
                return _MijnCollectie;
            }
            set
            {
                _MijnCollectie = value;
                OnPropertyChanged();
            }
        }
        private clsFrequentieModel _MijnSelectedItem;
        public clsFrequentieModel MijnSelectedItem
        {
            get
            {
                return _MijnSelectedItem;
            }
            set
            {

                if (value != null)
                {
                    if (_MijnSelectedItem != null && _MijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("wil je " + _MijnSelectedItem + " Opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }

                    }
                }
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }




        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedItem))
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

                else
                {
                    if (MijnService.Update(MijnSelectedItem))
                    {


                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }


        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            GefilterdeCollectie = new ObservableCollection<clsFrequentieModel>(MijnCollectie);


        }


        public clsFrequentieViewModel()
        {
            MijnService = new clsFrequentieDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);

            //de searchcommand
            SearchCommand = new RelayCommand(FilterFrequentie);
            //de clear SearchCommand
            ClearSearchCommand = new RelayCommand(ClearSearch);

            clsMessenger.Default.Register<clsFrequentieModel>(this, OnFrequentieReceived);

            
            
            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        #region Save - Delete - Cancel - Close

        private bool CanExecute_CloseCommand(object obj)
        {
            return true;
        }
        private void Execute_CloseCommand(object obj)
        {
                        
            MainWindow HomeWindow = obj as MainWindow;
            
            if (HomeWindow != null)
            {

                if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
                {
                    if (MessageBox.Show(MijnSelectedItem.ToString().ToUpper() +
                        " is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?",
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

            clsMessenger.Default.Send<clsUpdateListMessages>(new clsUpdateListMessages());
            

        }

        private bool CanExecute_CancelCommand(object obj)
        {
            return NewStatus;
        }

        private void Execute_CancelCommand(object obj)
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
        private bool CanExecute_NewCommand(object obj)
        {
            if (_permissionChecker.HasPermission("441"))
            {
                return !NewStatus;
            }
            return false;
        }

        private void Execute_NewCommand(object obj)
        {
            clsFrequentieModel ItemToInsert = new clsFrequentieModel()
            {
                FrequentieID = 0,
                Frequentie = string.Empty,
                AantalDagen = null
            };

            MijnSelectedItem = ItemToInsert;


            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
           
        }
        private bool CanExecute_DeleteCommand(object obj)
        {
            if (_permissionChecker.HasPermission("443"))
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

        private void Execute_DeleteCommand(object obj)
        {
            if (MessageBox.Show("wil je " + MijnSelectedItem + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
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

        private bool CanExecute_SaveCommand(object obj)
        {
            if (_permissionChecker.HasPermission("442"))
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

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();
         
        }

        private void OnFrequentieReceived(clsFrequentieModel obj)
        {
            
            _MijnSelectedItem = obj;
            
        }

        #endregion

        #region Filter_Frequentie

        private string _filterText;

        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                OnPropertyChanged();
                FilterFrequentie();
            }
        }

        //RelayCommand toevoegen voor de RelayCommand filters
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;
            private Action<object> executeBold;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public RelayCommand(Action<object> executeBold)
            {
                this.executeBold = executeBold;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

            public void Execute(object parameter) => _execute();

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }


        private ObservableCollection<clsFrequentieModel> _gefilterdeCollectie;

        public ObservableCollection<clsFrequentieModel> GefilterdeCollectie
        {
            get
            {
                return _gefilterdeCollectie;
            }
            set
            {
                _gefilterdeCollectie = value;
                OnPropertyChanged(nameof(GefilterdeCollectie));
            }
        }

        //ICommand voor mijn filters
        public ICommand SearchCommand { get; private set; }
        public ICommand ClearSearchCommand { get; private set; }

        // Methode voor filter uit te voeren
        private void FilterFrequentie()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                //niet in de zoekbalk
                GefilterdeCollectie = new ObservableCollection<clsFrequentieModel>(MijnCollectie);
            }
            else
            {
                var GefilterdeItems = MijnCollectie
                    .Where(item =>

                       (item.Frequentie.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                       )
                      .ToList();

                GefilterdeCollectie = new ObservableCollection<clsFrequentieModel>(GefilterdeItems);
            }
        }



        //Methode voor de zoekbalk te clearen
        private void ClearSearch()
        {
            FilterText = string.Empty;
            FilterFrequentie();

        }

        
        #endregion


    }
}
