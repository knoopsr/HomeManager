﻿using HomeManager.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using System.Diagnostics;

namespace HomeManager.ViewModel
{
    public class clsAdressenViewModel : clsCommonModelPropertiesBase
    {
        clsAdressenDataService MijnService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }

        private ObservableCollection<clsAdressenModel> mijnCollectie;

        public ObservableCollection<clsAdressenModel> MijnCollectie
        {
            get
            {
                return mijnCollectie;
            }
            set
            {
                mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsAdressenModel mijnSelectedItem;
        public clsAdressenModel MijnSelectedItem
        {
            get
            {
                return mijnSelectedItem;
            }
            set
            {
                if (value != null)
                {
                    if (mijnSelectedItem != null && mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + mijnSelectedItem + "Opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            mijnSelectedItem.IsDirty = false;
                            mijnSelectedItem.MijnSelectedIndex = 0;
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private clsPersonenViewModel _mijnSelectedPersoonItem;
        public clsPersonenViewModel MijnSelectedPersoonItem
        {
            get
            {
                return _mijnSelectedPersoonItem;
            }
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedPersoonItem != null && _mijnSelectedPersoonItem.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + _mijnSelectedPersoonItem + "Opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            _mijnSelectedPersoonItem.IsDirty = false;
                            _mijnSelectedPersoonItem.MijnSelectedIndex = 0;
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _mijnSelectedPersoonItem = value;
                OnPropertyChanged();
            }
        }

        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        private bool _IsFocused = false;
        public bool IsFocused
        {
            get
            {
                return _IsFocused;
            }
            set
            {
                _IsFocused = value;
                OnPropertyChanged();
            }
        }

        private bool _IsFocusedAfterNew = false;
        public bool IsFocusedAfterNew
        {
            get
            {
                return _IsFocusedAfterNew;
            }
            set
            {
                _IsFocusedAfterNew = value;
                OnPropertyChanged();
            }
        }

        public clsAdressenViewModel()
        {
            MijnService = new clsAdressenDataService();
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            clsMessenger.Default.Register<clsAdressenModel>(this, OnAdressenReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
            //MijnSelectedItem.MijnSelectedIndex = 0;
        }


        private void OnAdressenReceived(clsAdressenModel obj)
        {
            //mijnSelectedItem = obj;

            //if (obj.AdresID == 0)
            //{
            //    NewStatus = true;
            //}

            mijnSelectedItem = obj;

            if (mijnSelectedItem != null && mijnSelectedItem.AdresID == 0)
            {
                NewStatus = true;
                mijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            }
        }
        

        private bool CanExecute_NewCommand(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_NewCommand(object? obj)
        {
            clsAdressenModel ItemToInsert = new clsAdressenModel()
            {
                AdresID = 0,
                GemeenteID = 0,
                PersoonID = 0,
                FunctieID = 0,
                Straat = string.Empty,
                Nummer = string.Empty,
            };
            MijnSelectedItem = ItemToInsert;
            //MijnSelectedItem = ItemToInsert;

            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_DeleteCommand(object obj)
        {
            if (MijnSelectedItem != null)
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
            if (MessageBox.Show("Wil je " + MijnSelectedItem + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes) if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
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
                if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
                {
                    if (MessageBox.Show(MijnSelectedItem.ToString().ToUpper() + "is nog niet opgeslagen, wil je opslaan ?", "Opslaan of sluiten?",
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
            MijnSelectedItem = MijnService.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();

            //if (MijnSelectedItem != null)
            //{
            //    if (NewStatus)
            //    {
            //        if (MijnService.Insert(MijnSelectedItem))
            //        {
            //            MijnSelectedItem.IsDirty = false;
            //            MijnSelectedItem.MijnSelectedIndex = 0;
            //            MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            //            NewStatus = false;
            //            LoadData();
            //        }
            //        else
            //        {
            //            MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
            //        }
            //    }
            //    else
            //    {
            //        if (MijnService.Update(MijnSelectedItem))
            //        {
            //            MijnSelectedItem.IsDirty = false;
            //            MijnSelectedItem.MijnSelectedIndex = 0;
            //            NewStatus = false;
            //            LoadData();
            //        }
            //        else
            //        {
            //            MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
            //        }
            //    }
            //}
        }
    }
}