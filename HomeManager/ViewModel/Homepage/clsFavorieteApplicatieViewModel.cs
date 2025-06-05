using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace HomeManager.ViewModel.Homepage
{
    /// <summary>
    /// ViewModel voor het beheren van de favoriete applicaties van een gebruiker.
    /// </summary>
    public class clsFavorieteApplicatieViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFavorieteApplicatieDataService _dataService;

        /// <summary>
        /// Lijst met de favoriete applicaties die zichtbaar zijn in de UI.
        /// </summary>
        public ObservableCollection<clsFavorieteApplicatieModel> FavorieteApplicaties { get; set; }

        /// <summary>
        /// Commands voor de verschillende acties in het UI.
        /// </summary>
        public ICommand cmdSave { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

        /// <summary>
        /// Huidig geselecteerde applicatie (via UI-binding).
        /// </summary>
        private clsFavorieteApplicatieModel _selectedApplication;
        public clsFavorieteApplicatieModel SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor: initialisatie van commands en laden van applicaties.
        /// </summary>
        public clsFavorieteApplicatieViewModel()
        {
            _dataService = new clsFavorieteApplicatieDataService();
            FavorieteApplicaties = new ObservableCollection<clsFavorieteApplicatieModel>();

            // Initialiseer de commando's
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);

            // Laad bij opstarten de opgeslagen favoriete applicaties
            LoadFavorieteApplicaties();
        }

        /// <summary>
        /// Laadt de favoriete applicaties van de huidige gebruiker, en filtert op enkel nog aanwezige programma's.
        /// </summary>
        private void LoadFavorieteApplicaties()
        {
            FavorieteApplicaties.Clear();
            int accountId = clsLoginModel.Instance.AccountID;
            var applicaties = _dataService.GetByAccountId(accountId);

            foreach (var app in applicaties)
            {
                // Controleer of het pad nog bestaat op deze computer
                if (File.Exists(app.ApplicationPath))
                {
                    FavorieteApplicaties.Add(app);
                }
            }
        }

        /// <summary>
        /// Command voor het toevoegen van een nieuwe favoriete applicatie.
        /// Opent dialoog voor selectie, slaat icoon op en voegt toe aan de database + ObservableCollection.
        /// </summary>
        private void Execute_SaveCommand(object parameter)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Selecteer een applicatie",
                Filter = "Programma's (*.exe)|*.exe",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            };

            if (dialog.ShowDialog() == true)
            {
                string applicationPath = dialog.FileName;
                string applicationName = Path.GetFileNameWithoutExtension(applicationPath);
                string iconPath = SaveApplicationIcon(applicationPath, "C:\\Icons");

                var newApplication = new clsFavorieteApplicatieModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    ApplicationName = applicationName,
                    ApplicationPath = applicationPath,
                    IconPath = iconPath
                };

                if (_dataService.Insert(newApplication))
                {
                    FavorieteApplicaties.Add(newApplication);
                    clsMessenger.Default.Send("RefreshFavorieteApplicaties");
                }
                else
                {
                    System.Windows.MessageBox.Show("Fout bij het toevoegen van de applicatie.");
                }
            }
        }

        /// <summary>
        /// Haalt het icoon op van de .exe en slaat deze op als PNG in de opgegeven map.
        /// </summary>
        private string SaveApplicationIcon(string exePath, string saveDirectory)
        {
            try
            {
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                Icon icon = Icon.ExtractAssociatedIcon(exePath);
                if (icon != null)
                {
                    using (Bitmap bitmap = icon.ToBitmap())
                    {
                        string iconFileName = Path.Combine(saveDirectory, $"{Path.GetFileNameWithoutExtension(exePath)}.png");
                        bitmap.Save(iconFileName, System.Drawing.Imaging.ImageFormat.Png);
                        return iconFileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fout bij ophalen icoon: {ex.Message}");
            }

            return null;
        }

        private bool CanExecute_SaveCommand(object parameter) => true;

        /// <summary>
        /// Command voor het verwijderen van een geselecteerde applicatie.
        /// </summary>
        private void Execute_DeleteCommand(object parameter)
        {
            if (parameter is clsFavorieteApplicatieModel applicatie)
            {
                if (_dataService.Delete(applicatie))
                {
                    FavorieteApplicaties.Remove(applicatie);
                }
                else
                {
                    System.Windows.MessageBox.Show("Fout bij het verwijderen van de applicatie.");
                }
            }
        }

        private bool CanExecute_DeleteCommand(object parameter) => SelectedApplication != null;

        /// <summary>
        /// Command voor het openen van de geselecteerde applicatie.
        /// </summary>
        private void Execute_OpenCommand(object parameter)
        {
            if (parameter is clsFavorieteApplicatieModel applicatie && !string.IsNullOrEmpty(applicatie.ApplicationPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = applicatie.ApplicationPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Kan de applicatie niet openen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedApplication != null;
    }
}
