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

namespace HomeManager.ViewModel
{
    public class clsPersoonVM : clsCommonModelPropertiesBase
    {
        clsPersoonDataService MijnService;
        
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdUploadPicture { get; set; }


        private ObservableCollection<clsPersoonM> _MijnCollectie;
        public ObservableCollection<clsPersoonM> MijnCollectie
        {
            get
            {
                return _MijnCollectie;
            }
            set
            {
                _MijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private clsPersoonM _MijnSelectedItem;
        public clsPersoonM MijnSelectedItem
        {
            get
            {
                return _MijnSelectedItem;
            }
            set
            {
                if (value != null)
                {
                    if (_MijnSelectedItem != null && _MijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + _MijnSelectedItem + "Opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                _MijnSelectedItem = value;
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
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        public clsPersoonVM()
        {
            MijnService = new clsPersoonDataService();
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdUploadPicture = new clsCustomCommand(Execute_UploadPictureCommand, CanExecute_UploadPictureCommand);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        private bool CanExecute_UploadPictureCommand(object? obj)
        {
            return true;
        }

        private void Execute_UploadPictureCommand(object? obj)
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


        private bool CanExecute_NewCommand(object obj)
        {
            return !NewStatus;
        }

        private void Execute_NewCommand(object obj)
        {
            clsPersoonM ItemToInsert = new clsPersoonM()
            {
                PersoonID = 0,
                Naam = string.Empty,
                Voornaam = string.Empty,
                Foto = null,
                Geboortedatum = DateOnly.FromDateTime(DateTime.Now),
                IsApplicationUser = false
            };
            MijnSelectedItem = ItemToInsert;
            MijnSelectedItem = ItemToInsert;

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
            if (MessageBox.Show("wil je " + MijnSelectedItem + "verwijderen?", "Vewijderen?", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
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

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedItem != null
                && MijnSelectedItem.Error == null
                && MijnSelectedItem.IsDirty == true)
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
    }
}

