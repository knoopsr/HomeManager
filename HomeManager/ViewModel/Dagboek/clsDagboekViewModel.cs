using HomeManager.Common;
using HomeManager.DataService.Dagboek;
using HomeManager.Helpers;
using HomeManager.Model.Dagboek;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace HomeManager.ViewModel
{
    public class clsDagboekViewModel : clsCommonModelPropertiesBase
    {
        private int PersoonID
        {
            get { return clsLoginModel.Instance.AccountID; }
        }

        public clsRTBLayout MyRTBLayout { get; set; }
            
        private bool isNew = false;
        private bool isEmptyCollection = false;

        
        private ObservableCollection<clsDagboekModel> _mijnCollectie;

        public ObservableCollection<clsDagboekModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private clsDagboekModel _mySelectedItem;

        public clsDagboekModel MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
                if (_mySelectedItem != value)
                {
                    if (_mySelectedItem != null)
                    {
                        IsDirty = true;
                    }
                }
                _mySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public clsDagboekDataService MyService { get; set; }

        private void GenerateCollection()
        {
            MijnCollectie = new ObservableCollection<clsDagboekModel>();
            MijnCollectie = MyService.GetAllByPersoonID(PersoonID);
            if (MijnCollectie.Count == 0)
            {
                isEmptyCollection = true;
                //MessageBox.Show("collection is empty = " + isEmptyCollection.ToString());
            }


        }

        public clsDagboekViewModel()
        {
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdTest = new clsCustomCommand(Execute_Test, CanExecute_Test);
            UpdateRichTextBoxCommand = new clsCustomCommand(UpdateRichTextBox, CanExecute_UpdateRTB);
            cmdDecreaseTextSize = new clsCustomCommand(DecreaseTextSize, CanDecreaseTextSize);
            cmdIncreaseTextSize = new clsCustomCommand(IncreaseTextSize, CanIncreaseTextSize);

            //relaycommands for the layout
            cmdSetFontWeight = new clsRelayCommand(SetFontWeight);
            cmdSetUnderline = new clsRelayCommand(SetUnderline);
            cmdToggleItalic = new clsRelayCommand(ToggleItalic);
            cmdToggleStrikeTrough = new clsRelayCommand(ToggleStrikeTrough);
            cmdSetForegroundToText = new clsRelayCommand(SetForegroundToText);
            cmdSetBackgroundToText = new clsRelayCommand(SetBackgroundToText);
            cmdSetFondSize = new clsRelayCommand(SetFondSize);
            cmdSetFondFamily = new clsRelayCommand(SetFondFamily);
            cmdSetSubScript = new clsRelayCommand(SetSubScript);
            cmdSetSuperScript = new clsRelayCommand(SetSuperScript);
            cmdSetTextAlignmentLeft = new clsRelayCommand(SetTextAlignmentLeft);
            cmdSetTextAlignmentRight = new clsRelayCommand(SetTextAlignmentRight);
            cmdSetTextAlignmentCenter = new clsRelayCommand(SetTextAlignmentCenter);
            cmdSetTextAlignmentJustify = new clsRelayCommand(SetTextAlignmentJustify);
            cmdCreateBullets = new clsRelayCommand(CreateBullets);
            cmdCreateNumbering = new clsRelayCommand(CreateNumbering);
            cmdIncreaseTextIndent = new clsRelayCommand(IncreaseTextIndent);
            cmdDecreaseTextIndent = new clsRelayCommand(DecreaseTextIndent);

            //AplicationCommands
            cmdCut = ApplicationCommands.Cut;
            cmdCopy = ApplicationCommands.Copy;
            CmdPaste = ApplicationCommands.Paste;
            cmdUndo = ApplicationCommands.Undo;
            cmdRedo = ApplicationCommands.Redo;
            

            MyRTBLayout = new clsRTBLayout();
            MyService = new clsDagboekDataService();
            GenerateCollection();

            //validatie
            if (isEmptyCollection)
            {
                //dit maakt een item aan om geen lege collecties te hebben
                var _obj = new clsDagboekModel()
                {
                    PersoonID = this.PersoonID,
                    DateCreated = DateTime.Now,
                    DagboekContentString = "Er zijn nog geen files opgeslagen"
                };
                MijnCollectie.Add(_obj);
                MySelectedItem = _obj;

                isNew = true;
                isEmptyCollection = false;
            }
            else
            {
                MySelectedItem = MijnCollectie.FirstOrDefault();
               
                
            }

        }

        private void DecreaseTextIndent(object? obj)
        {
            if (obj is RichTextBox rtb)
            {
                var paragraphs = GetSelectedParagraphs(rtb.Selection);
                MyRTBLayout.DecreaseTextIndentation(paragraphs);
            }
        }

        private void IncreaseTextIndent(object? obj)
        {
            if (obj is RichTextBox rtb)
            {
                var paragraphs = GetSelectedParagraphs(rtb.Selection);
                MyRTBLayout.IncreaseTextIndentation(paragraphs);
            }
        }

        private void CreateNumbering(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                var selection = rtb.Selection;
                var paragraph = selection.Start.Paragraph;
                if (paragraph != null)
                {
                    var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
                    list.ListItems.Add(new ListItem(paragraph));
                    rtb.Document.Blocks.Add(list);
                }
            }
        }

        private void CreateBullets(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                var selection = rtb.Selection;
                var paragraph = selection.Start.Paragraph;
                if (paragraph != null)
                {
                    var list = new List { MarkerStyle = TextMarkerStyle.Box };
                    list.ListItems.Add(new ListItem(paragraph));
                    rtb.Document.Blocks.Add(list);
                }
            }
        }

        private void SetTextAlignmentJustify(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetTextAlignmentToSelection(range, TextAlignment.Justify);
            }
        }

        private void SetTextAlignmentCenter(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetTextAlignmentToSelection(range, TextAlignment.Center);
            }
        }

        private void SetTextAlignmentRight(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetTextAlignmentToSelection(range, TextAlignment.Right);
            }
        }

        private void SetTextAlignmentLeft(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetTextAlignmentToSelection(range, TextAlignment.Left);
            }
        }

        private void SetSuperScript(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetSelectionToSuperscript(range);
            }
        }

        private void SetSubScript(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetSelectionToSubscript(range);
            }
        }

        private bool CanIncreaseTextSize(object? obj)
        {
            if (MyRTBLayout.IsBiggestFondSize)
            {
                return false;
            }
            return true;
        }

        private bool CanDecreaseTextSize(object? obj)
        {
            if (MyRTBLayout.IsSmallestFondSize)
            {
                return false;
            }
            return true;
        }

        private void IncreaseTextSize(object? obj)
        {
            MyRTBLayout.MyFondSize++;
        }

        private void DecreaseTextSize(object? obj)
        {
            MyRTBLayout.MyFondSize--;
        }

        private void SetFondFamily(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetFondFamily(range);

            }
        }

        private void SetFondSize(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.SetFondSize(range);

            }

        }

        private void SetBackgroundToText(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.ApplyTextBackgroundColorToTextRange(range);

            }
        }

        private void SetForegroundToText(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.ApplyForegroundColorToTextRange(range);

            }
        }

        private void ToggleStrikeTrough(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.ToggleTrikeTrough(range);

            }
        }

        private void ToggleItalic(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb != null)
            {
                TextRange range = rtb.Selection;
                MyRTBLayout.ToggleItalic(range);
            }
        }

        private void SetUnderline(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb == null)
            {
                MessageBox.Show("CommandParameter = null");
                return;
            }
            TextRange range = rtb.Selection;

            MyRTBLayout.ToggleUnderline(range);
        }

        private void SetFontWeight(object? obj)
        {
            RichTextBox rtb = obj as RichTextBox;
            if (rtb == null)
            {
                MessageBox.Show("CommandParameter = null");
                return;
            }
            TextRange range = rtb.Selection;

            MyRTBLayout.SetFontWeight(MyRTBLayout.GetFontWeight(range), range);
        }

        private bool CanExecute_Test(object? obj)
        {
            return true;
        }

        private void Execute_Test(object? obj)
        {
            MessageBox.Show(MyRTBLayout.SelectedTextColor.ColorName);
        }


        //converting to rtb
        public string ConvertRichTextBoxToRtf(RichTextBox richTextBox)
        {
            if (richTextBox == null) throw new ArgumentNullException(nameof(richTextBox));

            using (var memoryStream = new System.IO.MemoryStream())
            {
                var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                range.Save(memoryStream, DataFormats.Rtf);
                return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        //converting to byte[]
        private byte[] ConvertRichTextBoxToByteArray(RichTextBox richTextBox)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                textRange.Save(stream, DataFormats.Xaml);
                return stream.ToArray();
            }
        }


        /*Commands zijn onderverdeeld in 3 regions
         * -Fields
         * -CanExecutes
         * -Actions
         */
        #region Commands
        #region CommandFields
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdTest { get; set; }
        public ICommand UpdateRichTextBoxCommand { get; }
        public ICommand cmdIncreaseTextSize {  get; set; }
        public ICommand cmdDecreaseTextSize { get; set; }

        //relaycommands
        public ICommand cmdSetFontWeight {  get; set; }
        public ICommand cmdSetUnderline { get; set; }
        public ICommand cmdToggleItalic { get; set; }
        public ICommand cmdToggleStrikeTrough { get; set; }
        
        public ICommand cmdSetForegroundToText { get; set; }
        public ICommand cmdSetBackgroundToText { get; set; }
        public ICommand cmdSetFondFamily { get; set; }
        public ICommand cmdSetFondSize { get; set; }
        public ICommand cmdSetSuperScript { get; set; }
        public ICommand cmdSetSubScript { get; set; }
        public ICommand cmdSetTextAlignmentLeft { get; set; }
        public ICommand cmdSetTextAlignmentRight { get; set; }
        public ICommand cmdSetTextAlignmentCenter { get; set; }
        public ICommand cmdSetTextAlignmentJustify { get; set; }
        public ICommand cmdCreateBullets { get; set; }
        public ICommand cmdCreateNumbering { get; set; }
        public ICommand cmdIncreaseTextIndent { get; set; }
        public ICommand cmdDecreaseTextIndent { get; set; }

        //aplicationCommands
        public ICommand cmdCut { get; set; }
        public ICommand cmdCopy { get; set; }
        public ICommand CmdPaste { get; set; }
        public ICommand cmdUndo { get; set; }
        public ICommand cmdRedo { get; set; }
       
        #endregion



        #region Command CanExecutes
        private bool CanExecute_UpdateRTB(object? obj)
        {
            return true;
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            //if (isNew || MySelectedItem.IsDirty)
            //{
            //    return true;
            //}
            //return false;

            return true;
        }
        #endregion

        #region Command Actions
        private void UpdateRichTextBox(object? obj)
        {
            var richTextBox = obj as RichTextBox;

            if (MySelectedItem != null && richTextBox != null)
            {
                string rtfString = MySelectedItem.MyRTFString;
                if (!string.IsNullOrEmpty(rtfString))
                {
                    using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfString)))
                    {
                        richTextBox.Selection.Load(stream, DataFormats.Rtf);
                    }
                }
            }
        }

        private void Execute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_New_Command(object? obj)
        {
            safetySave();

            var _obj = new clsDagboekModel()
            {
                PersoonID = this.PersoonID,
                DateCreated = DateTime.Now,
                DagboekContentString = string.Empty
            };
            MijnCollectie.Add(_obj);
            MySelectedItem = _obj;

            isNew = true;
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show("wil je deze entry verwijderen?", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MyService.Delete(MySelectedItem);
                MijnCollectie.Remove(MySelectedItem);
                if (MijnCollectie.Count == 0)
                {
                    var _obj = new clsDagboekModel()
                    {
                        PersoonID = this.PersoonID,
                        DateCreated = DateTime.Now,
                        DagboekContentString = "Er zijn nog geen files opgeslagen"
                    };
                    MijnCollectie.Add(_obj);
                    MySelectedItem = _obj;

                    isNew = true;
                    isEmptyCollection = false;
                }
                MySelectedItem = MijnCollectie.FirstOrDefault();
            }
        }

        private void Execute_Save_Command(object? obj)
        {
            RichTextBox richTextBox = obj as RichTextBox;
            if (richTextBox != null)
            {
                MySelectedItem.MyFlowDocument = ConvertRichTextBoxToByteArray(richTextBox);
                //MessageBox.Show(MySelectedItem.MyRTFString.ToString());

                if (isNew)
                {
                    if (!MyService.Insert(MySelectedItem))
                    {
                        MessageBox.Show(MySelectedItem.ErrorBoodschap);
                    }
                    isNew = false;
                }
                else
                {
                    if(!MyService.Update(MySelectedItem))
                    {
                        MessageBox.Show(MySelectedItem.ErrorBoodschap);
                    }
                }

                MySelectedItem.IsDirty = false;

            }
            else
            {
                MessageBox.Show("casting error -> rtb isn't correct");
            }



            
        }
        #endregion
        #endregion

        //om een alinea te indenteren moet ik een paragraaf doorsturen niet gewoon een selectie
        private IEnumerable<Paragraph> GetSelectedParagraphs(TextSelection selection)
        {
            HashSet<Paragraph> paragraphs = new HashSet<Paragraph>(); // Avoid duplicates
            TextPointer current = selection.Start;

            while (current != null && current.CompareTo(selection.End) <= 0)
            {
                // Move up the tree to find the nearest paragraph
                DependencyObject parent = current.Parent;
                while (parent != null && !(parent is Paragraph))
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }

                if (parent is Paragraph paragraph)
                {
                    paragraphs.Add(paragraph); // Add the full paragraph
                }

                current = current.GetNextContextPosition(LogicalDirection.Forward);
            }

            return paragraphs;
        }





        private void safetySave()
        {
            if (MySelectedItem.IsDirty = true)
            {
                if (MessageBox.Show("wil je opslaan? laatste veranderingen worden anders niet opgeslagen", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes )
                {
                    if (isNew)
                    {
                        MyService.Insert(MySelectedItem);
                    }
                    else
                    {
                        MyService.Update(MySelectedItem);
                    }
                }
                else
                {
                    if (isEmptyCollection)
                    {
                        if (MessageBox.Show("wil verwijderen", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            MijnCollectie.Remove(MySelectedItem);
                        }
                        
                    }
                }
            }
        }
    }
}

