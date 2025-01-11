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
using HomeManager.Model.Security;


namespace HomeManager.ViewModel
{
    public class clsOverzichtViewModel : clsCommonModelPropertiesBase
    {
    //    clsTransactieDataService MijnService;

    //    private clsDialogService _DialogService;

    //    private bool NewStatus = false;
    //    public ICommand cmdDelete { get; set; }
    //    public ICommand cmdNew { get; set; }
    //    public ICommand cmdCancel { get; set; }
    //    public ICommand cmdClose { get; set; }
    //    public ICommand cmdSave { get; set; }
    //    public ICommand cmdEditBegunstigden { get; set; }
    //    public ICommand cmdEditCategorie { get; set; }
    //    public ICommand cmdFilter { get; set; }


    //    public int IsUitgaven { get; set; }

    //    private ObservableCollection<clsTransactieModel> _MijnCollectie;
    //    public ObservableCollection<clsTransactieModel> MijnCollectie
    //    {
    //        get
    //        {
    //            return _MijnCollectie;
    //        }
    //        set
    //        {
    //            _MijnCollectie = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    private clsTransactieModel _MijnSelectedItem;
    //    public clsTransactieModel MijnSelectedItem
    //    {
    //        get
    //        {
    //            return _MijnSelectedItem;
    //        }
    //        set
    //        {

    //            if (value != null)
    //            {
    //                if (_MijnSelectedItem != null && _MijnSelectedItem.IsDirty)
    //                {
    //                    if (MessageBox.Show("wil je de geselecteerde Transactie opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
    //                    {
    //                        OpslaanCommando();
    //                        LoadData();
    //                    }

    //                }
    //            }
    //            _MijnSelectedItem = value;
    //            OnPropertyChanged();
    //        }
    //    }




    //    private void OpslaanCommando()
    //    {
    //        if (MijnSelectedItem != null)
    //        {
    //            if (NewStatus)
    //            {
    //                if (MijnService.Insert(MijnSelectedItem))
    //                {
    //                    MijnSelectedItem.IsDirty = false;
    //                    MijnSelectedItem.MijnSelectedIndex = 0;
    //                    MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
    //                    NewStatus = false;
    //                    LoadData();
    //                }
    //                else
    //                {
    //                    MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
    //                }
    //            }

    //            else
    //            {
    //                if (MijnService.Update(MijnSelectedItem))
    //                {


    //                    MijnSelectedItem.IsDirty = false;
    //                    MijnSelectedItem.MijnSelectedIndex = 0;
    //                    NewStatus = false;
    //                    LoadData();
    //                }
    //                else
    //                {
    //                    MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
    //                }
    //            }
    //        }
    //    }


    //    private void LoadData()
    //    {
    //        MijnCollectie = MijnService.GetAll();
    //    }





    //    public clsTransactieViewModel()
    //    {
    //        MijnService = new clsTransactieDataService();
    //        _DialogService = new clsDialogService();

    //        cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
    //        cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
    //        cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
    //        cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
    //        cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
    //        cmdEditBegunstigden = new clsCustomCommand(EditBegunstigde, CanExecute_EditBegunstigde);
    //        cmdEditCategorie = new clsCustomCommand(EditCategorie, CanExecute_EditCategorie);
    //        cmdFilter = new clsCustomCommand(Execute_FilterCommand, CanExecute_FilterCommand);

    //        clsMessenger.Default.Register<clsUpdateListMessages>(this, OnUpdateListMessageReceived);

    //        LoadData();
    //        MijnSelectedItem = MijnService.GetFirst();
    //    }



    //    private bool CanExecute_CloseCommand(object obj)
    //    {
    //        return true;
    //    }
    //    private void Execute_CloseCommand(object obj)
    //    {
    //        MainWindow HomeWindow = obj as MainWindow;
    //        if (HomeWindow != null)
    //        {

    //            if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
    //            {
    //                if (MessageBox.Show("Deze domiciliering is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?",
    //                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
    //                {
    //                    OpslaanCommando();
    //                    clsHomeVM vm2 = (clsHomeVM)HomeWindow.DataContext;
    //                    vm2.CurrentViewModel = null;
    //                }
    //            }
    //            clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
    //            vm.CurrentViewModel = null;
    //        }

    //    }


    //    private bool CanExecute_CancelCommand(object obj)
    //    {
    //        return NewStatus;
    //    }

