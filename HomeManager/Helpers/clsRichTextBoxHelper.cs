using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace HomeManager.Helpers
{
    public static class clsRichTextBoxHelper
    {
        public static readonly DependencyProperty BoundDocumentProperty =
            DependencyProperty.RegisterAttached(
                "BoundDocument",
                typeof(string),
                typeof(clsRichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundDocumentChanged));

        public static string GetBoundDocument(DependencyObject obj) =>
            (string)obj.GetValue(BoundDocumentProperty);

        public static void SetBoundDocument(DependencyObject obj, string value) =>
            obj.SetValue(BoundDocumentProperty, value);

        private static void OnBoundDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox richTextBox)
            {
                // Remove event handler to prevent recursion
                richTextBox.TextChanged -= RichTextBox_TextChanged;

                // Preserve caret position before updating the document content
                var caretPosition = richTextBox.CaretPosition;

                // Update RichTextBox.Document content from the bound property
                if (e.NewValue is string newText)
                {
                    var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                    // Update only if the text is different to avoid unnecessary changes
                    if (range.Text != newText)
                    {
                        range.Text = newText;
                    }
                }

                // Restore caret position after updating the document
                richTextBox.CaretPosition = caretPosition;
                richTextBox.Focus();  // Ensure RichTextBox regains focus

                // Reattach event handler
                richTextBox.TextChanged += RichTextBox_TextChanged;
            }
        }

        private static void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                // Get the current content of the RichTextBox
                var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string newText = range.Text;

                // Update the bound property
                SetBoundDocument(richTextBox, newText);
            }
        }
    }

}
