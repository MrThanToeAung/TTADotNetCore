using TTADotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello, World!");

//Console App -> Client (Frontend)
//Asp.net Core Web Api -> Server (Backend)

HttpClientExample httpCleint = new HttpClientExample();
await httpCleint.RunAsync();

Console.ReadLine();