using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectPointManager
    {
        #region 属性
        IQueryable<ProjectPoint> projectPoints { get; }
        #endregion

        #region 方法
        PageResult<ProjectPointViewModel> GetProjectPointByQuery(ProjectPointQueryModel queryModel);
        int[] GetUserIdsByProjectId(int projectId);
        IEnumerable<ProjectPoint> GetPointListByProjectId(int projectId);
        int CreateProjectPoint(ProjectPointViewModel point);
        #endregion
    }
}
