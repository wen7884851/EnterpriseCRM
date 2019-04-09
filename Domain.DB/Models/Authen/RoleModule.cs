using Framework.Tool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Models
{
    public class RoleModule : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public virtual Role Role { get; set; }
        public virtual Module Module { get; set; }
    }
}
