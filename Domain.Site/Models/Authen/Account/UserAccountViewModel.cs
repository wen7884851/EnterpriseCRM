using AutoMapper;
using Domain.DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class UserAccountViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string NewLoginPwd { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public int? PwdErrorCount { get; set; }
        public bool? IsRemeberUserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }

    public class UserAccountViewModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserAccountViewModel>();
            configuration.CreateMap<UserAccountViewModel, User>();
        }
    }
}
