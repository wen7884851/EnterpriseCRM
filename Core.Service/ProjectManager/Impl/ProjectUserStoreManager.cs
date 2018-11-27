using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DB.Models;
using Domain.Site.Models.Project;

namespace Business.Service.Impl
{
    public class ProjectUserStoreManager : IProjectUserStoreManager
    {
        public IQueryable<ProjectUserStore> projectUserStores => throw new NotImplementedException();

        public int CreateProjectPoint(ProjectUserStoreViewModel projectUserStore)
        {
            throw new NotImplementedException();
        }

        public void DeleteLogById(int projectUserStoreId)
        {
            throw new NotImplementedException();
        }
    }
}
