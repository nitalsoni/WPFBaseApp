using LoggerService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseService.DBCore
{
    public class DBWorker
    {
        private ILoggerManager _logger = new LoggerManager();
        public delegate void HandleRow(IDataReader dataReader);
        public DBWorker(DatabaseInfo dbInfo)
        {
            database = DatabaseKeeper.GetDatabase(dbInfo);
        }

        private Database database;
        public Database Database
        {
            get
            {
                return database;
            }
        }

        public void ExecuteQuery(string selectQ, HandleRow handleRow)
        {
            using (IDbConnection connection = this.Database.CreateOpenConnection())
            {
                using (IDbCommand command = this.Database.CreateCommand(selectQ, connection))
                {
                    this.LogQuery(selectQ);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            handleRow(reader);
                        }
                    }
                }
            }
        }

        public int ExecuteNonQuery(string crudQ)
        {
            using (IDbConnection connection = this.Database.CreateOpenConnection())
            {
                using (IDbCommand command = this.Database.CreateCommand(crudQ, connection))
                {
                    this.LogQuery(crudQ);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string selectQ)
        {
            using (IDbConnection connection = this.Database.CreateOpenConnection())
            {
                using (IDbCommand command = this.Database.CreateCommand(selectQ, connection))
                {
                    this.LogQuery(selectQ);
                    return command.ExecuteScalar();
                }
            }
        }


        private void LogQuery(string query)
        {
            this._logger.Debug(query);
        }
    }
}
