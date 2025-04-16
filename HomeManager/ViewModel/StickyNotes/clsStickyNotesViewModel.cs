using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using HomeManager.Common;
using HomeManager.DataService.StickyNotes;
using HomeManager.Helpers;
using HomeManager.MailService;
using HomeManager.Model;
using HomeManager.Model.StickyNotes;
using Microsoft.Win32;


namespace HomeManager.ViewModel.StickyNotes
{
    public class clsStickyNotesViewModel : clsCommonModelPropertiesBase
    {
        #region FIELDS
        public clsRTBLayout MyRTBLayout { get; set; }

        private clsStickyNotesDataService _myService;
        private ObservableCollection<clsStickyNotesModel> _myCollection;
        private clsStickyNotesModel _mySelectedItem;
        private clsStickyNotesModel _previousSelectedItem;
        private bool _isFocused = false;
        private bool _isFocusedAfterNew = false;
        private bool _newStatus = false; //Fix convention

        public ICommand CreateNoteCommand { get; set; }
        public ICommand RemoveNoteCommand { get; set; }
        public ICommand HandleImageCommand { get; set; }
        public ICommand SaveNotesCommand { get; set; }
        public ICommand ItemReceivedCommand { get; set; }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Fix width gimmick
        /// </summary>
        public ObservableCollection<clsStickyNotesModel> MyCollection
        {
            get
            {
                return _myCollection;
            }
            set
            {
                _myCollection = value;

                // Initialize the size of each item in the collection
                foreach (var item in _myCollection)
                {
                    item.Width = 450 * 0.5f; // Set the initial width
                    item.Height = 350 * 0.5f; // Set the initial height
                }
                OnPropertyChanged();
            }
        }

        public clsStickyNotesModel MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
                if (value != null)
                {
                    if (_mySelectedItem != null && _mySelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Do you want to save:" + _mySelectedItem + " ?", "SAVE STICKYNOTE",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            SaveCommand();
                            LoadData();
                        }
                    }
                }
                _mySelectedItem = value;
                IncreaseSelectedNoteSize();
                OnPropertyChanged();
            }
        }

        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                _isFocused = value;
                OnPropertyChanged();
            }
        }

        public bool IsFocusedAfterNew
        {
            get
            {
                return _isFocusedAfterNew;
            }
            set
            {
                _isFocusedAfterNew = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CONSTRUCTOR
        public clsStickyNotesViewModel()
        {
            _myService = new clsStickyNotesDataService();

            CreateNoteCommand = new clsCustomCommand(Execute_CreateNoteCommand, CanExecute_CreateNoteCommand);
            RemoveNoteCommand = new clsCustomCommand(Execute_RemoveNoteCommand, CanExecute_RemoveNoteCommand);
            HandleImageCommand = new clsCustomCommand(Execute_HandleImageCommand, CanExecute_HandleImageCommand);
            SaveNotesCommand = new clsCustomCommand(Execute_SaveNotesCommand, CanExecute_SaveNotesCommand);
            ItemReceivedCommand = new clsStickyNotesReceivedCommand(this);

            LoadData();
            MySelectedItem = _myService.GetFirst();
        }
        #endregion

        #region METHODS
        private async void SaveCommand(object? parameter = null)
        {
            if (_mySelectedItem != null)
            {
                if (_newStatus)
                {
                    if (_myService.Insert(_mySelectedItem))
                    {
                        _mySelectedItem.IsDirty = false;
                        _mySelectedItem.MijnSelectedIndex = 0;
                        _mySelectedItem.MyVisibility = (int)Visibility.Visible;
                        _newStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mySelectedItem.ErrorBoodschap, "SaveCommand: Error?");
                    }
                }
                else
                {
                    if (_myService.Update(_mySelectedItem))
                    {
                        _mySelectedItem.IsDirty = false;
                        _mySelectedItem.MijnSelectedIndex = 0;
                        _newStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mySelectedItem.ErrorBoodschap, "SaveCommand: Error?");
                    }
                }
            }
        }

        private void IncreaseSelectedNoteSize()
        {
            if (_previousSelectedItem != null)
            {
                // Reset the size of the previously selected sticky note
                _previousSelectedItem.Width = 450 * 0.5f; // Ensure a fixed increase instead of stacking
                _previousSelectedItem.Height = 350 * 0.5f;
            }

            if (MySelectedItem != null)
            {
                // Apply 20% increase only to the newly selected note
                MySelectedItem.Width = 450;
                MySelectedItem.Height = 350;
            }

            // Update the previous item tracker
            _previousSelectedItem = MySelectedItem;
        }

        private void LoadData()
        {
            MyCollection = _myService.GetAll();
        }

        #region IMAGEHANDLER
        private void UploadImage()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.jpeg; *.gif)|*.jpg; *.png; *.jpeg; *.gif";
            //string myFileName = string.Empty;

            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                // Convert the selected image to a byte array and update the Thumbnail property
                MySelectedItem.Thumbnail = File.ReadAllBytes(openFileDialog.FileName);
                MySelectedItem.ThumbnailName = openFileDialog.FileName;
                MySelectedItem.IsDirty = true;
            }
        }

        private void ViewImage()
        {
            try
            {
                // Save the image to a temporary file and open it
                string tempFilePath = Path.GetTempFileName() + Path.GetExtension(MySelectedItem.ThumbnailName);
                File.WriteAllBytes(tempFilePath, MySelectedItem.Thumbnail);

                var process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = tempFilePath;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

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
        #endregion

        #region COMMANDS
        private bool CanExecute_CreateNoteCommand(object obj) { return true; }
        private void Execute_CreateNoteCommand(object obj)
        {
            clsStickyNotesModel newItem = new clsStickyNotesModel
            {
                Title = "Add a title!",
                Content = "Hello, world!",
                ThumbnailName = string.Empty,
                Date = DateTime.Now,
                SelectedBrush = "titleBrush"
            };

            MyCollection.Add(newItem);
            MySelectedItem = newItem;
            MySelectedItem.MyVisibility = (int)Visibility.Hidden;
            _newStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_RemoveNoteCommand(object obj) { return true; }
        private void Execute_RemoveNoteCommand(object obj)
        {
            if (MySelectedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to delete your sticky note?", "DELETE STICKYNOTE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (_myService.Delete(MySelectedItem))
                    {
                        _newStatus = false;   
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error while deleting the selected item:", MySelectedItem.ErrorBoodschap);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a sticky note to delete.", "SELECT STICKYNOTE", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanExecute_HandleImageCommand(object obj) { return true; }
        private void Execute_HandleImageCommand(object obj)
        {
            // If Thumbnail doesn't exist, upload a picture immediately.
            if (MySelectedItem.Thumbnail == null)
            {
                UploadImage();
                return;
            }

            // Ask the user what they want to do.
            MessageBoxResult result = MessageBox.Show(
                $"Do you want to upload a new thumbnail?\n\n" +
                "\"Yes\" - Upload a new thumbnail.\n" +
                "\"No\" - View your current thumbnail.\n" +
                "\"Cancel\" - Close this message box.",
                "Image Options", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                UploadImage();
            }
            else if (result == MessageBoxResult.No)
            {
                ViewImage();
            }
            else return;
        }

        private bool CanExecute_SaveNotesCommand(object obj)
        {
            //if (MySelectedItem != null &&
            //    MySelectedItem.Error == null &&
            //    MySelectedItem.IsDirty == true)
            //{
                return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        private void Execute_SaveNotesCommand(object obj)
        {
            SaveCommand();
        }
        #endregion
    }
}