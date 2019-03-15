using AutoMapper;
using Domain.DB.Models;
using Domain.Site.Enum;
using Framework.EFData;
using System;
using System.Collections.Generic;

namespace Domain.Site.Models
{
    public class ProjectPointViewModel
    {
        public int? Id { get; set; }
        public int? ProjectTypeId { get; set; }
        public int? FormulaId { get; set; }
        public int? ProjectId { get; set; }
        public int? ProfessionalType { get; set; }
        public string PointName { get; set; }
        public int? Status { get; set; }
        public decimal? PointFund { get; set; }
        public string PonitContent { get; set; }
        // public ProjectPointStatus Status { get; set; }
        public string LeaderName { get; set; }
        public decimal? Budget { get; set; }
        public int? PointLeader { get; set; }
        public decimal? Commission { get; set; }
        public decimal? ManagementProportion { get; set; }
        public decimal? AuditProportion { get; set; }
        public decimal? JudgementProportion { get; set; }
        public decimal? PointProportion { get; set; }
        public string CreateTime { get; set; }
    }

    public class ProjectPointQueryModel : PageQuery
    {
        public int? ProjectId { get; set; }
        public string PointName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class ProjectPointModelMapping : IHaveMapping
    {
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ProjectPoint, ProjectPointViewModel>();
            configuration.CreateMap<ProjectCalculationViewModel, ProjectPointViewModel>();
        }
    }
}
