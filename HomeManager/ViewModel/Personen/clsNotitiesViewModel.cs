﻿using HomeManager.Helpers;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using HomeManager.Messages;
using System.Windows.Documents;
using System.Windows.Controls;
using System.IO;
using HomeManager.Behaviors;

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
        public ICommand UnderlineCommand { get; }




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
                        clsMessenger.Default.Send(new clsUpdateListMessages()); // Bericht verzenden
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
                        Console.WriteLine("Update gelukt!");
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                        clsMessenger.Default.Send(new clsUpdateListMessages()); // Bericht verzenden
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

            //LAYOUT
            BoldCommand = new RelayCommando<RichTextBox>(ToggleBold);
            ItalicCommand = new RelayCommando<RichTextBox>(ToggleItalic);
            BulletCommand = new RelayCommando<RichTextBox>(AddBullets);
            NumberedCommand = new RelayCommando<RichTextBox>(AddNumbering);
            UnderlineCommand = new RelayCommando<RichTextBox>(ApplyUnderline);

            clsMessenger.Default.Register<clsNotitiesModel>(this, OnNotitiesReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }



        //Layout Notities
        private void ToggleBold(RichTextBox richTextBox)
        {
            if (richTextBox != null && !richTextBox.Selection.IsEmpty)
            {
                var selection = richTextBox.Selection;
                var weight = selection.GetPropertyValue(TextElement.FontWeightProperty);

                // Wissel tussen vet en normaal
                selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                    weight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold);

                // Zorg ervoor dat de wijzigingen worden opgeslagen
                var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                using (var stream = new MemoryStream())
                {
                    textRange.Save(stream, DataFormats.Rtf);
                    clsRichTextBoxHelper.SetRtfText(richTextBox, Encoding.Default.GetString(stream.ToArray())); // Zorg ervoor dat je de helper aanroept
                }
            }
        }

        private void ToggleItalic(RichTextBox richTextBox)
        {
            if (richTextBox != null && !richTextBox.Selection.IsEmpty)
            {
                var selection = richTextBox.Selection;
                var style = selection.GetPropertyValue(TextElement.FontStyleProperty);
                selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                    style.Equals(FontStyles.Italic) ? FontStyles.Normal : FontStyles.Italic);
            }
        }

        private void AddBullets(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                var selection = richTextBox.Selection;
                var paragraph = selection.Start.Paragraph;
                if (paragraph != null)
                {
                    var list = new List { MarkerStyle = TextMarkerStyle.Disc };
                    list.ListItems.Add(new ListItem(paragraph));
                    richTextBox.Document.Blocks.Add(list);
                }
            }
        }

        private void AddNumbering(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                var selection = richTextBox.Selection;
                var paragraph = selection.Start.Paragraph;
                if (paragraph != null)
                {
                    var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
                    list.ListItems.Add(new ListItem(paragraph));
                    richTextBox.Document.Blocks.Add(list);
                }
            }
        }
        private void ApplyUnderline(RichTextBox richTextBox)
        {
            if (richTextBox == null) return;

            TextRange selection = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
            if (!selection.IsEmpty)
            {
                var currentDecorations = selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;

                // Toggle underline
                if (currentDecorations == null || currentDecorations.Count == 0)
                {
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
                else
                {
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
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
                Notitie = string.Empty
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


