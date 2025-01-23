using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Personen;
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
    /// Interaction logic for ucEmailAdressen.xaml
    /// </summary>
    public partial class ucEmailAdressen : UserControl
    {
        public ucEmailAdressen()
        {
            InitializeComponent();
            this.DataContext = new clsEmailAdressenViewModel();
        }
    }
}
