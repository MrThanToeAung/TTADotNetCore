namespace TTADotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _da_Blog;

        public BL_Blog()
        {
            _da_Blog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _da_Blog.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _da_Blog.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            var result = _da_Blog.CreateBlog(requestModel);
            return result;
        }

        public int PutBlog(int id, BlogModel requestModel)
        {
            var result = _da_Blog.PutBlog(id,requestModel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var result = _da_Blog.PatchBlog(id, requestModel);
            return result;
        }

        public int DeleteBlog(int id)
        {
            var result = _da_Blog.DeleteBlog(id);
            return result;
        }
    }
}
