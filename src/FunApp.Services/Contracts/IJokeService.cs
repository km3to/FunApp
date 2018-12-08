using System.Collections.Generic;
using FunApp.Models.ViewModels.Home;

namespace FunApp.Services.Contracts
{
    public interface IJokeService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);
    }
}