using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using HomeManager.Model.Personen;

namespace HomeManager.ViewModel.Homepage
{
    /// <summary>
    /// ViewModel voor het beheren van snelkoppelingen naar mappen of bestanden.
    /// Gebruikers kunnen snel een map of bestand toevoegen, openen of verwijderen.
    /// </summary>
    public class clsSnelkoppelingViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsSnelkoppelingDataService _dataService;

        /// <summary>
        /// ObservableCollection met alle snelkoppelingen van de gebruiker.
        /// </summary>
        public ObservableCollection<clsSnelkoppelingModel> Snelkoppelingen { get; set; }

        /// <summary>
        /// De momenteel geselecteerde snelkoppeling (bijvoorbeeld in de UI).
        /// </summary>
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

        /// <summary>
        /// Commands voor verschillende acties in de UI.
        /// </summary>
        public ICommand cmdSaveMap { get; }
        public ICommand cmdSaveBestand { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

        /// <summary>
        /// Constructor: initialisatie van commands en laden van snelkoppelingen.
        /// </summary>
        public clsSnelkoppelingViewModel()
        {
            _dataService = new clsSnelkoppelingDataService();
            Snelkoppelingen = new ObservableCollection<clsSnelkoppelingModel>();

            // Commands aanmaken
            cmdSaveMap = new clsCustomCommand(Execute_SaveMapCommand, CanExecute_SaveCommand);
            cmdSaveBestand = new clsCustomCommand(Execute_SaveBestandCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);

            LoadSnelkoppelingen();

            // Luister naar eventuele 'refresh'-boodschappen (bijvoorbeeld na toevoegen/verwijderen elders)
            clsMessenger.Default.Register<string>(this, message =>
            {
                if (message == "RefreshSnelkoppelingen")
                {
                    LoadSnelkoppelingen();
                }
            });
        }

        /// <summary>
        /// Laadt de snelkoppelingen uit de database.
        /// Controleert ook of het bestand of de map nog wel bestaat.
        /// </summary>
        private void LoadSnelkoppelingen()
        {
            Snelkoppelingen.Clear();
            var data = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);

            foreach (var item in data)
            {
                // Controleer of het pad nog geldig is
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

        /// <summary>
        /// Command om een map toe te voegen aan de snelkoppelingen.
        /// </summary>
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

        /// <summary>
        /// Command om een bestand toe te voegen aan de snelkoppelingen.
        /// </summary>
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

        /// <summary>
        /// Command om een geselecteerde snelkoppeling te verwijderen.
        /// </summary>
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

        /// <summary>
        /// Command om het bestand of de map van de geselecteerde snelkoppeling te openen.
        /// </summary>
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
