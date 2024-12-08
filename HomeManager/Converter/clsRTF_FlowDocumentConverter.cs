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
    public static class clsRTF_FlowDocumentConverter
    {
        // Converts FlowDocument to RTF string
        public static string ConvertFlowDocumentToString(FlowDocument flowDocument)
        {
            var range = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            using (var stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Rtf); // Save as RTF
                return Encoding.UTF8.GetString(stream.ToArray()); // Return the RTF string
            }
        }

        // Converts RTF string to FlowDocument
        public static FlowDocument ConvertStringToFlowDocument(string rtfString)
        {
            var flowDocument = new FlowDocument();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(rtfString)))
            {
                var range = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                range.Load(stream, DataFormats.Rtf); // Load RTF string into FlowDocument
            }
            return flowDocument;
        }

        // Two-way binding logic for RichTextBox and model
        public static void PopulateRTB(RichTextBox richTextBox, string rtfString)
        {
            // Convert string to FlowDocument and bind to RichTextBox
            var flowDocument = ConvertStringToFlowDocument(rtfString);
            richTextBox.Document = flowDocument;
        }

        public static string PopulateRTF(RichTextBox richTextBox)
        {
            // Convert FlowDocument to RTF string and return
            return ConvertFlowDocumentToString(richTextBox.Document);
        }
    }
}
