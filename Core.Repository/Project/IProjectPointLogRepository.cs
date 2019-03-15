using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Domain.Repository
{
    public interface IProjectPointLogRepository : IRepository<PointLog, Int32>
    {
    }
}

