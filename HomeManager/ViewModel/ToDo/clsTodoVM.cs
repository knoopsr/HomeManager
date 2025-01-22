using HomeManager.Common;
using HomeManager.DataService.ToDo;
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
        clsCollectiesVM CollectiesVM;

        public clsTodoVM()
        {
            TodoPopupViewModel = new clsTodoPopupVM();

            MijnService = new clsCollectiesDataService();
            MijnServiceTodoPopup = new clsTodoPopupDataService();

            //CollectiesVM = collectiesVM;

            //MijnCollectie = CollectiesVM.MijnCollectie;
            MijnCollectie = MijnService.GetAll();
            MijnCollectieTodoPopup = MijnServiceTodoPopup.GetAll();

            //MijnSelectedItem = CollectiesVM.MijnSelectedItem;

            OpenTodoPopupCommand = new clsRelayCommand<object>(param => OpenTodoPopup());
            OpenTodoCollectiesCommand = new clsRelayCommand<object>(param => OpenTodoCollecties());
            OpenTodoDetailsCommand = new clsRelayCommand<object>(param => OpenTodoDetails());
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

        //TODO: public ObservableCollection voor clsTodoPopup maken !!!!

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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isKlaar)
            {
                if (parameter.ToString() == "Background")
                {
                    return isKlaar ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.MistyRose);
                }
                else if (parameter.ToString() == "BorderBrush")
                {
                    return isKlaar ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
