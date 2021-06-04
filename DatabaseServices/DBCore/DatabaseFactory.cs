using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DatabaseService.DBCore
{
    public sealed class DatabaseFactory
    {
        private DatabaseFactory() { }

        public static Database CreateDatabase(string connectionString, string className)
        {
            // Verify a DatabaseFactoryConfiguration line exists in the web.config.
            try
            {
                // Find the class
                Type database = Type.GetType(className);
                // Get it's constructor
                ConstructorInfo constructor = database.GetConstructor(new Type[] { });
                // Invoke it's constructor, which returns an instance.
                Database createdObject = (Database)constructor.Invoke(null);
                // Initialize the connection string property for the database.
                createdObject.connectionString = connectionString;
                // Pass back the instance as a Database
                return createdObject;
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating database " + className + ". " + excep.Message);
            }
        }

    }
}
