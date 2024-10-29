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

        private void Persoon_Click(object sender, RoutedEventArgs e)
        {
            //ucPersonen _ucPersonen = new ucPersonen();

            //Grid.SetRow(_ucPersonen, 1);
            //Grid.SetColumn(_ucPersonen, 1);

            //grdMain.Children.Add(_ucPersonen);

            ucPersoon _ucPersoon = new ucPersoon();
            Grid.SetRow(_ucPersoon, 1);
            Grid.SetColumn(_ucPersoon, 1);
            grdMain.Children.Add(_ucPersoon);

        }

        private void EmailType_Click(object sender, RoutedEventArgs e)
        {
            ucEmailType _ucEmailType = new ucEmailType();

            Grid.SetRow(_ucEmailType, 1);
            Grid.SetColumn(_ucEmailType, 1);

            grdMain.Children.Add(_ucEmailType);
        }

        private void TelefoonType_Click(object sender, RoutedEventArgs e)
        {
            ucTelefoonType _ucTelefoonType = new ucTelefoonType();

            Grid.SetRow(_ucTelefoonType, 1);
            Grid.SetColumn(_ucTelefoonType, 1);

            grdMain.Children.Add(_ucTelefoonType);
        }

        private void Land_Click(object sender, RoutedEventArgs e)
        {
            ucLanden _ucLanden = new ucLanden();

            Grid.SetRow(_ucLanden, 1);
            Grid.SetColumn(_ucLanden, 1);

            grdMain.Children.Add(_ucLanden);
        }

        private void Provincie_Click(object sender, RoutedEventArgs e)
        {
            ucProvicies _ucProvicies = new ucProvicies();

            Grid.SetRow(_ucProvicies, 1);
            Grid.SetColumn(_ucProvicies, 1);

            grdMain.Children.Add(_ucProvicies);
        }

        private void Gemeente_Click(object sender, RoutedEventArgs e)
        {
            ucGemeente _ucGemeente = new ucGemeente();

            Grid.SetRow(_ucGemeente, 1);
            Grid.SetColumn(_ucGemeente, 1);

            grdMain.Children.Add(_ucGemeente);
        }

        private void Functies_Click(object sender, RoutedEventArgs e)
        {
            ucFuncties _ucFuncties = new ucFuncties();

            Grid.SetRow(_ucFuncties, 1);
            Grid.SetColumn(_ucFuncties, 1);

            grdMain.Children.Add(_ucFuncties);
        }


    }
}