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
using HomeManager.DataService.Logging;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using HomeManager.View.Personen;
using HomeManager.View.StickyNotes;
using System.Windows.Forms;
using System.Drawing;
using HomeManager.Services;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Nugget package geïnstalleerd voor het selecteren van folders.(Microsoft-WindowsAPICodePack-Shell)
    /// Nugget package geïnstalleerd voor het gebruiken van api. (Newtonsoft.Json)
    /// Bram z'n using.System.Drawing in commentaar uit clsRTBLayout want zorgde voor veel conflicten.
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Singleton instance of the StickyNotes window.
        /// Only one instance is allowed at any given time during the application's lifetime.
        /// </summary>
        private static StickyNotesView stickyNotesView;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region METHODS
        /// <summary>
        /// Displays the StickyNotes window when the user clicks the appropriate menu button.
        /// If the window was previously closed, it will be re-instantiated.
        /// If it is hidden, it will be made visible again.
        /// Also logs the user's action to the database.
        /// </summary>
        /// <param name="sender">The control that triggered the event (e.g., menu button).</param>
        /// <param name="e">Routed event arguments.</param>
        private void Show_StickyNotes(object sender, RoutedEventArgs e)
        {
            if (stickyNotesView == null || !stickyNotesView.IsLoaded)
            {
                stickyNotesView = new StickyNotesView();
                clsWindowService.HandleWindowOverlay(stickyNotesView, this);
                stickyNotesView.Show();

                // Logging van deze actie naar de database
                clsButtonLoggingDataService MijnLoggingService = new clsButtonLoggingDataService();
                MijnLoggingService.Insert(new clsButtonLoggingModel()
                {
                    AccountId = clsLoginModel.Instance.AccountID,
                    ActionName = "MenuKnop",
                    ActionTarget = "Sticky Notes Window"
                });
            }
            else if (!stickyNotesView.IsVisible)
            {
                stickyNotesView.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// Triggered when the location of the MainWindow changes.
        /// Used to reposition the overlay window (StickyNotes) accordingly.
        /// </summary>
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            clsWindowService.HandleWindowOverlay(stickyNotesView, this);
        }

        /// <summary>
        /// Triggered when the size of the MainWindow changes.
        /// Updates the StickyNotes overlay position to remain aligned.
        /// </summary>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            clsWindowService.HandleWindowOverlay(stickyNotesView, this);
        }

        /// <summary>
        /// Triggered when the MainWindow state changes (e.g. minimized, maximized, restored).
        /// Ensures StickyNotes stays positioned relative to the MainWindow.
        /// </summary>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            clsWindowService.HandleWindowOverlay(stickyNotesView, this);
        }

        /// <summary>
        /// Triggered when the MainWindow is closing.
        /// Ensures the StickyNotes window is properly closed to release resources.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stickyNotesView.Close();
        }
        #endregion
    }
}