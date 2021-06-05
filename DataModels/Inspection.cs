using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Inspection : BaseModel
    {
        public int TrolleyId { get; set; }

        public string TrolleyName { get; set; }

        public string TrolleyNum { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PMDate { get; set; }

        public string OilGreesing { get; set; }

        public string SlotCondition { get; set; }

        public string WheelCondition { get; set; }

        public string DoorLocking { get; set; }

        public string WeildingHinges { get; set; }

        public string CoverCompartment { get; set; }

        public string ForkGuide { get; set; }

        public string PaintCorrosionLess { get; set; }

        public string Cleaningness { get; set; }

        public string OtherWork { get; set; }

        public string MaintenanceStatus { get; set; }
    }
}
