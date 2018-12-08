using System.Collections.Generic;
using System.Threading.Tasks;
using FunApp.Models.ViewModels.Home;
using FunApp.Models.ViewModels.Jokes;

namespace FunApp.Services.Contracts
{
    public interface IJokeService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();

        Task<int> CreateAsync(int categoryId, string content);

        JokeDetailsViewModel GetJokeById(int id);
    }
}