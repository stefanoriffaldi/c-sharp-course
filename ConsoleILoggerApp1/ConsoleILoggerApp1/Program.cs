using Microsoft.Extensions.Logging;

namespace ConsoleILoggerApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole()); // code configuration, dont like  
            ILogger logger = factory.CreateLogger<Program>();
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
        }
    }
}
