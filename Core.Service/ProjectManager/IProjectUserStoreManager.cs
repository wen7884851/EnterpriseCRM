using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectUserStoreManager
    {
        IQueryable<ProjectPointUserStore> projectPointUserStores { get; }
        ProjectUserStoreViewModel GetUserStoreById(int storeId);
        ActionResultViewModel CreateProjectUserStore(ProjectUserStoreViewModel model);
        ActionResultViewModel UpdateProjectUserStore(ProjectUserStoreViewModel model);
        ActionResultViewModel DeleteUserStoreById(int storeId);
        PageResult<ProjectUserStoreViewModel> GetUserStoreListByQuery(ProjectUserStoreQueryModel queryModel);
        IEnumerable<ProjectUserStoreViewModel> GetUserStoreListByPointId(int pointId);
    }
}
