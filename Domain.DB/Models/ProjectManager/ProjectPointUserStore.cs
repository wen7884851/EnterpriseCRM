using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class ProjectPointUserStore : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public ProjectPointUserStore()
        {
            this.projectPoint = new ProjectPoint();
        }
        public int? UserId { get; set; }
        public string StoreContent { get; set; }
        public decimal? StoreFund { get; set; }
        public decimal? ProjectPointProportion { get; set; }
        public int? ProjectPointId { get; set; }
        public ProjectPoint projectPoint { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
