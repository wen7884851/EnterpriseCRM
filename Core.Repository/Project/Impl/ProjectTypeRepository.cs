using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectTypeRepository))]
    public class ProjectTypeRepository : EFRepositoryBase<ProjectType, int>, IProjectTypeRepository
    {
    }
}