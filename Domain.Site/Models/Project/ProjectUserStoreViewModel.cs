using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DB.Models;

namespace Domain.Site.Models
{
    public class ProjectUserStoreViewModel
    {
        public int? Id { get; set; }
        public int ProjectPointId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string StoreContent { get; set; }
        public decimal? StoreFund { get; set; }
        public decimal? ProjectPointProportion { get; set; }
        public string CreateTime { get; set; }
        public bool DeleteItem { get; set; }
    }

    public class ProjectUserStoreQueryModel : PageQuery
    {
        public int? PointId { get; set; }
        public int? UserId { get; set; }
    }


    public class ProjectUserStoreModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ProjectPointUserStore, ProjectUserStoreViewModel>();
        }
    }

}
