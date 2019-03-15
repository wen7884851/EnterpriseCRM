using AutoMapper;

namespace Domain.Site
{
    public interface IHaveMapping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
