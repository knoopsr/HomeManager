using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace HomeManager.Converter
{
    public class RichTextToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string rtfText)
            {
                try
                {
                    var richTextBox = new RichTextBox();
                    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfText)))
                    {
                        var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        textRange.Load(stream, DataFormats.Rtf);
                    }
                    return richTextBox.Document;
                }
                catch
                {
                    return new FlowDocument();
                }
            }
            return new FlowDocument();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FlowDocument document)
            {
                using (var stream = new MemoryStream())
                {
                    var textRange = new TextRange(document.ContentStart, document.ContentEnd);
                    textRange.Save(stream, DataFormats.Rtf);
                    return System.Text.Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            return string.Empty;
        }
    }
}
