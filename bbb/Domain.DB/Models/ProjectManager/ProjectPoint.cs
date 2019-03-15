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
        }
        public int FormulaId { get; set; }
        public int? ProjectId { get; set; }
        public string PointName { get; set; }
        public int Status { get; set; }
        public decimal? PointFund { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Tax { get; set; }
        public int? UserId { get; set; }
        public decimal? UserTax { get; set; }
        public decimal? Commission { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Project project { get; set; }
        public ICollection<PointLog> logs { get; set; }
    }
}
