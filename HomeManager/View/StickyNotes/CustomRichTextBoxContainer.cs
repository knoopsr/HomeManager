using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeManager.View.StickyNotes
{
    public class CustomRichTextBoxContainer : Control
    {
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.Register("RichText", typeof(string), typeof(CustomRichTextBoxContainer),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string RichText
        {
            get => (string)GetValue(RichTextProperty);
            set => SetValue(RichTextProperty, value);
        }

        static CustomRichTextBoxContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomRichTextBoxContainer),
                new FrameworkPropertyMetadata(typeof(CustomRichTextBoxContainer)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_RichTextBox") is RichTextBox rtb)
            {
                // Load initial content
                if (!string.IsNullOrEmpty(RichText))
                {
                    var flowDocument = new FlowDocument();
                    var range = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                    range.Load(new MemoryStream(Encoding.UTF8.GetBytes(RichText)), DataFormats.Xaml);
                    rtb.Document = flowDocument;
                }

                // Handle content changes
                rtb.TextChanged += (s, e) =>
                {
                    var range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    using (var stream = new MemoryStream())
                    {
                        range.Save(stream, DataFormats.Xaml);
                        RichText = Encoding.UTF8.GetString(stream.ToArray());
                    }
                };
            }
        }
    }
}
