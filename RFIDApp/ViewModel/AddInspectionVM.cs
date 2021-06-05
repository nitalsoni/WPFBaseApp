using DatabaseService.Services;
using DataModel;
using GalaSoft.MvvmLight.Command;
using LoggerService;
using RFIDApp.Interface;
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
        InspectionDBProvider inspectionProvider = new InspectionDBProvider();
        ILoggerManager logger = new LoggerManager();
        #endregion

        public AddInspectionVM(Trolley trolley)
        {
            this._inspection = new Inspection();
            this._inspection.TrolleyId = trolley.Id;
            this._inspection.TrolleyName = trolley.TrolleyName;
            this._inspection.TrolleyNum = trolley.TrolleyNum;
            this._inspection.PMDate = trolley.MaintenanceDate;
            this._inspection.MaintenanceStatus = trolley.Status;
            this._inspection.DueDate = trolley.MaintenanceDate;

            if (trolley.MaintenanceDate != DateTime.MinValue || trolley.MaintenanceDate != DateTime.MaxValue)
            {
                if ((trolley.MaintenanceDate - DateTime.Now).TotalDays <= 0)
                    this._inspection.DueDate = DateTime.Now;
            }
        }

        #region Properties

        private Inspection _inspection;
        public Inspection Inspection
        {
            get
            {
                return _inspection;
            }
            set
            {
                _inspection = value;
                NotifyPropertyChanged("Inspection");
            }
        }


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
        private RelayCommand<IClosable> _addInspectionCommand;
        public RelayCommand<IClosable> AddInspectionCommand
        {
            get
            {
                return _addInspectionCommand
                  ?? (_addInspectionCommand = new RelayCommand<IClosable>((IClosable window) =>
                  {
                      try
                      {
                          if (inspectionProvider.AddInspection(this.Inspection))
                          {
                              trolleyProvider.UpdateTrolley(new Trolley()
                              {
                                  Id = this.Inspection.TrolleyId,
                                  Status = this.Inspection.MaintenanceStatus,
                                  MaintenanceDate = this.Inspection.DueDate
                              });

                              logger.Info($"New Inspection added succesfully. {this.Inspection.ToString()}");
                          }
                          
                          window.Close();
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"failed to add new inspection. {ex.Message}", ex);
                      }
                  }));
            }
        }
        #endregion

    }
}
