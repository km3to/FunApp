using System.Diagnostics;
using FunApp.Models;
using FunApp.Models.ViewModels.Home;
using FunApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJokeService jokeService;

        public HomeController(IJokeService jokeService)
        {
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
