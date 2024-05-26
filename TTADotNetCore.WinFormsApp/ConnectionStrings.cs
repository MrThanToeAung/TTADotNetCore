using System.Data.SqlClient;

namespace TTADotNetCore.WinFormsApp;

public static class ConnectionStrings
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "HP",
        InitialCatalog = "TTADotNetCore",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true
    };
}

