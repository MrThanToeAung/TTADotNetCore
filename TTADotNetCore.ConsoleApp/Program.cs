// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "HP"; //server name
stringBuilder.InitialCatalog = "TTADotNetCore"; //Database name
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";

SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//SqlConnection connection = new SqlConnection("Data Source=HP;Initial Catalog=TTADotNetCore;User ID=sa;Password=sa@123");
connection.Open();
Console.WriteLine("Connection Opened");

string query = "select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapters = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();

sqlDataAdapters.Fill(dt);

connection.Close();
Console.WriteLine("Connection Closed");

// dataset =>> datatable => datarow => datacolumn

foreach(DataRow dr in dt.Rows){
    Console.WriteLine("BlogId =>" + dr["BlogID"]);
    Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
    Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
    Console.WriteLine("----------------------------");
}

//Ado.net Read

Console.ReadKey();