using HomeManager.Agenda.DataService;
using HomeManager.Agenda.Helpers;
using HomeManager.Common;
using HomeManager.Model.Agenda;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HomeManager.Agenda.ViewModel
{
    public class clsAgendaViewModel : clsCommonModelPropertiesBase
    {
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand SizeChangedCommand { get; set; }
        public ICommand MouseLeftUpAgendaItem { get; set; }
        public ICommand MouseEnterAgendaItem { get; set; }
        public ICommand MouseLeaveAgendaItem { get; set; }
        public ICommand ToggleAgendaItemCommand { get; }
        public ICommand cmdPrev { get; set; }
        public ICommand cmdToday { get; set; }
        public ICommand cmdNext { get; set; }
        public ICommand ResizeTopCommand { get; }
        public ICommand ResizeBottomCommand { get; }

        private readonly DispatcherTimer _timer;

        bool NewStatus = false;
        private string oudekleur;
        private DateOnly _selectedDate;
        private bool _isCurrentView = true;


        #region Collections

        clsAgendaItemsDataService MijnService;
        clsAgendaCategoryDataService MijnCategoryService;

        private ObservableCollection<clsLineModel> _uurlijn;
        public ObservableCollection<clsLineModel> Uurlijn
        {
            get { return _uurlijn; }
            set
            {
                _uurlijn = value;
                OnPropertyChanged();
            }
        }




        private ObservableCollection<clsLineModel> _horlines;
        public ObservableCollection<clsLineModel> HorLines
        {
            get { return _horlines; }
            set
            {
                _horlines = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<clsLineModel> _verlines;
        public ObservableCollection<clsLineModel> VerLines
        {
            get { return _verlines; }
            set
            {
                _verlines = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<clsTekstModel> _kopDagen;
        public ObservableCollection<clsTekstModel> KopDagen
        {
            get { return _kopDagen; }
            set
            {
                _kopDagen = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTekstModel> _uren;
        public ObservableCollection<clsTekstModel> Uren
        {
            get { return _uren; }
            set
            {
                _uren = value;
                OnPropertyChanged();
            }
        }




        private ObservableCollection<clsAgendaItemModel> _mijnCollectie;
        public ObservableCollection<clsAgendaItemModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<clsAgendaCategoryModel> _mijnCategoryCollectie;
        public ObservableCollection<clsAgendaCategoryModel> MijnCategoryCollectie
        {
            get { return _mijnCategoryCollectie; }
            set
            {
                _mijnCategoryCollectie = value;
                OnPropertyChanged();
            }
        }




        private clsAgendaItemModel _mijnSelectedItem;
        public clsAgendaItemModel MijnSelectedItem
        {
            get { return _mijnSelectedItem; }
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //OpslaanCommando();
                            //LoadData();
                        }
                    }
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Extra Properties

        private clsLineModel _currentTimeLine;
        public clsLineModel CurrentTimeLine
        {
            get => _currentTimeLine;
            set
            {
                _currentTimeLine = value;
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


        private bool _isAgendaItemVisible;
        public bool IsAgendaItemVisible
        {
            get { return _isAgendaItemVisible; }
            set
            {
                _isAgendaItemVisible = value;
                OnPropertyChanged();
            }
        }




        #endregion


        public clsAgendaViewModel()
        {

            _selectedDate = DateOnly.FromDateTime(System.DateTime.Now);
            MijnService = new clsAgendaItemsDataService();
            MijnCategoryService = new clsAgendaCategoryDataService();

            _mijnCollectie = new ObservableCollection<clsAgendaItemModel>();
            _horlines = new ObservableCollection<clsLineModel>();
            _verlines = new ObservableCollection<clsLineModel>();
            _kopDagen = new ObservableCollection<clsTekstModel>();
            _uren = new ObservableCollection<clsTekstModel>();
            _uurlijn = new ObservableCollection<clsLineModel>();


            cmdSave = new clsCustomCommand(Execute_cmdSave_Command, CanExecute_cmdSave_Command);
            cmdNew = new clsCustomCommand(Execute_cmdNew_Command, CanExecute_cmdNew_Command);
            cmdDelete = new clsCustomCommand(Execute_cmdDelete_Command, CanExecute_cmdDelete_Command);
            cmdCancel = new clsCustomCommand(Execute_cmdCancel_Command, CanExecute_cmdCancel_Command);
            cmdClose = new clsCustomCommand(Execute_cmdClose_Command, CanExecute_cmdClose_Command);
            SizeChangedCommand = new clsRelayCommand<object>(ExecuteSizeChangedCommand);
            MouseLeftUpAgendaItem = new clsRelayCommand<int>(ExecuteMouseLeftUpAgendaItem);
            MouseEnterAgendaItem = new clsRelayCommand<int>(ExecuteMouseEnterAgendaItem);
            MouseLeaveAgendaItem = new clsRelayCommand<int>(ExecuteMouseLeaveAgendaItem);
            ToggleAgendaItemCommand = new clsCustomCommand(ToggleAgendaItem, canExecute_ToggleAgendaItem);

            cmdNext = new clsCustomCommand(Execute_cmdNext_Command, CanExecute_cmdNext_Command);
            cmdPrev = new clsCustomCommand(Execute_cmdPrev_Command, CanExecute_cmdPrev_Command);
            cmdToday = new clsCustomCommand(Execute_cmdToday_Command, CanExecute_cmdToday_Command);
            ResizeTopCommand = new clsRelayCommand<int>(OnResizeTop);
            ResizeBottomCommand = new clsRelayCommand<int>(OnResizeBottom);




            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += UpdateCurrentTimeLine;
            _timer.Start();


            SETLAYOUT();
            LoadData();
            MijnCategoryCollectie = MijnCategoryService.GetAll();

        }

        private double? _oldMouseY = null;

        private void OnResizeTop(int ID)
        {
            var item = MijnCollectie.FirstOrDefault(a => a.AgendaID == ID);
            if (item != null)
            {
                // Huidige muispositie ophalen
                var mousePositionY = Mouse.GetPosition(Application.Current.MainWindow).Y;

                // Controleer of dit de eerste aanroep is
                if (_oldMouseY == null)
                {
                    // Stel de initiële waarde in en beëindig de methode
                    _oldMouseY = mousePositionY;
                    return;
                }

                // Bereken het verschil
                double verschil = _oldMouseY.Value - mousePositionY;

                // Update `_oldMouseY` naar de huidige muispositie
                _oldMouseY = mousePositionY;

                if (verschil < 60)
                {

           

                // Pas de hoogte en top-positie aan
                item.CanvasTop -= verschil;  // Verplaats het item
                item.Height += verschil;     // Pas de hoogte aan

                // Optioneel: Debug-informatie bijwerken
                item.AgendaDescription = $"Verschil: {verschil:F2}\nOude Y: {_oldMouseY:F2}\nNieuwe Y: {mousePositionY:F2}";
                }
            }
        }


        private void OnResizeBottom(int ID)
        {
            var item = MijnCollectie.FirstOrDefault(a => a.AgendaID == ID);
            if (item != null)
            {
                // Pas de grootte aan zoals gewenst
                item.Height += 1; // Pas dit aan afhankelijk van je logica
            }
        }


        private void LoadData()
        {
            MijnCollectie = MijnService.GetWeek(_selectedDate);
            SetItemPositions();
            SetKopdagen();
            UpdateCurrentTimeLine(null, null);
        }
        private void SETLAYOUT()
        {

            for (int i = 0;
                i < 48;
                i++)
            {
                double x1 = 50;
                if (i % 2 == 0)
                {
                    x1 = -2;
                }
                HorLines.Add(new clsLineModel
                {
                    X1 = x1,
                    Y1 = 20 + (i * 20),
                    X2 = 790,
                    Y2 = 20 + (i * 20),
                    Color = "Black",
                    Thickness = 0.1
                });
            }





            VerLines = new ObservableCollection<clsLineModel>
            {
                 new clsLineModel { X1 = 30, Y1 = -2, X2 = 30, Y2 = 540, Color = "Black", Thickness = 1 },

                 new clsLineModel { X1 = 130, Y1 = -2, X2 = 130, Y2 = 540, Color = "Black", Thickness = 0.5 },

                 new clsLineModel { X1 = 230, Y1 = -2, X2 = 230, Y2 = 540, Color = "Black", Thickness = 0.5 },

                 new clsLineModel { X1 = 330, Y1 = -2, X2 = 330, Y2 = 540, Color = "Black", Thickness = 0.5 },

                 new clsLineModel { X1 = 430, Y1 = -2, X2 = 430, Y2 = 540, Color = "Black", Thickness = 0.5 },

                 new clsLineModel { X1 = 530, Y1 = -2, X2 = 530, Y2 = 540, Color = "Black", Thickness = 0.5 },

                 new clsLineModel { X1 = 630, Y1 = -2, X2 = 630, Y2 = 540, Color = "Black", Thickness = 0.5 }
            };

            KopDagen = new ObservableCollection<clsTekstModel>
            {
                new clsTekstModel { Tekst = "Maandag", Top = -2, Left = 50, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Dinsdag", Top = -2, Left = 150, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Woensdag", Top = -2, Left = 250, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Donderdag", Top = -2, Left = 350, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Vrijdag", Top = -2, Left = 450, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Zaterdag", Top = -2, Left = 550, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "Zondag", Top = -2, Left = 650, Width = 80, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" }
            };

            Uren = new ObservableCollection<clsTekstModel>
            {
                new clsTekstModel { Tekst = "00:00", Top = 20, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "01:00", Top = 40, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "02:00", Top = 60, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "03:00", Top = 80, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "04:00", Top = 100, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "05:00", Top = 120, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                new clsTekstModel { Tekst = "06:00", Top = 140, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "07:00", Top = 160, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "08:00", Top = 180, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "09:00", Top = 200, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "10:00", Top = 220, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "11:00", Top = 240, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "12:00", Top = 260, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "13:00", Top = 280, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "14:00", Top = 300, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "15:00", Top = 320, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "16:00", Top = 340, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "17:00", Top = 360, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "18:00", Top = 380, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "19:00", Top = 400, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "20:00", Top = 420, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "21:00", Top = 440, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "22:00", Top = 460, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" },
                    new clsTekstModel { Tekst = "23:00", Top = 480, Left = 10, Width = 40, Height = 20, FontSize = 15, FontFamily = "Arial", Color = "Black" }
            };
        
        }

        private void OpslaanCommando()
        {
            if (_mijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        MijnSelectedItem = null;
                        IsAgendaItemVisible = false;
                    
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error??");
                    }
                }
                else
                {
                    if (MijnService.Update(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        //MijnSelectedItem.MijnSelectedIndex = 0;
            
                        NewStatus = false;
                        LoadData();
                        MijnSelectedItem = null;
                        IsAgendaItemVisible = false;

                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        private void UpdateCurrentTimeLine(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double pixelsPerHour = 49; // Het aantal pixels per uur op het canvas
            double topPosition = 30 + now.TimeOfDay.TotalHours * pixelsPerHour;
            double pixelsPerDay = ((CanvasWidth - 50) / 7);

            if (pixelsPerDay < 0)
            {
                pixelsPerDay = 0;
            }

            double hoursAfterMidnight = now.TimeOfDay.TotalHours;
                double offset = 0;
                if (hoursAfterMidnight >= 4)
                {
                    offset = -1;
                }
                if (hoursAfterMidnight >= 8)
                {
                    offset = -2;
                }
                if (hoursAfterMidnight >= 12)
                {
                    offset = -3;
                }
                if (hoursAfterMidnight >= 16)
                {
                    offset = -4;
                }
                if (hoursAfterMidnight >= 20)
                {
                    offset = -5;
                }

            // Bereken de Canvas.Top-positie gebaseerd op het aantal uren en minuten
            topPosition = Math.Round((hoursAfterMidnight * pixelsPerHour), 0) + 30 + offset;



            int dayOfWeek = (int)now.DayOfWeek ; 
            double posdag = 0;
            if (dayOfWeek == 1)
            {
                posdag = 50;
            }
            if (dayOfWeek == 2)
            {
                posdag = 50 + pixelsPerDay;
            }
            if (dayOfWeek == 3)
            {
                posdag = 50 + (2 * pixelsPerDay);
            }
            if (dayOfWeek == 4)
            {
                posdag = 50 + (3 * pixelsPerDay);
            }
            if (dayOfWeek == 5)
            {
                posdag = 50 + (4 * pixelsPerDay);
            }
            if (dayOfWeek == 6)
            {
                posdag = 50 + (5 * pixelsPerDay);
            }
            if (dayOfWeek == 0)
            {
                posdag = 50 + (6 * pixelsPerDay);
            }

            Uurlijn.Clear();
            double br = Math.Round(pixelsPerDay, 0);
            bool isInCurrentWeek = IsDateInCurrentWeek();
          
            if (isInCurrentWeek)
            {        
                Uurlijn = new ObservableCollection<clsLineModel>
                {
                    new clsLineModel { X1 = Math.Round(posdag,0), Y1 = topPosition, X2 = Math.Round(posdag,0) + br , Y2 = topPosition, Color = "Red", Thickness = 5 },
                };
            }   
        }



        #region Functies Voor Layout


        private bool IsDateInCurrentWeek()
        {
            // Bepaal de huidige datum
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Bepaal de start en einddatum van de huidige week (begin op maandag)
            int daysUntilMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysUntilMonday < 0)
            {
                daysUntilMonday += 7; // Zondag moet terugkijken naar maandag
            }

            DateOnly weekStart = today.AddDays(-daysUntilMonday);
            DateOnly weekEnd = weekStart.AddDays(6);

            // Controleer of _selectedDate binnen de week valt
            return _selectedDate >= weekStart && _selectedDate <= weekEnd;
        }


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

        private void SetHorLines(Double breedte)
        {
            double tussenhoogte = (CanvasHeight - 30) / 48;
            double begin = 30;

            int nr = 0;


            foreach (clsLineModel line in HorLines)
            {
                if (nr == 0)
                {
                    line.Y1 = begin;
                    line.Y2 = begin;
                    line.X2 = breedte + 2;

                }
                else
                {
                    line.Y1 = begin + Math.Round((nr * tussenhoogte), 0);
                    line.Y2 = begin + Math.Round((nr * tussenhoogte), 0);
                    line.X2 = breedte + 2;

                }
                nr = nr + 1;

            }
        }

        private void SetVerLines(double hoogte)
        {
            double begin = 50;
            double tussenbreedte = (CanvasWidth - begin) / 7;

            int nr = 0;

            foreach (clsLineModel line in VerLines)
            {
                if (nr == 0)
                {
                    line.X1 = begin;
                    line.X2 = begin;
                    line.Y2 = hoogte + 2;
                }
                else
                {
                    line.X1 = begin + Math.Round((nr * tussenbreedte), 0);
                    line.X2 = begin + Math.Round((nr * tussenbreedte), 0);
                    line.Y2 = hoogte + 2;
                }
                nr = nr + 1;
            }
        }

        private void SetKopdagen()
        {
            double tussenbreedte = (CanvasWidth - 50) / 7;
            double begin =  Math.Round((tussenbreedte / 2), 0)-10;
            int nr = 0;

            // Vind de maandag van de week voor de geselecteerde datum
            DateOnly startOfWeek = _selectedDate.AddDays(-(int)_selectedDate.DayOfWeek + (int)DayOfWeek.Monday);

            foreach (clsTekstModel tekst in KopDagen)
            {
                if (nr == 0)
                {
                    tekst.Left = begin;       
                }
                else
                {
                    tekst.Left = begin + Math.Round((nr * tussenbreedte), 0);     
                }
                tekst.Width = Math.Max(Math.Round(tussenbreedte, 0), 20);

                // Voeg de datum aan de dag toe in het format "Maandag 11-11-2024"
                DateOnly dagVanDeWeek = startOfWeek.AddDays(nr);
                tekst.Tekst = $"{dagVanDeWeek:dddd dd-MM}";

                nr = nr + 1;
                tekst.Top = 4;
                tekst.FontSize = 17;
            }
        }

        private void SetUren()
        {
            double tussenhoogte = (CanvasHeight - 30) / 48;
            double begin = 30;
            int nr = 0;

            foreach (clsTekstModel tekst in Uren)
            {
                if (nr == 0)
                {
                    tekst.Top = begin;
                    tekst.Height = Math.Round(tussenhoogte, 0);
                }
                else
                {
                    tekst.Top = begin + Math.Round((nr * (tussenhoogte * 2)), 0);
                    tekst.Height = Math.Round(tussenhoogte, 0);
                }
                nr = nr + 1;
                tekst.Left = 4;
                tekst.FontSize = 14;
            }
        }

        private void SetItemPositions()
        {
            // Stel een hoogtefactor in voor elk uur, bijvoorbeeld 60 pixels per uur
            double pixelsPerHour = 49;
            double pixelsPerDay = ((CanvasWidth - 50) / 7);

            if (pixelsPerDay < 0)
            {
                pixelsPerDay = 0;
            }

            foreach (var item in MijnCollectie)
            {
                // Bereken hoeveel uren en minuten na middernacht het item begint
                double hoursAfterMidnight = item.AgendaBeginTime.TotalHours;
                double offset = 0;
                if (hoursAfterMidnight >= 4)
                {
                    offset = -1;
                }
                if (hoursAfterMidnight >= 8)
                {
                    offset = -2;
                }
                if (hoursAfterMidnight >= 12)
                {
                    offset = -3;
                }
                if (hoursAfterMidnight >= 16)
                {
                    offset = -4;
                }
                if (hoursAfterMidnight >= 20)
                {
                    offset = -5;
                }

                // Bereken de Canvas.Top-positie gebaseerd op het aantal uren en minuten
                item.CanvasTop = Math.Round((hoursAfterMidnight * pixelsPerHour), 0) + 30 + offset;

                // Stel de hoogte van het item in op basis van de duur
                double durationInHours = (item.AgendaEndTime - item.AgendaBeginTime).TotalHours;
                item.Height = Math.Abs(durationInHours) * pixelsPerHour;


                // Bereken de horizontale positie (Canvas.Left) op basis van de dag van de week
                int dayOfWeek = (int)item.AgendaDate.DayOfWeek; // 0 = zondag, 1 = maandag, enz.
                double posdag = 0;
                if (dayOfWeek == 1)
                {
                    posdag = 50;
                }
                if (dayOfWeek == 2)
                {
                    posdag = 50 + pixelsPerDay;
                }
                if (dayOfWeek == 3)
                {
                    posdag = 50 + (2 * pixelsPerDay);
                }
                if (dayOfWeek == 4)
                {
                    posdag = 50 + (3 * pixelsPerDay);
                }
                if (dayOfWeek == 5)
                {
                    posdag = 50 + (4 * pixelsPerDay);
                }
                if (dayOfWeek == 6)
                {
                    posdag = 50 + (5 * pixelsPerDay);
                }
                if (dayOfWeek == 0)
                {
                    posdag = 50 + (6 * pixelsPerDay);
                }

                // Verplaats maandag naar de eerste kolom en zondag naar de laatste
                item.CanvasLeft = posdag;
                item.Width = Math.Max(Math.Round(pixelsPerDay, 0) - 4, 20);   
            }
        }


        #endregion


        #region Commands

        private bool CanExecute_cmdToday_Command(object? obj)
        {
            return true;
        }

        private void Execute_cmdToday_Command(object? obj)
        {
            _selectedDate = DateOnly.FromDateTime(System.DateTime.Now);
            IsAgendaItemVisible = false;
            MijnSelectedItem = null;
            LoadData();
        }

        private bool CanExecute_cmdPrev_Command(object? obj)
        {
            return true;
        }

        private void Execute_cmdPrev_Command(object? obj)
        {
            IsAgendaItemVisible = false;
            MijnSelectedItem = null;
            _selectedDate = _selectedDate.AddDays(-7);
            LoadData();
        }

        private bool CanExecute_cmdNext_Command(object? obj)
        {
     return true;
        }

        private void Execute_cmdNext_Command(object? obj)
        {
            IsAgendaItemVisible = false;
            MijnSelectedItem = null;
            _selectedDate = _selectedDate.AddDays(7);
            LoadData();
        }

        private void ExecuteSizeChangedCommand(object size)
        {
            if (size is Canvas canvasSize)
            {
                // Gebruik canvasSize.Width en canvasSize.Height
                CanvasWidth = canvasSize.ActualWidth;
                CanvasHeight = canvasSize.ActualHeight;
                SetHorLines(canvasSize.ActualWidth);
                SetVerLines(canvasSize.ActualHeight);
                SetKopdagen();
                SetUren();
                SetItemPositions();
                UpdateCurrentTimeLine(null,null);
            }
        }
        private void ToggleAgendaItem(object? obj)
        {
            IsAgendaItemVisible = !IsAgendaItemVisible;
        }

        private bool canExecute_ToggleAgendaItem(object? obj)
        {
            return true;
        }

        private void ExecuteMouseLeftUpAgendaItem(int agendaID)
        {
            IsAgendaItemVisible = true;
            MijnSelectedItem = MijnService.GetById(agendaID);
            MijnSelectedItem.IsDirty = false;

        }

        private void ExecuteMouseLeaveAgendaItem(int ID)
        {
            var item = MijnCollectie.FirstOrDefault(a => a.AgendaID == ID);
            if (item != null)
            {
                item.AgendaColor = oudekleur;
            }
        }

        private void ExecuteMouseEnterAgendaItem(int ID)
        {
            var item = MijnCollectie.FirstOrDefault(a => a.AgendaID == ID);
            if (item != null)
            {
                oudekleur = item.AgendaColor;
                item.AgendaColor = LighterColor(oudekleur, 0.3);
            }
        }

        private void Execute_cmdCancel_Command(object? obj)
        {
     
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem = null;
                
            }


            IsAgendaItemVisible = false;
            NewStatus = false;
        }

        private void Execute_cmdClose_Command(object? obj)
        {

            clsMessenger.Default.Send<Messages.clsCloseUsercontrolAgenda>(new Messages.clsCloseUsercontrolAgenda());

        }

        private bool CanExecute_cmdClose_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_cmdCancel_Command(object? obj)
        {     
            if (IsAgendaItemVisible)
            {
                return true;
            }
            else
            {
                return false;
            }        
        }

        private bool CanExecute_cmdDelete_Command(object? obj)
        {
            if (MijnSelectedItem == null)
            {
                return NewStatus;

            }
            else
            {
                return true;
            }



       
        }

        private void Execute_cmdDelete_Command(object? obj)
        {
            if (MessageBox.Show(
            "Wil je " + MijnSelectedItem + " verwijderen?",
            "Verwijderen?",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        NewStatus = false;
                        LoadData();
                        MijnSelectedItem = null;
                        IsAgendaItemVisible = false;
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
            }
        }

        private bool CanExecute_cmdNew_Command(object? obj)
        {
            if (NewStatus)
            {
                return false;
            } else
            {
                return true;
            }

        }

        private void Execute_cmdNew_Command(object? obj)
        {
            clsAgendaItemModel _itemToInsert = new clsAgendaItemModel()
            {
                AgendaID = 0,
                AgendaTitle = "Nieuw item",
                AgendaDescription = "Omschrijving",
                AgendaCategoryID = 1,
                AgendaDate = System.DateTime.Now,
                AgendaBeginTime = new System.TimeSpan(8, 0, 0),
                AgendaEndTime = new System.TimeSpan(9, 0, 0),
                AgendaBorderColor = "Black",
                AgendaColor = "LightGreen",
            };
            MijnSelectedItem = _itemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            IsFocusedAfterNew = true;
            NewStatus = true;
            IsAgendaItemVisible = true;
        }

        private bool CanExecute_cmdSave_Command(object? obj)
        {
            if (MijnSelectedItem != null &&
             MijnSelectedItem.Error == null &&
             MijnSelectedItem.IsDirty == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute_cmdSave_Command(object? obj)
        {
            OpslaanCommando();
            SetItemPositions();
        }
        #endregion
    }

}
