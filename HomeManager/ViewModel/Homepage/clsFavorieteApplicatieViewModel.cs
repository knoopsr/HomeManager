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
    public  class clsFavorieteApplicatieViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFavorieteApplicatieDataService _dataService;

        // Lijst met favoriete applicaties
        public ObservableCollection<clsFavorieteApplicatieModel> FavorieteApplicaties { get; set; }

        // Commands
        public ICommand cmdSave { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

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


        public clsFavorieteApplicatieViewModel()
        {
            _dataService = new clsFavorieteApplicatieDataService();
            FavorieteApplicaties = new ObservableCollection<clsFavorieteApplicatieModel>();


            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);

            LoadFavorieteApplicaties();
        }

        
        private void LoadFavorieteApplicaties()
        {
            FavorieteApplicaties.Clear();
            int accountId = clsLoginModel.Instance.AccountID;
            var applicaties = _dataService.GetByAccountId(accountId);

            foreach (var app in applicaties)
            {
                FavorieteApplicaties.Add(app);
            }
        }

        
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
                    FavorieteApplicaties.Add(newApplication); // 
                    clsMessenger.Default.Send("RefreshFavorieteApplicaties"); // 
                }
                else
                {
                    System.Windows.MessageBox.Show("Fout bij het toevoegen van de applicatie.");
                }
            }
        }


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
                    System.Windows. MessageBox.Show($"Kan de applicatie niet openen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedApplication != null;
    }
}
