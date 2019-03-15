using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Core.Service.Authen;
using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;
using Framework.EFData.DBExtend;
using Framework.Tool;
using Framework.Tool.Operator;

namespace Core.Service.ProjectManager.Impl
{
    [Export(typeof(IProjectPointManager))]
    public class ProjectPointManager : IProjectPointManager
    {
        [Import]
        private IProjectPointRepository _projectPointsRepository { get; set; }
        public IQueryable<ProjectPoint> projectPoints
        {
            get { return _projectPointsRepository.NoCahecEntities; }
        }
        [Import]
        private IProjectTypeRepository _projectTypeRepository { get; set; }
        [Import]
        private IUserService _userService { get; set; }
        [Import]
        private IFormulaManager _formulaManager { get; set; }
        [Import]
        private IProjectCalculationFormula _projectCalculationFormula { get; set; }

        private IQueryable<ProjectPoint> GetCurrentUserProjectPoint()
        {
            var user = OperatorProvider.Provider.GetCurrent();
            return projectPoints.Where(t => t.PointLeader == user.UserId);
        }

        public PageResult<ProjectPointViewModel> GetProjectPointListByQuery(ProjectPointQueryModel queryModel)
        {
            var expr = BuildSearchProject(queryModel);
            var projectPointDTO= projectPoints.Where(expr).AsEnumerable();
            var Items = projectPointDTO.Select(t => new ProjectPointViewModel()
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                PointName = t.PointName,
                Budget = t.Budget,
                FormulaId = t.FormulaId,
                PointFund = t.PointFund,
                PonitContent = t.PonitContent,
                LeaderName = _userService.Users.FirstOrDefault(u => u.Id == t.PointLeader).LoginName,
                Commission = t.Commission,
                CreateTime = t.CreateTime.Value.ToLocalTime().ToString()
            }).OrderByDescending(t=>t.CreateTime).ToList();
            var result = new PageResult<ProjectPointViewModel>()
            {
                Items= Items,
                TotalItemsCount= Items.Count()
            };
            return result;
        }

        public ActionResultViewModel CreateProjectPoint(ProjectPointViewModel point)
        {
            var projectPointDTO = Mapper.Map<ProjectPoint>(point);
            var result = new ActionResultViewModel()
            {
                IsSuccess=false
            };
            if(projectPointDTO!=null&& projectPointDTO.ProjectId!=null)
            {
                try
                {
                    var calculation = Mapper.Map<ProjectCalculationViewModel>(point);
                    projectPointDTO.Commission = _projectCalculationFormula.CommonCalculationCommission(calculation);
                    projectPointDTO.projectType = _projectTypeRepository.GetByKey(point.ProjectTypeId.Value);
                    using (UnitOfWork tran = new UnitOfWork())
                    {
                        projectPointDTO.Create();
                        result.Result = _projectPointsRepository.Insert(projectPointDTO);
                        tran.Commit();
                        result.IsSuccess = true;
                    }
                }
                catch(Exception ex)
                {
                    result.IsSuccess = false;
                    result.Result = ex.Message;
                    return result;
                }
            }
            return result;
        }

        public ActionResultViewModel UpdateProjectPoint(ProjectPointViewModel point)
        {
            var result = CheckUpdatePoint(point);
            if (result.IsSuccess)
            {
                var pointDTO = projectPoints.FirstOrDefault(t => t.Id == point.Id);
                UpdateItemMap(point, pointDTO);
                using (UnitOfWork tran = new UnitOfWork())
                {
                    _projectPointsRepository.Update(pointDTO);
                }
            }
            return result;
        }

        private void UpdateItemMap(ProjectPointViewModel pointViewModel, ProjectPoint pointDTO)
        {
            pointDTO.ProjectTypeId = pointViewModel.ProjectTypeId?? pointDTO.ProjectTypeId;
            pointDTO.ProfessionalType = pointViewModel.ProfessionalType ?? pointViewModel.ProfessionalType;
            pointDTO.PointName = pointViewModel.PointName ?? pointDTO.PointName;
            pointDTO.PointFund = pointViewModel.PointFund?? pointDTO.PointFund;
            pointDTO.PonitContent = pointViewModel.PonitContent?? pointDTO.PonitContent;
            pointDTO.Budget = pointViewModel.Budget?? pointDTO.Budget;
            pointDTO.PointLeader = pointViewModel.PointLeader?? pointDTO.PointLeader;
            pointDTO.Commission = pointViewModel.Commission?? pointDTO.Commission;
            pointDTO.ManagementProportion = pointViewModel.ManagementProportion?? pointDTO.ManagementProportion;
            pointDTO.AuditProportion = pointViewModel.AuditProportion?? pointDTO.AuditProportion;
            pointDTO.JudgementProportion = pointViewModel.JudgementProportion?? pointDTO.JudgementProportion;
            pointDTO.PointProportion = pointViewModel.PointProportion?? pointDTO.PointProportion;
            pointDTO.Modify();
        }

        private ActionResultViewModel CheckUpdatePoint(ProjectPointViewModel point)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = true
            };
            if(point.Id==0)
            {
                result.IsSuccess = false;
                result.Result = "PointId is Null Or Empty";
            }
            return result;
        }

        public IEnumerable<ProjectPoint> GetPointListByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        public ProjectPointViewModel GetPointById(int pointId)
        {
            var point = projectPoints.FirstOrDefault(t=>t.Id== pointId);
            return Mapper.Map<ProjectPointViewModel>(point);
        }

        public int[] GetPointUserId(int pointId)
        {
            var point = projectPoints.FirstOrDefault(t => t.Id == pointId);
            if (point != null)
            {
                return point.projectPointUserStores.Where(t => (t.IsDeleted == false) && t.UserId != null)
                    .Select(t => t.UserId.Value).AsEnumerable().ToArray();
            }
            return null;
        }

        public decimal getPointSurplusMoney(int pointId)
        {
            var point = projectPoints.FirstOrDefault(t => t.Id == pointId);
            if (point != null)
            {
                decimal occupiedFund = 0;
                foreach(var userItem in point.projectPointUserStores)
                {
                    occupiedFund += userItem.StoreFund.Value;
                }
                return (point.PointFund.Value-occupiedFund);
            }
            return 0;
        }

        public int[] GetUserIdsByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<ProjectPoint, bool>> BuildSearchProject(ProjectPointQueryModel model)
        {
            var bulider = new DynamicLambda<ProjectPoint>();
            Expression<Func<ProjectPoint, bool>> expr = t => t.IsDeleted == false;
            Expression<Func<ProjectPoint, bool>> tmp;
            if (model.ProjectId.HasValue)
            {
                tmp = t => t.ProjectId == model.ProjectId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (!string.IsNullOrEmpty(model.PointName))
            {
                tmp = t => t.PointName == model.PointName;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (model.UserId.HasValue)
            {
                tmp = t => t.PointLeader == model.UserId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                int userId = _userService.Users.FirstOrDefault(t => t.LoginName == model.UserName).Id;
                tmp = t => t.PointLeader == userId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if(model.StartTime!=null)
            {
                tmp = t => t.CreateTime >= model.StartTime;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (model.EndTime != null)
            {
                tmp = t => t.CreateTime <= model.EndTime;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }
    }
}
