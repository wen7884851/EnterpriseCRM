using AutoMapper;
using Domain.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Core
{
    public class RoleViewModel
    {
        public int? RoleId { get; set; }
        public string Name { get; set; }
        public int[] RoleModuleConfiguration { get; set; }
        public int UserCount { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }

    public class RoleQueryModel : PageQuery
    {
        public string Name { get; set; }
    }

    public class RoleViewModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Role, RoleViewModel>().ForMember(d => d.UserCount, opt => { opt.MapFrom(s => s.Users.Count); })
                .ForMember(d=>d.RoleId,opt=>{ opt.MapFrom(s => s.Id); });
            configuration.CreateMap<RoleViewModel, Role>();
        }
    }
}
