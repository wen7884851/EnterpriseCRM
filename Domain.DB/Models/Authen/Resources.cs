using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class Resources: EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int ResourcesType { get; set; }
        public int ResourcesId { get; set; }

        public int SystemId{ get; set; }
        public int? ModuleId { get; set; }
		public int? PermissionId { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
