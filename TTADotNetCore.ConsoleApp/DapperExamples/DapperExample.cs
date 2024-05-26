using Dapper;
using System.Data;
using System.Data.SqlClient;
using TTADotNetCore.ConsoleApp.Dtos;
using TTADotNetCore.ConsoleApp.Services;

namespace TTADotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Create("Title 7", "Author 7", "Content 7");
            //Update(1003,"Title 1003", "Author 1003", "Content 1003");
            //Delete(1003);
            Delete(1004);

        }

        private void Read()
        {
            //using (IDbConnection db1 = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString)) {
            //    db1.Open();
            //};
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> datalist = dbConnection.Query<BlogDto>("SELECT * FROM Tbl_Blog").ToList();

            foreach (BlogDto item in datalist)
            {
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------------");
            }
        }

        private void Edit(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = dbConnection.Query<BlogDto>("SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId ", new BlogDto { BlogID = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(item.BlogID);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------------");
        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                            VALUES
                               (@BlogTitle, 
                                @BlogAuthor,
		                        @BlogContent)";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogID = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogID = @BlogID";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogID = id
            };

            string query = "DELETE FROM Tbl_Blog WHERE BlogID = @BlogID";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Delete Successfully" : "Deleting Failed";
            Console.WriteLine(message);
        }
    }
}
