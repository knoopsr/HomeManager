using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace HomeManager.Converter
{
    public static class RichTextBoxHelper
    {
        public static readonly DependencyProperty BoundDocumentProperty =
            DependencyProperty.RegisterAttached(
                "BoundDocument",
                typeof(string),
                typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundDocumentChanged));

        public static string GetBoundDocument(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundDocumentProperty);
        }

        public static void SetBoundDocument(DependencyObject obj, string value)
        {
            obj.SetValue(BoundDocumentProperty, value);
        }

        private static void OnBoundDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox richTextBox)
            {
                var newValue = e.NewValue as string;

                if (newValue != null)
                {
                    var document = new FlowDocument();
                    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(newValue)))
                    {
                        var range = new TextRange(document.ContentStart, document.ContentEnd);
                        range.Load(stream, DataFormats.Rtf);
                    }
                    richTextBox.Document = document;
                }

                // Handle when text changes in RichTextBox
                richTextBox.TextChanged -= RichTextBox_TextChanged;
                richTextBox.TextChanged += RichTextBox_TextChanged;
            }
        }

        private static void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                using (var stream = new MemoryStream())
                {
                    range.Save(stream, DataFormats.Rtf);
                    SetBoundDocument(richTextBox, System.Text.Encoding.UTF8.GetString(stream.ToArray()));
                }
            }
        }
    }
}
