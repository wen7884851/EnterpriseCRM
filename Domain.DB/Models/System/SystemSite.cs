//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Tool.Entity;

namespace Domain.DB.Models.System
{
    public class SystemSite : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public SystemSite()
        {
            this.Module=new List<Module>();
        }

        public string SystemName { get; set; }

        public string Gid { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Code { get; set; }

        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual ICollection<Module> Module { get; set; }
    }
}
