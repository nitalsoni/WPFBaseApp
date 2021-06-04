using DatabaseService.Services;
using DataModel;
using GalaSoft.MvvmLight.Command;
using LoggerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDApp.ViewModel
{
    public class AddInspectionVM : BaseVM
    {
        #region Local Vars
        TrolleyDBProvider trolleyProvider = new TrolleyDBProvider();
        ILoggerManager logger = new LoggerManager();
        #endregion

        public AddInspectionVM(Trolley trolley)
        {
            this.TrolleyName = trolley.TrolleyName;
            this.TrolleyNum = trolley.TrolleyNum;
            this.PMDate = trolley.MaintenanceDate;
            this.MaintenanceStatus = trolley.Status;
            this.DueDate = trolley.MaintenanceDate;

            if (trolley.MaintenanceDate != DateTime.MinValue || trolley.MaintenanceDate != DateTime.MaxValue)
            {
                if ((trolley.MaintenanceDate - DateTime.Now).TotalDays <= 0)
                    this.DueDate = DateTime.Now;
            }
        }

        #region Properties
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

        private List<string> _allStatus;
        public List<string> AllStaus
        {
            get
            {
                if (_allStatus == null)
                {
                    _allStatus = trolleyProvider.GetAllStatus().ToList();
                }

                return _allStatus;
            }
        }
        #endregion

        #region Commands
        private RelayCommand _addInspectionCommand;
        public RelayCommand AddInspectionCommand
        {
            get
            {
                return _addInspectionCommand
                  ?? (_addInspectionCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          //dbProvider.AddTrolley(this.Trolley);
                          //logger.Debug($"New trolley added succesfully. {this.Trolley.ToString()}");
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"failed to add trolley insepction. {ex.Message}", ex);
                      }
                  }, () =>
                  {
                      return true;
                  }));
            }
        }
        #endregion
        
    }
}
