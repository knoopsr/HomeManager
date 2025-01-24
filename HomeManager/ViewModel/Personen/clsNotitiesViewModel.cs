using HomeManager.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using HomeManager.Messages;
using System.Windows.Documents;
using HomeManager.Converter;
using System.Reflection.Metadata;
using System.Windows.Controls;
using RazorLight.Text;
using System.IO;
using static HomeManager.ViewModel.clsPersonenViewModel;

namespace HomeManager.ViewModel
{
    public class clsNotitiesViewModel : clsCommonModelPropertiesBase
    {
        clsNotitiesDataService MijnService;
        clsPersoonDataService MijnPersoonService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }



        public ICommand BoldCommand { get; }
        public ICommand ItalicCommand { get; }
        public ICommand BulletCommand { get; }
        public ICommand NumberedCommand { get; }

        private ObservableCollection<clsNotitiesModel> mijnCollectie;

        public ObservableCollection<clsNotitiesModel> MijnCollectie
        {
            get
            {
                return mijnCollectie;
            }
            set
            {
                mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsNotitiesModel mijnSelectedItem;
        public clsNotitiesModel MijnSelectedItem
        {
            get
            {
                return mijnSelectedItem;
            }
            set
            {
                if (value != null)
                {
                    if (mijnSelectedItem != null && mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + mijnSelectedItem + "Opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            mijnSelectedItem.IsDirty = false;
                            mijnSelectedItem.MijnSelectedIndex = 0;
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private clsPersoonModel _mijnSelectedPersoonItem;
        public clsPersoonModel MijnSelectedPersoonItem
        {
            get
            {
                return _mijnSelectedPersoonItem;
            }
            set
            {
                _mijnSelectedPersoonItem = value;
                OnPropertyChanged();
            }
        }

        private void OpslaanCommando()
        {
            //model.notitie converteren naar rtf
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


        public clsNotitiesViewModel()
        {
            MijnService = new clsNotitiesDataService();
            MijnPersoonService = new clsPersoonDataService();
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);

            //Test Notities
            BoldCommand = new RelayCommand(ApplyBold);
            ItalicCommand = new RelayCommand(ApplyItalic);
            BulletCommand = new RelayCommand(ApplyBullets);
            NumberedCommand = new RelayCommand(ApplyNumbering);



            clsMessenger.Default.Register<clsNotitiesModel>(this, OnNotitiesReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
            //MijnSelectedItem.MijnSelectedIndex = 0;
        }


        //Test Notities
        private void ApplyBold(object parameter)
        {
            if (parameter is RichTextBox rtb)
            {
                var selection = rtb.Selection;
                if (!selection.IsEmpty)
                {
                    var currentWeight = selection.GetPropertyValue(TextElement.FontWeightProperty);
                    var newWeight = currentWeight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold;
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, newWeight);
                }
            }
        }

        private void ApplyItalic(object parameter)
        {
            if (parameter is RichTextBox rtb)
            {
                var selection = rtb.Selection;
                if (!selection.IsEmpty)
                {
                    var currentStyle = selection.GetPropertyValue(TextElement.FontStyleProperty);
                    var newStyle = currentStyle.Equals(FontStyles.Italic) ? FontStyles.Normal : FontStyles.Italic;
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, newStyle);
                }
            }
        }

        private void ApplyBullets(object parameter)
        {
            if (parameter is RichTextBox rtb)
            {
                var selection = rtb.Selection;
                if (!selection.IsEmpty)
                {
                    var parent = selection.Start.Paragraph?.Parent as List;
                    if (parent == null)
                    {
                        var list = new List { MarkerStyle = TextMarkerStyle.Disc };
                        var paragraph = new Paragraph(new Run(selection.Text));
                        list.ListItems.Add(new ListItem(paragraph));
                        rtb.Document.Blocks.Add(list);
                        selection.Text = string.Empty;
                    }
                }
            }
        }

        private void ApplyNumbering(object parameter)
        {
            if (parameter is RichTextBox rtb)
            {
                var selection = rtb.Selection;
                if (!selection.IsEmpty)
                {
                    var parent = selection.Start.Paragraph?.Parent as List;
                    if (parent == null)
                    {
                        var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
                        var paragraph = new Paragraph(new Run(selection.Text));
                        list.ListItems.Add(new ListItem(paragraph));
                        rtb.Document.Blocks.Add(list);
                        selection.Text = string.Empty;
                    }
                }
            }
        }

        private void OnNotitiesReceived(clsNotitiesModel obj)
        {
            if (obj != null)
            {
                MijnSelectedItem = obj;
                MijnSelectedPersoonItem = MijnPersoonService.GetById(MijnSelectedItem.PersoonID);

                if (obj.NotitieID == 0)
                {
                    NewStatus = true;
                }
            }
        }

        private bool CanExecute_NewCommand(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_NewCommand(object? obj)
        {
            clsNotitiesModel ItemToInsert = new clsNotitiesModel()
            {
                NotitieID = 0,
                PersoonID = 0,
                Onderwerp = string.Empty,
                Notitie = string.Empty,
                //Datum = DateTime.Now
            };
            MijnSelectedItem = ItemToInsert;
            //MijnSelectedItem = ItemToInsert;

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
            if (MessageBox.Show("Wil je " + MijnSelectedItem + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes) if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
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

            clsMessenger.Default.Send<clsUpdateListMessages>(new clsUpdateListMessages());
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

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
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


