using HomeManager.Common;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Todo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Todo
{
    public class clsTodoBijlageVM : clsCommonModelPropertiesBase
    {
        clsTodoBijlageDataService MijnService;

        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdUploadFile { get; set; }
        public ICommand cmdViewFile { get; set; }

        public clsTodoBijlageVM()
        {
            MijnService = new clsTodoBijlageDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdUploadFile = new clsCustomCommand(Execute_UploadFileCommand, CanExecute_UploadFileCommand);
            cmdViewFile = new clsCustomCommand(Execute_ViewFileCommand, CanExecute_ViewFileCommand);

            LoadData();
            MijnSelectedTodoBijlage = MijnService.GetFirst();

        }

        private ObservableCollection<clsTodoBijlageM> _MijnTodoBijlage;
        public ObservableCollection<clsTodoBijlageM> MijnTodoBijlage
        {
            get
            {
                return _MijnTodoBijlage;
            }
            set
            {
                _MijnTodoBijlage = value;
                OnPropertyChanged();
            }
        }

        private clsTodoBijlageM _MijnSelectedTodoBijlage;
        public clsTodoBijlageM MijnSelectedTodoBijlage
        {
            get
            {
                return _MijnSelectedTodoBijlage;
            }
            set
            {
                if (value != null)
                {
                    if (_MijnSelectedTodoBijlage != null && _MijnSelectedTodoBijlage.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + _MijnSelectedTodoBijlage + " opslaan?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _MijnSelectedTodoBijlage = value;
                OnPropertyChanged();
            }
        }

        private void LoadData()
        {
            MijnTodoBijlage = MijnService.GetAll();
        }

        private void OpslaanCommando()
        {
            if (MijnSelectedTodoBijlage != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedTodoBijlage))
                    {
                        MijnSelectedTodoBijlage.IsDirty = false;
                        MijnSelectedTodoBijlage.MijnSelectedIndex = 0;
                        //MijnSelectedTodoBijlage = My
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_MijnSelectedTodoBijlage.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(MijnSelectedTodoBijlage))
                    {
                        MijnSelectedTodoBijlage.IsDirty = false;
                        MijnSelectedTodoBijlage.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_MijnSelectedTodoBijlage.ErrorBoodschap, "Error?");
                    }
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

                if (MijnSelectedTodoBijlage != null && MijnSelectedTodoBijlage.Error == null && MijnSelectedTodoBijlage.IsDirty == true)
                {
                    if (MessageBox.Show(MijnSelectedTodoBijlage.ToString().ToUpper() +
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
            MijnSelectedTodoBijlage = MijnService.GetFirst();
            if (MijnSelectedTodoBijlage != null)
            {
                MijnSelectedTodoBijlage.MijnSelectedIndex = 0;
                MijnSelectedTodoBijlage.MyVisibility = (int)Visibility.Visible;
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
            clsTodoBijlageM ItemToInsert = new clsTodoBijlageM()
            {
               TodoBijlageID = 0,
               TodoID = 0, //TODO: volgens mij moet ik deze linken met de todoID van tblTodos
               Bijlage = new byte[0],
               BijlageNaam = string.Empty, 
               ControlField = null 
            };

            MijnSelectedTodoBijlage = ItemToInsert;


            MijnSelectedTodoBijlage.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;

            // Verstuur een bericht dat een nieuwe collectie is aangemaakt
            //clsMessenger.Default.Send(new clsCollectieAangemaaktMessage(ItemToInsert));
        }

        private bool CanExecute_DeleteCommand(object obj)
        {
            if (MijnSelectedTodoBijlage != null)

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
            if (MessageBox.Show("wil je " + MijnSelectedTodoBijlage + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
            if (MijnSelectedTodoBijlage != null)
            {
                if (MijnService.Delete(MijnSelectedTodoBijlage))
                {
                    NewStatus = false;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error?", MijnSelectedTodoBijlage.ErrorBoodschap);
                }
            }
        }

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedTodoBijlage != null &&
            MijnSelectedTodoBijlage.Error == null &&
            MijnSelectedTodoBijlage.IsDirty == true)
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

        }

        private void OnCollectiesReceived(clsTodoBijlageM obj)
        {
            _MijnSelectedTodoBijlage = obj;
        }

        private bool CanExecute_UploadFileCommand(object? obj)
        {
            return true;
        }

        private void Execute_UploadFileCommand(object? obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] fileData = File.ReadAllBytes(filePath);
                if (MijnSelectedTodoBijlage != null)
                {
                    MijnSelectedTodoBijlage.Bijlage = fileData;
                    MijnSelectedTodoBijlage.BijlageNaam = Path.GetFileName(filePath);
                    MijnSelectedTodoBijlage.IsDirty = true;
                }
            }
        }

        internal void HandleFileDrop(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            if (MijnSelectedTodoBijlage != null)
            {
                MijnSelectedTodoBijlage.Bijlage = fileData;
                MijnSelectedTodoBijlage.BijlageNaam = Path.GetFileName(filePath);
                MijnSelectedTodoBijlage.IsDirty = true;
            }
        }

        private bool CanExecute_ViewFileCommand(object obj)
        {
            return MijnSelectedTodoBijlage != null && MijnSelectedTodoBijlage.Bijlage != null && MijnSelectedTodoBijlage.Bijlage.Length > 0;
        }

        private void Execute_ViewFileCommand(object obj)
        {
            if (MijnSelectedTodoBijlage != null && MijnSelectedTodoBijlage.Bijlage != null)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), MijnSelectedTodoBijlage.BijlageNaam);
                File.WriteAllBytes(tempFilePath, MijnSelectedTodoBijlage.Bijlage);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(tempFilePath) { UseShellExecute = true });
            }
        }
    }
}
