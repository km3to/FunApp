using System;
using System.Diagnostics;
using System.Linq;
using FunApp.Data;
using FunApp.Models.ViewModels.Home;
using FunApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using FunApp.Web.Models;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJokeService jokeService;
        private readonly FunAppContext db;

        public HomeController(FunAppContext db, IJokeService jokeService)
        {
            this.db = db;
            this.jokeService = jokeService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Jokes = this.jokeService.GetRandomJokes(20)
            };

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            var viewModel = this.db.Jokes.Count();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
