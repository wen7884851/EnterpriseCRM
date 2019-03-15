using System;
using System.Collections.Generic;
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
            this.projectType = new ProjectType();
            this.projectPointUserStores = new List<ProjectPointUserStore>();
        }
        public int? ProjectTypeId { get; set; }
        public int? FormulaId { get; set; }
        public int? ProjectId { get; set; }
        public int? ProfessionalType { get; set; }
        public string PointName { get; set; }
        public int? Status { get; set; }
        public decimal? PointFund { get; set; }
        public string PonitContent { get; set; }
        public decimal? Budget { get; set; }
        public int? PointLeader { get; set; }
        public decimal? Commission { get; set; }
        public decimal? ManagementProportion { get; set; }
        public decimal? AuditProportion { get; set; }
        public int Auditer { get; set; }
        public decimal? JudgementProportion { get; set; }
        public int Judgementer { get; set; }
        public decimal? PointProportion { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Project project { get; set; }
        public ProjectType projectType { get; set; }
        public  ICollection<ProjectPointUserStore> projectPointUserStores { get; set; }
        public ICollection<PointLog> logs { get; set; }
    }
}
