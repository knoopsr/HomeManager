using HomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HomeManager.View
{
    /// <summary>
    /// Interaction logic for ucDagboek.xaml
    /// </summary>
    public partial class ucDagboek : UserControl
    {
        public ucDagboek()
        {
            InitializeComponent();

            

            // Attach Loaded event
            //this.Loaded += ucDagboek_Loaded;
        }

        private void ucDagboek_Loaded(object sender, RoutedEventArgs e)
        {
            //// Ensure the DataContext is set
            //if (this.DataContext is clsDagboekViewModel viewModel)
            //{
            //    var richTextBox = MyRTB;  // Your RichTextBox control

            //    // Execute the command to update the RichTextBox with MyRTFString
            //    if (viewModel.UpdateRichTextBoxCommand.CanExecute(richTextBox))
            //    {
            //        viewModel.UpdateRichTextBoxCommand.Execute(richTextBox);
            //    }
            //}
            //else
            //{
            //    // Handle the case where DataContext is not set or is not of the expected type
            //    Debug.WriteLine("DataContext is not set correctly.");
            //}
        }
    }
}
