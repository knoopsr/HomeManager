using HomeManager.Helpers;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeManager.Behaviors
{
    public class clsRichTextBoxSelectionBehavior : Behavior<RichTextBox>
    {
        // Register a DependencyProperty for the Layout property
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", 
                                        typeof(clsRTBLayout), 
                                        typeof(clsRichTextBoxSelectionBehavior), 
                                        new PropertyMetadata(null));

        public clsRTBLayout Layout
        {
            get { return (clsRTBLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
            AssociatedObject.PreviewMouseDown += OnPreviewMouseDown;  // Use PreviewMouseDown instead
        }
        

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            AssociatedObject.PreviewMouseDown -= OnPreviewMouseDown;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!Layout.SelectionChangedIsEnabled)
            {
                return;
            }

            if (Layout != null)
            {
                TextRange range = AssociatedObject.Selection;
                Layout.UpdateLayoutFromSelection(range); // Update the layout from selection
                
            }
        }

        // MouseDown event handler to detect image click
        // PreviewMouseDown event handler to detect image click
        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is RichTextBox rtb)
            {
                // ✅ Get the clicked point
                Point clickPosition = e.GetPosition(rtb);

                // ✅ Perform a hit test to find the image under the clicked position
                HitTestResult result = VisualTreeHelper.HitTest(rtb, clickPosition);

                if (result != null && result.VisualHit is Image image)
                {
                    // ✅ Image was clicked! Apply the resize handle
                    AttachResizeHandle(image);
                    e.Handled = true; // Mark the event as handled
                    return;
                }

                // 🔍 Get the TextPointer at clicked point
                TextPointer pointer = rtb.GetPositionFromPoint(clickPosition, true);
                if (pointer != null)
                {
                    // 🧠 Walk up logical tree to find a Hyperlink
                    var parent = pointer.Parent;
                    while (parent != null && !(parent is Hyperlink))
                    {
                        parent = LogicalTreeHelper.GetParent(parent);
                    }

                    if (parent is Hyperlink hyperlink && hyperlink.NavigateUri != null)
                    {
                        System.Diagnostics.Process.Start(new ProcessStartInfo(hyperlink.NavigateUri.AbsoluteUri)
                        {
                            UseShellExecute = true
                        });
                        e.Handled = true;
                        return;
                    }
                }
            }
        }


        private void CreateThumb()
        {

        }


        private static void AttachResizeHandle(Image image)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(image);
            if (adornerLayer != null)
            {
                var resizeAdorner = new clsDagboekResizeAdorner(image);
                adornerLayer.Add(resizeAdorner);
            }


            //if (image.Parent is Grid) return; // Prevent multiple wrappers

            ////var container = image.Parent as InlineUIContainer;
            ////if (container == null) return;




            //// ✅ Create a dashed border for selection
            //var selectionBorder = new Border
            //{
            //    BorderBrush = Brushes.Blue,  // Blue selection border
            //    BorderThickness = new Thickness(2),
            //    Margin = new Thickness(-2),  // Align border with the image
            //    SnapsToDevicePixels = true
            //};

            //// ✅ Create a Grid to hold the Image and Selection Border
            //var grid = new Grid();
            //grid.Children.Add(selectionBorder); // Selection border behind
            ////grid.Children.Add(image);

            //// ✅ Ensure the border resizes dynamically with the image
            //image.SizeChanged += (s, e) =>
            //{
            //    selectionBorder.Width = image.Width;
            //    selectionBorder.Height = image.Height;
            //};

            //// ✅ Add resize handles
            //AddResizeHandles(grid, image, selectionBorder);

            //// ✅ Replace the original Image with the Grid
            //Window window = new Window();
            //window.Content = grid;
            //window.Width = image.Width;
            //window.Height = image.Height;
            //window.ShowDialog();

            ////MessageBox.Show(image.Parent.GetType().Name);
            ///


        }

        //private static void AddResizeHandles(Grid grid, Image image, Border selectionBorder)
        //{
        //    var positions = new (VerticalAlignment, HorizontalAlignment)[]
        //    {
        //        (VerticalAlignment.Top, HorizontalAlignment.Left),     // Top-left
        //        (VerticalAlignment.Top, HorizontalAlignment.Right),    // Top-right
        //        (VerticalAlignment.Bottom, HorizontalAlignment.Left),  // Bottom-left
        //        (VerticalAlignment.Bottom, HorizontalAlignment.Right)  // Bottom-right
        //    };

        //    foreach (var (vertAlign, horAlign) in positions)
        //    {
        //        var thumb = new Thumb
        //        {
        //            Width = 8,
        //            Height = 8,
        //            Background = Brushes.Red,
        //            Opacity = 0.8, // Slightly transparent
        //            VerticalAlignment = vertAlign,
        //            HorizontalAlignment = horAlign,
        //            Cursor = Cursors.SizeNWSE
        //        };

        //        // ✅ Handle resizing
        //        thumb.DragDelta += (s, e) =>
        //        {
        //            double newWidth = Math.Max(10, image.Width + e.HorizontalChange);
        //            double newHeight = Math.Max(10, image.Height + e.VerticalChange);

        //            image.Width = newWidth;
        //            image.Height = newHeight;

        //            selectionBorder.Width = newWidth;
        //            selectionBorder.Height = newHeight;
        //        };

        //        grid.Children.Add(thumb); // ✅ Add resize handle to the grid
        //    }
        //}
    }
}
