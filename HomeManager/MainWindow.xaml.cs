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

        //private void btnMenuPerson_Checked(object sender, RoutedEventArgs e)
        //{
        //    stckPerson.Visibility = Visibility.Visible;
        //}

        //private void btnMenuPerson_Unchecked(object sender, RoutedEventArgs e)
        //{

        //    stckPerson.Visibility = Visibility.Collapsed;
        //}

        //private void btnBudget_Checked(object sender, RoutedEventArgs e)
        //{
        //    stckBudget.Visibility = Visibility.Visible;
        //}

        //private void btnBudget_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    stckBudget.Visibility = Visibility.Collapsed;
        //}

        //private void btnTodo_Checked(object sender, RoutedEventArgs e)
        //{
        //    stckTodo.Visibility = Visibility.Visible;
        //}

        //private void btnTodo_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    stckTodo.Visibility = Visibility.Collapsed;
        //}

        //private void btnSecurity_Checked(object sender, RoutedEventArgs e)
        //{
        //        stckSecurity.Visibility = Visibility.Visible;
        //}

        //private void btnSecurity_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    stckSecurity.Visibility = Visibility.Collapsed;
        //}

        //private void btnStickyNotes_Checked(object sender, RoutedEventArgs e)
        //{
        //    stckStickynotes.Visibility = Visibility.Visible;
        //}

        //private void btnStickyNotes_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    stckStickynotes.Visibility = Visibility.Collapsed;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
 
            ucPersonen _ucPersonen = new ucPersonen();

            Grid.SetRow(_ucPersonen, 1);
            Grid.SetColumn(_ucPersonen, 1);

            grdMain.Children.Add(_ucPersonen);
        }
    }
}