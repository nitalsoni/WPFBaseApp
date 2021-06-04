using Symbol.RFID3;
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
using static Symbol.RFID3.Events;

namespace RFIDApp.UI
{
    /// <summary>
    /// Interaction logic for MaintenanceStatus.xaml
    /// </summary>
    public partial class MaintenanceStatus : UserControl
    {
        internal RFIDReader m_ReaderAPI;
        private TagData m_ReadTag = null;
        public MaintenanceStatus()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            //var addInData = readEventArgs.ReadEventData.TagData.TagID.ToString();
            //var antinaData = readEventArgs.ReadEventData.TagData.AntennaID.ToString();

            m_ReadTag = new Symbol.RFID3.TagData();
            m_ReaderAPI.Disconnect();
        }

        private void Events_ReadNotify(object sender, ReadEventArgs readEventArgs)
        {
            var addInData = "";
            try
            {
                addInData = readEventArgs.ReadEventData.TagData.TagID.ToString();
                var antinaData = readEventArgs.ReadEventData.TagData.AntennaID.ToString();

                addInData = antinaData = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            m_ReaderAPI = new RFIDReader("FX7500F9695E", uint.Parse("5084"), 10000);
            m_ReaderAPI.Connect();

            m_ReaderAPI.Actions.Inventory.Perform(null, null, null);
            m_ReaderAPI.Events.ReadNotify += new ReadNotifyHandler(Events_ReadNotify);
        }
    }
}
