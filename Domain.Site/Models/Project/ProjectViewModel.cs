using AutoMapper;
using Domain.DB.Models;

namespace Domain.Site.Models
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectLeader { get; set; }
        public string LeaderName { get; set; }
        public string Content { get; set; }
        public string LinkPerson { get; set; }
        public string LinkPhoneNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        [IgnoreMap]
        public string CreateTime { get; set; }
    }

    public class ProjectSerchModel:DataTableParameter
    {
        public int? projectId { get; set; }
        public string projectName { get; set; }
    }

    public class ProjectModelMapping:IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Domain.DB.Models.Project, ProjectViewModel>();
        }
    }
}
