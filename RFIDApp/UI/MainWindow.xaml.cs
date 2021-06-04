using RFIDApp.Helpers;
using RFIDApp.Interface;
using RFIDApp.ViewModel;
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

namespace RFIDApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClosable
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainVM(Event.EventInstance.EventAggregator);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_trolleystatus.Visibility = Visibility.Collapsed;
                tt_trolleyDetails.Visibility = Visibility.Collapsed;
                tt_transactions.Visibility = Visibility.Collapsed;
                tt_reports.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_trolleystatus.Visibility = Visibility.Visible;
                tt_trolleyDetails.Visibility = Visibility.Visible;
                tt_transactions.Visibility = Visibility.Visible;
                tt_reports.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
