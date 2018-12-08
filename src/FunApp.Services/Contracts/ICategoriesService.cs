using System.Collections.Generic;
using FunApp.Models.ViewModels.Jokes;

namespace FunApp.Services.Contracts
{
    public interface ICategoriesService
    {
        IEnumerable<IdAndNameViewModel> GetAll();

        bool IsCategoryValid(int categoryId);
    }
}