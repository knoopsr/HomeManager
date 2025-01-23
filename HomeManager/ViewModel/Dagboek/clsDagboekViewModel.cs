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

        private ObservableCollection<clsDagboekModel> _mijnCollectie;

        public ObservableCollection<clsDagboekModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
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
            if (MijnCollectie.Count == 0)
            {
                isEmptyCollection = true;
                //MessageBox.Show("collection is empty = " + isEmptyCollection.ToString());
            }

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

            //validatie
            if (isEmptyCollection)
            {
                //dit maakt een item aan om geen lege collecties te hebben
                var _obj = new clsDagboekModel()
                {
                    PersoonID = this.PersoonID,
                    DateCreated = DateTime.Now,
                    DagboekContentString = "Er zijn nog geen files opgeslagen"
                };
                MijnCollectie.Add(_obj);
                MySelectedItem = _obj;

                isNew = true;
                isEmptyCollection = false;
            }
            else
            {
                MySelectedItem = MijnCollectie.FirstOrDefault();
               
                
            }


        }

        private bool CanExecute_Test(object? obj)
        {
            return true;
        }

        private void Execute_Test(object? obj)
        {
            MessageBox.Show(MySelectedItem.DagboekContentString + "\n" +
                            "IsDirty = " + MySelectedItem.IsDirty);
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
            if (isNew || MySelectedItem.IsDirty)
            {
                return true;
            }
            return false;
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
            safetySave();

            var _obj = new clsDagboekModel()
            {
                PersoonID = this.PersoonID,
                DateCreated = DateTime.Now,
                DagboekContentString = string.Empty
            };
            MijnCollectie.Add(_obj);
            MySelectedItem = _obj;

            isNew = true;
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show("wil je deze entry verwijderen?", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MyService.Delete(MySelectedItem);
                MijnCollectie.Remove(MySelectedItem);
                if (MijnCollectie.Count == 0)
                {
                    var _obj = new clsDagboekModel()
                    {
                        PersoonID = this.PersoonID,
                        DateCreated = DateTime.Now,
                        DagboekContentString = "Er zijn nog geen files opgeslagen"
                    };
                    MijnCollectie.Add(_obj);
                    MySelectedItem = _obj;

                    isNew = true;
                    isEmptyCollection = false;
                }
                MySelectedItem = MijnCollectie.FirstOrDefault();
            }
        }

        private void Execute_Save_Command(object? obj)
        {
            if (isNew)
            {
                MyService.Insert(MySelectedItem);
                isNew = false;
            }
            else
            {
                MyService.Update(MySelectedItem);
            }
            
            MySelectedItem.IsDirty = false;
        }
        #endregion
        #endregion

        private void safetySave()
        {
            if (MySelectedItem.IsDirty = true)
            {
                if (MessageBox.Show("wil je opslaan? laatste veranderingen worden anders niet opgeslagen", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes )
                {
                    if (isNew)
                    {
                        MyService.Insert(MySelectedItem);
                    }
                    else
                    {
                        MyService.Update(MySelectedItem);
                    }
                }
                else
                {
                    if (isEmptyCollection)
                    {
                        MijnCollectie.Remove(MySelectedItem);
                    }
                }
            }
        }
    }
}

