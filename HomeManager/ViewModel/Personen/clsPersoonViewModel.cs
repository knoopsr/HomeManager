using HomeManager.Common;
using HomeManager.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using System.IO;
using System.Windows.Media.Imaging;
using HomeManager.Messages;

namespace HomeManager.ViewModel
{
    public class clsPersoonViewModel : clsCommonModelPropertiesBase
    {
        clsPersoonDataService MijnService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        public ICommand cmdUploadPicture { get; set; }

        private ObservableCollection<clsPersoonModel> _mijnCollectie;
        public ObservableCollection<clsPersoonModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsPersoonModel _mijnSelectedItem;
        public clsPersoonModel MijnSelectedItem
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

        public clsPersoonViewModel()
        {
            MijnService = new clsPersoonDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdUploadPicture = new clsCustomCommand(Execute_UploadPicture_Command, CanExecute_UploadPicture_Command);
            clsMessenger.Default.Register<clsNewPersoonMessage>(this, OnNewPersonenReceive);

            LoadData();

            MijnSelectedItem = MijnService.GetFirst();
        }

        private void OnNewPersonenReceive(clsNewPersoonMessage message)
        {
            CreateNewStatus();
            clsMessenger.Default.Unregister(this);
        }

        private void Execute_UploadPicture_Command(object? obj)
        {
            Microsoft.Win32.OpenFileDialog _OpenFileDialog = new Microsoft.Win32.OpenFileDialog();
            string myFileName = string.Empty;

            _OpenFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.JPEG)|*.jpg |GIF  Files (*.gif)|*.gif";

            Nullable<bool> result = _OpenFileDialog.ShowDialog();
            if (result == true)
            {
                myFileName = _OpenFileDialog.FileName;
            }

            if (File.Exists(_OpenFileDialog.FileName))
            {
                MijnSelectedItem.Foto = DocumentContent(_OpenFileDialog.FileName);
                MijnSelectedItem.IsDirty = true;
            }
        }

        //FROM PATH TO BYTE
        public byte[] DocumentContent(string FullPath)
        {
            if (!File.Exists(FullPath))
            {
                return null;
            }
            // HIER GA IK ALLE BYTES VAN HET BESTAND IN IN HET GEHEUGEN STEKEN
            byte[] FileContent = File.ReadAllBytes(FullPath);
            return FileContent;
        }

        //BYTES OMZETTEN NAAR EEN FIGUUR
        public BitmapImage ImageFromBuffer_GoodQality(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;

            image.EndInit();
            return image;
        }

        private bool CanExecute_UploadPicture_Command(object? obj)
        {
            return true;
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

        private void CreateNewStatus()
        {
            clsPersoonModel _itemToInsert = new clsPersoonModel()
            {
                PersoonID = 0,
                Naam = string.Empty,
                Voornaam = string.Empty,
                Foto = null,
                Geboortedatum = DateOnly.FromDateTime(DateTime.Now),
                IsApplicationUser = null

            };
            MijnSelectedItem = _itemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }
        private void Execute_New_Command(object? obj)
        {
            CreateNewStatus();
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

