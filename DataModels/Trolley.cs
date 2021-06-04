using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Trolley : BaseModel
    {

        public Trolley()
        {
            this.MaintenanceDate = DateTime.Now;
        }
        public int Id { get; set; }

        private string trolleyName;
        public string TrolleyName 
        {
            get 
            {
                return trolleyName;
            }
            set
            {
                trolleyName = value;
                NotifyPropertyChanged("TrolleyName");
            }
        }

        public string TrolleyNum { get; set; }

        public string TagId { get; set; }

        public DateTime MaintenanceDate { get; set; }

        
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {TrolleyName}, Number: {TrolleyNum}, TagId: {TagId}, MaintenanceDate: { MaintenanceDate }, Status: {Status}";
        }
    }
}
