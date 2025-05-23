﻿using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Todo;
using HomeManager.View;
using HomeManager.ViewModel.Todo;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Timers;

namespace HomeManager.ViewModel
{
    public class clsTodoVM : clsCommonModelPropertiesBase
    {
        clsCollectiesDataService MijnService;
        clsTodoPopupDataService MijnServiceTodoPopup;
        clsTodoDetailsDataService MijnServiceTodoDetails;
        clsCollectiesVM CollectiesVM;
        public ICommand RefreshCommand { get; }

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

            OpenTodoPopupCommand = new clsRelayCommand<object>(param => OpenTodoPopup());
            OpenTodoCollectiesCommand = new clsRelayCommand<object>(param => OpenTodoCollecties());
            OpenTodoDetailsCommand = new clsRelayCommand<object>(param => OpenTodoDetails(param));
            OpenTodoBijlageCommand = new clsRelayCommand<object>(param => OpenTodoBijlage(param as clsTodoPopupM));
            EditTodoCommand = new clsRelayCommand<object>(param => EditTodoItem(param as clsTodoPopupM));
            DeleteTodoCommand = new clsRelayCommand<object>(param => DeleteTodoItem(param as clsTodoPopupM), param => CanDeleteTodoItem(param as clsTodoPopupM));
            EditTodoDetailCommand = new clsRelayCommand<object>(param => EditTodoDetailItem(param as clsTodoDetailsM));
            DeleteTodoDetailCommand = new clsRelayCommand<object>(param => DeleteTodoDetailItem(param as clsTodoDetailsM), param => CanDeleteTodoDetailItem(param as clsTodoDetailsM));
            BelangrijkCommand = new clsRelayCommand<object>(param => OnBelangrijkClicked(param as clsTodoPopupM));
            IsKlaarCommand = new clsRelayCommand<object>(param => OnIsKlaarClicked(param as clsTodoPopupM));
            IsKlaarTodoDetailCommand = new clsRelayCommand<object>(param => OnIsKlaarTodoDetailClicked(param as clsTodoDetailsM));

            RefreshCommand = new clsRelayCommand<object>(param => RefreshData());

            // Registreer om berichten te ontvangen
            //clsMessenger.Default.Register<clsCollectieAangemaaktMessage>(this, OnCollectieAangemaakt);
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

        private void OpenTodoPopup()
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
        private void OpenTodoCollecties()
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
        private void OpenTodoDetails(object parameter)
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
        private void OpenTodoBijlage(clsTodoPopupM todoItem)
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

        public ICommand EditTodoCommand { get; }
        private void EditTodoItem(clsTodoPopupM todoItem)
        {
            if (todoItem == null) return;

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


        public ICommand DeleteTodoCommand { get; }
        private bool CanDeleteTodoItem(clsTodoPopupM todoItem)
        {
            return todoItem != null;
        }

        private void DeleteTodoItem(clsTodoPopupM todoItem)
        {
            if (todoItem == null) return;

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

        public ICommand EditTodoDetailCommand { get; }
        private void EditTodoDetailItem(clsTodoDetailsM todoDetail)
        {
            if (todoDetail == null) return;

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

        public ICommand DeleteTodoDetailCommand { get; }
        private bool CanDeleteTodoDetailItem(clsTodoDetailsM todoDetail)
        {
            return todoDetail != null;
        }

        private void DeleteTodoDetailItem(clsTodoDetailsM todoDetail)
        {
            if (todoDetail == null) return;

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

        // Methode die wordt uitgevoerd wanneer de knop wordt geklikt
        private void OnBelangrijkClicked(clsTodoPopupM todoItem)
        {
            if (todoItem == null)
            {
                MessageBox.Show("CommandParameter is null!", "Debug", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

        public ICommand IsKlaarCommand { get; }

        private void OnIsKlaarClicked(clsTodoPopupM todoItem)
        {
            if (todoItem == null)
            {
                MessageBox.Show("Geen Todo-item geselecteerd!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
        public ICommand IsKlaarTodoDetailCommand { get; }
        private void OnIsKlaarTodoDetailClicked(clsTodoDetailsM todoDetailItem)
        {
            if (todoDetailItem == null)
            {
                MessageBox.Show("Geen Todo Detail-item geselecteerd!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update de database
            if (!MijnServiceTodoDetails.Update(todoDetailItem))
            {
                // Toon een foutmelding als de update mislukt
                MessageBox.Show("Kon de waarde van IsKlaar niet bijwerken in de database.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
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
