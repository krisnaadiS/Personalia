using PersonaliaLPDKuta.Utilities;
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
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta
{
    /// <summary>
    /// Interaction logic for MainTemplate.xaml
    /// </summary>
    public partial class MainTemplate : UserControl
    {
        public MainTemplate()
        {
            InitializeComponent();
            PageManager.GridSubContent = gridSubContent;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadToggleButton;
            if (btn.IsChecked == true)
                mainMenu.Visibility = Visibility.Collapsed;
            else
                mainMenu.Visibility = Visibility.Visible;
        }
    }
}
