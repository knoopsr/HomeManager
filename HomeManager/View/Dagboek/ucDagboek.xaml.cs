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

namespace HomeManager.View.Dagboek
{
    /// <summary>
    /// Interaction logic for ucDagboek.xaml
    /// </summary>
    public partial class ucDagboek : UserControl
    {
        public List<string> contentList = new List<string>();

        public ucDagboek()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                contentList.Add(DateTime.Now.ToLongDateString());

                Thread.Sleep(750);
            }

            lstMyItems.ItemsSource = contentList;
        }
    }
}
