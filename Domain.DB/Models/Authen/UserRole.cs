using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Framework.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class UserRole : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
