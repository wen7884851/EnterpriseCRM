using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectPointLogRepository))]
    public class ProjectPointLogRepository : EFRepositoryBase<PointLog, Int32>, IProjectPointLogRepository
    {
    }
}