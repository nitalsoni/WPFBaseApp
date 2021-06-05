using DatabaseService.Services;
using DataModel;
using GalaSoft.MvvmLight.Command;
using LoggerService;
using Prism.Events;
using RFIDApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using RFIDApp.UI;

namespace RFIDApp.ViewModel
{
    public class TrolleyVM : BaseVM
    {
        #region Local Variables
        TrolleyDBProvider dbProvider = new TrolleyDBProvider();
        private IEventAggregator _eventAgg;
        private AddTrolleyVM _addTrolleyVM;
        ILoggerManager logger = new LoggerManager();
        #endregion

        public TrolleyVM(IEventAggregator eventAgg)
        {
            this._eventAgg = eventAgg;
            this._addTrolleyVM = new AddTrolleyVM();

            _eventAgg.GetEvent<PubSubEvent<ScreenNames>>()
                        .Subscribe((screen) =>
                        {
                            if (screen == ScreenNames.TROLLEYDETAILS)
                            {
                                this.ResetCommand.Execute(null);
                            }
                        });
        }

        #region Properties

        private ObservableCollection<Trolley> _records;
        public ObservableCollection<Trolley> Records
        {
            get
            {
                return _records;
            }
            set
            {
                _records = value;
                NotifyPropertyChanged("Records");
            }
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                this._searchText = value;
                NotifyPropertyChanged("SearchText");
            }
        }

        public Trolley SelectedRow { get; set; }

        #endregion

        #region Commands

        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                  ?? (_searchCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.Records = new ObservableCollection<Trolley>(dbProvider.GetRecords(this.SearchText));
                            logger.Info($"Search trolley successful. Records fetched {this.Records.Count}");
                        }
                        catch (Exception ex)
                        {
                            logger.Error($"failed to search trolley. {ex.Message}", ex);
                        }

                    }));
            }
        }

        private RelayCommand _resetCommand;
        public RelayCommand ResetCommand
        {
            get
            {
                return _resetCommand
                  ?? (_resetCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          this.SearchText = string.Empty;
                          this.Records = new ObservableCollection<Trolley>(dbProvider.GetAllRecords());
                          logger.Debug($"Reset search successful. Records fetched {this.Records.Count}");
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"failed to reset trolley search. {ex.Message}", ex);
                      }
                  }));
            }
        }

        private RelayCommand _exportCommand;
        public RelayCommand ExportCommand
        {
            get
            {
                return _exportCommand
                  ?? (_exportCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          logger.Debug($"Start exporting to excel. Records to export {this.Records.Count}");
                          var app = ExcelOps.ExportToExcel(this.Records, "");
                          var workBook = app.ActiveWorkbook;

                          var saveFileDailog = new SaveFileDialog();
                          saveFileDailog.FileName = "Trolley_Details" + " " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                          saveFileDailog.DefaultExt = ".xlsx";
                          if (saveFileDailog.ShowDialog() == true)
                          {
                              workBook.SaveAs(saveFileDailog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                          }
                          app.Quit();
                          logger.Info($"Export to excel completed successfully");
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"Failed Export data to excel. {ex.Message}", ex);
                      }
                  }));
            }
        }

        private RelayCommand _inspectCommand;
        public RelayCommand InspectCommand
        {
            get
            {
                return _inspectCommand
                  ?? (_inspectCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          if (this.SelectedRow != null)
                          {
                              AddInspection inspect = new AddInspection();
                              inspect.DataContext = new AddInspectionVM(this.SelectedRow);
                              if (inspect.ShowDialog() == true)
                              {
                                  this.ResetCommand.Execute(null);
                                  logger.Info($"Inpection completed successfully for {this.SelectedRow.TrolleyName}");
                              }
                          }
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"Failed to complete insepction. {ex.Message}", ex);
                      }
                  }));
            }
        }

        private RelayCommand _addTrolleyCommand;
        public RelayCommand AddTrolleyCommand
        {
            get
            {
                return _addTrolleyCommand
                  ?? (_addTrolleyCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          AddTrolley addWin = new AddTrolley();
                          addWin.DataContext = this.AddTrolleyVM;
                          if (addWin.ShowDialog() == true)
                          {
                              this.AddTrolleyVM = new AddTrolleyVM();
                              this.ResetCommand.Execute(null);
                          }
                      }
                      catch (Exception ex)
                      {
                          logger.Error($"Failed to complete insepction. {ex.Message}", ex);
                      }
                  }));
            }
        }

        #endregion

        #region Child VMs
        public AddTrolleyVM AddTrolleyVM
        {
            get
            {
                return this._addTrolleyVM;
            }
            set
            {
                this._addTrolleyVM = value;
                NotifyPropertyChanged("AddTrolleyVM");
            }
        }
        #endregion

        #region Methods
        private void ExportToExcel()
        {
        }
        #endregion
    }
}
