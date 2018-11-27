using Domain.DB.Models;
using Domain.Site.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public interface IProjectUserStoreManager
    {
        #region 属性
        IQueryable<ProjectUserStore> projectUserStores { get; }
        #endregion

        #region 方法
        int CreateProjectPoint(ProjectUserStoreViewModel projectUserStore);
        void DeleteLogById(int projectUserStoreId);
        #endregion
    }
}
