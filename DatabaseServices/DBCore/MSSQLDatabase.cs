using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DatabaseService.DBCore
{
    public class MSSQLDatabase : Database
    {
        public override IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            return new SqlCommand(commandText, (SqlConnection)connection);
        }

        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(this.connectionString);
        }

        public override IDbConnection CreateOpenConnection()
        {
            SqlConnection connection = (SqlConnection)CreateConnection();
            connection.Open();
            return connection;
        }

        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
