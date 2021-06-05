using RFIDApp.Interface;
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
using System.Windows.Shapes;

namespace RFIDApp.UI
{
    /// <summary>
    /// Interaction logic for Inspection.xaml
    /// </summary>
    public partial class AddInspection : Window, IClosable
    {
        public AddInspection()
        {
            InitializeComponent();
        }

        public new void Close()
        {
            this.DialogResult = true;
            base.Close();
        }
    }
}
