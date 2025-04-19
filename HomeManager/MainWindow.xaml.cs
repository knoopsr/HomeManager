using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Wordprocessing;
using HomeManager.DataService.Logging;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using HomeManager.View.Personen;
using HomeManager.View.StickyNotes;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /// Nugget package geïnstalleerd voor het selecteren van folders.(Microsoft-WindowsAPICodePack-Shell)
    /// Nugget package geïnstalleerd voor het gebruiken van api. (Newtonsoft.Json)
    /// Bram z'n using.System.Drawing in commentaar uit clsRTBLayout want zorgde voor veel conflicten.
    /// 
    public partial class MainWindow : Window
    {
        private static StickyNotesView stickyNotesView;

        public MainWindow()
        {
            InitializeComponent();

            // Register the LocationChanged event to update the sticky note position
            this.LocationChanged += MainWindow_LocationChanged;
        }

        /// <summary>
        /// Updates the overlayWindow position
        /// </summary>
        private void UpdateOverlayPosition(Window overlayWindow, Window ownerWindow)
        {
            // Sets the width and the height
            overlayWindow.Width = ownerWindow.Width * 0.40f;
            overlayWindow.Height = ownerWindow.Height;

            // Positions the overlayWindow to the right of the ownerWindow
            overlayWindow.Left = ownerWindow.Left + ownerWindow.Width - overlayWindow.Width;
            overlayWindow.Top = ownerWindow.Top;
        }

        private void Show_StickyNotes(object sender, RoutedEventArgs e)
        {
            if (stickyNotesView == null || !stickyNotesView.IsVisible)
            {
                stickyNotesView = new StickyNotesView();
                UpdateOverlayPosition(stickyNotesView, this);
                stickyNotesView.Show();

                clsButtonLoggingDataService MijnLoggingService = new clsButtonLoggingDataService();
                MijnLoggingService.Insert(new clsButtonLoggingModel()
                {
                    AccountId = clsLoginModel.Instance.AccountID,
                    ActionName = "MenuKnop",
                    ActionTarget = "Sticky Notes Window"
                });
            }
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this == null) stickyNotesView.Close();

            if (stickyNotesView != null)
            {
                UpdateOverlayPosition(stickyNotesView, this);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (stickyNotesView != null)
            {
                UpdateOverlayPosition(stickyNotesView, this);
            }
        }
    }
}