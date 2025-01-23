using HomeManager.Common;
using HomeManager.Model.StickyNotes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeManager.ViewModel.StickyNotes
{
    public class clsNoteViewModel : clsCommonModelPropertiesBase
    {

        public string LighterColor(string hexColor, double factor)
        {
            // Converteer hex string naar Color object
            Color color = (Color)ColorConverter.ConvertFromString(hexColor);

            // Pas de RGB-waarden aan om de kleur lichter te maken
            byte r = (byte)(color.R + (255 - color.R) * factor);
            byte g = (byte)(color.G + (255 - color.G) * factor);
            byte b = (byte)(color.B + (255 - color.B) * factor);

            // Zet de nieuwe kleur om naar een hex string
            Color lighterColor = Color.FromRgb(r, g, b);
            return lighterColor.ToString();
        }


        public ICommand MouseLeftUpNoteItem { get; set; }
        public ICommand MouseLeftDownNoteItem { get; set; }
        public ICommand MouseMoveNoteItem { get; set; }

        public ICommand MouseEnterNoteItem { get; set; }
        public ICommand MouseLeaveNoteItem { get; set; }

        public ICommand CanvasSizeChangedCommand { get; set; }

        private bool IsDragging = false;
        private string oudekleur;
        private clsNoteModel _selectedNote; // Houd de huidige geselecteerde notitie bij
        private Point _startPoint;
        private bool _BeginDragging;
        private int _oldZIndex;




        #region Collecties
        private ObservableCollection<clsNoteModel> _mijnCollectie;
        public ObservableCollection<clsNoteModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private double _canvasWidth;
        public double CanvasWidth
        {
            get { return _canvasWidth; }
            set
            {
                _canvasWidth = value;
                OnPropertyChanged();
            }
        }

        private double _canvasHeight;
        public double CanvasHeight
        {
            get { return _canvasHeight; }
            set
            {
                _canvasHeight = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public clsNoteViewModel()
        {
            MijnCollectie = new ObservableCollection<clsNoteModel>();

            MouseMoveNoteItem = new clsRelayCommand<object>(MouseMoveNoteItemExecute);
            MouseLeftDownNoteItem = new clsRelayCommand<object>(MouseLeftDownNoteItemExecute);
            MouseLeftUpNoteItem = new clsRelayCommand<object>(MouseLeftUpNoteItemExecute);
            MouseEnterNoteItem = new clsRelayCommand<object>(MouseEnterNoteItemExecute);
            MouseLeaveNoteItem = new clsRelayCommand<object>(MouseLeaveNoteItemExecute);
            CanvasSizeChangedCommand = new clsRelayCommand<object>(CanvasSizeChangedCommandExecute);

            LoadData();
        }
        public double _oldCanvasSizewidth;
        private double _MaxNoteSpace;




        private void CanvasSizeChangedCommandExecute(object obj)
        {
            if (obj is Canvas canvasSize)
            {


                // Gebruik canvasSize.Width en canvasSize.Height
                CanvasWidth = canvasSize.ActualWidth;
                _canvasHeight = canvasSize.ActualHeight;

                _MaxNoteSpace = canvasSize.ActualWidth * 0.4;

                ResetZIndex();

            }
        }

        private void MouseLeaveNoteItemExecute(object obj)
        {
            if (obj is clsNoteModel note)
            {
                note.NoteColor = oudekleur;
                _BeginDragging = false;
            }
        }

        private void MouseEnterNoteItemExecute(object obj)
        {
            if (obj is clsNoteModel note)
            {
                oudekleur = note.NoteColor;
                note.NoteColor = LighterColor(oudekleur, 0.3);
            }
        }



        private void MouseMoveNoteItemExecute(object obj)
        {
            if (obj is clsNoteModel note)
            {
                if (IsDragging && _selectedNote != null)
                {
                    // Haal de muispositie op ten opzichte van de applicatie
                    var mousePosition = Mouse.GetPosition(Application.Current.MainWindow);
                    if (!_BeginDragging)
                    {
                        _startPoint = mousePosition;
                        _BeginDragging = true;
                    }



                    // Bereken het verschil tussen de huidige muispositie en de startpositie
                    var diff = mousePosition - _startPoint;
                    note.CanvasLeft += diff.X;
                    note.CanvasTop += diff.Y;

          
                    ResetZIndex();

                    // Update de startpositie
                    _startPoint = mousePosition;

                }
            }

        }

        private void ResetZIndex()
        {
            // tel het aantal notities in de collectie
            // zet het laagste zindex nummer op 1 en zet het tweede op 2 enz

            int i = 1;
            foreach (var item in MijnCollectie.OrderBy(x => x.ZIndex))
            {
                double verschil = CanvasWidth - item.CanvasLeft;
                if (verschil > _MaxNoteSpace)
                {
                    item.CanvasRelativeRight = _MaxNoteSpace;
                    item.CanvasLeft = CanvasWidth - _MaxNoteSpace;
                    IsDragging = false;
                }
                else
                {
                    if (verschil <= item.Width)
                    {
                        item.CanvasRelativeRight = item.Width;
                        item.CanvasLeft = CanvasWidth - item.Width;
                    }
                    else
                    {
                        item.CanvasRelativeRight = CanvasWidth - item.CanvasLeft;
                    }


                }

                if (!_BeginDragging)
                {             
                    item.Width = 150;
                    item.Height = 150;
                    item.ZIndex = i;
                    i++;
                }
            }




        }






        private void MouseLeftDownNoteItemExecute(object obj)
        {
            if (obj is clsNoteModel note)
            {
                // zoek het hoogste zindex nummer en verhoog het met 1
                int maxZIndex = 0;
                foreach (var item in MijnCollectie)
                {
                    if (item.ZIndex > maxZIndex)
                    {
                        maxZIndex = item.ZIndex;
                    }
                }
                note.NoteText = "lol";

                note.Width = 180;
                note.Height = 180;


                _oldZIndex = maxZIndex + 1;
                IsDragging = true;
                _selectedNote = note;
                _selectedNote.ZIndex = maxZIndex + 1;
            }
        }

        private void MouseLeftUpNoteItemExecute(object obj)
        {

            IsDragging = false;
            _selectedNote.ZIndex = _oldZIndex;
            _BeginDragging = false;
            _selectedNote = null; // Stop het slepen

            ResetZIndex();
        }

        private void LoadData()
        {
            MijnCollectie.Add(new clsNoteModel
            {
                NoteID = 1,
                CanvasTop = 10,
                CanvasRelativeRight = 100,
                Height = 150,
                Width = 150,
                NoteText = "Dit is een test notitie",
                NoteTitle = "Test Notitie",
                NoteColor = "Yellow",
                NoteBorderColor = "Black"
            });

            MijnCollectie.Add(new clsNoteModel
            {
                NoteID = 2,
                CanvasTop = 150,
                CanvasRelativeRight = 100,
                Height = 150,
                Width = 150,
                NoteText = "Dit is een test notitie",
                NoteTitle = "Test Notitie",
                NoteColor = "Green",
                NoteBorderColor = "Black"
            });

            MijnCollectie.Add(new clsNoteModel
            {
                NoteID = 3,
                CanvasTop = 300,
                CanvasRelativeRight = 100,
                Height = 150,
                Width = 150,
                NoteText = "Dit is een test notitie",
                NoteTitle = "Test Notitie",
                NoteColor = "Red",
                NoteBorderColor = "Black"
            });
        }
    }
}
