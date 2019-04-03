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
        public decimal ContractMoney { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? ManagementProportion { get; set; }
        public int? Managementer { get; set; }
        public decimal? AuditProportion { get; set; }
        public int? Auditer { get; set; }
        public decimal? JudgementProportion { get; set; }
        public int? Judgementer { get; set; }
        public decimal? CommissionProportion { get; set; }
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
            configuration.CreateMap<ProjectViewModel, Domain.DB.Models.Project>();
        }
    }
}
