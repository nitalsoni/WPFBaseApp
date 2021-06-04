using DatabaseService.DBCore;
using System;
//using System.Data.SQLite;
namespace DatabaseService
{
    public class DBService
    {
        public void GetData()
        {
            //string cs = ("Data Source=D:\\Learn\\Angular\\ng8\\UserAdminService\\UserAdminService\\DB\\UserAdmin.db");
            //string stm = "select * from config";

            //var con = new SQLiteConnection(cs);
            //con.Open();

            //var cmd = new SQLiteCommand(stm, con);
            //var sqlReader = cmd.ExecuteReader();
            //while (sqlReader.Read()) {
            //    Console.WriteLine(sqlReader.GetString(0));
            //}

            DatabaseInfo dbInfo = new DatabaseInfo()
            {
                ConnectionString = @"Data Source=D:\Source\UserAdmin\API\UserAdminAPI\UserAdminService\DB\UserAdmin.db",
                ClassName = "DatabaseService.DBCore.SQLiteDatabase"
            };

            Database db = DatabaseKeeper.GetDatabase(dbInfo);
            string stm = "select * from config";
            var cmd = db.CreateCommand(stm, db.CreateOpenConnection());
            var sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine(sqlReader.GetString(0));
            }
        }
    }
}
