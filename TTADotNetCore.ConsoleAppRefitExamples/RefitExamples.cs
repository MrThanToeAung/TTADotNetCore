using Refit;

namespace TTADotNetCore.ConsoleAppRefitExamples;

public class RefitExample
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7232");
    public async Task RunAsync()
    {
        await ReadAsync();
        //await EditAsync(1);
        //await EditAsync(100);
        //await CreateAsync("TTA", "TTA", "TTA");
        //await UpdateAsync(1, "TTA", "TTA", "TTA");
        await DeleteAsync(2);
        await ReadAsync();
    }

    private async Task ReadAsync()
    {
        var blogList = await _service.GetBlogs();
        foreach (var blog in blogList)
        {
            Console.WriteLine($"ID => {blog.BlogID}");
            Console.WriteLine($"Title => {blog.BlogTitle}");
            Console.WriteLine($"Author => {blog.BlogAuthor} ");
            Console.WriteLine($"Content => {blog.BlogContent}");
            Console.WriteLine($"--------------------------------");
        }
    }

    private async Task EditAsync(int id)
    {
        try
        {
            var blog = await _service.GetBlog(id);
            Console.WriteLine($"ID => {blog.BlogID}");
            Console.WriteLine($"Title => {blog.BlogTitle}");
            Console.WriteLine($"Author => {blog.BlogAuthor} ");
            Console.WriteLine($"Content => {blog.BlogContent}");
            Console.WriteLine($"--------------------------------");
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blog = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var message = _service.CreateBlog(blog);
        Console.WriteLine(message);
    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        BlogModel blog = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var message = _service.UpdateBlog(id, blog);
        Console.WriteLine(message);
    }
    private async Task DeleteAsync(int id)
    {
        var message = _service.DeleteBlog(id);
        Console.WriteLine(message);
    }
}
