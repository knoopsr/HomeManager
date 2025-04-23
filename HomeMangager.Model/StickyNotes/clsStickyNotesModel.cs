using HomeManager.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
// using System.Windows.Media;

namespace HomeManager.Model.StickyNotes
{
    /// <summary>
    /// Represents a sticky note, including its content, appearance, and metadata.
    /// Implements validation and change tracking.
    /// </summary>
    public class clsStickyNotesModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region FIELDS
        private int _userID;
        private int _stickyNoteID;
        private string _title;
        private string _content;
        private byte[] _thumbnail; 
        private string _thumbnailName;
        private DateTime _date;
        private string _selectedBrush;
        private int _position;
        private double _width = 225; // Default smaller size
        private double _height = 175;

        /// <summary>
        /// A predefined collection of available brushes for sticky note styling.
        /// </summary>
        private static readonly ObservableCollection<string> _brushCollection = new()
        {
            "titleBrush",
            "menuBrush",
            "toolbarDisabledBrush",
            "backgroundBrush",
            "iconBrush",
            "redBrush",
            "greenBrush",
            "blueBrush",
            "yellowBrush",
            "orangeBrush"
        };
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets or sets the note's position index in a collection.
        /// </summary>
        public int Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the note's width.
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the note's height.
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected brush (style) for this note.
        /// </summary>
        public string SelectedBrush
        {
            get => _selectedBrush;
            set
            {
                if (_selectedBrush != value)
                {
                    _selectedBrush = value;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the creation or last modification date of the note.
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ID of the user who owns the note.
        /// </summary>
        public int UserID
        {
            get => _userID;
            set
            {
                _userID = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the sticky note.
        /// </summary>
        public int StickyNoteID
        {
            get => _stickyNoteID;
            set
            {
                _stickyNoteID = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title of the sticky note.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    if (_title != null)
                    {
                        IsDirty = true;
                    }
                }
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the main (RichTextBox) text content of the note.
        /// </summary>
        public string Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    if (_content != null)
                    {
                        IsDirty = true;
                    }
                }
                _content = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the file name of the associated thumbnail image.
        /// </summary>
        public string ThumbnailName
        {
            get => _thumbnailName;
            set
            {
                if (_thumbnailName != value)
                {
                    if (_thumbnailName != null)
                    {
                        IsDirty = true;
                    }
                }
                _thumbnailName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the image thumbnail as a byte array.
        /// </summary>
        public byte[] Thumbnail
        {
            get => _thumbnail;
            set
            {
                if (_thumbnail != value)
                {
                    if (_thumbnail != null)
                    {
                        IsDirty = true;
                    }
                }
                _thumbnail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the available brush styles for note appearance customization.
        /// </summary>
        public ObservableCollection<string> BrushCollection
        {
            get => _brushCollection;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Returns a string representation of the note with its title and content.
        /// </summary>
        public override string ToString()
        {
            return $"{Title}, {Content}";
        }

        /// <summary>
        /// Provides property-level validation errors based on the column name.
        /// </summary>
        /// <param name="columnName">The name of the property to validate.</param>
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Title":
                        if (string.IsNullOrWhiteSpace(Title))
                        {
                            error = "The title is a required field.";
                            if (ErrorList.Contains("Title") == false)
                            {
                                ErrorList.Add("Title");
                            }
                            else
                            {
                                ErrorList.Remove("Title");
                            }
                        }
                        else if (Title.Length > 50)
                        {
                            error = "Your text is too long!";
                            if (ErrorList.Contains("Title") == false)
                            {
                                ErrorList.Add("Title");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Title"))
                            {
                                ErrorList.Remove("Title");
                            }
                        }
                        return error;

                    default:
                        error = null;
                        return error;
                }
            }
        }
        #endregion
    }
}
