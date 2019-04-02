using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class PointProfessionalType : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public PointProfessionalType()
        {
            this.points=new List<ProjectPoint>();
        }
        public virtual ICollection<ProjectPoint> points { get; set; }
        public string TypeName { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