    //    private void Execute_CancelCommand(object obj)
    //    {
    //        MijnSelectedItem = MijnService.GetFirst();
    //        if (MijnSelectedItem != null)
    //        {
    //            MijnSelectedItem.MijnSelectedIndex = 0;
    //            MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
    //        }
    //        NewStatus = false;
    //        IsFocusedAfterNew = false;
    //        IsFocused = true;
    //    }
    //    private bool CanExecute_NewCommand(object obj)
    //    {
    //        return !NewStatus;
    //    }

    //    private void Execute_NewCommand(object obj)
    //    {
    //        clsTransactieModel ItemToInsert = new clsTransactieModel()
    //        {
    //            IsUitgaven = false,
    //            BudgetTransactionID = 0,
    //            Bedrag = null,
    //            Datum = DateOnly.FromDateTime(DateTime.Now),
    //            Onderwerp = String.Empty,
    //            BegunstigdeID = 0,
    //            BudgetCategorieID = 0,
    //            Bijlage=null,

    //        };

    //        MijnSelectedItem = ItemToInsert;


    //        MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
    //        NewStatus = true;
    //        IsFocusedAfterNew = true;
    //    }
    //    private bool CanExecute_DeleteCommand(object obj)
    //    {
    //        if (MijnSelectedItem != null)

    //        {
    //            if (NewStatus)
    //            {
    //                return false;
    //            }
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private void Execute_DeleteCommand(object obj)
    //    {
    //        if (MessageBox.Show("wil je deze domiciliering verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
    //        {

    //        }
    //        if (MijnSelectedItem != null)
    //        {
    //            if (MijnService.Delete(MijnSelectedItem))
    //            {
    //                NewStatus = false;
    //                LoadData();
    //            }
    //            else
    //            {
    //                MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
    //            }
    //        }
    //    }

    //    private bool CanExecute_SaveCommand(object obj)
    //    {
    //        if (MijnSelectedItem != null &&
    //        MijnSelectedItem.Error == null &&
    //        MijnSelectedItem.IsDirty == true)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private void Execute_SaveCommand(object obj)
    //    {
    //        OpslaanCommando();

    //    }
    //    #region Popup Windows
    //    private void OnUpdateListMessageReceived(clsUpdateListMessages obj)
    //    {
    //        //refresh
    //        LoadData();
    //        _DialogService.CloseDialog();
    //    }

        

    //    private bool CanExecute_EditBegunstigde(object obj)
    //    {
    //        return true;
    //    }

    //    private void EditBegunstigde(object obj)
    //    {
    //        _DialogService.ShowDialog(new ucBegunstigden(), "Begunstigde");
    //    }


    //    private bool CanExecute_EditCategorie(object obj)
    //    {
    //        return true;
    //    }

    //    private void EditCategorie(object obj)
    //    {
    //        _DialogService.ShowDialog(new ucCategorie(), "Categorie");
    //    }

    //    #endregion


    //    #region Filter_Domciliering

    //    private string _filterTekst;

    //    public string FilterTekst
    //    {
    //        get
    //        {
    //            return _filterTekst;
    //        }
    //        set
    //        {
    //            _filterTekst = value;
    //            OnPropertyChanged(nameof(FilterTekst));
    //        }
    //    }

    //    private ObservableCollection<clsDomicilieringModel> _gefilterdeCollectie;

    //    public ObservableCollection<clsDomicilieringModel> GefilterdeCollectie
    //    {
    //        get
    //        {
    //            return _gefilterdeCollectie;
    //        }
    //        set
    //        {
    //            _gefilterdeCollectie = value;
    //            OnPropertyChanged(nameof(_gefilterdeCollectie));
    //        }
    //    }

    //    private bool CanExecute_FilterCommand(object obj)
    //    {
    //        return true;
    //    }

    //    private void Execute_FilterCommand(object obj)
    //    {
    //        //if (string.IsNullOrWhiteSpace(FilterTekst))
    //        //{
    //        //    // Als er geen filtertekst is, toon alles
    //        //    GefilterdeCollectie = new ObservableCollection<clsTransactieModel>(MijnCollectie);
    //        //}
    //        //else
    //        //{
    //        //    // Filter de collectie op basis van FilterTekst
    //        //    var gefilterdeItems = MijnCollectie
    //        //        .Where(item =>

    //        //            (item.Begunstigde.IndexOf(FilterTekst, StringComparison.OrdinalIgnoreCase) >= 0))
    //        //        .ToList();

    //        //    // Update de gefilterde collectie
    //        //    GefilterdeCollectie = new ObservableCollection<clsTransactieModel>(gefilterdeItems);
    //        //}
    //    }

    //    #endregion
    }
}
//To Do : Filter installeren voor zoeken op Frequentie, Begunstigde, Categorie

