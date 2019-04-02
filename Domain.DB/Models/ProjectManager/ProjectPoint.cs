using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class ProjectPoint : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public ProjectPoint()
        {
            this.logs = new List<PointLog>();
            this.projectPointUserStores = new List<ProjectPointUserStore>();
        }
        public int? ProjectId { get; set; }
        public int? ProfessionalTypeId { get; set; }
        public string PointName { get; set; }
        public int? Status { get; set; }
        public string PonitContent { get; set; }
        public decimal? PointProportion { get; set; }
        public decimal? PointCommission { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public virtual Project project { get; set; }
        public virtual PointProfessionalType professionalType { get; set; }
        public virtual ICollection<ProjectPointUserStore> projectPointUserStores { get; set; }
        public virtual ICollection<PointLog> logs { get; set; }
    }
}
