using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace HomeManager.Helpers
{
    public static class clsRichTextBoxHelper
    {
        public static readonly DependencyProperty RtfTextProperty =
            DependencyProperty.RegisterAttached(
                "RtfText",
                typeof(string),
                typeof(clsRichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRtfTextChanged));

        public static string GetRtfText(DependencyObject obj) => 
            (string)obj.GetValue(RtfTextProperty);

        public static void SetRtfText(DependencyObject obj, string value)
        {
            if (obj is RichTextBox rtb)
            {
                rtb.TextChanged -= Rtb_TextChanged; // Avoid duplicate
                rtb.TextChanged += Rtb_TextChanged;
            }
            obj.SetValue(RtfTextProperty, value);
        }

        private static void OnRtfTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb && e.NewValue is string newRtf)
            {
                // Only update if changed to avoid infinite loop
                if (GetCurrentRtfText(rtb) != newRtf)
                {
                    rtb.TextChanged -= Rtb_TextChanged; // Prevent recursive call
                    var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

                    rtb.Document.Blocks.Clear();

                    using (var stream = new MemoryStream(Encoding.Default.GetBytes(newRtf)))
                    {
                        try
                        {
                            textRange.Load(stream, DataFormats.Rtf);
                        }
                        catch
                        {
                            rtb.Document.Blocks.Clear(); // fallback on error
                        }
                    }

                    rtb.TextChanged += Rtb_TextChanged; // Re-attach
                }
            }
        }

        private static void Rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is RichTextBox rtb)
            {
                var currentRtf = GetCurrentRtfText(rtb);
                SetRtfText(rtb, currentRtf);
            }
        }

        public static readonly DependencyProperty BindBackProperty =
            DependencyProperty.RegisterAttached(
                "BindBack",
                typeof(bool),
                typeof(clsRichTextBoxHelper),
                new PropertyMetadata(false, OnBindBackChanged));

        public static bool GetBindBack(DependencyObject obj)
        {
            return (bool)obj.GetValue(BindBackProperty);
        }

        public static void SetBindBack(DependencyObject obj, bool value)
        {
            obj.SetValue(BindBackProperty, value);
        }

        private static void OnBindBackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb && (bool)e.NewValue)
            {
                rtb.LostFocus += (s, args) =>
                {
                    var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    using (var stream = new MemoryStream())
                    {
                        try
                        {
                            textRange.Save(stream, DataFormats.Rtf);
                            SetRtfText(rtb, Encoding.Default.GetString(stream.ToArray())); // 🔹 Gebruik ASCII encoding
                        }
                        catch
                        {
                            SetRtfText(rtb, string.Empty);
                        }
                    }
                };
            }
        }

        private static string GetCurrentRtfText(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            using (var stream = new MemoryStream())
            {
                textRange.Save(stream, DataFormats.Rtf);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }
    }  
}