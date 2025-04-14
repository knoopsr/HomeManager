using HomeManager.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
// using System.Windows.Media;

namespace HomeManager.Model.StickyNotes
{
    public class clsStickyNotesModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region FIELDS
        private string _selectedBrush;
        private DateTime _date;
        private int _userID;
        private int _stickyNoteID;
        private string _title;
        private string _content;
        private string _thumbnailName;
        private byte[] _thumbnail;
        private double _width = 225; // Default smaller size
        private double _height = 175;
        private static readonly ObservableCollection<string> _brushCollection = new()
        {
            "titleBrush",
            "menuBrush",
            "toolbarBrush",
            "toolbarDisabledBrush",
            "backgroundBrush",
            "iconBrush"
        };
        #endregion

        #region PROPERTIES
        /*
         * Width + Heigth zijn VIEW-gebonden componenten en horen normaal niet thuis in de model.
         * Ze staan omwille van de manier waarop WPF ListViewItems worden gerenderd / weergegeven.
         * Indien je dit doet op de "perfecte" MVVM manier,
         * met een Trigger / DataTrigger op de style die dan <Trigger Property="IsSelected" value="False"> de with en height veranderd
         * dan wordt de note weergegeven in de helft van de grootte bij he initializeren van de app. Een bug dus lijkt me.
         * Los daarvan worden ook alle background ingekort waardoor je de mooie DarkSlateGray Background met Opacity niet tegoei kan weergeven.
        */

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

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public int UserID
        {
            get => _userID;
            set
            {
                _userID = value;
                OnPropertyChanged();
            }
        }

        public int StickyNoteID
        {
            get => _stickyNoteID;
            set
            {
                _stickyNoteID = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<string> BrushCollection
        {
            get => _brushCollection;
        }
        #endregion

        public override string ToString()
        {
            return $"{Title}, {Content}";
        }

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
    }
}
