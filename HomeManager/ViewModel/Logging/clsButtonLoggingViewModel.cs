using HomeManager.Common;
using HomeManager.DataService.Logging;
using HomeManager.Helpers;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Logging
{
    public class clsButtonLoggingViewModel : clsCommonModelPropertiesBase
    {
        clsButtonLoggingDataService MijnService;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }


        private ObservableCollection<clsButtonLoggingModel> _mijnCollectie;
        public ObservableCollection<clsButtonLoggingModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        public clsButtonLoggingViewModel() 
        {
            MijnService = new clsButtonLoggingDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }


        #region Command Methods

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private void Execute_Close_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {  
                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return true;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return false;
        }

        private void Execute_Delete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            return false;
        }

        private void Execute_Save_Command(object? obj)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
