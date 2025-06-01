using HomeManager.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ucPersonen.xaml
    /// </summary>
    public partial class ucPersonen : UserControl
    {
        public ucPersonen()
        {
            InitializeComponent();
            this.DataContext = new clsPersoonViewModel();
        }

        private void ucWerkBalk_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        //private void EmailAdressenListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void ucPersonItem_Loaded(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
