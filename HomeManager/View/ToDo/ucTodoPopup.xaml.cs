using HomeManager.Helpers;
using HomeManager.Model.Todo;
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
    /// Interaction logic for winTodoPopupView.xaml
    /// </summary>
    public partial class ucTodoPopup : UserControl
    {
        public ucTodoPopup(clsCollectiesM todoCollectieItem)
        {
            InitializeComponent();

            var viewModel = new clsTodoPopupVM();
            DataContext = viewModel;

            clsMessenger.Default.Send(todoCollectieItem);
            viewModel.SetDefaultCollectieItem(todoCollectieItem);
        }

        public ucTodoPopup(clsTodoPopupM todoItem)
        {
            InitializeComponent();

            var viewModel = new clsTodoPopupVM();
            DataContext = viewModel;

            // Stel de geselecteerde item in de TodoPopupViewModel in
            viewModel.MijnSelectedItem = todoItem;
        }
    }
}
