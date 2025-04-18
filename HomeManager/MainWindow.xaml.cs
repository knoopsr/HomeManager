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

    /// Nugget package geïnstalleerd voor het selecteren van folders.(Microsoft-WindowsAPICodePack-Shell)
    /// Nugget package geïnstalleerd voor het gebruiken van api. (Newtonsoft.Json)
    /// Bram z'n using.System.Drawing in commentaar uit clsRTBLayout want zorgde voor veel conflicten.
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}