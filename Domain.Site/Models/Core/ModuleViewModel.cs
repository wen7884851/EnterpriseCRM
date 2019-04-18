using AutoMapper;
using Domain.DB.Models;

namespace Domain.Site.Models
{
    public class ModuleViewModel
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; }
        public int Layer { get; set; }
        public string Icon { get; set; }
        public int OrderSort { get; set; }
        public string Description { get; set; }
    }

    public class ModuleViewModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Module, ModuleViewModel>();
            configuration.CreateMap<ModuleViewModel, Module>();
        }
    }
}
