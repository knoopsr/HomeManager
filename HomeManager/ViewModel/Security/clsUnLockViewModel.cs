using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.MailService;
using HomeManager.Model.Security;
using HomeManager.View.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Security
{
   public class clsUnLockViewModel : clsCommonModelPropertiesBase
    {
        clsLockedAccountDataService MijnService;

        private bool _isDirtyLocal = false;

        private ObservableCollection<clsLockedAccountModel> _mijnCollectie;
        public ObservableCollection<clsLockedAccountModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<clsLockedAccountModel> _selectedItem;
        public ObservableCollection<clsLockedAccountModel> SelectedItem
        {
            get { return _selectedItem; }
            set
            {   

                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        public ICommand cmdUnLockUser { get; set; }
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            SelectedItem = new ObservableCollection<clsLockedAccountModel>();

        }
        public clsUnLockViewModel()
        {
            MijnService = new clsLockedAccountDataService();
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();
        }


        private void Execute_Close_Command(object? obj)
        {
            // sluit het venster

            if (obj is Window win)
            {
                win.Close();
            }
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return _isDirtyLocal;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            foreach (var item in MijnCollectie)
            {
                item.IsSelected = false;
                item.IsDirty = false;
            }
            _isDirtyLocal = false;
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

        //private void Execute_Save_Command(object? obj)
        //{
        //    string accountIds = "";
        //    foreach (var item in SelectedItem)
        //    {
        //        accountIds += item.Account.AccountID + "|";
        //    }
        //    accountIds = accountIds.Remove(accountIds.Length - 1);

        //    clsLockedAccountModel model = new clsLockedAccountModel()
        //    {
        //        SelectedItems = accountIds
        //    };


        //    if (MijnService.UnLockUsers(model))
        //    {
        //        _isDirtyLocal = false;
        //        LoadData();
        //    }
        //    else
        //    {
        //        MessageBox.Show(model.ErrorBoodschap, "Error?");
        //    }

        //}

        private async void Execute_Save_Command(object? obj)
        {


            // Maak een DataTable aan voor de TVP
            DataTable inputTable = new DataTable();
            inputTable.Columns.Add("AccountID", typeof(int));
            inputTable.Columns.Add("Wachtwoord", typeof(string));

            PasswordGenerator generator = new PasswordGenerator();

            // Vul de DataTable met gegevens uit de geselecteerde items
            foreach (var item in SelectedItem)
            {
                // Controleer of het item is geselecteerd
                if (item.IsSelected)
                {
                    // Genereer een nieuw wachtwoord
                    item.Account.Wachtwoord = generator.GeneratePassword(8);

                    inputTable.Rows.Add(item.Account.AccountID, item.Account.Wachtwoord);
                }
            }

            // Maak een model en roep de repository aan
            clsLockedAccountModel model = new clsLockedAccountModel
            {
                SelectedItemsList = new ObservableCollection<(int AccountID, string Wachtwoord)>(
                    inputTable.AsEnumerable().Select(row => (
                        AccountID: row.Field<int>("AccountID"),
                        Wachtwoord: row.Field<string>("Wachtwoord")
                    ))
                )
            };

            if (MijnService.UnLockUsers(model))
            {
                _isDirtyLocal = false;
                List<string> verzondenEmails = null;
                foreach (var item in SelectedItem)
                {
                    if (item.IsSelected)
                    {                    
                        clsMailService mailService = new clsMailService();
                        verzondenEmails = await mailService.SendNewPassToPerson(                
                           item.Account,
                           item.Persoon
                       );
                    }
                }


                if (verzondenEmails != null)
                {
                    MessageBox.Show("E-mail succesvol verzonden naar:\n" + string.Join(Environment.NewLine, verzondenEmails));
                }          



                LoadData();
            }
            else
            {
                MessageBox.Show(model.ErrorBoodschap, "Error?");
            }
        }





        private bool CanExecute_Save_Command(object? obj)
        {           
            foreach (var item in SelectedItem)
            {
                if (item.IsDirty)
                {
                    _isDirtyLocal = true;
                    return true;
                }
            }
            _isDirtyLocal = false;
            return false;
        }
    }
}
