using System;
using AutoMapper;
using FunApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FunApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = this.categoriesService.GetAll();

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            throw new NotImplementedException();
        }
    }
}