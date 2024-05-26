using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTADotNetCore.RestApi.Models;

namespace TTADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        //Read
        [HttpGet]
        private IActionResult GetBlog()
        {
            string query = "SELECT * FROM tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> dataList = db.Query<BlogModel>(query).ToList();

            return Ok(dataList);
        }

        [HttpGet("{id}")]
        private IActionResult GetBlog(int id)
        {
            //string query = "SELECT * FROM tbl_blog WHERE BlogID = @BlogID";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogID = id}).FirstOrDefault(); 
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found!!!");
            }
            return Ok(item);
        }

        [HttpPost]
        private IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                ([BlogTitle],
                                [BlogAuthor],
                                [BlogContent])
                           VALUES
                              (@BlogTitle, 
                                @BlogAuthor,
		                        @BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        private IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            blog.BlogID = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        private IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found!");
            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No Data To Update!!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogID = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {conditions}
                           WHERE BlogID = @BlogID";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        private IActionResult DeleteBlog(int id)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            string query = "DELETE FROM tbl_blog WHERE BlogID = @BlogID";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, new BlogModel { BlogID = id });
            string message = result > 0 ? "Delete Successfully" : "Deleting Failed";
            return Ok(message);
        }

        private BlogModel? FindByID(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogID = @BlogID";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogID = id }).FirstOrDefault();
            return item;
        }

    }
}
