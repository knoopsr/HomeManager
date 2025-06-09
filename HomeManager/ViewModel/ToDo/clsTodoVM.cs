using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Model.Todo;
using HomeManager.View;
using HomeManager.ViewModel.Todo;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTodoVM : clsCommonModelPropertiesBase
    {
        clsCollectiesDataService MijnService;
        clsTodoPopupDataService MijnServiceTodoPopup;
        clsTodoDetailsDataService MijnServiceTodoDetails;
        clsCollectiesVM CollectiesVM;
        public ICommand RefreshCommand { get; }
        public ICommand cmdClose { get; set; }

        public clsTodoVM()
        {
            TodoPopupViewModel = new clsTodoPopupVM();

            MijnService = new clsCollectiesDataService();
            MijnServiceTodoPopup = new clsTodoPopupDataService();
            MijnServiceTodoDetails = new clsTodoDetailsDataService();

            MijnCollectie = MijnService.GetAll();
            MijnCollectieTodoPopup = MijnServiceTodoPopup.GetAll();
            MijnTodoDetails = MijnServiceTodoDetails.GetAll();

            FilteredTodoItems = new ObservableCollection<clsTodoPopupM>();
            AfgehandeldeTodoItems = new ObservableCollection<clsTodoPopupM>();
            OnAfgehandeldeTodoItems = new ObservableCollection<clsTodoPopupM>();
            IsCollectieItemSelected = false;

            // OpenTodoPopupCommand = new clsRelayCommand<object>(param => OpenTodoPopup());
            //OpenTodoCollectiesCommand = new clsRelayCommand<object>(param => OpenTodoCollecties());
            //OpenTodoDetailsCommand = new clsRelayCommand<object>(param => OpenTodoDetails(param));
            //OpenTodoBijlageCommand = new clsRelayCommand<object>(param => OpenTodoBijlage(param as clsTodoPopupM));
            //EditTodoCommand = new clsRelayCommand<object>(param => EditTodoItem(param as clsTodoPopupM));
            //DeleteTodoCommand = new clsRelayCommand<object>(param => DeleteTodoItem(param as clsTodoPopupM), param => CanDeleteTodoItem(param as clsTodoPopupM));
            //EditTodoDetailCommand = new clsRelayCommand<object>(param => EditTodoDetailItem(param as clsTodoDetailsM));
            //DeleteTodoDetailCommand = new clsRelayCommand<object>(param => DeleteTodoDetailItem(param as clsTodoDetailsM), param => CanDeleteTodoDetailItem(param as clsTodoDetailsM));
            //BelangrijkCommand = new clsRelayCommand<object>(param => OnBelangrijkClicked(param as clsTodoPopupM));
            //IsKlaarCommand = new clsRelayCommand<object>(param => OnIsKlaarClicked(param as clsTodoPopupM));
            //IsKlaarTodoDetailCommand = new clsRelayCommand<object>(param => OnIsKlaarTodoDetailClicked(param as clsTodoDetailsM));

            OpenTodoPopupCommand = new clsCustomCommand(Execute_OpenTodoPopupCommand, CanExecute_OpenTodoPopupCommand);
            OpenTodoCollectiesCommand = new clsCustomCommand(Execute_OpenTodoCollectiesCommand, CanExecute_OpenTodoCollectiesCommand);
            OpenTodoDetailsCommand = new clsCustomCommand(Execute_OpenTodoDetailsCommand, CanExecute_OpenTodoDetailsCommand);
            OpenTodoBijlageCommand = new clsCustomCommand(Execute_OpenTodoBijlageCommand, CanExecute_OpenTodoBijlageCommand);
            EditTodoCommand = new clsCustomCommand(Execute_EditTodoCommand, CanExecute_EditTodoCommand);
            DeleteTodoCommand = new clsCustomCommand(Execute_DeleteTodoCommand, CanExecute_DeleteTodoCommand);
            EditTodoDetailCommand = new clsCustomCommand(Execute_EditTodoDetailCommand, CanExecute_EditTodoDetailCommand);
            DeleteTodoDetailCommand = new clsCustomCommand(Execute_DeleteTodoDetailCommand, CanExecute_DeleteTodoDetailCommand);
            BelangrijkCommand = new clsCustomCommand(Execute_BelangrijkCommand, CanExecute_BelangrijkCommand);
            IsKlaarCommand = new clsCustomCommand(Execute_IsKlaarCommand, CanExecute_IsKlaarCommand);
            IsKlaarTodoDetailCommand = new clsCustomCommand(Execute_IsKlaarTodoDetailCommand, CanExecute_IsKlaarTodoDetailCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);

            RefreshCommand = new clsRelayCommand<object>(param => RefreshData());

            // Registreer om berichten te ontvangen
            //clsMessenger.Default.Register<clsCollectieAangemaaktMessage>(this, OnCollectieAangemaakt);
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

                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }


        }

        private void RefreshData()
        {
            // Laad de gegevens opnieuw
            MijnCollectie = MijnService.GetAll();
            MijnCollectieTodoPopup = MijnServiceTodoPopup.GetAll();
            MijnTodoDetails = MijnServiceTodoDetails.GetAll();

            // Update de gefilterde lijsten
            if (MijnSelectedCollectieItem != null)
            {
                UpdateFilteredTodoItems(MijnSelectedCollectieItem.ToDoCollectieID);
            }
            if (MijnSelectedItemTodoPopup != null)
            {
                UpdateFilteredTodoDetails(MijnSelectedItemTodoPopup.TodoID);
            }
        }

        //private void OnCollectieAangemaakt(clsCollectieAangemaaktMessage message)
        //{
        //    // Voeg de nieuwe collectie toe aan de collectie
        //    MijnCollectie.Add(message.NieuweCollectie);
        //}
        //private void OnCollectieAangemaakt(clsCollectieAangemaaktMessage message)
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        MijnCollectie.Add(message.NieuweCollectie);
        //    });
        //}


        public clsTodoPopupVM TodoPopupViewModel { get; set; }


        private ObservableCollection<clsCollectiesM> _MijnCollectie;
        public ObservableCollection<clsCollectiesM> MijnCollectie
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
        private clsCollectiesM _MijnSelectedCollectieItem;
        public clsCollectiesM MijnSelectedCollectieItem
        {
            get
            {
                return _MijnSelectedCollectieItem;
            }
            set
            {
                _MijnSelectedCollectieItem = value;
                IsCollectieItemSelected = value != null;
                if (value != null)
                {
                    UpdateFilteredTodoItems(value.ToDoCollectieID);
                }
                OnPropertyChanged();
            }
        }


        private ObservableCollection<clsTodoPopupM> _MijnCollectieTodoPopup;
        public ObservableCollection<clsTodoPopupM> MijnCollectieTodoPopup
        {
            get
            {
                return _MijnCollectieTodoPopup;
            }
            set
            {
                _MijnCollectieTodoPopup = value;
                OnPropertyChanged();
            }
        }

        private clsTodoPopupM _MijnSelectedItemTodoPopup;
        public clsTodoPopupM MijnSelectedItemTodoPopup
        {
            get
            {
                return _MijnSelectedItemTodoPopup;
            }
            set
            {
                _MijnSelectedItemTodoPopup = value;
                IsTodoItemSelected = value != null;
                if (value != null)
                {
                    UpdateFilteredTodoDetails(value.TodoID);
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTodoDetailsM> _MijnTodoDetails;
        public ObservableCollection<clsTodoDetailsM> MijnTodoDetails
        {
            get
            {
                return _MijnTodoDetails;
            }
            set
            {
                _MijnTodoDetails = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTodoDetailsM> _MijnSelectedTodoDetail;
        public ObservableCollection<clsTodoDetailsM> MijnSelectedTodoDetail
        {
            get
            {
                return _MijnSelectedTodoDetail;
            }
            set
            {
                _MijnSelectedTodoDetail = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenTodoPopupCommand { get; }
        private bool CanExecute_OpenTodoPopupCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("552");
        }

        private void Execute_OpenTodoPopupCommand(object? obj)
        {
            if (MijnSelectedCollectieItem == null)
            {
                MessageBox.Show("Selecteer eerst collectie uit de lijst!", "Fout", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Stel de geselecteerde item in de TodoPopupViewModel in
            TodoPopupViewModel.MijnSelectedCollectieItem = MijnSelectedCollectieItem;

            var todoPopupWindow = new Window
            {
                Content = new ucTodoPopup(MijnSelectedCollectieItem),
                Title = "Todo Popup",
                Width = 800,
                Height = 450,
                DataContext = TodoPopupViewModel // Stel de DataContext in op de TodoPopupViewModel
            };

            todoPopupWindow.ShowDialog();
        }

        public ICommand OpenTodoCollectiesCommand { get; }
        private bool CanExecute_OpenTodoCollectiesCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("551");
        }
        private void Execute_OpenTodoCollectiesCommand(object? obj)
        {
            var collectiesWindow = new Window
            {
                Content = new ucCollecties(),
                Title = "Collecties",
                Width = 800,
                Height = 450
            };
            collectiesWindow.ShowDialog();
        }

        public ICommand OpenTodoDetailsCommand { get; }
        private bool CanExecute_OpenTodoDetailsCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("553");
        }
        private void Execute_OpenTodoDetailsCommand(object? obj)
        {
            if (MijnSelectedItemTodoPopup != null)
            {
                int todoID = MijnSelectedItemTodoPopup.TodoID;
                var todoDetailsVM = new clsTodoDetailsVM(todoID);


                var todoDetailsWindow = new Window
                {
                    Content = new ucTodoDetails(),
                    Title = "Todo Details",
                    Width = 800,
                    Height = 450
                };

                todoDetailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een Todo Item!", "Fout", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public ICommand OpenTodoBijlageCommand { get; }
        private bool CanExecute_OpenTodoBijlageCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("554");
        }
        private void Execute_OpenTodoBijlageCommand(object? obj)
        {
            if (obj != null && obj is clsTodoPopupM todoItem)
            {
                MijnSelectedItemTodoPopup = todoItem;
                //// Controleer of er een item is geselecteerd
                if (MijnSelectedItemTodoPopup == null)
                {
                    MessageBox.Show("Selecteer eerst een Todo Item om een bijlage toe te voegen", "Fout", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Stel de TodoID in op de static Instance
                clsTodoPopupM.Instance.TodoID = MijnSelectedItemTodoPopup.TodoID;

                var todoBijlageVM = new clsTodoBijlageVM();

                var todoBijlageWindow = new Window
                {
                    Content = new ucTodoBijlage(),
                    Title = "Todo Bijlage",
                    Width = 800,
                    Height = 450,
                    DataContext = todoBijlageVM // Stel de DataContext in op het ViewModel
                };

                todoBijlageWindow.ShowDialog();
            }
        }

        public ICommand EditTodoCommand { get; }
        private bool CanExecute_EditTodoCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("559");
        }
        private void Execute_EditTodoCommand(object? obj)
        {
            if (obj != null && obj is clsTodoPopupM todoItem)
            {
                // Stel de MijnSelectedItemTodoPopup in op het geselecteerde item
                MijnSelectedItemTodoPopup = todoItem;

                var todoPopupWindow = new Window
                {
                    Content = new ucTodoPopup(todoItem), // Pas de constructor aan om het juiste todoItem te gebruiken
                    Title = "Edit Todo",
                    Width = 800,
                    Height = 450
                };

                // Stel de DataContext van het venster in op de TodoPopupViewModel
                todoPopupWindow.DataContext = TodoPopupViewModel;

                // Update de geselecteerde item in de TodoPopupViewModel
                TodoPopupViewModel.MijnSelectedItem = todoItem;

                todoPopupWindow.ShowDialog();
            }
        }


        public ICommand DeleteTodoCommand { get; }
        private bool CanExecute_DeleteTodoCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            if (permissionChecker.HasPermission("560"))
            {
                return obj != null;
            }
            return false;
        }
        private void Execute_DeleteTodoCommand(object? obj)
        {
            if (obj != null && obj is clsTodoPopupM todoItem)
            {
                if (MessageBox.Show($"Wil je {todoItem.Onderwerp} verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnServiceTodoPopup.Delete(todoItem))
                    {
                        MijnCollectieTodoPopup.Remove(todoItem);
                    }
                    else
                    {
                        MessageBox.Show("Error?", todoItem.ErrorBoodschap);
                    }
                }
            }
        }




        public ICommand EditTodoDetailCommand { get; }
        private bool CanExecute_EditTodoDetailCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("542");
        }
        private void Execute_EditTodoDetailCommand(object? obj)
        {
            if (obj != null && obj is clsTodoDetailsM todoDetail)
            {

                // Stel de MijnSelectedTodoDetail in op het geselecteerde detail
                MijnSelectedTodoDetail = new ObservableCollection<clsTodoDetailsM> { todoDetail };

                var todoDetailsWindow = new Window
                {
                    Content = new ucTodoDetails(),
                    Title = "Edit Todo Detail",
                    Width = 800,
                    Height = 450
                };

                // Stel de DataContext van het venster in op de huidige ViewModel
                todoDetailsWindow.DataContext = this;

                todoDetailsWindow.ShowDialog();
            }
        }

        public ICommand DeleteTodoDetailCommand { get; }
        private bool CanExecute_DeleteTodoDetailCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            if (permissionChecker.HasPermission("543"))
            {
                return obj != null;
            }
            return false;
        }
        private void Execute_DeleteTodoDetailCommand(object? obj)
        {
            if (obj != null && obj is clsTodoDetailsM todoDetail)
            {

                if (MessageBox.Show($"Wil je {todoDetail.TodoDetail} verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnServiceTodoDetails.Delete(todoDetail))
                    {
                        MijnTodoDetails.Remove(todoDetail);
                    }
                    else
                    {
                        MessageBox.Show("Error?", todoDetail.ErrorBoodschap);
                    }
                }
            }
        }

        private bool _isTodoItemSelected;
        public bool IsTodoItemSelected
        {
            get { return _isTodoItemSelected; }
            set
            {
                _isTodoItemSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTodoDetailsM> _filteredTodoDetails;
        public ObservableCollection<clsTodoDetailsM> FilteredTodoDetails
        {
            get { return _filteredTodoDetails; }
            set
            {
                _filteredTodoDetails = value;
                OnPropertyChanged();
            }
        }

        private void UpdateFilteredTodoDetails(int todoID)
        {
            FilteredTodoDetails = new ObservableCollection<clsTodoDetailsM>(
                MijnTodoDetails.Where(detail => detail.TodoID == todoID).ToList());
        }

        private bool _isCollectieItemSelected;
        public bool IsCollectieItemSelected
        {
            get { return _isCollectieItemSelected; }
            set
            {
                _isCollectieItemSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTodoPopupM> _filteredTodoItems;
        public ObservableCollection<clsTodoPopupM> FilteredTodoItems
        {
            get { return _filteredTodoItems; }
            set
            {
                _filteredTodoItems = value;
                OnPropertyChanged();
            }
        }
        private void UpdateFilteredTodoItems(int todoCollectieID)
        {
            var items = MijnCollectieTodoPopup.Where(item => item.TodoCollectieID == todoCollectieID).ToList();

            AfgehandeldeTodoItems.Clear();
            OnAfgehandeldeTodoItems.Clear();

            foreach (var item in items)
            {
                if (item.IsKlaar)
                {
                    AfgehandeldeTodoItems.Add(item);
                }
                else
                {
                    OnAfgehandeldeTodoItems.Add(item);
                }
            }

            FilteredTodoItems = new ObservableCollection<clsTodoPopupM>(items);
        }

        public ICommand BelangrijkCommand { get; }
        private bool CanExecute_BelangrijkCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("556");
        }

        // Methode die wordt uitgevoerd wanneer de knop wordt geklikt
        private void Execute_BelangrijkCommand(object? obj)
        {
            if (obj != null && obj is clsTodoPopupM todoItem)
            {

                MijnSelectedItemTodoPopup = todoItem;

                // Toggle de waarde van Belangrijk
                MijnSelectedItemTodoPopup.Belangrijk = !MijnSelectedItemTodoPopup.Belangrijk;

                // Debug-melding om de nieuwe waarde te controleren
                // TODO: Na het werkend krijgen van de kleur -> debuggers verwijderen
                //MessageBox.Show($"Belangrijk is nu: {MijnSelectedItemTodoPopup.Belangrijk}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

                // Update de database
                if (!MijnServiceTodoPopup.Update(MijnSelectedItemTodoPopup))
                {
                    // Toon een foutmelding als de update mislukt
                    MessageBox.Show("Kon de waarde van Belangrijk niet bijwerken in de database.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public ICommand IsKlaarCommand { get; }
        private bool CanExecute_IsKlaarCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("555");
        }
        private void Execute_IsKlaarCommand(object? obj)
        {
            if (obj != null && obj is clsTodoPopupM todoItem)
            {

                // Verplaats het item tussen de lijsten op basis van de IsKlaar waarde
                if (todoItem.IsKlaar)
                {
                    OnAfgehandeldeTodoItems.Remove(todoItem);
                    AfgehandeldeTodoItems.Add(todoItem);
                }
                else
                {
                    AfgehandeldeTodoItems.Remove(todoItem);
                    OnAfgehandeldeTodoItems.Add(todoItem);
                }

                // Update de database
                if (!MijnServiceTodoPopup.Update(todoItem))
                {
                    MessageBox.Show("Kon de waarde van IsKlaar niet bijwerken in de database.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public ICommand IsKlaarTodoDetailCommand { get; }
        private bool CanExecute_IsKlaarTodoDetailCommand(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("544");
        }
        private void Execute_IsKlaarTodoDetailCommand(object? obj)
        {
            if (obj != null && obj is clsTodoDetailsM todoDetailItem)
            {

                // Update de database
                if (!MijnServiceTodoDetails.Update(todoDetailItem))
                {
                    // Toon een foutmelding als de update mislukt
                    MessageBox.Show("Kon de waarde van IsKlaar niet bijwerken in de database.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private ObservableCollection<clsTodoPopupM> _afgehandeldeTodoItems; //TodoItems die Isklaar = true zijn
        public ObservableCollection<clsTodoPopupM> AfgehandeldeTodoItems
        {
            get => _afgehandeldeTodoItems;
            set
            {
                _afgehandeldeTodoItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTodoPopupM> _onAfgehandeldeTodoItems; //TodoItems die Isklaar = false zijn
        public ObservableCollection<clsTodoPopupM> OnAfgehandeldeTodoItems
        {
            get => _onAfgehandeldeTodoItems;
            set
            {
                _onAfgehandeldeTodoItems = value;
                OnPropertyChanged();
            }
        }
    }
}
