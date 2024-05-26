using System.Data.SqlClient;

namespace TTADotNetCore.ConsoleApp.Services
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {

            DataSource = "HP",
            InitialCatalog = "TTADotNetCore",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}

