using HomeManager.Helpers;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace HomeManager.Behaviors
{
    public class ImageResizeBehavior : Behavior<RichTextBox>
    {
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", typeof(clsRTBLayout), typeof(ImageResizeBehavior), new PropertyMetadata(null));

        public clsRTBLayout Layout
        {
            get { return (clsRTBLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        private Image selectedImage;
        private Thumb resizeHandleTopLeft, resizeHandleTopRight, resizeHandleBottomLeft, resizeHandleBottomRight;
        private Canvas handleContainer;

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
            if (sender is RichTextBox rtb)
            {
                TextPointer caretPosition = rtb.Selection.Start;
                var paragraph = caretPosition.Paragraph;

                // Loop through inlines to find the selected image
                foreach (Inline inline in paragraph.Inlines)
                {
                    if (inline is InlineUIContainer container && container.Child is Image image)
                    {
                        // Image selected
                        selectedImage = image;
                        AttachResizeHandles(image, rtb);
                        return;
                    }
                }

                // No image selected, remove resize handles if any
                RemoveResizeHandles(rtb);
            }
        }

        private void AttachResizeHandles(Image image, RichTextBox rtb)
        {
            // Ensure a container exists for the resize handles
            if (handleContainer == null)
            {
                handleContainer = new Canvas();
                var parent = VisualTreeHelper.GetParent(rtb) as Panel;
                parent?.Children.Add(handleContainer);  // Add Canvas to the RichTextBox's parent
            }

            // Create Thumb controls for resize handles
            resizeHandleTopLeft = CreateResizeHandle(ResizeHandleTopLeft_DragDelta);
            resizeHandleTopRight = CreateResizeHandle(ResizeHandleTopRight_DragDelta);
            resizeHandleBottomLeft = CreateResizeHandle(ResizeHandleBottomLeft_DragDelta);
            resizeHandleBottomRight = CreateResizeHandle(ResizeHandleBottomRight_DragDelta);

            // Create a container for the resize handles (Canvas or similar)
            handleContainer.Children.Clear();
            handleContainer.Children.Add(resizeHandleTopLeft);
            handleContainer.Children.Add(resizeHandleTopRight);
            handleContainer.Children.Add(resizeHandleBottomLeft);
            handleContainer.Children.Add(resizeHandleBottomRight);


            // Position the resize handles around the image
            PositionResizeHandles(image, resizeHandleTopLeft, resizeHandleTopRight, resizeHandleBottomLeft, resizeHandleBottomRight);
        }

        private void RemoveResizeHandles(RichTextBox rtb)
        {
            // Remove the resize handles from the container
            if (handleContainer != null)
            {
                handleContainer.Children.Clear();
            }
        }

        private static Thumb CreateResizeHandle(DragDeltaEventHandler dragDeltaHandler)
        {
            var thumb = new Thumb
            {
                Width = 10,
                Height = 10,
                Background = Brushes.Black
            };
            thumb.DragDelta += dragDeltaHandler;
            return thumb;
        }

        private static void ResizeHandleTopLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeImage(sender, e, -1, -1); // Resize image up and left
        }

        private static void ResizeHandleTopRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeImage(sender, e, 1, -1); // Resize image up and right
        }

        private static void ResizeHandleBottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeImage(sender, e, -1, 1); // Resize image down and left
        }

        private static void ResizeHandleBottomRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeImage(sender, e, 1, 1); // Resize image down and right
        }

        private static void ResizeImage(object sender, DragDeltaEventArgs e, double xMultiplier, double yMultiplier)
        {
            if (sender is Thumb thumb && thumb.TemplatedParent is RichTextBox rtb)
            {
                var data = rtb.Tag as dynamic;
                var image = data?.Image;
                if (image != null)
                {
                    // Resize the image
                    image.Width += e.HorizontalChange * xMultiplier;
                    image.Height += e.VerticalChange * yMultiplier;

                    // Ensure the image doesn't shrink below a minimum size
                    if (image.Width < 50) image.Width = 50;
                    if (image.Height < 50) image.Height = 50;

                    // Reposition the resize handles based on the new image size
                    PositionResizeHandles(image, data.Handles[0], data.Handles[1], data.Handles[2], data.Handles[3]);
                }
            }
        }

        private static void PositionResizeHandles(Image image, Thumb topLeft, Thumb topRight, Thumb bottomLeft, Thumb bottomRight)
        {
            // Position the resize handles around the image
            var x = Canvas.GetLeft(image);
            var y = Canvas.GetTop(image);

            // Position handles based on the image size and position
            Canvas.SetLeft(topLeft, x - 5);
            Canvas.SetTop(topLeft, y - 5);

            Canvas.SetLeft(topRight, x + image.Width - 5);
            Canvas.SetTop(topRight, y - 5);

            Canvas.SetLeft(bottomLeft, x - 5);
            Canvas.SetTop(bottomLeft, y + image.Height - 5);

            Canvas.SetLeft(bottomRight, x + image.Width - 5);
            Canvas.SetTop(bottomRight, y + image.Height - 5);
        }
    }

}
