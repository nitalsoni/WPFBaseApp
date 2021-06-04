using System.Configuration;

namespace DatabaseService.DBCore
{
    public sealed class DatabaseFactoryInfo
    {
        public string Name { get; set; }

        public string DBClass { get; set; }

        public string ConnectionString { get; set; }
    }
}
