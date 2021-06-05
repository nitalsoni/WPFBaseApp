using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService
{
    public static class Helper
    {

        public static string ReadBlob(object data)
        {
            if (data == null)
                return string.Empty;

            return data.ToString();
        }

        public static string DefaultUser
        {
            get
            {
                return "System";
            }
        }

    }
}
