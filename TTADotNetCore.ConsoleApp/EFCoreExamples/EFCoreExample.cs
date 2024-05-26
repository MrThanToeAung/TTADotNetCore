using TTADotNetCore.ConsoleApp.Dtos;

namespace TTADotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(100);
            //Create("TTA", "TTA", "TTA");
            //Update(200, "NLM", "NLM", "NLM");
            Delete(1005);
        }

        private void Read()
        {
            var dataList = db.Blogs.ToList();

            foreach (BlogDto item in dataList)
            {
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------------");
            }
        }

        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogID == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!!!");
                return;
            }
            Console.WriteLine(item.BlogID);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------------");

        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(data => data.BlogID == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!!");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();
            string message = result > 0 ? "Saving Successfully" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(data => data.BlogID == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!!");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successfully" : "Deleting Failed";
            Console.WriteLine(message);
        }
    }
}
