using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Model.Todo;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeManager.ViewModel;

public class clsKleurenVM : clsCommonModelPropertiesBase
{
    clsKleurenDataService MijnService;

    private bool NewStatus = false;
    public ICommand cmdDelete { get; set; }
    public ICommand cmdNew { get; set; }
    public ICommand cmdCancel { get; set; }
    public ICommand cmdClose { get; set; }
    public ICommand cmdSave { get; set; } 

    private ObservableCollection<clsKleurenM> _MijnCollectie;
    public ObservableCollection<clsKleurenM> MijnCollectie
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

    private clsKleurenM _MijnSelectedItem;
    public clsKleurenM MijnSelectedItem
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

    public clsKleurenVM()
    {
        MijnService = new clsKleurenDataService();

        cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
        cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
        cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
        cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
        cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);

        clsMessenger.Default.Register<clsKleurenM>(this, OnCollectiesReceived);
        LoadData();
        LoadColors();
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
        clsKleurenM ItemToInsert = new clsKleurenM()
        {
            ToDoColorID = 0,
            ToDoColor = string.Empty
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

    private void OnCollectiesReceived(clsKleurenM obj)
    {
        _MijnSelectedItem = obj;
    }

    private object _selectedColor;

    // ObservableCollection voor databinding met de ComboBox
    public ObservableCollection<ColorItem> Colors { get; set; }

    // Geselecteerde kleur
    public object SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            OnPropertyChanged(); // Notificeer de View over de wijziging
        }
    }

    private void LoadColors()
    {
        Colors = new ObservableCollection<ColorItem>(
            typeof(Colors).GetProperties()
                          .Select(c => new ColorItem
                          {
                              Name = c.Name,
                              Color = (Color)c.GetValue(null)
                          }));
    }
}

public class ColorItem
{
    public string Name { get; set; }
    public Color Color { get; set; }
}