using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class UserProfile : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string IDCardNo { get; set; }
        public string Aptitude { get; set; }
        public string Education { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string Age { get; set; }
        public int? Sex { get; set; }
        public string Motto { get; set; }
        public DateTime? InductionDate { get; set; }
        public DateTime? PositiveDate { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public User Users { get; set; }
    }
}
