using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FunApp.Data;
using FunApp.Models.DbModels;
using Xunit;

namespace FunApp.Services.Tests
{
    public class JokeServiceTests
    {
        [Fact]
        public async Task GetCountReturnsCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<FunAppContext>()
                .UseInMemoryDatabase(databaseName: "WhateverDb")
                .Options;

            var db = new FunAppContext(options);

            db.Jokes.Add(new Joke());
            db.Jokes.Add(new Joke());
            db.Jokes.Add(new Joke());
            db.Jokes.Add(new Joke());
            db.Jokes.Add(new Joke());

            await db.SaveChangesAsync();

            var jokesService = new JokeService(db);
            var expectedCount = jokesService.GetCount();

            Assert.Equal(5, expectedCount);
        }
    }
}
