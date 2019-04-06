using Domain.DB.Models;
using Domain.Site.Models;
using Framework.Tool;
using System.Collections.Generic;
using System.Linq;

namespace Core.Service
{
    public interface IProjectPointManager
    {
        #region 属性
        IQueryable<ProjectPoint> projectPoints { get; }
        IQueryable<PointProfessionalType> pointProfessionalTypes { get; }
        #endregion

        #region 方法
        PageResult<ProjectPointViewModel> GetProjectPointListByQuery(ProjectPointQueryModel queryModel);
        int[] GetUserIdsByProjectId(int projectId);
        ProjectPointViewModel GetPointById(int pointId);
        int[] GetPointUserId(int pointId);
        IEnumerable<ProjectPointViewModel> GetProjectPointListByProjectId(int projectId);
        ActionResultViewModel CreateProjectPoint(ProjectPointViewModel point);
        ActionResultViewModel UpdateProjectPoint(ProjectPointViewModel point);
        ActionResultViewModel DeleteProjectPoint(int pointId);
        void InitProjectPointCommission(int projectId);
        decimal GetPointRestProportion(int pointId);
        #endregion
    }
}
