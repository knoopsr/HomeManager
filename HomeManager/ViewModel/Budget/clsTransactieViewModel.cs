﻿using HomeManager.Common;
using HomeManager.DataService.Budget;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Budget;
using HomeManager.Services;
using HomeManager.View;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;




namespace HomeManager.ViewModel
{
    public class clsTransactieViewModel : clsCommonModelPropertiesBase
    {
        clsTransactieDataService MijnService;
        clsBijlageDataService BijlageService;
        private clsPermissionChecker _permissionChecker = new();
        private clsDialogService _DialogService;



        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdEditBegunstigden { get; set; }
        public ICommand cmdEditCategorie { get; set; }
        public ICommand cmdUploadBijlage { get; set; }
        public ICommand cmdShowBijlage { get; set; }
        public ICommand cmdDeleteBijlage { get; set; }
        public ICommand cmdDropBijlage { get; set; }
        public ICommand cmdFilter { get; set; }



        public int IsUitgaven { get; set; }



        private ObservableCollection<clsTransactieModel> _MijnCollectie;
        public ObservableCollection<clsTransactieModel> MijnCollectie
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

        

        private ObservableCollection<clsBijlageModel> _mijnCollectieBijlage;

        public ObservableCollection<clsBijlageModel> MijnCollectieBijlage
        {
            get
            {
                return _mijnCollectieBijlage;
            }
            set
            {
                _mijnCollectieBijlage = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<clsBijlageModel> _mijnTijdelijkeBijlage;
        public ObservableCollection<clsBijlageModel> MijnTijdelijkeBijlage
        {
            get
            {
                return _mijnTijdelijkeBijlage;
            }
            set
            {
                _mijnTijdelijkeBijlage = value;
                OnPropertyChanged();
            }
        }




        private clsBijlageModel _MijnSelectedBijlage;
        public clsBijlageModel MijnSelectedBijlage
        {
            get
            {
                return _MijnSelectedBijlage;
            }
            set
            {
                _MijnSelectedBijlage = value;
                OnPropertyChanged();
            }
        }


        private clsTransactieModel _MijnSelectedItem;
        public clsTransactieModel MijnSelectedItem
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
                        if (MessageBox.Show("wil je de geselecteerde transactie opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }

                    }
                    MijnTijdelijkeBijlage = BijlageService.GetAll(value.BudgetTransactionID);
                    MijnCollectieBijlage = BijlageService.GetAll(value.BudgetTransactionID);
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

                    clsTransactieModel _newTransaction = MijnService.Insert2(MijnSelectedItem);


                    if (_newTransaction != null)
                    {
                        foreach (clsBijlageModel _bijlage in MijnTijdelijkeBijlage)
                        {       
                            if (_bijlage.IsNew)
                            {
                                _bijlage.BudgetTransactionID = _newTransaction.BudgetTransactionID;
                                BijlageService.Insert(_bijlage);
                            }

                        }

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
                        foreach (clsBijlageModel _bijlage in MijnTijdelijkeBijlage)
                        {
                            if (_bijlage.IsNew)
                            {
                                _bijlage.BudgetTransactionID = MijnSelectedItem.BudgetTransactionID;
                                BijlageService.Insert(_bijlage);
                            }
              
                        }
                        foreach (clsBijlageModel _bijlage in MijnCollectieBijlage)
                        {
                            if (_bijlage.IsDeleted)
                            {
                                BijlageService.Delete(_bijlage);
                            }
                        }

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


        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            MijnCollectieBijlage = new ObservableCollection<clsBijlageModel>();
            MijnTijdelijkeBijlage = new ObservableCollection<clsBijlageModel>();
            GefilterdeCollectie = new ObservableCollection<clsTransactieModel>(MijnCollectie);


        }



        public clsTransactieViewModel()
        {
            MijnService = new clsTransactieDataService();
            BijlageService = new clsBijlageDataService();


            _DialogService = new clsDialogService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdEditBegunstigden = new clsCustomCommand(EditBegunstigde, CanExecute_EditBegunstigde);
            cmdEditCategorie = new clsCustomCommand(EditCategorie, CanExecute_EditCategorie);
            cmdUploadBijlage = new clsCustomCommand(Execute_UploadBijlage, CanExecute_UploadBijlage);
            cmdShowBijlage = new clsCustomCommand(Execute_ShowBijlage, CanExecute_ShowBijlage);
            cmdDeleteBijlage = new clsCustomCommand(Execute_DeleteBijlage, CanExecute_DeleteBijlage);
            cmdDropBijlage = new clsRelayCommand<object>(Execute_Drop);

            //de searchcommand
            SearchCommand = new RelayCommand(FilterTransactie);
            //de clear SearchCommand
            ClearSearchCommand = new RelayCommand(ClearSearch);


            clsMessenger.Default.Register<clsUpdateListMessages>(this, OnUpdateListMessageReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        #region Save - Delete - Cancel - Close

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
                    if (MessageBox.Show("Deze transactie is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?",
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
            if (_permissionChecker.HasPermission("411"))
            {
                return !NewStatus;
            }
            return false;
        }

        private void Execute_NewCommand(object obj)
        {
            clsTransactieModel ItemToInsert = new clsTransactieModel()
            {
                IsUitgaven = false,
                BudgetTransactionID = 0,
                Bedrag = null,
                Datum = DateOnly.FromDateTime(DateTime.Now),
                Onderwerp = String.Empty,
                BegunstigdeID = 0,
                BudgetCategorieID = 0      
            };

            MijnCollectieBijlage.Clear();

            MijnSelectedItem = ItemToInsert;


            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }
        private bool CanExecute_DeleteCommand(object obj)
        {
            if (_permissionChecker.HasPermission("413"))
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
            return false;
        }

        private void Execute_DeleteCommand(object obj)
        {
            if (MessageBox.Show("wil je deze transactie verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
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

        private bool CanExecute_SaveCommand(object obj)
        {
            if (_permissionChecker.HasPermission("412"))
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
            return false;
        }

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();

        }

        #endregion

        #region Popup Windows

        private void OnUpdateListMessageReceived(clsUpdateListMessages obj)
        {
            //refresh
            LoadData();
            _DialogService.CloseDialog();
        }



        private bool CanExecute_EditBegunstigde(object obj)
        {
            return _permissionChecker.HasPermission("414");
        }

        private void EditBegunstigde(object obj)
        {
            _DialogService.ShowDialog(new ucBegunstigden(), "Begunstigde");
        }

        private bool CanExecute_EditCategorie(object obj)
        {
            return _permissionChecker.HasPermission("415");
        }

        private void EditCategorie(object obj)
        {
            _DialogService.ShowDialog(new ucCategorie(), "Categorie");
        }

        #endregion

        #region Bijlage 

        //private object bijlage;


        private bool CanExecute_UploadBijlage(object obj)
        {
            return true;
        }
        private void Execute_UploadBijlage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Alle bestanden (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {

                    string fileName = Path.GetFileName(filePath);

                    // Controleer of er al een bijlage met dezelfde naam bestaat
                    if (MijnTijdelijkeBijlage.Any(b => b.BijlageNaam.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Toon een waarschuwing aan de gebruiker
                        MessageBox.Show($"Er bestaat al een bijlage met de naam '{fileName}'.", "Duplicaat bijlage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MijnTijdelijkeBijlage.Add(new clsBijlageModel
                        {
                            BijlageNaam = Path.GetFileName(filePath),
                            IsNew = true,
                            Bijlage = File.ReadAllBytes(filePath)
                        });
                        MijnSelectedItem.IsDirty = true;
                        // Sla de bijlage tijdelijk op de schijf op
                        string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                        File.WriteAllBytes(tempFilePath, File.ReadAllBytes(filePath));
                    }
                }
            }
        }

        private bool CanExecute_ShowBijlage(object obj)
        {
            if (MijnSelectedBijlage == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Execute_ShowBijlage(object obj)
        {
            if (MijnSelectedBijlage is clsBijlageModel bijlage)
            {
                try
                {
                    // Zoek het tijdelijke bestandspad
                    string tempFilePath = Path.Combine(Path.GetTempPath(), bijlage.BijlageNaam);

                    // Controleer of het bestand bestaat
                    if (!File.Exists(tempFilePath))
                    {
                        // Als het bestand niet bestaat, maak het dan opnieuw aan
                        File.WriteAllBytes(tempFilePath, bijlage.Bijlage);
                    }

                    // Open het bestand met de standaardtoepassing
                    Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden bij het openen van de bijlage: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Geen geldige bijlage geselecteerd.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private bool CanExecute_DeleteBijlage(object obj)
        {

            if (MijnSelectedBijlage == null)
            {
                return false;
            }
            else
            {
                return true;
            }    
        }

        private void Execute_DeleteBijlage(object obj)
        {
            if (MijnSelectedBijlage is clsBijlageModel bijlage)
            {
                // Check of de bijlage in de tijdelijke collectie zit
                if (MijnTijdelijkeBijlage.Contains(bijlage))
                {
                    // Zoek dezelfde bijlage in MijnCollectieBijlage en markeer als verwijderd
                    var bestaandeBijlage = MijnCollectieBijlage
                        .FirstOrDefault(b => b.BudgetBijlageID == bijlage.BudgetBijlageID);

                    if (bestaandeBijlage != null)
                    {
                        bestaandeBijlage.IsDeleted = true;
                    }

                    // Verwijder bijlage uit tijdelijke collectie
                    MijnTijdelijkeBijlage.Remove(bijlage);
                    MijnSelectedItem.IsDirty = true;

                    // Verwijder tijdelijk bestand indien aanwezig
                    string tempFilePath = Path.Combine(Path.GetTempPath(), bijlage.BijlageNaam);
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
            }
            else
            {
                MessageBox.Show("Het geselecteerde item is geen geldige bijlage.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        #endregion

        #region Drag and Drop

        private void Execute_Drop(object obj)
        {
            //if (obj is DataObject dataObject && dataObject.GetDataPresent(DataFormats.FileDrop))
            //{
            //    var files = (string[])dataObject.GetData(DataFormats.FileDrop);
            //    if (files.Length > 0)
            //    {
            //        foreach (var file in files)
            //        {
            //            string bijlageNaam = Path.GetFileName(file);
            //            MijnCollectieBijlage.Add(new clsBijlageModel
            //            {
            //                IsNew = true,
            //                BijlageNaam = bijlageNaam,
            //                Bijlage = File.ReadAllBytes(file),
            //                BudgetBijlageID = 0,
            //                BudgetTransactionID = 0
            //            });
            //        }

            //    }
            //}

            if (obj is DataObject dataObject && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                string[] bestanden = (string[])dataObject.GetData(DataFormats.FileDrop);
                foreach (var bestand in bestanden)
                {
                    string bestandsnaam = Path.GetFileName(bestand);
                    
                    // Controleer of er al een bijlage met dezelfde naam bestaat
                    if (MijnTijdelijkeBijlage.Any(b => b.BijlageNaam.Equals(bestandsnaam, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Toon een waarschuwing aan de gebruiker
                        MessageBox.Show($"Er bestaat al een bijlage met de naam '{bestandsnaam}'.", "Duplicaat bijlage", MessageBoxButton.OK, MessageBoxImage.Warning);
                        continue;
                    }
                    
                    // Lees Bytes in en voeg toe aan de lijst
                    byte[] bestandBytes = File.ReadAllBytes(bestand);

                    MijnTijdelijkeBijlage.Add(new clsBijlageModel
                    {
                         IsNew = true,
                         BijlageNaam = bestandsnaam,
                         Bijlage = File.ReadAllBytes(bestand),
                         BudgetBijlageID = 0,
                         BudgetTransactionID = 0
                    });

                    // Bestand tijdelijk opslaan
                    string tempPad = Path.Combine(Path.GetTempPath(), bestandsnaam);
                    File.WriteAllBytes(tempPad, bestandBytes);

                    // Markeer het item als gewijzigd
                    MijnSelectedItem.IsDirty = true;
                }
            }
            
        }


        #endregion


        #region Filter_Transactie

        private string _filterText;

        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                OnPropertyChanged();
                FilterTransactie();
            }
        }

        //RelayCommand toevoegen voor de RelayCommand filters
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;
            private Action<object> executeBold;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public RelayCommand(Action<object> executeBold)
            {
                this.executeBold = executeBold;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

            public void Execute(object parameter) => _execute();

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }


        private ObservableCollection<clsTransactieModel> _gefilterdeCollectie;

        public ObservableCollection<clsTransactieModel> GefilterdeCollectie
        {
            get
            {
                return _gefilterdeCollectie;
            }
            set
            {
                _gefilterdeCollectie = value;
                OnPropertyChanged(nameof(GefilterdeCollectie));
            }
        }

        //ICommand voor mijn filters
        public ICommand SearchCommand { get; private set; }
        public ICommand ClearSearchCommand { get; private set; }

        // Methode voor filter uit te voeren
        private void FilterTransactie()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                //niet in de zoekbalk
                GefilterdeCollectie = new ObservableCollection<clsTransactieModel>(MijnCollectie);
            }
            else
            {
                var GefilterdeItems = MijnCollectie
                    .Where(item =>

                       (item.Begunstigde.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (item.BudgetCategorie.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (item.Onderwerp.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                       )
                      .ToList();

                GefilterdeCollectie = new ObservableCollection<clsTransactieModel>(GefilterdeItems);
            }
        }



        //Methode voor de zoekbalk te clearen
        private void ClearSearch()
        {
            FilterText = string.Empty;
            FilterTransactie();

        }

        

        #endregion
    }
}

