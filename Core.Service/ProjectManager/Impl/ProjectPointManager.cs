using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DB.Models;
using Domain.Site.Models;

namespace Business.Service.Impl
{
    public class ProjectPointManager : IProjectPointManager
    {
        public IQueryable<ProjectPoint> projectPoints => throw new NotImplementedException();

        public int CreateProjectPoint(ProjectPointViewModel point)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectPoint> GetPointListByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        public int[] GetUserIdsByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
