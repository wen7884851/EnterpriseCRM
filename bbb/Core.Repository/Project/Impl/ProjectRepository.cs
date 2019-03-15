using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectRepository))]
    public class ProjectRepository : EFRepositoryBase<Project, Int32>, IProjectRepository
    {
    }
}
