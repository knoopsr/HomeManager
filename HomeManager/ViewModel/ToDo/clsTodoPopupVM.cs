using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.Model.Todo;
//using HomeManager.Model.ToDo;
using HomeManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTodoPopupVM : clsCommonModelPropertiesBase
    {
        clsTodoPopupDataService MijnService;
        clsAccountDataService MijnserviceGebruikers;

        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand OpenCollectiesCommand { get; }
        public ICommand OpenAccountCommand { get; }

        public clsTodoPopupVM()
        {
            MijnService = new clsTodoPopupDataService();
            MijnserviceGebruikers = new clsAccountDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);

            clsMessenger.Default.Register<clsTodoPopupM>(this, OnCollectiesReceived);
            OpenCollectiesCommand = new clsRelayCommand<object>(OpenCollecties);
            OpenAccountCommand = new clsRelayCommand<object>(OpenAccount);


            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        private void OpenCollecties(object parameter)
        {
            // Logic to open ucCollecties.xaml
            var collectiesWindow = new Window
            {
                Content = new ucCollecties(),
                Title = "Collecties",
                Width = 800,
                Height = 450
            };
            collectiesWindow.ShowDialog();
        }

        private void OpenAccount(object parameter)
        {
            // Logic to open ucAccount.xaml
            var accountWindow = new Window
            {
                Content = new ucAccount(),
                Title = "Account",
                Width = 800,
                Height = 450
            };
            accountWindow.ShowDialog();
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<clsTodoPopupM> _MijnCollectie;
        public ObservableCollection<clsTodoPopupM> MijnCollectie
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

        private clsTodoPopupM _MijnSelectedItem;
        public clsTodoPopupM MijnSelectedItem
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
            MijnCollectieGebruikers = MijnserviceGebruikers.GetAll();

        }

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

            // clsMessenger.Default.Send<clsUpdateListMessages>(new clsUpdateListMessages());
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
            return !NewStatus;
        }

        private void Execute_NewCommand(object obj)
        {
            clsTodoPopupM ItemToInsert = new clsTodoPopupM()
            {
                TodoID = 0,
                Onderwerp = string.Empty,
                Detail = string.Empty,
                //GebruikerID = 0, // Of een standaardwaarde als dat nodig is
                GebruikerID = clsLoginModel.Instance.AccountID,
                Belangrijk = false,
                TodoCollectieID = null, // Of een standaardwaarde als dat nodig is
                TodoCategorieID = null, // Of een standaardwaarde als dat nodig is
                TodoColorID = null, // Of een standaardwaarde als dat nodig is
                IsKlaar = false,
                Volgorde = null // Of een standaardwaarde als dat nodig is
            };

            MijnSelectedItem = ItemToInsert;

            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }



        private bool CanExecute_DeleteCommand(object obj)
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

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();
        }

        private void OnCollectiesReceived(clsTodoPopupM obj)
        {
            _MijnSelectedItem = obj;
        }

        private int _gebruikerID;
        public int GebruikerID
        {
            get
            {
                return _gebruikerID;
            }
            set
            {
                _gebruikerID = clsLoginModel.Instance.PersoonID;
            }

        }

        private ObservableCollection<clsAccountModel> _MijnCollectieGebruikers;
        public ObservableCollection<clsAccountModel> MijnCollectieGebruikers
        {
            get
            {
                return _MijnCollectieGebruikers;
            }
            set
            {
                _MijnCollectieGebruikers = value;
                OnPropertyChanged();
            }
        }

        private clsAccountModel _MijnSelectedGebruiker;
        public clsAccountModel MijnSelectedGebruiker
        {
            get
            {
                return _MijnSelectedGebruiker;
            }
            set
            {

                _MijnSelectedGebruiker = MijnserviceGebruikers.GetById(clsLoginModel.Instance.AccountID);
               //_MijnSelectedGebruiker = clsLoginModel.Instance.
                //_MijnSelectedGebruiker.ToString();
                OnPropertyChanged();
            }
        }
        //public void SetTodoID(int todoID)
        //{
        //    clsTodoPopupM.Instance.TodoID = todoID;
        //}
    }
}
