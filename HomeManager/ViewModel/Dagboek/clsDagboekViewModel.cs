using HomeManager.Common;
using HomeManager.DataService.Dagboek;
using HomeManager.Helpers;
using HomeManager.Model.Dagboek;
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
    public class clsDagboekViewModel : clsCommonModelPropertiesBase
    {
        private int PersoonID
        {
            get { return clsLoginModel.Instance.AccountID; }
        }
            
        private bool isNew = false;
        private bool isEmptyCollection = false;

        //test
        private ObservableCollection<string> _myTestString;
        public ObservableCollection<string> myTestString
        { get { return _myTestString; }

            set
            {
                _myTestString = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsDagboekModel> _mijnCollectie;

        public ObservableCollection<clsDagboekModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
            }
        }

        private clsDagboekModel _mySelectedItem;

        public clsDagboekModel MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
                if (_mySelectedItem != value)
                {
                    if (_mySelectedItem != null)
                    {
                        IsDirty = true;
                    }
                }
                _mySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public clsDagboekDataService MyService { get; set; }

        private void GenerateCollection()
        {
            MijnCollectie = new ObservableCollection<clsDagboekModel>();
            MijnCollectie = MyService.GetAllByPersoonID(PersoonID);

        }

        public clsDagboekViewModel()
        {
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdTest = new clsCustomCommand(Execute_Test, CanExecute_Test);


            MyService = new clsDagboekDataService();
            GenerateCollection();

            myTestString = new ObservableCollection<string> { };
            myTestString.Add("test");
            myTestString.Add("test2");


        }

        private bool CanExecute_Test(object? obj)
        {
            return true;
        }

        private void Execute_Test(object? obj)
        {
            MessageBox.Show("test");
        }




        /*Commands zijn onderverdeeld in 3 regions
         * -Fields
         * -CanExecutes
         * -Actions
         */
        #region Commands
        #region CommandFields
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdTest { get; set; }
        #endregion

        #region Command CanExecutes
        private bool CanExecute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            //if(isNew)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
        #endregion

        #region Command Actions
        private void Execute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_New_Command(object? obj)
        {
            MessageBox.Show("new");
        }

        private void Execute_Delete_Command(object? obj)
        {
            MessageBox.Show("delete");
        }

        private void Execute_Save_Command(object? obj)
        {
            MessageBox.Show("save");
        }
        #endregion

        #endregion

    }
}

