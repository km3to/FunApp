using System;
using System.Collections.Generic;
using System.Linq;
using FunApp.Data;
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
            var result =
                this.db.Jokes
                    .OrderBy(j => Guid.NewGuid())
                    .Take(20)
                    .Select(j => new IndexJokeViewModel
                    {
                        Content = j.Content,
                        CategoryName = j.Category.Name
                    });

            return result;
        }
    }
}