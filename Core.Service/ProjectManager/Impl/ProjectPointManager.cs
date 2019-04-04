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
        [Import]
        private IPointProfessionalTypeRepository _pointProfessionalTypeRepository { get; set; }
        public IQueryable<ProjectPoint> projectPoints
        {
            get { return _projectPointsRepository.NoCahecEntities; }
        }
        public IQueryable<PointProfessionalType> pointProfessionalTypes
        {
            get { return _pointProfessionalTypeRepository.NoCahecEntities; }
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
                PointProportion = t.PointProportion,
                PointContent = t.PonitContent,
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
            var result = new ActionResultViewModel()
            {
                IsSuccess=false
            };
            if((point != null)&& point.ProjectId!=null)
            {
                var projectPointDTO = Mapper.Map<ProjectPoint>(point);
                try
                {
                    using (UnitOfWork tran = new UnitOfWork())
                    {
                        projectPointDTO.Status = 1;
                        projectPointDTO.Create();
                        _projectPointsRepository.Insert(projectPointDTO);
                        tran.Commit();
                        result.Result = projectPointDTO.Id;
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
            else
            {
                result.Result = "数据有误，请查证！";
            }
            return result;
        }

        public ActionResultViewModel UpdateProjectPoint(ProjectPointViewModel point)
        {
            var result = CheckUpdatePoint(point);
            if (result.IsSuccess)
            {
                var pointDTO = projectPoints.FirstOrDefault(t => t.Id == point.Id);
                
                using (UnitOfWork tran = new UnitOfWork())
                {
                    _projectPointsRepository.Update(pointDTO);
                    tran.Commit();
                }
            }
            return result;
        }

        public void InitProjectPointCommission(int projectId)
        {
            var pointList = projectPoints.Where(t => t.ProjectId == projectId);
            using (UnitOfWork tran = new UnitOfWork())
            {
                foreach (var point in pointList)
                {
                    point.PointProportion = 0;
                    _projectPointsRepository.Update(point);
                }
                tran.Commit();
            }
        }

        private ActionResultViewModel CheckUpdatePoint(ProjectPointViewModel point)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = true
            };
            if((point.Id==null)|| point.Id == 0)
            {
                result.IsSuccess = false;
                result.Result = "PointId is Null Or Empty";
            }
            return result;
        }

        public ActionResultViewModel DeleteProjectPointById(int pointId)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = true
            };
            try
            {
                var point = _projectPointsRepository.Entities.FirstOrDefault(t => t.Id == pointId);
                point.IsDeleted = true;
                using (UnitOfWork tran = new UnitOfWork())
                {
                    _projectPointsRepository.Update(point);
                    tran.Commit();
                }
            }
            catch(Exception e)
            {
                result.IsSuccess = false;
                result.Result = e.Message;
            }
            return result;
        }

        public IEnumerable<ProjectPointViewModel> GetProjectPointListByProjectId(int projectId)
        {
            var pointDTOList = projectPoints.Where(t => t.ProjectId == projectId);
            var pointViewModelList = pointDTOList.Select(t => Mapper.Map<ProjectPointViewModel>(t));
            foreach (var point in pointDTOList)
            {
                pointViewModelList.FirstOrDefault(t => t.Id == point.Id).ProfessionalTypeName = point.professionalType.TypeName;
            }
            return pointViewModelList;
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
