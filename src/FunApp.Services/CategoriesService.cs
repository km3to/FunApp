using System.Collections.Generic;
using System.Linq;
using FunApp.Data;
using FunApp.Models.ViewModels.Jokes;
using FunApp.Services.Contracts;

namespace FunApp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly FunAppContext db;

        public CategoriesService(FunAppContext db)
        {
            this.db = db;
        }

        public IEnumerable<IdAndNameViewModel> GetAll()
        {
            return
                this.db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new IdAndNameViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
        }

        public bool IsCategoryValid(int categoryId) => this.db.Categories.Any(c => c.Id == categoryId);
    }
}