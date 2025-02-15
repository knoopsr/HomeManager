using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Todo
{
    public class clsTodoDetailsVM : clsCommonModelPropertiesBase
    {
        clsTodoDetailsDataService MijnService;

        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }

        public clsTodoDetailsVM(int todoID)
        {
            clsTodoPopupM.Instance.TodoID = todoID;
            MijnService = new clsTodoDetailsDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);

            LoadData();
            MijnSelectedTodoDetail = MijnService.GetFirst();
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

        private clsTodoDetailsM _MijnSelectedTodoDetail;
        public clsTodoDetailsM MijnSelectedTodoDetail
        {
            get
            {
                return _MijnSelectedTodoDetail;
            }
            set
            {
                if (value != null)
                {
                    if (_MijnSelectedTodoDetail != null && _MijnSelectedTodoDetail.IsDirty)
                    {
                        if (MessageBox.Show("wil je " + _MijnSelectedTodoDetail + " Opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _MijnSelectedTodoDetail = value;
                OnPropertyChanged();
            }
        }

        private void OpslaanCommando()
        {
            if (MijnSelectedTodoDetail != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedTodoDetail))
                    {
                        MijnSelectedTodoDetail.IsDirty = false;
                        MijnSelectedTodoDetail.MijnSelectedIndex = 0;
                        MijnSelectedTodoDetail.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedTodoDetail.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(MijnSelectedTodoDetail))
                    {
                        MijnSelectedTodoDetail.IsDirty = false;
                        MijnSelectedTodoDetail.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedTodoDetail.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        private void LoadData()
        {
            MijnTodoDetails = MijnService.GetAll();
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

                if (MijnSelectedTodoDetail != null && MijnSelectedTodoDetail.Error == null && MijnSelectedTodoDetail.IsDirty == true)
                {
                    if (MessageBox.Show(MijnSelectedTodoDetail.ToString().ToUpper() +
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
            MijnSelectedTodoDetail = MijnService.GetFirst();
            if (MijnSelectedTodoDetail != null)
            {
                MijnSelectedTodoDetail.MijnSelectedIndex = 0;
                MijnSelectedTodoDetail.MyVisibility = (int)Visibility.Visible;
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
            if (clsTodoPopupM.Instance.TodoID == 0)
            {
                MessageBox.Show("TodoID is niet correct ingesteld.");
                return;
            }

            clsTodoDetailsM ItemToInsert = new clsTodoDetailsM()
            {
                TodoDetailID = 0,
                TodoID = clsTodoPopupM.Instance.TodoID,
                TodoDetail = string.Empty,
                IsKlaar = false,
                Volgorde = null
            };

            MijnSelectedTodoDetail = ItemToInsert;

            MijnSelectedTodoDetail.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_DeleteCommand(object obj)
        {
            if (MijnSelectedTodoDetail != null)

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
            if (MijnSelectedTodoDetail != null)
            {
                if (MessageBox.Show("Wil je " + MijnSelectedTodoDetail.ToString().ToUpper() + " verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnService.Delete(MijnSelectedTodoDetail))
                    {
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedTodoDetail.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        private bool CanExecute_SaveCommand(object obj)
        {
            return MijnSelectedTodoDetail != null &&
                   MijnSelectedTodoDetail.Error == null &&
                   MijnSelectedTodoDetail.IsDirty;
        }


        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();
        }

        private int _todoID;
        public int TodoID
        {
            get
            {
                return _todoID;
            }
            set
            {
                _todoID = clsTodoPopupM.Instance.TodoID;
            }
        }
    }
}
