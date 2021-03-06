﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using AngleSharp.Parser.Html;
using FunApp.Data;
using FunApp.Models.DbModels;
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

            for (int i = 21_400; i < 50_000; i++)
            {
                var url = "http://fun.dir.bg/vic_open.php?id=" + i;

                string html = null;
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        html = webClient.DownloadString(url);
                        break;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(10_000);
                    }
                }

                if (string.IsNullOrWhiteSpace(html))
                {
                    continue;
                }

                var document = parser.Parse(html);
                var jokeContent = document.QuerySelector("#newsbody")?.TextContent?.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(categoryName) && !string.IsNullOrWhiteSpace(jokeContent))
                {
                    var category = context.Categories.FirstOrDefault(c => c.Name == categoryName);

                    if (category == null)
                    {
                        category = new Category
                        {
                             Name = categoryName
                        };
                    }

                    var joke = new Joke
                    {
                        Category = category,
                        Content = jokeContent
                    };

                    context.Jokes.Add(joke);
                    context.SaveChanges();
                }

                Console.WriteLine($"{i} => {categoryName}");
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
