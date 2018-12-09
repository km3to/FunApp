using System.Linq;
using System.Threading.Tasks;
using FunApp.Models.ViewModels.Jokes;
using FunApp.Services.Contracts;
using FunApp.Web.ML.Contracts;
using FunApp.Web.Models.Jokes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FunApp.Web.Controllers
{
    public class JokesController : Controller
    {
        private readonly IJokeService jokeService;
        private readonly ICategoriesService categoriesService;
        private readonly IJokesCategorizer jokesCategorizer;

        public JokesController(
            IJokeService jokeService, 
            ICategoriesService categoriesService, 
            IJokesCategorizer jokesCategorizer)
        {
            this.jokeService = jokeService;
            this.categoriesService = categoriesService;
            this.jokesCategorizer = jokesCategorizer;
        }

        [Authorize]
        public IActionResult Create()
        {
            this.ViewData["Categories"] =
                this.categoriesService
                    .GetAll()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.NameAndCount
                    });

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateJokeViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var newJokeId = await this.jokeService.CreateAsync(viewModel.CategoryId, viewModel.Content);

            return this.RedirectToAction("Details", new { id = newJokeId });
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.jokeService.GetJokeById<JokeDetailsViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public SuggestCategoryResult SuggestCategory(string joke)
        {
            var category = this.jokesCategorizer.Categorize("ML/Models/JokesCategoryModel.zip", joke);
            var categoryId = this.categoriesService.GetCategoryId(category);

            return new SuggestCategoryResult
            {
                CategoryId = categoryId ?? 0,
                CategoryName = category
            };
        }
    }

    public class SuggestCategoryResult
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}