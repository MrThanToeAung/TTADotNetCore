using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTADotNetCore.RestApi.Database;
using TTADotNetCore.RestApi.Models;

namespace TTADotNetCore.RestApi.Controllers
{
    //https://localhost:3000 => domain url
    // api/blog => endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var dataList = _context.Blogs.ToList();
            
            return Ok(dataList);    
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=> x.BlogID == id);
            if(item is null)
            {
                return NotFound( "No Data Found !!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            _context.Blogs.Add(blogModel);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Save Successfully" : "Saving Failed";            
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x=> x.BlogID == id);
            if(item is null)
            {
                return NotFound("No data Found!!");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return NotFound("No data Found!!");
            }

            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return NotFound("No data Found!!");
            }

            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete  Successfully" : "Deleting Failed";
            return Ok(message);
        }
    }
}
