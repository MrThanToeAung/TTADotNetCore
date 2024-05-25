// See https://aka.ms/new-console-template for more information
using TTADotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");

RestClientExamples restClientExample = new RestClientExamples();
await restClientExample.RunAsync();
