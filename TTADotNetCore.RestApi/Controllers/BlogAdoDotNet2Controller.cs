using Microsoft.AspNetCore.Mvc;
using TTADotNetCore.RestApi.Models;
using TTADotNetCore.Shared;

namespace TTADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "SELECT * FROM tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter
            //var item = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            //SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //sqlConnection.Open();
            //SqlCommand cmd = new SqlCommand(query, sqlConnection);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //SqlDataAdapter sqlDataAdapters = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sqlDataAdapters.Fill(dt);
            //sqlConnection.Close();

            if (item is null)
            {
                return NotFound("No Data Found");
            }

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
            var result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut]
        public IActionResult PutBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogID = @BlogID";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogID", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Update Successfully" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch]
        //public IActionResult PatchBlog(int id, BlogModel blog)
        //{
        //    List<AdoDotNetParameter> lst = new List<AdoDotNetParameter>();
        //    string conditions = string.Empty;
        //    if (!string.IsNullOrEmpty(blog.BlogTitle))
        //    {
        //        conditions += "[BlogTitle] = @BlogTitle, ";
        //        lst.Add("@BlogTitle", blog.BlogTitle);
        //    }
        //    if (!string.IsNullOrEmpty(blog.BlogAuthor))
        //    {
        //        conditions += "[BlogAuthor] = @BlogAuthor, ";
        //        lst.Add("@BlogAuthor", blog.BlogAuthor);
        //    }
        //    if (!string.IsNullOrEmpty(blog.BlogContent))
        //    {
        //        conditions += "[BlogContent] = @BlogContent, ";
        //        lst.Add("@BlogContent", blog.BlogContent);
        //    }

        //    if (conditions.Length == 0)
        //    {
        //        var response = new { IsSuccess = false, Message = "No data found." };
        //        return NotFound(response);
        //    }

        //    conditions = conditions.Substring(0, conditions.Length - 2);
        //    blog.BlogID = id;
        //    string query = $@"UPDATE [dbo].[Tbl_Blog]
        //                   SET {conditions}
        //                   WHERE BlogID = @BlogID";

        //    int result = _adoDotNetService.Execute(query,lst.ToArray());

        //    string message = result > 0 ? "Update Successfully" : "Update Failed";
        //    return Ok(message);
        //}

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM tbl_blog WHERE BlogId = @BlogId";

            var result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Delete Successfully" : "Deleting Failed";
            return Ok(message);
        }
    }
}
