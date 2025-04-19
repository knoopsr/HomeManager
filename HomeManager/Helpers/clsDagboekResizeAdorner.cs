using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeManager.Helpers
{
    public class clsDagboekResizeAdorner : Adorner
    {
        private readonly Thumb topLeftThumb;
        private readonly Thumb topRightThumb;
        private readonly Thumb bottomLeftThumb;
        private readonly Thumb bottomRightThumb;
        private readonly UIElement adornedElement;

        public clsDagboekResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.adornedElement = adornedElement;

            // Create resize thumbs
            topLeftThumb = CreateThumb(VerticalAlignment.Top, HorizontalAlignment.Left);
            topRightThumb = CreateThumb(VerticalAlignment.Top, HorizontalAlignment.Right);
            bottomLeftThumb = CreateThumb(VerticalAlignment.Bottom, HorizontalAlignment.Left);
            bottomRightThumb = CreateThumb(VerticalAlignment.Bottom, HorizontalAlignment.Right);

            // Add thumbs to the adorner's visual tree
            AddVisualChild(topLeftThumb);
            AddVisualChild(topRightThumb);
            AddVisualChild(bottomLeftThumb);
            AddVisualChild(bottomRightThumb);

            // Handle drag events to resize
            topLeftThumb.DragDelta += OnDragDelta;
            topRightThumb.DragDelta += OnDragDelta;
            bottomLeftThumb.DragDelta += OnDragDelta;
            bottomRightThumb.DragDelta += OnDragDelta;
        }

        private Thumb CreateThumb(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
            return new Thumb
            {
                Width = 8,
                Height = 8,
                Background = Brushes.Red,
                VerticalAlignment = verticalAlignment,
                HorizontalAlignment = horizontalAlignment,
                Cursor = Cursors.SizeNWSE
            };
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double sensitivity = 0.2;

            // Scale the horizontal and vertical change to reduce sensitivity
            double scaledHorizontalChange = e.HorizontalChange * sensitivity;
            double scaledVerticalChange = e.VerticalChange * sensitivity;

            // Logic to resize the adorned element based on the scaled change
            double newWidth = Math.Max(10, adornedElement.RenderSize.Width + scaledHorizontalChange);
            double newHeight = Math.Max(10, adornedElement.RenderSize.Height + scaledVerticalChange);

            //// Logic to resize the adorned element based on thumb movement
            //double newWidth = Math.Max(10, adornedElement.RenderSize.Width + e.HorizontalChange);
            //double newHeight = Math.Max(10, adornedElement.RenderSize.Height + e.VerticalChange);

            // Resize the adorned element (e.g., Image)
            if (adornedElement is FrameworkElement element)
            {
                element.Width = newWidth;
                element.Height = newHeight;
            }


            // Invalidate the adorner to update the thumb positions
            InvalidateVisual();
        }

        // Override to position thumbs correctly
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // Position the thumbs based on the adorned element's size and location
            var bounds = new Rect(adornedElement.RenderSize);
            topLeftThumb.Arrange(new Rect(bounds.Left - 4, bounds.Top - 4, 8, 8));
            topRightThumb.Arrange(new Rect(bounds.Right - 4, bounds.Top - 4, 8, 8));
            bottomLeftThumb.Arrange(new Rect(bounds.Left - 4, bounds.Bottom - 4, 8, 8));
            bottomRightThumb.Arrange(new Rect(bounds.Right - 4, bounds.Bottom - 4, 8, 8));
        }

        protected override int VisualChildrenCount => 4;

        protected override Visual GetVisualChild(int index)
        {
            return index switch
            {
                0 => topLeftThumb,
                1 => topRightThumb,
                2 => bottomLeftThumb,
                3 => bottomRightThumb,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
