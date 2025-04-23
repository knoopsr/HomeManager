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
using HomeManager.Model.Security;
using HomeManager.Model.StickyNotes;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.Win32;


namespace HomeManager.ViewModel.StickyNotes
{
    /// <summary>
    /// ViewModel for managing Sticky Notes.
    /// Handles note creation, selection, image management, and persistence.
    /// </summary>
    public class clsStickyNotesViewModel : clsCommonModelPropertiesBase
    {
        #region FIELDS
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
        public clsRTBLayout MyRTBLayout { get; set; }

        /// <summary>Returns <c>clsLoginModel.Instance.PersoonID;</c></summary>
        public int CurrentUserID { get => clsLoginModel.Instance.PersoonID; }

        public ObservableCollection<clsStickyNotesModel> MyCollection
        {
            get
            {
                return _myCollection;
            }
            set
            {
                _myCollection = value;

                // Dit is echt een lelijke quick fix
                // Om de initiele grootte van elke item in de collectie te zetten
                foreach (var item in _myCollection)
                {
                    item.Width = 450 * 0.5f;
                    item.Height = 350 * 0.5f;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the currently selected sticky note.
        /// Triggers size adjustment and property change notification: <c>IncreaseSelectedNoteSize();</c>
        /// </summary>
        public clsStickyNotesModel MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
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
        /// <summary>
        /// Initializes the Sticky Notes ViewModel and loads data.
        /// </summary>
        public clsStickyNotesViewModel()
        {
            _myService = new clsStickyNotesDataService();
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateLoginModel);

            CreateNoteCommand = new clsCustomCommand(Execute_CreateNoteCommand, CanExecute_CreateNoteCommand);
            RemoveNoteCommand = new clsCustomCommand(Execute_RemoveNoteCommand, CanExecute_RemoveNoteCommand);
            HandleImageCommand = new clsCustomCommand(Execute_HandleImageCommand, CanExecute_HandleImageCommand);
            SaveNotesCommand = new clsCustomCommand(Execute_SaveNotesCommand, CanExecute_SaveNotesCommand);
            ItemReceivedCommand = new clsStickyNotesReceivedCommand(this);

            LoadData();
            MySelectedItem = _myService.GetFirstByUserID(CurrentUserID);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads sticky notes for the current user.
        /// </summary>
        public void LoadData()
        {
            MyCollection = _myService.GetAllByUserID(CurrentUserID);
        }

        /// <summary>
        /// Triggered when login model updates. Reloads data.
        /// </summary>
        void OnUpdateLoginModel(clsLoginModel model)
        {
            LoadData();
            MySelectedItem = _myService.GetFirstByUserID(CurrentUserID);
        }

        /// <summary>
        /// Saves all notes in the collection to the database.
        /// </summary>
        private async void SaveCommand(object? parameter = null)
        {
            foreach (clsStickyNotesModel item in MyCollection)
            {
                for (int i = 0; i < MyCollection.Count; i++)
                {
                    MyCollection[i].Position = i;
                    item.UserID = CurrentUserID;
                }

                if (!_newStatus)
                {
                    if (_myService.Update(item))
                    {
                        item.IsDirty = false;
                        item.MijnSelectedIndex = 0;
                        _newStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(item.ErrorBoodschap, "SaveCommand: Update error?");
                    }
                }
                else
                {
                    MessageBox.Show(item.ErrorBoodschap, "SaveCommand: NEWSTATUS error?");
                }
            }
        }

        /// <summary>
        /// Enlarges the newly selected note and resets the previous.
        /// </summary>
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

        /// <summary>
        /// Opens a dialog to upload an image and sets it as a note thumbnail.
        /// </summary>
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

        /// <summary>
        /// Saves and opens the thumbnail image in the default viewer.
        /// </summary>
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

        #region COMMANDS
        private bool CanExecute_HandleImageCommand(object obj) => true;
        private bool CanExecute_CreateNoteCommand(object obj) => true;
        private bool CanExecute_SaveNotesCommand(object obj) => true;
        private bool CanExecute_RemoveNoteCommand(object obj) => true;

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
        
        private void Execute_CreateNoteCommand(object obj)
        {
            clsStickyNotesModel newItem = new clsStickyNotesModel()
            {
                Title = "Hello, world!",
                Content = @"{\rtf1\ansi\ansicpg1252\uc1\htmautsp\deff2{\fonttbl{\f0\fcharset0 Times New Roman;}{\f2\fcharset0 Segoe UI;}}{\colortbl\red0\green0\blue0;\red255\green255\blue255;}\loch\hich\dbch\pard\plain\ltrpar\itap0{\lang1033\fs24\f2\cf0 \cf0\ql{\f2 {\b\ltrch WELCOME TO STICKY NOTES}{\ltrch , }{\i\ltrch The }{\i\ul\ltrch features }{\i\ltrch are}{\ltrch :}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 1. Use the Window's buttons: }{\b\ltrch Create}{\ltrch , }{\b\ltrch Save All}{\ltrch  and }{\b\ltrch Remove }{\ltrch to manage your sticky notes.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 2. While Selected:}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch - Use }{\b\ltrch CTRL+B }{\ltrch for Bold Text}\li0\ri0\sa0\sb0\fi300\ql\par}
{\f2 {\ltrch - Use }{\i\ltrch CTRL+I }{\ltrch for Italic Text}\li0\ri0\sa0\sb0\fi300\ql\par}
{\f2 {\ltrch - Use }{\ul\ltrch CTRL+U }{\ltrch for Underlined Text}\li0\ri0\sa0\sb0\fi300\ql\par}
{\f2 {\ltrch 3. Scroll while hovering the note }{\b\ltrch scrolls }{\ltrch downward, if enough text is visible.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 4. Hover, Click, }{\b\ltrch Drag and Drop }{\ltrch a sticky note in between two others or onto another to change it's position.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 5. Upload a }{\b\ltrch thumbnail}{\ltrch , click it again and use the prompt to manage it.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 6. Enter a }{\b\ltrch title }{\ltrch in between the thumbnail and date.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 7. Select a }{\b\ltrch date}{\ltrch , past dates are red, future ones are green.}\li0\ri0\sa0\sb0\fi0\ql\par}
{\f2 {\ltrch 8. Use the combobox and select a }{\b\ltrch color }{\ltrch from it's dropdown menu to change the border color of a note.}\li0\ri0\sa0\sb0\fi0\ql\par}
}
}",
                ThumbnailName = string.Empty,
                Date = DateTime.Now,
                SelectedBrush = "titleBrush",
                UserID = CurrentUserID
            };

            if (MyCollection.IsNullOrEmpty()) newItem.Position = 0;
            else newItem.Position = MyCollection.Count;

            _newStatus = true;

            if (_myService.Insert(newItem))
            {
                newItem.IsDirty = false;
                newItem.MijnSelectedIndex = 0;
                newItem.MyVisibility = (int)Visibility.Visible;
                _newStatus = false;
                LoadData();
                MySelectedItem = newItem;
            }
            else
            {
                MessageBox.Show(newItem.ErrorBoodschap, "CreateCommand: Error?");
            }
        }

        private void Execute_SaveNotesCommand(object obj)
        {
            SaveCommand();
        }

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

        #endregion
    }
}