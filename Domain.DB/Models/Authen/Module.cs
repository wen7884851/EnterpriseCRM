using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class Module : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public Module()
        {
			this.ChildModule = new List<Module>();
            this.RoleModules = new List<RoleModule>();
        }

		public int? ParentId { get; set; }

        public int? SystemID { get; set; }

        public string Sys_Gid { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; }

        public int? Layer { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }
        public int OrderSort { get; set; }
        public string Description { get; set; }
        public bool IsMenu { get; set; }
        public bool Enabled { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual Module ParentModule { get; set; }
        public virtual ICollection<Module> ChildModule { get; set; }   
        public virtual ICollection<RoleModule> RoleModules { get; set; }

    }
}
