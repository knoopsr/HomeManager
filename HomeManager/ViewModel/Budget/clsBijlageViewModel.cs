using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Budget;
using HomeManager.DataService.Budget;
using System.Data.SqlTypes;
using HomeManager.Messages;
using HomeManager.Services;
using HomeManager.View;
using HomeManager.Model.Security;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Transactions;
using System.Windows.Media;




namespace HomeManager.ViewModel
{
    public class clsBijlageViewModel : clsCommonModelPropertiesBase
    {
        
        clsBijlageDataService BijlageService;
        
        
        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdUploadBijlage { get; set; }
        public ICommand cmdShowBijlage { get; set; }
        public ICommand cmdDeleteBijlage { get; set; }
        public ICommand cmdDragEnter { get; set; }
        public ICommand cmdDragOver { get; set; }
        public ICommand cmdDrop { get; set; }


        private ObservableCollection<clsBijlageModel> _BijlageCollectie;

        public ObservableCollection<clsBijlageModel> BijlageCollectie
        {
            get
            {
                return _BijlageCollectie;
            }

            set
            {
                _BijlageCollectie = value;
                OnPropertyChanged(nameof(BijlageCollectie));
            }
        }

        private ObservableCollection<string> _DroppedItems;

        public ObservableCollection<string> DroppedItems
        {
            get
            {
                return _DroppedItems;
            }
            set
            {
                _DroppedItems = value;
                OnPropertyChanged(nameof(DroppedItems));
            }
        }

        private object bijlage;


        private clsBijlageModel _MijnSelectedItem;
        public clsBijlageModel MijnSelectedItem
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
                        if (MessageBox.Show("wil je de geselecteerde bijlage opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                    if (BijlageService.Insert(MijnSelectedItem))
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
                    if (BijlageService.Update(MijnSelectedItem))
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


        private void LoadData()
        {
            
            BijlageCollectie = BijlageService.GetAll();
            
        }





        public clsBijlageViewModel()
        {
            
            BijlageService = new clsBijlageDataService();

            DroppedItems = new ObservableCollection<string>();


            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdUploadBijlage = new clsCustomCommand(Execute_UploadBijlage, CanExecute_UploadBijlage);
            cmdShowBijlage = new clsCustomCommand(Execute_ShowBijlage, CanExecute_ShowBijlage);
            cmdDeleteBijlage = new clsCustomCommand(Execute_DeleteBijlage, CanExecute_DeleteBijlage);
            //cmdDragEnter = new clsCustomCommand(OnDragEnter, null);
            //cmdDragOver = new clsCustomCommand(OnDragOver, null);
            //cmdDrop = new clsCustomCommand(OnDrop, null);




            LoadData();
            MijnSelectedItem = BijlageService.GetFirst();
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
                    if (MessageBox.Show("Deze Bijlage is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?",
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
            MijnSelectedItem = BijlageService.GetFirst();
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
            clsBijlageModel ItemToInsert = new clsBijlageModel()
            {
                
                BudgetTransactionID = 0,
                BijlageNaam = null,
                Bijlage = null,
                

            };

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
            if (MessageBox.Show("wil je deze transactie verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
            if (MijnSelectedItem != null)
            {
                if (BijlageService.Delete(MijnSelectedItem))
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

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();

        }

        #endregion

        

        #region Bijlage 
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
                    if (BijlageCollectie.Any(b => b.BijlageNaam.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Toon een waarschuwing aan de gebruiker
                        MessageBox.Show($"Er bestaat al een bijlage met de naam '{fileName}'.", "Duplicaat bijlage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        BijlageCollectie.Add(new clsBijlageModel
                        {
                            BijlageNaam = Path.GetFileName(filePath),
                            Bijlage = File.ReadAllBytes(filePath)
                        });

                        // Sla de bijlage tijdelijk op de schijf op
                        string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                        File.WriteAllBytes(tempFilePath, File.ReadAllBytes(filePath));
                    }
                }
            }
        }
        private bool CanExecute_ShowBijlage(object obj)
        {
            return true;
        }

        private void Execute_ShowBijlage(object obj)
        {
            if (obj is clsBijlageModel bijlage)
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
            return obj is clsBijlageModel bijlage && bijlage != null;
        }

        private void Execute_DeleteBijlage(object obj)
        {
            // Probeer het object te casten naar clsBijlageModel
            if (obj is clsBijlageModel bijlage)
            {
                // Controleer of de bijlage in de collectie voorkomt
                if (BijlageCollectie.Contains(bijlage))
                {
                    // Verwijder de bijlage uit de collectie
                    BijlageCollectie.Remove(bijlage);

                    // Optioneel: Verwijder het tijdelijke bestand van de schijf
                    string tempFilePath = Path.Combine(Path.GetTempPath(), bijlage.BijlageNaam);
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
            }
            else
            {
                // Toon een foutmelding als het object niet van het verwachte type is
                MessageBox.Show("Het geselecteerde item is geen geldige bijlage.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Drag and Drop

        //private void OnDragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effects = DragDropEffects.Copy;
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void OnDragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effects = DragDropEffects.Copy;
        //        DropPlace.Fill = Brushes.LightGoldenrodYellow;
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void OnDrop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        // Haal de bestandsnamen op
        //        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);


        //        // Voeg de bestandsnamen toe aan de ListBox
        //        foreach (string file in files)
        //        {
        //            if (!lstFiles.Items.Contains(file))
        //            {
        //                lstFiles.Items.Add(file);
        //            }
        //        }
        //        DropPlace.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#eeeeeeee");
        //    }
        //}


        #endregion
    }
}

