using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDApp.Helpers
{
    public class InvokeMaintenanceDetailsEvent : PubSubEvent<ScreenNames> { }


    public enum ScreenNames
    {
        TROLLEYSTATUS,
        TROLLEYDETAILS,
        TRANSACTIONS,
        REPORTS
    }

}
