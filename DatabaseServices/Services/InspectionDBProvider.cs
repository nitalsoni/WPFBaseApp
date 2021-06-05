using DatabaseService;
using DatabaseService.DBCore;
using DatabaseService.Services;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.Services
{
    public class InspectionDBProvider : DBWorker
    {
        public InspectionDBProvider() : base(DBInforProvider.DBInfo)
        {

        }

        public bool AddInspection(Inspection inspection)
        {
            if (ConfigReader.TryGetQuery("i_inspection", out string iQuery))
            {
                iQuery = string.Format(iQuery, inspection.TrolleyId, inspection.PMDate, inspection.DueDate, inspection.OilGreesing, inspection.SlotCondition, inspection.WheelCondition, inspection.DoorLocking, inspection.WeildingHinges, inspection.CoverCompartment, inspection.ForkGuide, inspection.PaintCorrosionLess, inspection.Cleaningness, inspection.OtherWork, Helper.DefaultUser, DateTime.Now, inspection.MaintenanceStatus);
                return this.ExecuteNonQuery(iQuery) > 0;
            }

            return false;
        }
    }
}
