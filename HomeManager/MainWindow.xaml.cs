using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeManager.View.Personen;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard showMenu = (Storyboard)FindResource("ShowMenu");
            showMenu.Begin();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Storyboard hideMenu = (Storyboard)FindResource("HideMenu");
            hideMenu.Begin();
        }

  



    }
}