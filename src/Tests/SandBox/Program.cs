using System;
using System.IO;
using System.Net;
using System.Text;
using AngleSharp.Parser.Html;
using FunApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SandBox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);
            
            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                SandBoxCode(serviceProvider);
            }
        }

        private static void SandBoxCode(IServiceProvider serviceProvider)
        {
            // TODO: Code here
            var context = serviceProvider.GetService<FunAppContext>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var parser = new HtmlParser();
            var webClient = new WebClient { Encoding = Encoding.GetEncoding("windows-1251") };

            for (int i = 3000; i < 10_000; i++)
            {
                var url = "http://fun.dir.bg/vic_open.php?id=" + i;
                var html = webClient.DownloadString(url);
                var document = parser.Parse(html);
                var jokeContent = document.QuerySelector("#newsbody")?.TextContent?.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(categoryName) && !string.IsNullOrWhiteSpace(jokeContent))
                {
                    Console.WriteLine(jokeContent);
                    Console.WriteLine(categoryName);
                    Console.WriteLine(new string('=', 100));
                }
            }
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddDbContext<FunAppContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
