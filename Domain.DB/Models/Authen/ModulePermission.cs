﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Framework.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class ModulePermission : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int ModuleId { get; set; }
        public int PermissionId { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual Module Module { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
