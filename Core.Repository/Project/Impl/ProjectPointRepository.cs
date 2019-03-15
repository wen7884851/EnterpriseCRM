using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectPointRepository))]
    public class ProjectPointRepository : EFRepositoryBase<ProjectPoint, Int32>, IProjectPointRepository
    {
    }
}

