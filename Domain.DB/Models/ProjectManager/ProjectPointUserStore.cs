using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class ProjectPointUserStore : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int? UserId { get; set; }
        public string StoreContent { get; set; }
        public decimal? UserProportion { get; set; }
        public int? ProjectPointId { get; set; }
        public virtual ProjectPoint projectPoint { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
