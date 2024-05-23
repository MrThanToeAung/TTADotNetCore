using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TTADotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_Blog;

        public BlogController()
        {
            _bl_Blog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var dataList = _bl_Blog.GetBlogs();

            return Ok(dataList);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found !!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            var result = _bl_Blog.CreateBlog(blogModel);

            string message = result > 0 ? "Save Successfully" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data Found!!");
            }

            var result = _bl_Blog.PutBlog(id, blog);

            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        { 
            var result = _bl_Blog.PatchBlog(id, blog);

            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data Found!!");
            }

            var result = _bl_Blog.DeleteBlog(id);

            string message = result > 0 ? "Delete  Successfully" : "Deleting Failed";
            return Ok(message);
        }
    }
}
