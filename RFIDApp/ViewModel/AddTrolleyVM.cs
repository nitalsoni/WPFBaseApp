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
    public class AddTrolleyVM : BaseVM
    {
        ILoggerManager logger = new LoggerManager();
        public AddTrolleyVM()
        {
            this._trolley = new Trolley();
        }

        #region Local Vars
        TrolleyDBProvider dbProvider = new TrolleyDBProvider();
        #endregion

        private Trolley _trolley;
        public Trolley Trolley
        {
            get
            {
                return _trolley;
            }
            set
            {
                _trolley = value;
                NotifyPropertyChanged("Trolley");
            }
        }

        private List<string> _allStatus;
        public List<string> AllStaus
        {
            get
            {
                if (_allStatus == null)
                {
                    _allStatus = dbProvider.GetAllStatus().ToList();
                }

                return _allStatus;
            }
        }

        #region Commands
        private RelayCommand<IClosable> _addCommand;
        public RelayCommand<IClosable> AddCommand
        {
            get
            {
                return _addCommand
                  ?? (_addCommand = new RelayCommand<IClosable>((IClosable window) =>
                  {
                      try
                      {
                          if (dbProvider.AddTrolley(this.Trolley))
                          {
                              logger.Debug($"New trolley added succesfully. {this.Trolley.ToString()}");
                          }

                          window.Close();
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"failed to add new trolley. {ex.Message}", ex);
                      }
                }));
            }
        }
        #endregion


    }
}
