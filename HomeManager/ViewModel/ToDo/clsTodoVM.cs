﻿using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Model.Todo;
using HomeManager.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsTodoVM : clsCommonModelPropertiesBase
    {
        clsTodoDataService MijnService;
        clsCollectiesVM CollectiesVM;

        public clsTodoVM(clsCollectiesVM collectiesVM)
        {
            MijnService = new clsTodoDataService();
            CollectiesVM = collectiesVM;
            MijnCollectie = CollectiesVM.MijnCollectie;

            OpenTodoPopupCommand = new clsRelayCommand<object>(param => OpenTodoPopup());
            OpenTodoCollectiesCommand = new clsRelayCommand<object>(param => OpenTodoCollecties());
            OpenTodoDetailsCommand = new clsRelayCommand<object>(param => OpenTodoDetails());
        }

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

        private bool _isTodoPopupOpen;
        public bool IsTodoPopupOpen
        {
            get { return _isTodoPopupOpen; }
            set
            {
                _isTodoPopupOpen = value;
                OnPropertyChanged();
            }
        }

        private bool _isTodoCollectiesOpen;
        public bool IsTodoCollectiesOpen
        {
            get { return _isTodoCollectiesOpen; }
            set
            {
                _isTodoCollectiesOpen = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenTodoPopupCommand { get; }

        private void OpenTodoPopup()
        {
           IsTodoPopupOpen = true;
        }

        public ICommand OpenTodoCollectiesCommand { get; }
        private void OpenTodoCollecties()
        {
            IsTodoCollectiesOpen = true;
        }

        public ICommand OpenTodoDetailsCommand { get; }
        private void OpenTodoDetails()
        {
            IsTodoDetailsOpen = true;
        }

        private bool _isTodoDetailsOpen;
        public bool IsTodoDetailsOpen
        {
            get { return _isTodoDetailsOpen; }
            set
            {
                _isTodoDetailsOpen = value;
                OnPropertyChanged();
            }
        }
    }
}
