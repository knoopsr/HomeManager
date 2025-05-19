using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using HomeManager.Model.Personen;

namespace HomeManager.ViewModel.Homepage
{
    public class clsSnelkoppelingViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsSnelkoppelingDataService _dataService;

        public ObservableCollection<clsSnelkoppelingModel> Snelkoppelingen { get; set; }

        private clsSnelkoppelingModel _selectedSnelkoppeling;
        public clsSnelkoppelingModel SelectedSnelkoppeling
        {
            get => _selectedSnelkoppeling;
            set
            {
                _selectedSnelkoppeling = value;
                OnPropertyChanged();
            }
        }


        public ICommand cmdSaveMap { get; }
        public ICommand cmdSaveBestand { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

        public clsSnelkoppelingViewModel()
        {
            _dataService = new clsSnelkoppelingDataService();
            Snelkoppelingen = new ObservableCollection<clsSnelkoppelingModel>();

            cmdSaveMap = new clsCustomCommand(Execute_SaveMapCommand, CanExecute_SaveCommand);
            cmdSaveBestand = new clsCustomCommand(Execute_SaveBestandCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);
            LoadSnelkoppelingen();
            clsMessenger.Default.Register<string>(this, message =>
            {
                if (message == "RefreshSnelkoppelingen")
                {
                    LoadSnelkoppelingen();
                }
            });
        }

        private void LoadSnelkoppelingen()
        {
            Snelkoppelingen.Clear();
            var data = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);

            foreach (var item in data)
            {
                bool bestaat = item.Type == "Bestand"
                    ? File.Exists(item.Pad)
                    : Directory.Exists(item.Pad);

                if (bestaat)
                {
                    Snelkoppelingen.Add(item);
                }
                else
                {
                    Debug.WriteLine($" Pad niet gevonden: {item.Pad}");
                }
            }
        }

        private void Execute_SaveMapCommand(object parameter)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Selecteer een map",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string pad = dialog.FileName;
                string naam = Path.GetFileName(pad);

                var nieuwItem = new clsSnelkoppelingModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    Naam = naam,
                    Pad = pad,
                    Type = "Map",
                    CreatedOn = DateTime.Now
                };

                if (_dataService.Insert(nieuwItem))
                {
                    
                    Snelkoppelingen.Add(nieuwItem);
                }
                else
                {
                    System.Windows.MessageBox.Show(" Fout bij toevoegen van snelkoppeling.");
                }
            }
        }

        private bool CanExecute_SaveCommand(object parameter) => true;
        private void Execute_SaveBestandCommand(object parameter)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Selecteer een bestand",
                Filter = "Alle bestanden (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                string pad = dialog.FileName;
                string naam = Path.GetFileName(pad);

                var nieuwItem = new clsSnelkoppelingModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    Naam = naam,
                    Pad = pad,
                    Type = "Bestand"
                };

                if (_dataService.Insert(nieuwItem))
                {
                    Snelkoppelingen.Add(nieuwItem);
                    clsMessenger.Default.Send("RefreshSnelkoppelingen");
                }
                else
                {
                    System.Windows.MessageBox.Show("Fout bij het toevoegen van het bestand.");
                }
            }
        }

        private void Execute_DeleteCommand(object parameter)
        {
            if (parameter is clsSnelkoppelingModel snelkoppeling)
            {
                if (_dataService.Delete(snelkoppeling))
                {
                    Snelkoppelingen.Remove(snelkoppeling);
                    clsMessenger.Default.Send("RefreshSnelkoppelingen");
                }
                else
                {
                    System.Windows.MessageBox.Show("Fout bij het verwijderen van de snelkoppeling.");
                }
            }
        }

        private bool CanExecute_DeleteCommand(object parameter)
        {
            return parameter is clsSnelkoppelingModel;
        }

        private void Execute_OpenCommand(object parameter)
        {
            if (parameter is clsSnelkoppelingModel item && !string.IsNullOrEmpty(item.Pad))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = item.Pad,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Fout bij openen: {ex.Message}");
                }
            }
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedSnelkoppeling != null;
    }
}
