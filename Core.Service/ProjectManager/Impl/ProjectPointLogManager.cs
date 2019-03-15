using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;

namespace Core.Service.ProjectManager.Impl
{
    [Export(typeof(IProjectPointLogManager))]
    public class ProjectPointLogManager : IProjectPointLogManager
    {
        [Import]
        private IProjectPointLogRepository _projectPointLogRepository { get; set; }
        public IQueryable<PointLog> pointLogs
        {
            get { return _projectPointLogRepository.Entities; }
        }

        public int CreateProjectPoint(ProjectPointLogViewModel pointLog)
        {
            throw new NotImplementedException();
        }

        public void DeleteLogById(int logId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointLog> GetPointListByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
