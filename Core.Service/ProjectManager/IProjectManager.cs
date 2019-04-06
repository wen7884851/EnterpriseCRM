using Domain.DB.Models;
using Domain.Site.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectManager
    {
        #region 属性
        IQueryable<Project> projects { get; }
        IQueryable<Project> GetCurrentUserProject();
        #endregion

        #region 方法
        int UpdateProject(ProjectViewModel projectViewModel);
        void DeleteProject(int projectId);
        int CreateProject(ProjectViewModel project);
        ActionResultViewModel SetProjectProportion(ProjectViewModel model);
        decimal GetUserProjectSouceByUserId(int UserId);
        int GetProjectUserCount(int projectId);
        decimal? GetProjectRestProportion(int projectId);
        PageResult<ProjectViewModel> GetCurrentUserProjectViewModel(ProjectSerchModel model);
        #endregion
    }
}
