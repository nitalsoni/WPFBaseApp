using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseService.DBCore
{
    public class SQLiteDatabase : Database
    {
        public override IDbCommand CreateCommand()
        {
            return new SqliteCommand();
        }

        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            return new SqliteCommand(commandText, (SqliteConnection)connection);
        }

        public override IDbConnection CreateConnection()
        {
            return new SqliteConnection(this.connectionString);
        }

        public override IDbConnection CreateOpenConnection()
        {
            SqliteConnection connection = (SqliteConnection)CreateConnection();
            connection.Open();
            return connection;
        }

        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqliteParameter(parameterName, parameterValue);
        }

        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SqliteCommand command = (SqliteCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqliteConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}