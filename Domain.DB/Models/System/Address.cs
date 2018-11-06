using Core.Tool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Models.System
{
    public class Address : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual Area Area { get; set; }
        public virtual int? CreateId { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual int? ModifyId { get; set; }

        public virtual string ModifyBy { get; set; }

        public virtual DateTime? ModifyTime { get; set; }

    }
}
