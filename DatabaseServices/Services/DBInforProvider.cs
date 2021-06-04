using DatabaseService.DBCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.Services
{
    public static class DBInforProvider
    {
        private static DatabaseInfo _dbInfo;
        public static DatabaseInfo DBInfo 
        {
            get
            {
                if (_dbInfo == null)
                {
                    _dbInfo = ConfigReader.getProvider();
                }

                return _dbInfo;
            }
        }
    }
}
