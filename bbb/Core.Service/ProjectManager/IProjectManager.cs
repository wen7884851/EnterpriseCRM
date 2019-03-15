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
        void UpdateProject(ProjectViewModel project);
        void DeleteProject(int projectId);
        int CreateProject(ProjectViewModel project);

        decimal GetUserProjectSouceByUserId(int UserId);

        PageResult<ProjectViewModel> GetCurrentUserProjectViewModel(ProjectSerchModel model);
        #endregion
    }
}
