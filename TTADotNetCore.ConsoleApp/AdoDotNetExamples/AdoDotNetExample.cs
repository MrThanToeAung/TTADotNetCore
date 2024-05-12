using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTADotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "HP",
            InitialCatalog = "TTADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
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

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogId =>" + dr["BlogID"]);
                Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
                Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
                Console.WriteLine("----------------------------");
            }

        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                            VALUES
                               (@BlogTitle, 
                               @BlogAuthor,
		                       @BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            Console.WriteLine(message);

        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Update Successfully" : "Update Failed";
            Console.WriteLine(message);

        }

        public void Delete(int blogId)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                            WHERE BlogID = @BlogID ";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogID", blogId);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Delete Successfully" : "Deleting Failed";
            Console.WriteLine(message);
        }

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"Select * from Tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine("BlogId =>" + dr["BlogID"]);
            Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
            Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
            Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
            Console.WriteLine("----------------------------");

        }
    }
}
