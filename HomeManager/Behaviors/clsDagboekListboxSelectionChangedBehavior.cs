using HomeManager.Model.Dagboek;
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
    public class clsDagboekListboxSelectionChangedBehavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty TargetRichTextBoxProperty =
        DependencyProperty.Register(
            nameof(TargetRichTextBox),
            typeof(RichTextBox),
            typeof(clsDagboekListboxSelectionChangedBehavior),
            new PropertyMetadata(null));

        public RichTextBox TargetRichTextBox
        {
            get => (RichTextBox)GetValue(TargetRichTextBoxProperty);
            set => SetValue(TargetRichTextBoxProperty, value);
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

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TargetRichTextBox != null && AssociatedObject.SelectedItem != null)
            {
                TargetRichTextBox.Document.Blocks.Clear();

                var selectedItem = AssociatedObject.SelectedItem as clsDagboekModel;

                if (selectedItem != null)
                {
                    var rtfString = selectedItem.MyRTFString;

                    if (!string.IsNullOrEmpty(rtfString))
                    {
                        using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfString)))
                        {
                            TargetRichTextBox.Selection.Load(stream, DataFormats.Rtf);
                        }
                    }
                }
            }
        }
    }
}
