using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Master : BaseModel
    {
        public Master()
        {
            this.IsToggleChecked = true;
        }

        private bool _isTrolleyStatusVisible = false;
        public bool IsTrolleyStatusVisible
        {
            get
            {
                return _isTrolleyStatusVisible;
            }
            set
            {
                _isTrolleyStatusVisible = value;
                NotifyPropertyChanged("IsTrolleyStatusVisible");
            }
        }

        private bool _isTrolleyDetailsVisible = false;
        public bool IsTrolleyDetailsVisible
        {
            get
            {
                return _isTrolleyDetailsVisible;
            }
            set
            {
                _isTrolleyDetailsVisible = value;
                NotifyPropertyChanged("IsTrolleyDetailsVisible");
            }
        }

        private bool _isReportsVisible = false;
        public bool IsReportsVisible
        {
            get
            {
                return _isReportsVisible;
            }
            set
            {
                _isReportsVisible = value;
                NotifyPropertyChanged("IsReportsVisible");
            }
        }

        private bool _isTransactionsVisible = false;
        public bool IsTransactionsVisible
        {
            get
            {
                return _isTransactionsVisible;
            }
            set
            {
                _isTransactionsVisible = value;
                NotifyPropertyChanged("IsTransactionsVisible");
            }
        }

        private bool _isToggleChecked;
        public bool IsToggleChecked
        {
            get
            {
                return _isToggleChecked;
            }
            set
            {
                _isToggleChecked = value;
                NotifyPropertyChanged("IsToggleChecked");
            }
        }

    }
}
