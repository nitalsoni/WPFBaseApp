using DatabaseService.Services;
using DataModel;
using RFIDApp.Helpers;
using RFIDApp.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RFIDApp.UI
{
    /// <summary>
    /// Interaction logic for MaintenanceDetails.xaml
    /// </summary>
    public partial class TrolleyMaster : UserControl
    {
        public TrolleyMaster()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTrolley addWin = new AddTrolley();
            addWin.DataContext = (this.DataContext as TrolleyVM).AddTrolleyVM;
            addWin.ShowDialog();
        }

        private void btnInspect_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgTrolley.SelectedItem != null)
            {
                var trolley = this.dgTrolley.SelectedItem as Trolley;
                AddInspection inspect = new AddInspection();
                inspect.DataContext = new AddInspectionVM(trolley);
                inspect.ShowDialog();
            }
        }
    }
}
