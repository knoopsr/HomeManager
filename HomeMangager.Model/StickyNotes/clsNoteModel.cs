using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.StickyNotes
{
    public class clsNoteModel :clsCommonModelPropertiesBase
    {



        private int _noteID;
        public int NoteID
        {
            get { return _noteID; }
            set
            {
                _noteID = value;
                OnPropertyChanged();
            }
        }

        private double _canvasTop;
        public double CanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged();
            }
        }

        private double _canvasLeft;
        public double CanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged();
            }
        }

        private double _canvasRelativeRight;
        public double CanvasRelativeRight
        {
            get { return _canvasRelativeRight; }
            set
            {
                _canvasRelativeRight = value;
                OnPropertyChanged();
            }
        }



        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }





        private string _noteColor;
        public string NoteColor
        {
            get { return _noteColor; }
            set
            {
                _noteColor = value;
                OnPropertyChanged();
            }
        }

        private string _noteBorderColor = "Black";
        public string NoteBorderColor
        {
            get { return _noteBorderColor; }
            set
            {
                _noteBorderColor = value;
                OnPropertyChanged();
            }
        }

        private string _noteTitle;
        public string NoteTitle
        {
            get { return _noteTitle; }
            set
            {
                _noteTitle = value;
                OnPropertyChanged();
            }
        }

        private string _noteText;
        public string NoteText
        {
            get { return _noteText; }
            set
            {
                _noteText = value;
                OnPropertyChanged();
            }
        }

        private int _zIndex;
        public int ZIndex
        {
            get { return _zIndex; }
            set
            {
                _zIndex = value;
                OnPropertyChanged();
            }
        }


    }
}
