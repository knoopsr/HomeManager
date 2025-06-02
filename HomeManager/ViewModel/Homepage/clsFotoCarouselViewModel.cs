using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace HomeManager.ViewModel.Homepage
{
    /// <summary>
    /// ViewModel voor de foto carrousel: toont foto's uit een geselecteerde map
    /// en wisselt automatisch om de 5 seconden naar de volgende foto.
    /// </summary>
    public class clsFotoCarouselViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFotoCarouselDataService _dataService;
        private readonly DispatcherTimer _photoTimer;
        private int _currentIndex = 0;

        /// <summary>
        /// Collectie van foto's die in de carrousel getoond worden.
        /// </summary>
        public ObservableCollection<clsFotoCarouselModel> FotoCollectie { get; set; } = new ObservableCollection<clsFotoCarouselModel>();

        /// <summary>
        /// Command om een map te selecteren en foto's in te laden.
        /// </summary>
        public ICommand cmdSave { get; }

        /// <summary>
        /// Command om de huidige foto full-screen weer te geven.
        /// </summary>
        public ICommand ToonFotoFullScreen { get; set; }

        /// <summary>
        /// Constructor: initialiseert de dataservice, commando's en timer.
        /// </summary>
        public clsFotoCarouselViewModel()
        {
            _dataService = new clsFotoCarouselDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            ToonFotoFullScreen = new clsCustomCommand(Execute_ToonFotoFullScreen, o => CurrentPhoto != null);

            // Timer die elke 5 seconden de foto wisselt
            _photoTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _photoTimer.Tick += OnTimerTick;

            // Foto's meteen inladen bij starten
            LoadPhotos();

            // Voorkom dat de designer deze code uitvoert (anders errors in Visual Studio)
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
        }

        /// <summary>
        /// Pad van de geselecteerde map (bindable property).
        /// </summary>
        private string _selectedFolder;
        public string SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// De huidige foto die getoond wordt.
        /// </summary>
        private clsFotoCarouselModel _currentPhoto;
        public clsFotoCarouselModel CurrentPhoto
        {
            get => _currentPhoto;
            set
            {
                _currentPhoto = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command voor het full-screen tonen van de huidige foto.
        /// Maakt een nieuw window aan dat gemaximaliseerd wordt weergegeven.
        /// </summary>
        private void Execute_ToonFotoFullScreen(object parameter)
        {
            if (CurrentPhoto == null || string.IsNullOrWhiteSpace(CurrentPhoto.FolderPath))
                return;

            var fullscreenWindow = new System.Windows.Window
            {
                WindowStyle = System.Windows.WindowStyle.None,
                WindowState = System.Windows.WindowState.Maximized,
                ResizeMode = System.Windows.ResizeMode.NoResize,
                Background = System.Windows.Media.Brushes.Black,
                Topmost = true,
                Content = new System.Windows.Controls.Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(CurrentPhoto.FolderPath)),
                    Stretch = System.Windows.Media.Stretch.Uniform,
                    Cursor = System.Windows.Input.Cursors.Hand
                }
            };

            // Sluit full-screen bij klikken
            fullscreenWindow.MouseDown += (s, e) => fullscreenWindow.Close();
            fullscreenWindow.ShowDialog();
        }

        /// <summary>
        /// Command om een map te selecteren waarin de foto's zitten.
        /// </summary>
        private void Execute_SaveCommand(object parameter)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Selecteer een map",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedFolder = dialog.FileName;

                var nieuwModel = new clsFotoCarouselModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    FolderPath = SelectedFolder
                };

                if (_dataService.Insert(nieuwModel))
                {
                    LoadPhotos();
                }
            }
        }

        /// <summary>
        /// Laadt de foto's uit de geselecteerde map.
        /// Filtert enkel op afbeeldingen en start de carrousel.
        /// </summary>
        private void LoadPhotos()
        {
            FotoCollectie.Clear();
            var result = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);

            if (result != null && result.Any() && Directory.Exists(result.First().FolderPath))
            {
                SelectedFolder = result.First().FolderPath;

                var bestanden = Directory.GetFiles(SelectedFolder)
                    .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                f.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

                foreach (var pad in bestanden)
                {
                    FotoCollectie.Add(new clsFotoCarouselModel
                    {
                        FolderPath = pad
                    });
                }

                if (FotoCollectie.Any())
                {
                    _currentIndex = 0;
                    CurrentPhoto = FotoCollectie[_currentIndex];
                    _photoTimer.Start();
                }
            }
        }

        /// <summary>
        /// Timer-event: toont de volgende foto in de collectie.
        /// </summary>
        
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (FotoCollectie.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % FotoCollectie.Count;
            CurrentPhoto = FotoCollectie[_currentIndex];
        }
        private bool CanExecute_SaveCommand(object parameter) => true;
    }
}
