﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.DataService.Security;

namespace HomeManager.ViewModel
{
    public class clsCredentialGroupViewModel : clsCommonModelPropertiesBase
    {
        clsWachtwoordGroepDataService MijnService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        private ObservableCollection<clsWachtWoordGroepModel> _mijnCollectie;
        public ObservableCollection<clsWachtWoordGroepModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsWachtWoordGroepModel _mijnSelectedItem;
        public clsWachtWoordGroepModel MijnSelectedItem
        {
            get { return _mijnSelectedItem; }
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        private void OpslaanCommando()
        {
            if (_mijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        public clsCredentialGroupViewModel()
        {
            MijnService = new clsWachtwoordGroepDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();

            MijnSelectedItem = MijnService.GetFirst();
        }

        private void Execute_Save_Command(object? obj)
        {
            OpslaanCommando();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            if (MijnSelectedItem != null &&
                MijnSelectedItem.Error == null &&
                MijnSelectedItem.IsDirty == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show(
                "Wil je " + MijnSelectedItem + " verwijderen?",
                "Verwijderen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
            }
        }

        private bool CanExecute_Delete_Command(object? obj)
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

        private void Execute_New_Command(object? obj)
        {
            clsWachtWoordGroepModel _itemToInsert = new clsWachtWoordGroepModel()
            {
                WachtwoordGroepID = 0,
                WachtwoordGroep = string.Empty

            };
            MijnSelectedItem = _itemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_Cancel_Command(object? obj)
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

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return NewStatus;
        }

        private void Execute_Close_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {
                if (MijnSelectedItem != null && MijnSelectedItem.IsDirty == true && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show(
                        MijnSelectedItem.ToString().ToUpper() + " is nog niet opgeslagen, wil je opslaan?",
                        "Opslaan of sluiten?",
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

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_SelectionChangedCommand(object obj)
        {
            return true;
        }

    }
}
