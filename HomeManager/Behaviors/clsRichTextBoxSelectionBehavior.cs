using HomeManager.Helpers;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
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
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!Layout.SelectionChangedIsEnabled)
            {
                return;
            }

            if (sender is RichTextBox rtb)
            {
                TextPointer caretPosition = rtb.Selection.Start;

                // ✅ Find the Paragraph containing the selection
                var paragraph = caretPosition.Paragraph;
                if (paragraph == null) return;

                // ✅ Loop through all inlines in the paragraph
                foreach (Inline inline in paragraph.Inlines)
                {
                    if (inline is InlineUIContainer container && container.Child is Image image)
                    {
                        // ✅ Image found! Apply selection effects
                        //MessageBox.Show("picture found");
                        AttachResizeHandle(image);
                        return;
                    }
                }
            }

            if (Layout != null)
            {
                TextRange range = AssociatedObject.Selection;
                Layout.UpdateLayoutFromSelection(range); // Update the layout from selection
                
            }
        }

        private void CreateThumb()
        {

        }

        private static void AttachResizeHandle(Image image)
        {
            if (image.Parent is Grid) return; // Prevent multiple wrappers

            var container = image.Parent as InlineUIContainer;
            if (container == null) return;

            
            

            // ✅ Create a dashed border for selection
            var selectionBorder = new Border
            {
                BorderBrush = Brushes.Blue,  // Blue selection border
                BorderThickness = new Thickness(2),
                Margin = new Thickness(-2),  // Align border with the image
                SnapsToDevicePixels = true
            };

            // ✅ Create a Grid to hold the Image and Selection Border
            var grid = new Grid();
            grid.Children.Add(selectionBorder); // Selection border behind
            

            // ✅ Ensure the border resizes dynamically with the image
            image.SizeChanged += (s, e) =>
            {
                selectionBorder.Width = image.Width;
                selectionBorder.Height = image.Height;
            };

            // ✅ Add resize handles
            AddResizeHandles(grid, image, selectionBorder);

            // ✅ Replace the original Image with the Grid
            Window window = new Window();
            window.Content = grid;
            window.Width = image.Width;
            window.Height = image.Height;
            window.ShowDialog();
        }

        private static void AddResizeHandles(Grid grid, Image image, Border selectionBorder)
        {
            var positions = new (VerticalAlignment, HorizontalAlignment)[]
            {
                (VerticalAlignment.Top, HorizontalAlignment.Left),     // Top-left
                (VerticalAlignment.Top, HorizontalAlignment.Right),    // Top-right
                (VerticalAlignment.Bottom, HorizontalAlignment.Left),  // Bottom-left
                (VerticalAlignment.Bottom, HorizontalAlignment.Right)  // Bottom-right
            };

            foreach (var (vertAlign, horAlign) in positions)
            {
                var thumb = new Thumb
                {
                    Width = 8,
                    Height = 8,
                    Background = Brushes.Red,
                    Opacity = 0.8, // Slightly transparent
                    VerticalAlignment = vertAlign,
                    HorizontalAlignment = horAlign,
                    Cursor = Cursors.SizeNWSE
                };

                // ✅ Handle resizing
                thumb.DragDelta += (s, e) =>
                {
                    double newWidth = Math.Max(10, image.Width + e.HorizontalChange);
                    double newHeight = Math.Max(10, image.Height + e.VerticalChange);

                    image.Width = newWidth;
                    image.Height = newHeight;

                    selectionBorder.Width = newWidth;
                    selectionBorder.Height = newHeight;
                };

                grid.Children.Add(thumb); // ✅ Add resize handle to the grid
            }
        }
    }
}
