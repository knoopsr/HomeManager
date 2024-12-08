using HomeManager.Common;
using HomeManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeManager.ViewModel.Security
{
   public class clsUnLockViewModel : clsCommonModelPropertiesBase
    {
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        public clsUnLockViewModel()
        {
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
        }

        private void Execute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
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
            throw new NotImplementedException();
        }

        private void Execute_New_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_Delete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_Save_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
