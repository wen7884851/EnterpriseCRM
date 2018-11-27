using Domain.DB.Models;
using Domain.Site.Models;
using System.Linq;

namespace Business.Service
{
    public interface IProjectManager
    {
        #region 属性
        IQueryable<Project> projects { get; }
        #endregion

        #region 方法
        void UpdateProject(ProjectViewModel project);
        void DeleteProject(int projectId);
        int CreateProject(ProjectViewModel project);

        decimal GetUserProjectSouceByUserId(int UserId);
        #endregion
    }
}
