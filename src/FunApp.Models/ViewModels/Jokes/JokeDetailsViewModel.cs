using FunApp.Common.Contracts;
using FunApp.Models.DbModels;

namespace FunApp.Models.ViewModels.Jokes
{
    public class JokeDetailsViewModel : IMapFrom<Joke>
    {
        public string Content { get; set; }

        public string CategoryName { get; set; }
    }
}