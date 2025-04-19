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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HomeManager.ViewModel.Homepage
{
    public class clsFotoCarouselViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFotoCarouselDataService _dataService;
        private readonly DispatcherTimer _photoTimer;
        private int _currentIndex = 0;

        public ObservableCollection<clsFotoCarouselModel> FotoCollectie { get; set; } = new ObservableCollection<clsFotoCarouselModel>();

        public ICommand cmdSave { get; }

        public clsFotoCarouselViewModel()
        {
            _dataService = new clsFotoCarouselDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);

            _photoTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _photoTimer.Tick += OnTimerTick;

            LoadPhotos(); 
        }

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

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (FotoCollectie.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % FotoCollectie.Count;
            CurrentPhoto = FotoCollectie[_currentIndex];
        }




        private bool CanExecute_SaveCommand(object parameter) => true;
    }
}
