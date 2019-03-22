using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class ProjectFragment : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public decimal FragmentFund { get; set; }
        public decimal FragmentProportion { get; set; }
        public virtual ProjectType projectType { get; set; }
        public int? ProjectTypeId { get; set; }

        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
