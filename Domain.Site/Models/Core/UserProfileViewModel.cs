using AutoMapper;
using Domain.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string IDCardNo { get; set; }
        public string Aptitude { get; set; }
        public string Education { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public string PhotoPath { get; set; }
        public int? Sex { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string Motto { get; set; }
        public DateTime? InductionDate { get; set; }
        public DateTime? PositiveDate { get; set; }
    }
    public class UserProfileViewModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UserProfile, UserProfileViewModel>();
            configuration.CreateMap<UserProfileViewModel, UserProfile>();
        }
    }
}
