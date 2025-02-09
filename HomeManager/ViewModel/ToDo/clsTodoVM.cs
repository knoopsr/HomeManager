using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Todo;
using HomeManager.View;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeManager.ViewModel
{
    public class clsTodoVM : clsCommonModelPropertiesBase
    {
        clsCollectiesDataService MijnService;
        clsTodoPopupDataService MijnServiceTodoPopup;
        clsTodoDetailsDataService MijnServiceTodoDetails;
        clsCollectiesVM CollectiesVM;

        public clsTodoVM()
        {
            TodoPopupViewModel = new clsTodoPopupVM();

            MijnService = new clsCollectiesDataService();
            MijnServiceTodoPopup = new clsTodoPopupDataService();
            MijnServiceTodoDetails = new clsTodoDetailsDataService();

            MijnCollectie = MijnService.GetAll();
            MijnCollectieTodoPopup = MijnServiceTodoPopup.GetAll();
            MijnTodoDetails = MijnServiceTodoDetails.GetAll();

            OpenTodoPopupCommand = new clsRelayCommand<object>(param => OpenTodoPopup());
            OpenTodoCollectiesCommand = new clsRelayCommand<object>(param => OpenTodoCollecties());
            OpenTodoDetailsCommand = new clsRelayCommand<object>(param => OpenTodoDetails());
            OpenTodoBijlageCommand = new clsRelayCommand<object>(param => OpenTodoBijlage());
            EditTodoCommand = new clsRelayCommand<object>(param => EditTodoItem(param as clsTodoPopupM));
            DeleteTodoCommand = new clsRelayCommand<object>(param => DeleteTodoItem(param as clsTodoPopupM), param => CanDeleteTodoItem(param as clsTodoPopupM));
            EditTodoDetailCommand = new clsRelayCommand<object>(param => EditTodoDetailItem(param as clsTodoDetailsM));
            DeleteTodoDetailCommand = new clsRelayCommand<object>(param => DeleteTodoDetailItem(param as clsTodoDetailsM), param => CanDeleteTodoDetailItem(param as clsTodoDetailsM));

            // Registreer om berichten te ontvangen
            clsMessenger.Default.Register<clsCollectieAangemaaktMessage>(this, OnCollectieAangemaakt);
        }

        //private void OnCollectieAangemaakt(clsCollectieAangemaaktMessage message)
        //{
        //    // Voeg de nieuwe collectie toe aan de collectie
        //    MijnCollectie.Add(message.NieuweCollectie);
        //}
        private void OnCollectieAangemaakt(clsCollectieAangemaaktMessage message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MijnCollectie.Add(message.NieuweCollectie);
            });
        }


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
                OnPropertyChanged();
            }
        }

        private clsTodoPopupM _SelectedCollectieTodoPopup;
        public clsTodoPopupM SelectedCollectieTodoPopup
        {
            get
            {
                return _SelectedCollectieTodoPopup;
            }
            set
            {
                _SelectedCollectieTodoPopup = value;
                OnPropertyChanged();
            }
        }

        private clsCollectiesM _MijnSelectedItem;
        public clsCollectiesM MijnSelectedItem
        {
            get
            {
                return _MijnSelectedItem;
            }
            set
            {
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private clsCollectiesM _SelectedCollectie;
        public clsCollectiesM SelectedCollectie
        {
            get
            {
                return _SelectedCollectie;
            }
            set
            {
                _SelectedCollectie = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenTodoPopupCommand { get; }

        private void OpenTodoPopup()
        {
            var todoPopupWindow = new Window
            {
                Content = new ucTodoPopup(),
                Title = "Todo Popup",
                Width = 800,
                Height = 450
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
        private void OpenTodoDetails()
        {
            var todoDetailsWindow = new Window
            {
                Content = new ucTodoDetails(),
                Title = "Todo Details",
                Width = 800,
                Height = 450
            };
            todoDetailsWindow.ShowDialog();
        }

        public ICommand OpenTodoBijlageCommand { get; }
        private void OpenTodoBijlage()
        {
            var todoBijlageWindow = new Window
            {
                Content = new ucTodoBijlage(),
                Title = "Todo Bijlage",
                Width = 800,
                Height = 450
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
                Content = new ucTodoPopup(),
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
    }
}
