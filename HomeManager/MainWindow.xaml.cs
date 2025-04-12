﻿using System.Text;
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
using HomeManager.View.Personen;
using HomeManager.View.StickyNotes;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            }
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
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