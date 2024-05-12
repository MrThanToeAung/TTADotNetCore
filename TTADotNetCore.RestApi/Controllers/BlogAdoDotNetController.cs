using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTADotNetCore.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TTADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "SELECT * FROM tbl_blog";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapters = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapters.Fill(dt);
            connection.Close();

            //List<BlogModel> blogList = new List<BlogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    //BlogModel blog = new BlogModel();
            //    //blog.BlogID = Convert.ToInt32(dr["BlogID"]);
            //    //blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    //blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    //blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //    BlogModel blog = new BlogModel
            //    {
            //        BlogID = Convert.ToInt32(dr["BlogID"]),
            //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //        BlogContent = Convert.ToString(dr["BlogContent"])
            //    };
            //    blogList.Add(blog);
            //}

            List<BlogModel> blogList = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogID = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(blogList);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapters = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapters.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogID = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };            
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                            VALUES
                               (@BlogTitle, 
                               @BlogAuthor,
		                       @BlogContent)";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            return Ok(message);
            //return StatusCode(200, message);
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = GetBlog(id);
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

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Update Successfully" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found to update!");
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

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            cmd.Parameters.AddWithValue("@BlogID", id);

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor)){
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Update Successfully" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            var item = GetBlog(id);
            if(item is null)
            {
                return NotFound("No Data Found");
            }

            string query = @"DELETE FROM tbl_blog WHERE BlogId = @BlogId";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            var result = cmd .ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Delete Successfully" : "Deleting Failed";
            return Ok(message);
        }
    }
}
