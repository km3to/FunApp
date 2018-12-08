using AutoMapper;
using FunApp.Common.Contracts;
using FunApp.Models.DbModels;

namespace FunApp.Models.ViewModels.Home
{
    public class IndexJokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Joke, IndexJokeViewModel>()
            //    .ForMember(x => x.CategoryName, x => x.MapFrom(j => j.Category.Name));
        }
    }
}