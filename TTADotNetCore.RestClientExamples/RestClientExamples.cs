using Newtonsoft.Json;
using RestSharp;
using TTADotNetCore.RestClientExamples;

namespace TTADotNetCore.ConsoleAppRestClientExamples
{
    internal class RestClientExamples
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7281/"));
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(2);
            //await EditAsync(100);
            //await CreateAsync("TTA", "TTA", "TTA");
            //await PutAsync(1,"TTA", "TTA", "TTA");
        }

        private async Task ReadAsync()
        {
            //RestRequest restRequest = new RestRequest(_blogEndPoint);
            //var response = await _client.GetAsync(restRequest);

            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                List<BlogDto> blogList = JsonConvert.DeserializeObject<List<BlogDto>>(jsonString)!;
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
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                BlogDto blog = JsonConvert.DeserializeObject<BlogDto>(jsonString)!;
                Console.WriteLine(JsonConvert.SerializeObject(blog));
                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Author => {blog.BlogAuthor} ");
                Console.WriteLine($"Content => {blog.BlogContent}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            var restRequest = new RestRequest(_blogEndPoint, Method.Post);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
