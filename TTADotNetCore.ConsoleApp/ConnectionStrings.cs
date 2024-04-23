using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTADotNetCore.ConsoleApp
{    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder() {

            DataSource = "HP",
            InitialCatalog = "TTADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
    }
}

