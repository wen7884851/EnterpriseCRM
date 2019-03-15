using Domain.Site.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Site.Models
{
    public class ProjectPointViewModel
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int FormulaId { get; set; }
        public string PointName { get; set; }
        public ProjectPointStatus Status { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Tax { get; set; }
        public decimal? PointFund { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public decimal? UserTax { get; set; }
        public decimal? Commission { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<FormulaItemViewModel> formulaItems { get; set; }
    }

    public class ProjectPointQueryModel
    {
        public int? ProjectId { get; set; }
        public string PointName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
