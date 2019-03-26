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
        int[] GetUserStoreUserIdsByPointId(int pointId);
        ProjectUserStoreViewModel GetUserStoreById(int storeId);
        int CreateProjectUserStore(ProjectUserStoreViewModel model);
        decimal GetPointOccupiedFundByPointId(int pointId);
        ActionResultViewModel UpdateProjectUserStore(ProjectUserStoreViewModel model);
        ActionResultViewModel DeleteUserStoreById(int storeId);
        PageResult<ProjectUserStoreViewModel> GetUserStoreListByQuery(ProjectUserStoreQueryModel queryModel);
    }
}
