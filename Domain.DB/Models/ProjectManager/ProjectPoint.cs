using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class ProjectPoint : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int ProjectId { get; set; }
        public string PointName { get; set; }
        public int Status { get; set; }
        public decimal Budget { get; set; }
        public decimal Tax { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
