using DataModel;
using GalaSoft.MvvmLight.Command;
using LoggerService;
using Prism.Events;
using RFIDApp.Helpers;
using RFIDApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RFIDApp.ViewModel
{
    public class MainVM
    {
        private ILoggerManager logger = new LoggerManager();
        private ListViewItem _currentSelection;
        private Master masterModel;
        private IEventAggregator _eventAgg;
        public MainVM(IEventAggregator eventAgg)
        {
            this._eventAgg = eventAgg;
            this.masterModel = new Master();

            this._trolleyMasterVM = new TrolleyVM(eventAgg);
        }

        #region Child VMs
        private TrolleyVM _trolleyMasterVM;
        public TrolleyVM TrolleyMasterVM 
        {
            get
            {
                return _trolleyMasterVM;
            }
        }
        #endregion

        #region Properties

        public Master MasterModel
        {
            get
            {
                return this.masterModel;
            }
        }

        private ListViewItem _selecteItem;
        public ListViewItem SelectedItem
        {
            get
            {
                return _selecteItem;
            }
            set
            {
                this._selecteItem = value;
                this.LoadControl(this._currentSelection, this._selecteItem);
                this._currentSelection = value;
            }
        }

        #endregion

        #region Commands
        private RelayCommand<bool> _mouseEnterLeaveCommand;
        public RelayCommand<bool> MouseEnterLeaveCommand
        {
            get
            {
                return _mouseEnterLeaveCommand
                  ?? (_mouseEnterLeaveCommand = new RelayCommand<bool>(
                    status =>
                    {
                        this.masterModel.IsToggleChecked = status;
                    }));
            }
        }
        #endregion

        #region Methods

        private void LoadControl(ListViewItem currentItem, ListViewItem newItem)
        {
            try
            {
                if (currentItem == null || (currentItem.Name != newItem.Name))
                {
                    if (newItem.Tag is Window)
                        (newItem.Tag as Window).Close();
                    else
                    {
                        string control = newItem.Tag.ToString();
                        if (Enum.TryParse(control.ToUpper(), out ScreenNames screen))
                        {
                            switch (screen)
                            {
                                case ScreenNames.TROLLEYSTATUS:
                                    masterModel.IsTrolleyStatusVisible = true;
                                    masterModel.IsReportsVisible = masterModel.IsTransactionsVisible = masterModel.IsTrolleyDetailsVisible = false;
                                    break;
                                case ScreenNames.TROLLEYDETAILS:
                                    masterModel.IsTrolleyDetailsVisible = true;
                                    masterModel.IsReportsVisible = masterModel.IsTransactionsVisible = masterModel.IsTrolleyStatusVisible = false;
                                    break;
                                case ScreenNames.TRANSACTIONS:
                                    masterModel.IsTransactionsVisible = true;
                                    masterModel.IsReportsVisible = masterModel.IsTrolleyDetailsVisible = masterModel.IsTrolleyStatusVisible = false;
                                    break;
                                case ScreenNames.REPORTS:
                                    masterModel.IsReportsVisible = true;
                                    masterModel.IsTransactionsVisible = masterModel.IsTrolleyDetailsVisible = masterModel.IsTrolleyStatusVisible = false;
                                    break;
                                default:
                                    masterModel.IsTransactionsVisible = masterModel.IsTrolleyDetailsVisible = masterModel.IsTrolleyStatusVisible = masterModel.IsReportsVisible = false;
                                    break;
                            }

                            this._eventAgg.GetEvent<PubSubEvent<ScreenNames>>().Publish(screen);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error loading control", ex);
            }
        }

        public void CloseWindow()
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}
