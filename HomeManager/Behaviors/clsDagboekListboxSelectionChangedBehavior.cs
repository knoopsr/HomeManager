using ClosedXML.Excel;
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
                    var xamlBytes = selectedItem.MyFlowDocument;  // Assuming MyFlowDocument holds the byte array for XAML

                    if (xamlBytes != null && xamlBytes.Length > 0)
                    {
                        using (var stream = new System.IO.MemoryStream(xamlBytes))
                        {
                            // Load the FlowDocument from the XAML byte array
                            TextRange textRange = new TextRange(TargetRichTextBox.Document.ContentStart, TargetRichTextBox.Document.ContentEnd);
                            textRange.Load(stream, DataFormats.Xaml);  // Load the XAML
                        }
                    }
                }
            }
        }
    }
}
