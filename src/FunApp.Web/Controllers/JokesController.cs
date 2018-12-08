using System.Linq;
using System.Threading.Tasks;
using FunApp.Services.Contracts;
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

        public JokesController(IJokeService jokeService, ICategoriesService categoriesService)
        {
            this.jokeService = jokeService;
            this.categoriesService = categoriesService;
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
            var viewModel = this.jokeService.GetJokeById(id);

            return this.View(viewModel);
        }
    }
}