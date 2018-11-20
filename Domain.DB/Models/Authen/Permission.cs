﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class Permission : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public Permission()
        {
			this.ModulePermission = new List<ModulePermission>();
			this.Resources = new List<Resources>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderSort { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual ICollection<ModulePermission> ModulePermission { get; set; }
		public virtual ICollection<Resources> Resources { get; set; }
    }
}
