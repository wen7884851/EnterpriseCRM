using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Service.Authen;
using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;
using Framework.Tool.Operator;

namespace Core.Service
{
    [Export(typeof(IProjectPointManager))]
    public class ProjectPointManager : IProjectPointManager
    {
        [Import]
        private IProjectPointRepository _projectPointsRepository { get; set; }
        public IQueryable<ProjectPoint> projectPoints
        {
            get { return _projectPointsRepository.Entities; }
        }
        [Import]
        private IUserService _userService { get; set; }
        [Import]
        private IFormulaManager _formulaManager { get; set; }

        private IQueryable<ProjectPoint> GetCurrentUserProjectPoint()
        {
            var user = OperatorProvider.Provider.GetCurrent();
            return projectPoints.Where(t => t.UserId == user.UserId);
        }

        public PageResult<ProjectPointViewModel> GetProjectPointByQuery(ProjectPointQueryModel queryModel)
       {
            var expr = BuildSearchProject(queryModel);
            var projectPointDTO= expr != null ? GetCurrentUserProjectPoint().Where(expr) : GetCurrentUserProjectPoint();
            var result = new PageResult<ProjectPointViewModel>()
            {
                Items= projectPointDTO.Select(t=>new ProjectPointViewModel() {
                    Id=t.Id,
                    ProjectId=t.ProjectId,
                    PointName=t.PointName,
                    Budget=t.Budget,
                    FormulaId=t.FormulaId,
                    Tax =t.Tax,
                    PointFund=t.PointFund,
                    UserId=t.UserId,
                    UserName =_userService.Users.FirstOrDefault(u=>u.Id==t.UserId).LoginName,
                    UserTax =t.UserTax,
                    Commission=t.Commission,
                    CreateTime=t.CreateTime
                }),
                TotalItemsCount= projectPointDTO.Count()
            };
            return result;
       }

        public int CreateProjectPoint(ProjectPointViewModel point)
        {
            if(point.formulaItems.Any())
            {
                var projectPointDTO = new ProjectPoint()
                {
                    Commission = _formulaManager.CalculateCommission(point.formulaItems),
                    FormulaId = point.FormulaId,
                    ProjectId = point.ProjectId,
                    PointName = point.PointName,
                    PointFund = point.PointFund,
                    UserId = point.UserId,
                };
                projectPointDTO.Create();

                foreach (var item in point.formulaItems)
                {
                    _formulaManager.CreateFormulaItem(item);
                }
                _projectPointsRepository.Insert(projectPointDTO);
            }
            return 0;
        }

        public IEnumerable<ProjectPoint> GetPointListByProjectId(int projectId)
        {
            throw new NotImplementedException();
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
                tmp = t => t.UserId == model.UserId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                int userId = _userService.Users.FirstOrDefault(t => t.LoginName == model.UserName).Id;
                tmp = t => t.UserId == userId;
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
