using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DatabaseService.DBCore;
using System.Linq;

namespace DatabaseService
{
    public class ConfigReader
    {
        public static DatabaseInfo getProvider()
        {
            var connString = ConfigurationManager.AppSettings.Get("DBConnString");
            var className = ConfigurationManager.AppSettings.Get("DBClass");
            var alias = ConfigurationManager.AppSettings.Get("Alias");

            DatabaseInfo dbInfo = new DatabaseInfo()
            {
                ClassName = className,
                ConnectionString = connString,
                Name = alias
            };

            return dbInfo;
        }

        public static bool TryGetQuery(string alias, out string query)
        {
            query = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(alias))
            {
                query = ConfigurationManager.AppSettings.Get(alias);
                return true;
            }

            return false;
        }
    }
}
