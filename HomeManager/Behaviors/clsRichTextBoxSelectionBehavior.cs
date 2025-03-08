using HomeManager.Helpers;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HomeManager.Behaviors
{
    public class clsRichTextBoxSelectionBehavior : Behavior<RichTextBox>
    {
        // Register a DependencyProperty for the Layout property
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", typeof(clsRTBLayout), typeof(clsRichTextBoxSelectionBehavior), new PropertyMetadata(null));

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
            if (Layout != null)
            {
                TextRange range = AssociatedObject.Selection;
                Layout.UpdateLayoutFromSelection(range); // Update the layout from selection
                
            }
        }
    }
}
