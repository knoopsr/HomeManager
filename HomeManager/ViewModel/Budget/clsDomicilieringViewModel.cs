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
using System.Data.SqlTypes;
using HomeManager.Messages;
using HomeManager.Services;
using HomeManager.View;


namespace HomeManager.ViewModel
{
    public class clsDomicilieringViewModel : clsCommonModelPropertiesBase
    {
        
        clsDomicilieringDataService MijnService;

        private clsDialogService _DialogService;

        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdEditFrequentie { get; set; }
        public ICommand cmdEditBegunstigden { get; set; }
        public ICommand cmdEditCategorie { get; set; }


        public int IsUitgaven { get; set; }
        
        private ObservableCollection<clsDomicilieringModel> _MijnCollectie;
        public ObservableCollection<clsDomicilieringModel> MijnCollectie
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
        private clsDomicilieringModel _MijnSelectedItem;
        public clsDomicilieringModel MijnSelectedItem
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
        }


        public clsDomicilieringViewModel()
        {
            MijnService = new clsDomicilieringDataService();
            _DialogService = new clsDialogService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdEditFrequentie = new clsCustomCommand(EditFrequentie, CanExecute_EditFrequentie);
            cmdEditBegunstigden = new clsCustomCommand(EditBegunstigde, CanExecute_EditBegunstigde);
            cmdEditCategorie = new clsCustomCommand(EditCategorie, CanExecute_EditCategorie);

            clsMessenger.Default.Register<clsUpdateListMessages>(this, OnUpdateListMessageReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
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
            clsDomicilieringModel ItemToInsert = new clsDomicilieringModel()
            {
                IsUitgaven = null,
                DomicilieringID = 0,
                Bedrag = 0,
                VanDatum = DateOnly.FromDateTime(DateTime.Now),
                TotDatum = DateOnly.FromDateTime(DateTime.Now),
                Onderwerp = String.Empty,
                FrequentieID = 0,
                BegunstigdeID = 0,
                BudgetCategorieID = 0,

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
        #region Popup Windows
        private void OnUpdateListMessageReceived(clsUpdateListMessages obj)
        {
            //refresh
            LoadData();
            _DialogService.CloseDialog();
        }

        private bool CanExecute_EditFrequentie(object obj)
        {
            return true;
        }

        private void EditFrequentie(object obj)
        {
            
            _DialogService.ShowDialog(new ucFrequentie(), "Frequentie");

        }

        private bool CanExecute_EditBegunstigde(object obj)
        {
            return true;
        }

        private void EditBegunstigde(object obj)
        {
            _DialogService.ShowDialog(new ucBegunstigden(), "Begunstigde");
        }


        private bool CanExecute_EditCategorie (object obj)
        {
            return true;
        }

        private void EditCategorie (object obj)
        {
            _DialogService.ShowDialog(new ucCategorie(), "Categorie");
        }

        #endregion

    }
}
