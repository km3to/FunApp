using AutoMapper;
using FunApp.Common.Contracts;
using FunApp.Models.DbModels;

namespace FunApp.Models.ViewModels.Jokes
{
    public class IdAndNameViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAndCount => $"{this.Name} ({this.CountOfAllJokes})";

        public int CountOfAllJokes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, IdAndNameViewModel>()
                .ForMember(x => x.CountOfAllJokes, m => m.MapFrom(c => c.Jokes.Count));
        }
    }
}