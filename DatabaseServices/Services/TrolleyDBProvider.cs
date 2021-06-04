using DatabaseService.DBCore;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace DatabaseService.Services
{
    public class TrolleyDBProvider : DBWorker
    {
        public TrolleyDBProvider() : base(DBInforProvider.DBInfo)
        {

        }

        public IEnumerable<Trolley> GetAllRecords()
        {
            List<Trolley> records = new List<Trolley>();
            if (ConfigReader.TryGetQuery("s_all_trollies", out string select))
            {
                this.ExecuteQuery(select, (reader) =>
                {
                    records.Add(new Trolley()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        TrolleyName = reader["trolleyName"].ToString(),
                        TrolleyNum = reader["trolleyNumber"].ToString(),
                        TagId = reader["tagId"].ToString(),
                        MaintenanceDate = Convert.ToDateTime(reader["maintenanceDate"]),
                        Status = reader["maintenanceStatus"].ToString(),
                    });
                });
            }

            return records;
        }

        public IEnumerable<Trolley> GetRecords(string search)
        {
            List<Trolley> records = new List<Trolley>();
            if (ConfigReader.TryGetQuery("s_search_trolley", out string select))
            {
                select = string.Format(select, search.ToUpper());
                this.ExecuteQuery(select, (reader) =>
                {
                    records.Add(new Trolley()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        TrolleyName = reader["trolleyName"].ToString(),
                        TrolleyNum = reader["trolleyNumber"].ToString(),
                        TagId = reader["tagId"].ToString(),
                        MaintenanceDate = Convert.ToDateTime(reader["maintenanceDate"]),
                        Status = reader["maintenanceStatus"].ToString(),
                    });
                });
            }

            return records;
        }

        public IEnumerable<string> GetAllStatus()
        {
            return new List<string>() { "Available", "Breakdown", "Schedule For Maintenance", "Under Maintenance" };
        }

        public bool AddTrolley(Trolley trolley)
        {
            if (ConfigReader.TryGetQuery("i_trolley", out string iQuery))
            {
                iQuery = string.Format(iQuery, trolley.TrolleyName, trolley.TrolleyNum, trolley.TagId, trolley.MaintenanceDate, trolley.Status);
                return this.ExecuteNonQuery(iQuery) > 0;
            }
            
            return false;
        }

        public bool UpdateTrolley(Trolley trolley)
        {
            if (ConfigReader.TryGetQuery("u_trolley", out string uQuery))
            {
                uQuery = string.Format(uQuery, trolley.Status, trolley.MaintenanceDate, trolley.TagId);
                return this.ExecuteNonQuery(uQuery) > 0;
            }

            return false;
        }
    }
}
