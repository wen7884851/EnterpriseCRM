using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectUserStoreRepository))]
    public class ProjectUserStoreRepository : EFRepositoryBase<ProjectUserStore, Int32>, IProjectUserStoreRepository
    {
    }
}

