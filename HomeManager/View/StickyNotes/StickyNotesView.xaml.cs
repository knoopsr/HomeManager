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
using HomeManager.Model;
using HomeManager.Model.StickyNotes;

namespace HomeManager.View.StickyNotes
{
    /// <summary>
    /// Interaction logic for StickyNotesView.xaml
    /// </summary>
    public partial class StickyNotesView : Window
    {
        MainWindow _mainWindow;
        private Point _startPoint;
        private object _draggedItem;

        public StickyNotesView()
        {
            InitializeComponent();
        }

        private void CloseOverlay(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dragging over the ITEM CONTAINER, seems to do nothing special
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        /// <summary>
        /// Dropping into the listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StickyNotesListView_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drop event triggered");

            if (_draggedItem == null)
            {
                Debug.WriteLine("Dragged item is null");
                return;
            }

            var collection = StickyNotesListView.ItemsSource as ObservableCollection<clsStickyNotesModel>;
            if (collection == null)
            {
                Debug.WriteLine("ItemsSource is not an ObservableCollection<clsStickyNotesModel>");
                return;
            }

            var targetItem = ((FrameworkElement)e.OriginalSource).DataContext as clsStickyNotesModel;
            if (targetItem == null || targetItem == _draggedItem)
            {
                Debug.WriteLine("Target item is null or same as dragged item");
                return;
            }

            int oldIndex = collection.IndexOf((clsStickyNotesModel)_draggedItem);
            int newIndex = collection.IndexOf(targetItem);

            if (oldIndex >= 0 && newIndex >= 0 && oldIndex != newIndex)
            {
                Debug.WriteLine($"Moving item from index {oldIndex} to {newIndex}");
                collection.Move(oldIndex, newIndex);
            }

            _draggedItem = null;
        }

        /// <summary>
        /// Move the listViewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Important to make the usercontrol "usable"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(StickyNotesListView); // Capture mouse position relative to ListView 
        }
    }
}