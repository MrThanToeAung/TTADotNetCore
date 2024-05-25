using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TTADotNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7281/") };
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(2);
            //await EditAsync(100);
            //await CreateAsync("TTA", "TTA", "TTA");
            //await PutAsync(1,"TTA", "TTA", "TTA");

        }

        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<BlogModel> blogList = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString)!;
                foreach (var blog in blogList)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"Title => {blog.BlogTitle}");
                    Console.WriteLine($"Author => {blog.BlogAuthor} ");
                    Console.WriteLine($"Content => {blog.BlogContent}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                BlogModel blog = JsonConvert.DeserializeObject<BlogModel>(jsonString)!;
                Console.WriteLine(JsonConvert.SerializeObject(blog));
                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Author => {blog.BlogAuthor} ");
                Console.WriteLine($"Content => {blog.BlogContent}");
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            //C# object
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            //C# to json
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(_blogEndPoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task PutAsync(int id, string title, string author, string content)
        {
            //C# object
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            //C# to json
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                //To do more process
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                //send error message and  break
            }
        }
    }
}
