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
using System.Diagnostics;
using System.Text.RegularExpressions;
using HomeManager.Model.Personen;
using HomeManager.DataService.Personen;
using HomeManager.Services;

namespace HomeManager.ViewModel
{
    public class clsDagboekViewModel : clsCommonModelPropertiesBase
    {
        private int AccountID
        {
            get { return clsLoginModel.Instance.AccountID; }
        }

        private string PersoonID
        {
            get { return Convert.ToString(AccountID); }
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

        public ObservableCollection<clsEmailAdressenModel> mijnEmailAdressen { get; }

       

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
        public clsEmailAdressenDataService EmailService { get; set; }
        private clsDialogService _DialogService;

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
            cmdPrint = new clsRelayCommand(PrintMyFlowDocument);

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
            cmdFindHyperlinks = new clsRelayCommand(FindHyperlinks);
            cmdFindEmails = new clsRelayCommand(FindEmailLinksWithCommand);
            cmdOpenEmails = new clsRelayCommand(SendEmails);


            //AplicationCommands
            cmdCut = ApplicationCommands.Cut;
            cmdCopy = ApplicationCommands.Copy;
            CmdPaste = ApplicationCommands.Paste;
            cmdUndo = ApplicationCommands.Undo;
            cmdRedo = ApplicationCommands.Redo;
            

            MyRTBLayout = new clsRTBLayout();
            MyService = new clsDagboekDataService();
            
            //email gedeelte met personen
            EmailService = new clsEmailAdressenDataService();
            mijnEmailAdressen = EmailService.GetAll();
            _DialogService = new clsDialogService();
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

       
        private void SendEmails(object? obj)
        {
            if (obj.ToString == null)
            {
                MessageBox.Show("ongeldig emailadress");
                return;
            }
            clsEmailVerzendenModel sendEmail = new clsEmailVerzendenModel()
            {
                PersoonID = this.AccountID,
                Ontvanger = obj.ToString(),
            };
            clsMessenger.Default.Send<clsEmailVerzendenModel>(sendEmail);

            _DialogService.ShowDialog(new HomeManager.View.Personen.ucEmailVerzenden(), "email verzenden");
        }

        //deze verwijderd layout
        //private void FindHyperlinks(object? obj)
        //{
        //    if (obj is RichTextBox rtb)
        //    {
        //        FlowDocument document = rtb.Document;

        //        foreach (var block in document.Blocks.ToList()) // Make a copy since we might replace blocks
        //        {
        //            if (block is Paragraph paragraph)
        //            {
        //                var inlines = paragraph.Inlines.ToList(); // Again, work on a copy
        //                paragraph.Inlines.Clear();

        //                foreach (var inline in inlines)
        //                {
        //                    if (inline is Run run && !(run.Parent is Hyperlink))
        //                    {
        //                        string text = run.Text;
        //                        var regex = new Regex(@"https:\/\/[^\s]+");
        //                        int lastIndex = 0;

        //                        foreach (Match match in regex.Matches(text))
        //                        {
        //                            // Add text before the match
        //                            if (match.Index > lastIndex)
        //                            {
        //                                string before = text.Substring(lastIndex, match.Index - lastIndex);
        //                                paragraph.Inlines.Add(new Run(before));
        //                            }

        //                            // Add hyperlink
        //                            string url = match.Value;
        //                            var link = new Hyperlink(new Run(url))
        //                            {
        //                                NavigateUri = new Uri(url),
        //                                Cursor = Cursors.Hand
        //                            };
        //                            link.RequestNavigate += (s, e) =>
        //                            {
        //                                System.Diagnostics.Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
        //                                {
        //                                    UseShellExecute = true
        //                                });
        //                                e.Handled = true;
        //                            };
        //                            paragraph.Inlines.Add(link);

        //                            lastIndex = match.Index + match.Length;
        //                        }

        //                        // Add remaining text
        //                        if (lastIndex < text.Length)
        //                        {
        //                            string after = text.Substring(lastIndex);
        //                            paragraph.Inlines.Add(new Run(after));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        paragraph.Inlines.Add(inline); // Keep non-Run inlines untouched
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void FindHyperlinks(object? obj)
        {
            if (obj is RichTextBox rtb)
            {
                FlowDocument document = rtb.Document;
                var regex = new Regex(@"https:\/\/[^\s]+");

                foreach (var block in document.Blocks.ToList())
                {
                    if (block is Paragraph paragraph)
                    {
                        var inlines = paragraph.Inlines.ToList();
                        paragraph.Inlines.Clear();

                        foreach (var inline in inlines)
                        {
                            if (inline is Run run && !(run.Parent is Hyperlink))
                            {
                                string text = run.Text;
                                int lastIndex = 0;

                                foreach (Match match in regex.Matches(text))
                                {
                                    // Text before match
                                    if (match.Index > lastIndex)
                                    {
                                        string before = text.Substring(lastIndex, match.Index - lastIndex);
                                        paragraph.Inlines.Add(CloneRunWithText(run, before));
                                    }

                                    // Matched hyperlink
                                    string url = match.Value;
                                    var link = new Hyperlink(CloneRunWithText(run, url))
                                    {
                                        NavigateUri = new Uri(url),
                                        Cursor = Cursors.Hand
                                    };
                                    link.RequestNavigate += (s, e) =>
                                    {
                                        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                                        e.Handled = true;
                                    };
                                    paragraph.Inlines.Add(link);

                                    lastIndex = match.Index + match.Length;
                                }

                                // Remaining text
                                if (lastIndex < text.Length)
                                {
                                    string after = text.Substring(lastIndex);
                                    paragraph.Inlines.Add(CloneRunWithText(run, after));
                                }
                            }
                            else
                            {
                                paragraph.Inlines.Add(inline); // Preserve other inlines as-is
                            }
                        }
                    }
                }
            }
        }

        private Run CloneRunWithText(Run original, string newText)
        {
            var newRun = new Run(newText)
            {
                FontWeight = original.FontWeight,
                FontStyle = original.FontStyle,
                FontFamily = original.FontFamily,
                FontSize = original.FontSize,
                Foreground = original.Foreground,
                Background = original.Background,
                TextDecorations = original.TextDecorations?.Clone()
            };

            return newRun;
        }


        //deze verwijderd layout
        //private void FindEmailLinksWithCommand(object? obj)
        //{
        //    if (obj is RichTextBox rtb)
        //    {
        //        FlowDocument document = rtb.Document;
        //        bool isValidEmail = false;

        //        foreach (var block in document.Blocks.ToList())
        //        {
        //            if (block is Paragraph paragraph)
        //            {
        //                var inlines = paragraph.Inlines.ToList();
        //                paragraph.Inlines.Clear();

        //                foreach (var inline in inlines)
        //                {
        //                    if (inline is Run run && !(run.Parent is Hyperlink))
        //                    {
        //                        string text = run.Text;
        //                        var regex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b", RegexOptions.IgnoreCase);
        //                        int lastIndex = 0;

        //                        foreach (Match match in regex.Matches(text))
        //                        {
        //                            // Add text before match
        //                            if (match.Index > lastIndex)
        //                            {
        //                                string before = text.Substring(lastIndex, match.Index - lastIndex);
        //                                paragraph.Inlines.Add(new Run(before));
        //                            }

        //                            // Create hyperlink with command binding
        //                            string email = match.Value;

        //                            isValidEmail = mijnEmailAdressen.Any(x => x.Emailadres == email);

        //                            if (isValidEmail == true)
        //                            {
        //                                var link = new Hyperlink(new Run(email))
        //                                {
        //                                    //test
        //                                    Command = new clsRelayCommand(_newCommand => SendEmails(email)), //this is not the right command
        //                                    Cursor = Cursors.Hand
        //                                };

        //                                paragraph.Inlines.Add(link);

        //                                lastIndex = match.Index + match.Length;
        //                            }
        //                        }

        //                        // Add remaining text
        //                        if (lastIndex < text.Length)
        //                        {
        //                            string after = text.Substring(lastIndex);
        //                            paragraph.Inlines.Add(new Run(after));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        paragraph.Inlines.Add(inline); // Keep other inlines
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void FindEmailLinksWithCommand(object? obj)
        {
            if (obj is RichTextBox rtb)
            {
                FlowDocument document = rtb.Document;

                foreach (var block in document.Blocks.ToList())
                {
                    if (block is Paragraph paragraph)
                    {
                        var inlines = paragraph.Inlines.ToList();
                        paragraph.Inlines.Clear();

                        foreach (var inline in inlines)
                        {
                            if (inline is Run run && !(run.Parent is Hyperlink))
                            {
                                string text = run.Text;
                                var regex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b", RegexOptions.IgnoreCase);
                                int lastIndex = 0;

                                foreach (Match match in regex.Matches(text))
                                {
                                    string email = match.Value;
                                    bool isValidEmail = mijnEmailAdressen.Any(x => x.Emailadres == email);

                                    if (!isValidEmail)
                                        continue;

                                    // Add text before match
                                    if (match.Index > lastIndex)
                                    {
                                        string before = text.Substring(lastIndex, match.Index - lastIndex);
                                        paragraph.Inlines.Add(CloneRunWithText(run, before));
                                    }

                                    // Add email hyperlink with command
                                    var link = new Hyperlink(CloneRunWithText(run, email))
                                    {
                                        Cursor = Cursors.Hand,
                                        Command = new clsRelayCommand(_ => SendEmails(email))
                                    };

                                    paragraph.Inlines.Add(link);
                                    lastIndex = match.Index + match.Length;
                                }

                                // Add remaining text after last match
                                if (lastIndex < text.Length)
                                {
                                    string after = text.Substring(lastIndex);
                                    paragraph.Inlines.Add(CloneRunWithText(run, after));
                                }
                            }
                            else
                            {
                                paragraph.Inlines.Add(inline); // Preserve non-Run inlines
                            }
                        }
                    }
                }
            }
        }



        private bool CanExecute_Test(object? obj)
        {
            return true;
        }

        private void Execute_Test(object? obj)
        {
            StringBuilder sb = new StringBuilder();
            foreach (clsEmailAdressenModel item in mijnEmailAdressen)
            {
                sb.AppendLine(item.Emailadres.ToString());
            }

            MessageBox.Show(sb.ToString());

            SendEmails("Brammer@Email.com");

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
                textRange.Save(stream, DataFormats.XamlPackage);
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
        public ICommand cmdPrint {  get; set; }

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
        public ICommand cmdFindHyperlinks { get; set; }
        public ICommand cmdFindEmails { get; set; }
        public ICommand cmdOpenEmails { get; set; }

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
            return true;
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
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {

                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_New_Command(object? obj)
        {
            //safetySave();

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
                    GenerateCollection();
                    MySelectedItem = MijnCollectie.FirstOrDefault();
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

        private void PrintMyFlowDocument(object? obj)
        {
            if (obj is RichTextBox rtb)
            {
                FlowDocument document = rtb.Document;

                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Print");
                }
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

