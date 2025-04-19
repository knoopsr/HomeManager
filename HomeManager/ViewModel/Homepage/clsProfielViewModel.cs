using DocumentFormat.OpenXml.Wordprocessing;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsProfielViewModel : clsBindableBase
    {
        public ICommand cmdClose {  get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public clsPersoonModel Persoon { get; set; }
        public ObservableCollection<clsEmailAdressenModel> Emailadressen { get; set; }
        public ObservableCollection<clsAdressenModel> Adressen { get; set; }
        public ObservableCollection<clsTelefoonNummersModel> Telefoonnummers { get; set; }
        public ObservableCollection<clsNotitiesModel> Notities { get; set; }
        public clsProfielViewModel()
        {
            cmdClose = new clsCustomCommand(ExecuteCloseCommand, CanExecuteCloseCommand);
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            LaadGegevens();
        }
        private void LaadGegevens()
        {
            var persoonID = clsLoginModel.Instance.PersoonID;

            Persoon = new clsPersoonDataService().GetById(persoonID);
            Emailadressen = new clsEmailAdressenDataService().GetByPersoonID(persoonID);
            Adressen = new clsAdressenDataService().GetByPersoonID(persoonID);
            Telefoonnummers = new clsTelefoonNummersDataService().GetByPersoonID(persoonID);
            Notities = new clsNotitiesDataService().GetByPersoonID(persoonID);
        }
        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window wnd)
            {
                wnd.Close();
            }
        }

        private bool CanExecuteCloseCommand(object obj)
        {
            return obj is Window;
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return false;
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
    }
}
