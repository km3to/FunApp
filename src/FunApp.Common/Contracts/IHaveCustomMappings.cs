using AutoMapper;

namespace FunApp.Common.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}