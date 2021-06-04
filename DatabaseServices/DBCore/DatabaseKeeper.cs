using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.DBCore
{
    public static class DatabaseKeeper
    {
        private static Dictionary<string, Database> _dbCollection;
        private static object syncLock = new object();

        static DatabaseKeeper()
        {
            _dbCollection = new Dictionary<string, Database>();
        }

        public static Database GetDatabase(DatabaseInfo dbInfo)
        {
            lock (syncLock)
            {
                if (!_dbCollection.ContainsKey(dbInfo.Name))
                {
                    _dbCollection.Add(dbInfo.Name, CreateDatabase(dbInfo));
                }

                return _dbCollection[dbInfo.Name];
            }
        }

        private static Database CreateDatabase(DatabaseInfo dbInfo)
        {
            return DatabaseFactory.CreateDatabase(dbInfo.ConnectionString, dbInfo.ClassName);
        }
    }
}
