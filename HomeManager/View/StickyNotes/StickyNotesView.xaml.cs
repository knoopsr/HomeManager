using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HomeManager.DataService.StickyNotes;
using HomeManager.Model;
using HomeManager.Model.StickyNotes;

namespace HomeManager.View.StickyNotes
{
    /// <summary>
    /// Interaction logic for the StickyNotesView. This window allows users to manage and drag sticky notes within the application.
    /// </summary>
    public partial class StickyNotesView : Window
    {
        /// <summary>
        /// The main window of the application.
        /// </summary>
        MainWindow _mainWindow;

        /// <summary>
        /// The starting point for mouse drag operations.
        /// </summary>
        private Point _startPoint;

        /// <summary>
        /// The item being dragged in the list.
        /// </summary>
        private object _draggedItem;

        public StickyNotesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the overlay window when invoked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void CloseOverlay(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the drag-over event for the item container. This method sets the drag effect to "Move" during a drag operation.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The drag event arguments.</param>
        private void ListViewItem_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        /// <summary>
        /// Handles the drop event when an item is dropped into the <see cref="StickyNotesListView"/>. The method reorders the sticky notes
        /// and updates their positions accordingly.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The drag event arguments.</param>
        private void StickyNotesListView_Drop(object sender, DragEventArgs e)
        {
            if (_draggedItem == null) return;

            var collection = StickyNotesListView.ItemsSource as ObservableCollection<clsStickyNotesModel>;
            if (collection == null) return;

            var targetItem = ((FrameworkElement)e.OriginalSource).DataContext as clsStickyNotesModel;
            if (targetItem == null || targetItem == _draggedItem) return;

            int oldIndex = collection.IndexOf((clsStickyNotesModel)_draggedItem);
            int newIndex = collection.IndexOf(targetItem);

            if (oldIndex >= 0 && newIndex >= 0 && oldIndex != newIndex)
            {
                collection.Move(oldIndex, newIndex);

                // Update the position property for each item after the move
                for (int i = 0; i < collection.Count; i++)
                {
                    collection[i].Position = i;
                }
            }

            _draggedItem = null;
        }

        /// <summary>
        /// Handles the mouse move event for list view items. It detects whether the user has moved the mouse far enough to start a drag operation.
        /// If the drag threshold is met, the drag operation is initiated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListViewItem item = sender as ListViewItem;
                if (item != null && item.IsSelected)
                {
                    // Get current mouse position relative to ListView
                    Point currentPosition = e.GetPosition(StickyNotesListView);

                    // Calculate the distance moved
                    double dx = currentPosition.X - _startPoint.X;
                    double dy = currentPosition.Y - _startPoint.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    // Set a threshold before dragging starts
                    if (distance > SystemParameters.MinimumHorizontalDragDistance)
                    {
                        _draggedItem = item.DataContext;
                        DragDrop.DoDragDrop(item, _draggedItem, DragDropEffects.Move);
                    }
                }
            }
        }

        /// <summary>
        /// Captures the starting point of a mouse click, which is used to determine if the user has moved the mouse far enough to begin a drag operation.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The mouse button event arguments.</param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(StickyNotesListView); // Capture mouse position relative to ListView 
        }
    }
}