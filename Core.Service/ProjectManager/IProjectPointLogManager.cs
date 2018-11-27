using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public interface IProjectPointLogManager
    {
        #region 属性
        IQueryable<PointLog> pointLogs { get; }
        #endregion

        #region 方法
        IEnumerable<PointLog> GetPointListByProjectId(int projectId);
        int CreateProjectPoint(ProjectPointLogViewModel pointLog);
        void DeleteLogById(int logId);
        #endregion
    }
}
