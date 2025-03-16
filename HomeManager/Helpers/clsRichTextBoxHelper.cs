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

        public static string GetRtfText(DependencyObject obj)
        {
            return (string)obj.GetValue(RtfTextProperty);
        }

        public static void SetRtfText(DependencyObject obj, string value)
        {
            obj.SetValue(RtfTextProperty, value);
        }

        //private static void OnRtfTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (d is RichTextBox rtb && e.NewValue is string newRtf)
        //    {
        //        // 🔹 Voorkom oneindige lus door alleen te laden als de waarde echt verandert
        //        if (GetCurrentRtfText(rtb) != newRtf)
        //        {
        //            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

        //            // 🔹 Eerst inhoud wissen om refresh te forceren
        //            rtb.Document.Blocks.Clear();

        //            using (var stream = new MemoryStream(Encoding.Default.GetBytes(newRtf))) // 🔹 Gebruik ASCII encoding
        //            {
        //                try
        //                {
        //                    textRange.Load(stream, DataFormats.Rtf);
        //                }
        //                catch
        //                {
        //                    // Als laden mislukt, leeg document instellen
        //                    rtb.Document.Blocks.Clear();
        //                }
        //            }
        //        }
        //    }
        //}

        private static void OnRtfTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb && e.NewValue is string newRtf)
            {
                // Voorkom oneindige lus door alleen te laden als de waarde echt verandert
                if (GetCurrentRtfText(rtb) != newRtf)
                {
                    var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

                    // Eerst inhoud wissen om refresh te forceren
                    rtb.Document.Blocks.Clear();

                    using (var stream = new MemoryStream(Encoding.Default.GetBytes(newRtf)))
                    {
                        try
                        {
                            textRange.Load(stream, DataFormats.Rtf);
                        }
                        catch
                        {
                            // Als laden mislukt, leeg document instellen
                            rtb.Document.Blocks.Clear();
                        }
                    }
                }
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
                try
                {
                    textRange.Save(stream, DataFormats.Rtf);
                    return Encoding.Default.GetString(stream.ToArray()); // 🔹 Gebruik ASCII encoding
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }  
}