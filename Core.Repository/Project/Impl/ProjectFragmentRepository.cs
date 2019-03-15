using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IProjectFragmentRepository))]
    public class ProjectFragmentRepository : EFRepositoryBase<ProjectFragment, int>, IProjectFragmentRepository
    {
    }
}