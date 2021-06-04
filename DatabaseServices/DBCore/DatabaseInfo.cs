using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.DBCore
{
    public class DatabaseInfo
    {
        public string ConnectionString { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $" Name: {Name}, Class: {this.ClassName} , Connection:  {this.ConnectionString }";
        }
    }
}
