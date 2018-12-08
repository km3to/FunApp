using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Common;
using FunApp.Data;
using FunApp.Models.DbModels;
using FunApp.Models.ViewModels.Home;
using FunApp.Services.Contracts;

namespace FunApp.Services
{
    public class JokeService : IJokeService
    {
        private readonly FunAppContext db;

        public JokeService(FunAppContext db)
        {
            this.db = db;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var result = this.db.Jokes
                .OrderBy(j => Guid.NewGuid())
                .Take(count)
                .To<IndexJokeViewModel>()
                .ToList();

            return result;
        }

        public int GetCount()
        {
            return this.db.Jokes.Count();
        }

        public async Task<int> CreateAsync(int categoryId, string content)
        {
            var joke = new Joke
            {
                CategoryId = categoryId,
                Content = content
            };

            await this.db.Jokes.AddAsync(joke);
            await this.db.SaveChangesAsync();

            return joke.Id;
        }

        public TViewModel GetJokeById<TViewModel>(int id)
        {
            var result = this.db.Jokes
                .Where(j => j.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();

            return result;
        }
    }
}